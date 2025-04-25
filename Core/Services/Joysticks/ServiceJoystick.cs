
using Godot;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Tools;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Joysticks.Requests;

namespace Overlay.Core.Services.Joysticks;

public sealed class ServiceJoystick() :
	IService
{
	Task IService.Setup()
	{
		this.RegisterForRetrievedJoystickData();
		return _ = Task.CompletedTask;
	}

	Task IService.Start()
	{
		return _ = Task.CompletedTask;
	}

	Task IService.Stop()
	{
		this.Shutdown();
		return _ = Task.CompletedTask;
	}

	internal void SendRequest(
		ServiceJoystickRequest serviceJoystickRequest
	)
	{
		Task.Run(
			function: async () =>
			{
				var json = _ = JsonHelper.Serialize(
					@object: _ = serviceJoystickRequest
				);
				await this.SendWebSocketMessage(
					message: _ = json
				);
			}
		);
	}
	
	private const string         c_joystickWebSocketAddress     = "wss://joystick.tv/cable";
	private const string         c_joystickSubscribeMessage     = "{\n  \"command\": \"subscribe\",\n  \"identifier\": \"{\\\"channel\\\":\\\"GatewayChannel\\\"}\"\n}";
	private const int            c_reconnectDelayInMilliseconds = 10000;
	
	private ServiceJoystickToken m_joystickToken                = null;
	private ClientWebSocket      m_clientWebSocket              = null;
	
	private string               m_joystickAuthorizationCode    = _ = string.Empty;
	private string               m_joystickClientId             = _ = string.Empty;
	private string               m_joystickClientSecret         = _ = string.Empty;
	private bool                 m_shutdownRequested            = _ = false;
	
	private void ConnectWebSocket()
	{
		Task.Run(
			function:
			async () =>
			{
				try
				{
					var uri = _ = new Uri(
						uriString: _ = $"{_ = ServiceJoystick.c_joystickWebSocketAddress}?token={_ = this.GetClientDataAsBase64String()}"
					);

					_ = this.m_clientWebSocket = _ = new ClientWebSocket();
					this.m_clientWebSocket.Options.AddSubProtocol(
						subProtocol: _ = $"actioncable-v1-json"
					);
					await this.m_clientWebSocket.ConnectAsync(
						uri:               _ = uri,
						cancellationToken: _ = CancellationToken.None
					);
				
					await this.SendWebSocketMessage(
						message: _ = ServiceJoystick.c_joystickSubscribeMessage
					);

#if DEBUG
					ConsoleLogger.LogMessage(
						message: _ = $"{_ = nameof(ServiceJoystick)}.{_ = nameof(this.ConnectWebSocket)}() - Joystick web socket connect successful."
					);
#endif
					while (_ = this.m_shutdownRequested is false)
					{
						var isWebSocketOpen = _ = this.m_clientWebSocket.State is WebSocketState.Open;
						if (_ = isWebSocketOpen is false)
						{
							continue;
						}
					
						var bytes  = _ = new byte[16384u];
						var result = _ = await this.m_clientWebSocket.ReceiveAsync(
							buffer:            _ = bytes,
							cancellationToken: _ = CancellationToken.None
						);

						var webSocketPayloadMessage = _ = ServiceJoystick.ParseWebSocketPayload(
							bytes:  _ = bytes,
							result: _ = result
						);
						ServiceJoystick.HandleWebSocketPayloadMessage(
							payloadMessage: _ = webSocketPayloadMessage
						);
					}
				}
				catch (Exception exception)
				{
					ConsoleLogger.LogMessageError(
						messageError: _ = 
							$"EXCEPTION: " +
							$"{_ = nameof(ServiceJoystick)}." +
							$"{_ = nameof(ServiceJoystick.ConnectWebSocket)}() - " +
							$"{_ = exception.Message}"
					);

					await this.m_clientWebSocket.CloseAsync(
						closeStatus:       _ = WebSocketCloseStatus.NormalClosure,
						statusDescription: _ = string.Empty,
						cancellationToken: _ = CancellationToken.None
					);

					_ = Task.Run(
						function:
						async () =>
						{
							await Task.Delay(
								millisecondsDelay: _ = ServiceJoystick.c_reconnectDelayInMilliseconds
							);
							
							this.ConnectWebSocket();
						}
					);
				}
			}
		);
	}

	private string GetClientDataAsBase64String()
	{
		var clientData        = _ = $"{_ = this.m_joystickClientId}:{_ = this.m_joystickClientSecret}";
		var clientDataAsBytes = _ = Encoding.UTF8.GetBytes(
			s: _ = clientData
		);
		return _ = Convert.ToBase64String(
			inArray: _ = clientDataAsBytes
		);
	}
	
	private void HandleRetrievedJoystickData(
		ServiceDatabaseTaskRetrievedJoystickData retrievedJoystickData
	)
	{
		var result = _ = retrievedJoystickData.Result;

		_ = this.m_joystickAuthorizationCode = _ = result.JoystickData_Authorization_Code;
		_ = this.m_joystickClientId          = _ = result.JoystickData_Client_Id;
		_ = this.m_joystickClientSecret      = _ = result.JoystickData_Client_Secret;
		
		this.ConnectWebSocket();
		this.RetrieveJoystickToken();
	}

	private static void HandleWebSocketPayloadChatMessage(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		ServiceJoystickWebSocketPayloadChatHandler.HandleWebSocketPayloadChat(
			payloadMessage: _ = payloadMessage
		);
	}
	
	private static void HandleWebSocketPayloadMessage(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		if (_ = payloadMessage is null)
		{
			return;
		}
		
		var payloadMessageEvent = _ = payloadMessage.Event;
		switch (_ = payloadMessageEvent)
		{
			case "ChatMessage":
				ServiceJoystick.HandleWebSocketPayloadChatMessage(
					payloadMessage: _ = payloadMessage
				);
				break;
			
			case "StreamEvent":
                ServiceJoystick.HandleWebSocketPayloadStreamEvent(
					payloadMessage: _ = payloadMessage
				);
				break;
			
			case "UserPresence":
				ServiceJoystick.HandleWebSocketPayloadUserPresence(
					payloadMessage: _ = payloadMessage
				);
				break;
			
			default:
				return;
		}
	}
	
	private static void HandleWebSocketPayloadStreamEvent(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		ServiceJoystickWebSocketPayloadStreamEventHandler.HandleWebSocketPayloadStreamEvent(
			payloadMessage: _ = payloadMessage
		);
	}
	
	private static void HandleWebSocketPayloadUserPresence(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		ServiceJoystickWebSocketPayloadUserPresenceHandler.HandleWebSocketPayloadUserPresence(
			payloadMessage: _ = payloadMessage
		);
	}

	private static ServiceJoystickWebSocketPayloadMessage ParseWebSocketPayload(
		byte[]                 bytes,
		WebSocketReceiveResult result
	)
	{
		var json = _ = Encoding.UTF8.GetString(
			bytes: _ = bytes,
			index: _ = 0,
			count: _ = result.Count
		);
		var payloadType = _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadType>(
			json: _ = json
		).Type;
		
		if (
			_ = payloadType.Equals(
				obj: _ = string.Empty
			) is false
		)
		{
			return null;
		}

		var payload = _ = JsonHelper.Deserialize<ServiceJoystickWebSocketPayload>(
			json: _ = json
		);
		return _ = payload.Message;
	}

	private void RegisterForRetrievedJoystickData()
	{
		_ = ServiceDatabaseTaskEvents.RetrievedJoystickData += this.HandleRetrievedJoystickData;
	}
	
	private void RetrieveJoystickToken()
	{
		var serviceGodots    = _ = Services.GetService<ServiceGodots>();
		var serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
		
		serviceGodotHttp.SendHttpRequest(
			url: _ =
				$"https://joystick.tv/api/oauth/token?" +
				$"redirect_uri=unused&" +
				$"code={_ = this.m_joystickAuthorizationCode}&" +
				$"grant_type=authorization_code",
			headers: [
				$"Authorization: Basic {_ = this.GetClientDataAsBase64String()}",
				$"Content-Type: application/x-www-form-urlencoded",
				$"Accept: application/json"
			],
			method: _ = HttpClient.Method.Post,
			json:   _ = string.Empty,
			requestCompletedHandler: (
				long     result,
				long     responseCode,
				string[] headers,
				byte[]   body
			) =>
			{
				var bodyAsString = _ = Encoding.UTF8.GetString(
					bytes: _ = body
				);
				_ = this.m_joystickToken = _ = JsonHelper.Deserialize<ServiceJoystickToken>(
					json: _ = bodyAsString
				);

				// todo: if joystick adds rest calls, implement refresh token loop
			}
		);
	}
	
	private void SendRequestTest()
	{
		// https://support.joystick.tv/developer_support/#testing-your-bot
		
		var serviceGodots    = _ = Services.GetService<ServiceGodots>();
		var serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
		
		serviceGodotHttp.SendHttpRequest(
			url: _ =
				$"https://joystick.tv/echo",
			headers: [
				$"Authorization: Basic {_ = this.GetClientDataAsBase64String()}",
				$"Content-Type: application/json"
			],
			method: _ = HttpClient.Method.Post,
			json:   _ = "{\n  \"sample\": {\n    \"event\": \"StreamEvent\",\n    \"data\": \"Tipped\"\n  }\n}",
			requestCompletedHandler: (
				long     result,
				long     responseCode,
				string[] headers,
				byte[]   body
			) =>
			{
				Task.Run(
					function: async () =>
					{
						await Task.Delay(3000);
						SendRequestTest();
					}
				);
			}
		);
	}

	private async Task SendWebSocketMessage(
		string message
	)
	{
		try
		{
			var buffer = _ = Encoding.UTF8.GetBytes(
				s: _ = message
			);
			await this.m_clientWebSocket.SendAsync(
				buffer:            _ = buffer,
				messageType:       _ = WebSocketMessageType.Text,
				endOfMessage:      _ = true,
				cancellationToken: _ = CancellationToken.None
			);
		}
		catch (Exception exception)
		{
			ConsoleLogger.LogMessageError(
				messageError: _ =
					$"{_ = nameof(ServiceJoystick)}." +
					$"{_ = nameof(ServiceJoystick.SendWebSocketMessage)}() - " +
					$"EXCEPTION: {_ = exception.Message}"
			);
		}
	}

	private void Shutdown()
	{
		_ = this.m_shutdownRequested = _ = true;
	}
}