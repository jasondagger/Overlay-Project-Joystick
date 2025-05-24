
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
using Overlay.Core.Services.Joysticks.Requests;

namespace Overlay.Core.Services.Joysticks;

public sealed class ServiceJoystick() :
	IService
{
	Task IService.Setup()
	{
		this.RegisterForRetrievedJoystickData();
		return Task.CompletedTask;
	}

	Task IService.Start()
	{
		return Task.CompletedTask;
	}

	Task IService.Stop()
	{
		this.Shutdown();
		return Task.CompletedTask;
	}

	internal void SendRequest(
		ServiceJoystickRequest serviceJoystickRequest
	)
	{
		Task.Run(
			function:
			async () =>
			{
				var json = JsonHelper.Serialize(
					@object: serviceJoystickRequest
				);
				await this.SendWebSocketMessage(
					message: json
				);
			}
		);
	}
	
	private const string         c_joystickWebSocketAddress     = "wss://joystick.tv/cable";
	private const string         c_joystickSubscribeMessage     = "{\n  \"command\": \"subscribe\",\n  \"identifier\": \"{\\\"channel\\\":\\\"GatewayChannel\\\"}\"\n}";
	private const int            c_reconnectDelayInMilliseconds = 100;
	
	private ServiceJoystickToken m_joystickToken                = null;
	private ClientWebSocket      m_clientWebSocket              = null;
	
	private string               m_joystickAuthorizationCode    = string.Empty;
	private string               m_joystickClientId             = string.Empty;
	private string               m_joystickClientSecret         = string.Empty;
	private bool                 m_shutdownRequested            = false;
	
	private void ConnectWebSocket()
	{
		Task.Run(
			function:
			async () =>
			{
				try
				{
					var uri = new Uri(
						uriString: $"{ServiceJoystick.c_joystickWebSocketAddress}?token={this.GetClientDataAsBase64String()}"
					);

					this.m_clientWebSocket = new ClientWebSocket();
					this.m_clientWebSocket.Options.AddSubProtocol(
						subProtocol: $"actioncable-v1-json"
					);
					await this.m_clientWebSocket.ConnectAsync(
						uri:               uri,
						cancellationToken: CancellationToken.None
					);
				
					await this.SendWebSocketMessage(
						message: ServiceJoystick.c_joystickSubscribeMessage
					);

#if DEBUG
					ConsoleLogger.LogMessage(
						message: $"{nameof(ServiceJoystick)}.{nameof(this.ConnectWebSocket)}() - Joystick web socket connect successful."
					);
#endif
					while (this.m_shutdownRequested is false)
					{
						var isWebSocketOpen = this.m_clientWebSocket.State is WebSocketState.Open;
						if (isWebSocketOpen is false)
						{
							continue;
						}
					
						var bytes  = new byte[16384u];
						var result = await this.m_clientWebSocket.ReceiveAsync(
							buffer:            bytes,
							cancellationToken: CancellationToken.None
						);
						
						var webSocketPayloadMessage = ServiceJoystick.ParseWebSocketPayload(
							bytes:  bytes,
							result: result
						);
						ServiceJoystick.HandleWebSocketPayloadMessage(
							payloadMessage: webSocketPayloadMessage
						);
					}
				}
				catch (Exception exception)
				{
					ConsoleLogger.LogMessageError(
						messageError: 
							$"EXCEPTION: " +
							$"{nameof(ServiceJoystick)}." +
							$"{nameof(ServiceJoystick.ConnectWebSocket)}() - " +
							$"{exception.Message}"
					);
					
					_ = Task.Run(
						function:
						async () =>
						{
							await Task.Delay(
								millisecondsDelay: ServiceJoystick.c_reconnectDelayInMilliseconds
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
		var clientData        = $"{this.m_joystickClientId}:{this.m_joystickClientSecret}";
		var clientDataAsBytes = Encoding.UTF8.GetBytes(
			s: clientData
		);
		return Convert.ToBase64String(
			inArray: clientDataAsBytes
		);
	}
	
	private void HandleRetrievedJoystickData(
		ServiceDatabaseTaskRetrievedJoystickData retrievedJoystickData
	)
	{
		var result = retrievedJoystickData.Result;

		this.m_joystickAuthorizationCode = result.JoystickData_Authorization_Code;
		this.m_joystickClientId          = result.JoystickData_Client_Id;
		this.m_joystickClientSecret      = result.JoystickData_Client_Secret;
		
		this.ConnectWebSocket();
		this.RetrieveJoystickToken();
	}

	private static void HandleWebSocketPayloadChatMessage(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		ServiceJoystickWebSocketPayloadChatHandler.HandleWebSocketPayloadChat(
			payloadMessage: payloadMessage
		);
	}
	
	private static void HandleWebSocketPayloadMessage(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		if (payloadMessage is null)
		{
			return;
		}
		
		var payloadMessageEvent = payloadMessage.Event;
		switch (payloadMessageEvent)
		{
			case "ChatMessage":
				ServiceJoystick.HandleWebSocketPayloadChatMessage(
					payloadMessage: payloadMessage
				);
				break;
			
			case "StreamEvent":
                ServiceJoystick.HandleWebSocketPayloadStreamEvent(
					payloadMessage: payloadMessage
				);
				break;
			
			case "UserPresence":
				ServiceJoystick.HandleWebSocketPayloadUserPresence(
					payloadMessage: payloadMessage
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
			payloadMessage: payloadMessage
		);
	}
	
	private static void HandleWebSocketPayloadUserPresence(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		ServiceJoystickWebSocketPayloadUserPresenceHandler.HandleWebSocketPayloadUserPresence(
			payloadMessage: payloadMessage
		);
	}

	private static ServiceJoystickWebSocketPayloadMessage ParseWebSocketPayload(
		byte[]                 bytes,
		WebSocketReceiveResult result
	)
	{
		var json = Encoding.UTF8.GetString(
			bytes: bytes,
			index: 0,
			count: result.Count
		);
		var payloadType = JsonHelper.Deserialize<ServiceJoystickWebSocketPayloadType>(
			json: json
		).Type;
		
		if (
			payloadType.Equals(
				obj: string.Empty
			) is false
		)
		{
			return null;
		}

		var payload = JsonHelper.Deserialize<ServiceJoystickWebSocketPayload>(
			json: json
		);
		return payload.Message;
	}

	private void RegisterForRetrievedJoystickData()
	{
		ServiceDatabaseTaskEvents.RetrievedJoystickData += this.HandleRetrievedJoystickData;
	}
	
	private void RetrieveJoystickToken()
	{
		var serviceGodots    = Services.GetService<ServiceGodots>();
		var serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
		
		serviceGodotHttp.SendHttpRequest(
			url: _ =
				$"https://joystick.tv/api/oauth/token?" +
				$"redirect_uri=unused&" +
				$"code={this.m_joystickAuthorizationCode}&" +
				$"grant_type=authorization_code",
			headers: [
				$"Authorization: Basic {this.GetClientDataAsBase64String()}",
				$"Content-Type: application/x-www-form-urlencoded",
				$"Accept: application/json"
			],
			method: HttpClient.Method.Post,
			json:   string.Empty,
			requestCompletedHandler: (
				long     result,
				long     responseCode,
				string[] headers,
				byte[]   body
			) =>
			{
				var bodyAsString = Encoding.UTF8.GetString(
					bytes: body
				);
				this.m_joystickToken = JsonHelper.Deserialize<ServiceJoystickToken>(
					json: bodyAsString
				);

				// todo: if joystick adds rest calls, implement refresh token loop
			}
		);
	}
	
	private void SendRequestTest()
	{
		// https://support.joystick.tv/developer_support/#testing-your-bot
		
		var serviceGodots    = Services.GetService<ServiceGodots>();
		var serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
		
		serviceGodotHttp.SendHttpRequest(
			url: _ =
				$"https://joystick.tv/echo",
			headers: [
				$"Authorization: Basic {this.GetClientDataAsBase64String()}",
				$"Content-Type: application/json"
			],
			method: HttpClient.Method.Post,
			json:   "{\n  \"sample\": {\n    \"event\": \"StreamEvent\",\n    \"data\": \"Tipped\"\n  }\n}",
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
						this.SendRequestTest();
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
			var buffer = Encoding.UTF8.GetBytes(
				s: message
			);
			await this.m_clientWebSocket.SendAsync(
				buffer:            buffer,
				messageType:       WebSocketMessageType.Text,
				endOfMessage:      true,
				cancellationToken: CancellationToken.None
			);
		}
		catch (Exception exception)
		{
			ConsoleLogger.LogMessageError(
				messageError: _ =
					$"{nameof(ServiceJoystick)}." +
					$"{nameof(ServiceJoystick.SendWebSocketMessage)}() - " +
					$"EXCEPTION: {exception.Message}"
			);
		}
	}

	private void Shutdown()
	{
		this.m_shutdownRequested = true;
	}
}