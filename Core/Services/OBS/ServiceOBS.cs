
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Tools;

namespace Overlay.Core.Services.OBS;

internal sealed partial class ServiceOBS() :
    IService
{
    Task IService.Setup()
    {
        return _ = Task.CompletedTask;
    }

    Task IService.Start()
    {
	    this.ConnectWebSocket();
        return _ = Task.CompletedTask;
    }

    Task IService.Stop()
    {
        return _ = Task.CompletedTask;
    }

    internal void ChangeScene(
	    string sceneName
	)
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (_ = this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }

			    _ = this.m_requestId = _ = Guid.NewGuid().ToString();
			    var request = new ServiceOBSRequestSceneChange(
				    requestType: _ = "SetCurrentSceneCollection",
				    sceneName:   _ = sceneName
			    );

			    var jsonRequest = _ = JsonSerializer.Serialize(
				    value: _ = request
				);
			    var buffer      = _ = Encoding.UTF8.GetBytes(
				    s: _ = jsonRequest
				);
			    var segment     = _ = new ArraySegment<byte>(
				    array: _ = buffer
				);

			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            _ = segment,
					    messageType:       _ = WebSocketMessageType.Text,
					    endOfMessage:      _ = true, 
					    cancellationToken: _ = CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: _ = 
						    $"EXCEPTION: " +
						    $"{_ = nameof(ServiceOBS)}." +
						    $"{_ = nameof(ServiceOBS.ChangeScene)}() - " +
						    $"{_ = exception.Message}"
				    );
			    }
		    }
		);
    }
    
    private const string    c_obsWebSocketAddress = "ws://localhost";
    private const string    c_obsWebSocketPort    = "4455";
    
    private ClientWebSocket m_clientWebSocket     = null;
    private bool            m_shutdownRequested   = _ = false;
    private string          m_requestId           = _ = string.Empty;

    private void ConnectWebSocket()
	{
		Task.Run(
			function: async () =>
			{
				var uri = _ = new Uri(
					uriString: _ = $"{_ = ServiceOBS.c_obsWebSocketAddress}:{_ = ServiceOBS.c_obsWebSocketPort}"
				);

				_ = this.m_clientWebSocket = _ = new ClientWebSocket();

				await this.m_clientWebSocket.ConnectAsync(
					uri:               _ = uri,
					cancellationToken: _ = CancellationToken.None
				);
				
#if DEBUG
				ConsoleLogger.LogMessage(
					message: _ = $"{_ = nameof(ServiceOBS)}.{_ = nameof(this.ConnectWebSocket)}() - OBS web socket connect successful."
				);
#endif

				while (_ = this.m_shutdownRequested is false)
				{
					try
					{
						var isWebSocketOpen = _ = this.m_clientWebSocket.State is WebSocketState.Open;
						if (_ = isWebSocketOpen is false)
						{
							continue;
						}
						
						var bytes  = _ = new byte[4096u];
						var result = _ = await this.m_clientWebSocket.ReceiveAsync(
							buffer:            _ = bytes,
							cancellationToken: _ = CancellationToken.None
						);

						ServiceOBS.ParseWebSocketPayload(
							bytes:  _ = bytes,
							result: _ = result
						);
					}
					catch (Exception exception)
					{
						ConsoleLogger.LogMessageError(
							messageError: _ = 
								$"EXCEPTION: " +
								$"{_ = nameof(ServiceOBS)}." +
								$"{_ = nameof(ServiceOBS.ConnectWebSocket)}() - " +
								$"{_ = exception.Message}"
						);

						await this.m_clientWebSocket.CloseAsync(
							closeStatus:       _ = WebSocketCloseStatus.NormalClosure,
							statusDescription: _ = string.Empty,
							cancellationToken: _ = CancellationToken.None
						);
						
						this.ConnectWebSocket();
						break;
					}
				}
			}
		);
	}
    
	private static void ParseWebSocketPayload(
		byte[]                 bytes,
		WebSocketReceiveResult result
	)
	{
		var json = _ = Encoding.UTF8.GetString(
			bytes: _ = bytes,
			index: _ = 0,
			count: _ = result.Count
		);
	}
}
