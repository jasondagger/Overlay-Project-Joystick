
using Overlay.Core.Tools;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Services.OBS;

internal sealed class ServiceOBS() :
    IService
{
    Task IService.Setup()
    {
        return Task.CompletedTask;
    }

    Task IService.Start()
    {
	    this.ConnectWebSocket();
        return Task.CompletedTask;
    }

    Task IService.Stop()
    {
        return Task.CompletedTask;
    }

    internal void ChangeScene(
	    string sceneName
	)
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer      = Encoding.UTF8.GetBytes(
				    s: $"{{  \"op\": 6,  \"d\": {{ \"requestType\": \"SetCurrentProgramScene\",  \"requestId\": \"1\",  \"requestData\": {{\"sceneName\": \"{sceneName}\"}}}}}}"
				);
			    var segment     = new ArraySegment<byte>(
				    array: buffer
				);

			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
						    $"EXCEPTION: " +
						    $"{nameof(ServiceOBS)}." +
						    $"{nameof(ServiceOBS.ChangeScene)}() - " +
						    $"{exception.Message}"
				    );
			    }
		    }
		);
    }
    
    internal void MuteMicrophone()
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer  = Encoding.UTF8.GetBytes(
				    s: $"{{ \"op\": 6, \"d\": {{ \"requestType\": \"SetInputMute\", \"requestId\": \"7\", \"requestData\": {{ \"inputName\": \"Mic/Aux\", \"inputMuted\": true }}}}}} "
			    );
			    var segment = new ArraySegment<byte>(
				    array: buffer
			    );

			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
					    $"EXCEPTION: " +
					    $"{nameof(ServiceOBS)}." +
					    $"{nameof(ServiceOBS.MuteMicrophone)}() - " +
					    $"{exception.Message}"
				    );
			    }
		    }
	    );
    }
    
    internal void StartRecord()
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer  = Encoding.UTF8.GetBytes(
				    s: $"{{  \"op\": 6,  \"d\": {{ \"requestType\": \"StartRecord\",  \"requestId\": \"5\"}}}}"
			    );
			    var segment = new ArraySegment<byte>(
				    array: buffer
			    );
        	
			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
					    $"EXCEPTION: " +
					    $"{nameof(ServiceOBS)}." +
					    $"{nameof(ServiceOBS.StartRecord)}() - " +
					    $"{exception.Message}"
				    );
			    }
		    }
	    );
    }
    
    internal void StartStream()
    {
	    Task.Run(
		    function: async () =>
	    	{
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer      = Encoding.UTF8.GetBytes(
				    s: $"{{  \"op\": 6,  \"d\": {{ \"requestType\": \"StartStream\",  \"requestId\": \"3\"}}}}"
			    );
			    var segment     = new ArraySegment<byte>(
				    array: buffer
			    );
        	
			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
					    $"EXCEPTION: " +
					    $"{nameof(ServiceOBS)}." +
					    $"{nameof(ServiceOBS.StartStream)}() - " +
					    $"{exception.Message}"
				    );
			    }
	    	}
		);
    }
    
    internal void StopRecord()
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer  = Encoding.UTF8.GetBytes(
				    s: $"{{  \"op\": 6,  \"d\": {{ \"requestType\": \"StopRecord\",  \"requestId\": \"4\"}}}}"
			    );
			    var segment = new ArraySegment<byte>(
				    array: buffer
			    );

			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
					    $"EXCEPTION: " +
					    $"{nameof(ServiceOBS)}." +
					    $"{nameof(ServiceOBS.StopRecord)}() - " +
					    $"{exception.Message}"
				    );
			    }
		    }
	    );
    }

    internal void StopStream()
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer  = Encoding.UTF8.GetBytes(
				    s: $"{{  \"op\": 6,  \"d\": {{ \"requestType\": \"StopStream\",  \"requestId\": \"2\"}}}}"
			    );
			    var segment = new ArraySegment<byte>(
				    array: buffer
			    );

			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
					    $"EXCEPTION: " +
					    $"{nameof(ServiceOBS)}." +
					    $"{nameof(ServiceOBS.StopStream)}() - " +
					    $"{exception.Message}"
				    );
			    }
		    }
	    );
    }

    internal void UnmuteMicrophone()
    {
	    Task.Run(
		    function: async () =>
		    {
			    if (this.m_clientWebSocket.State is not WebSocketState.Open)
			    {
				    return;
			    }
			    
			    var buffer  = Encoding.UTF8.GetBytes(
				    s: $"{{ \"op\": 6, \"d\": {{ \"requestType\": \"SetInputMute\", \"requestId\": \"8\", \"requestData\": {{ \"inputName\": \"Mic/Aux\", \"inputMuted\": false }}}}}} "
			    );
			    var segment = new ArraySegment<byte>(
				    array: buffer
			    );

			    try
			    {
				    await this.m_clientWebSocket.SendAsync(
					    buffer:            segment,
					    messageType:       WebSocketMessageType.Text,
					    endOfMessage:      true, 
					    cancellationToken: CancellationToken.None
				    );
			    }
			    catch (WebSocketException exception)
			    {
				    ConsoleLogger.LogMessageError(
					    messageError: 
					    $"EXCEPTION: " +
					    $"{nameof(ServiceOBS)}." +
					    $"{nameof(ServiceOBS.UnmuteMicrophone)}() - " +
					    $"{exception.Message}"
				    );
			    }
		    }
	    );
    }
    
    private const string    c_obsWebSocketAddress = "ws://localhost";
    private const string    c_obsWebSocketPort    = "4455";
    
    private ClientWebSocket m_clientWebSocket     = null;

    private void ConnectWebSocket()
	{
		Task.Run(
			function: async () =>
			{
				var uri = new Uri(
					uriString: $"{ServiceOBS.c_obsWebSocketAddress}:{ServiceOBS.c_obsWebSocketPort}"
				);

				this.m_clientWebSocket = new ClientWebSocket();

				await this.m_clientWebSocket.ConnectAsync(
					uri:               uri,
					cancellationToken: CancellationToken.None
				);
				
#if DEBUG
				ConsoleLogger.LogMessage(
					message: $"{nameof(ServiceOBS)}.{nameof(this.ConnectWebSocket)}() - OBS web socket connect successful."
				);
#endif
				
				var buffer  = Encoding.UTF8.GetBytes(
					s: "{\n  \"op\": 1,\n  \"d\": {\n    \"rpcVersion\": 1,\n    \"eventSubscriptions\": 33\n  }\n}"
				);
				var segment = new ArraySegment<byte>(
					array: buffer
				);

				try
				{
					await this.m_clientWebSocket.SendAsync(
						buffer:            segment,
						messageType:       WebSocketMessageType.Text,
						endOfMessage:      true, 
						cancellationToken: CancellationToken.None
					);
				}
				catch (WebSocketException exception)
				{
					ConsoleLogger.LogMessageError(
						messageError: 
						$"EXCEPTION: " +
						$"{nameof(ServiceOBS)}." +
						$"{nameof(ServiceOBS.ConnectWebSocket)}() - " +
						$"{exception.Message}"
					);
				}

				while (true)
				{
					try
					{
						var isWebSocketOpen = this.m_clientWebSocket.State is WebSocketState.Open;
						if (isWebSocketOpen is false)
						{
							continue;
						}
						
						var bytes  = new byte[4096u];
						var result = await this.m_clientWebSocket.ReceiveAsync(
							buffer:            bytes,
							cancellationToken: CancellationToken.None
						);

						this.ParseWebSocketPayload(
							bytes:  bytes,
							result: result
						);

						await Task.Delay(
							millisecondsDelay: 16
						);
					}
					catch (Exception exception)
					{
						ConsoleLogger.LogMessageError(
							messageError: 
								$"EXCEPTION: " +
								$"{nameof(ServiceOBS)}." +
								$"{nameof(ServiceOBS.ConnectWebSocket)}() - " +
								$"{exception.Message}"
						);

						await this.m_clientWebSocket.CloseAsync(
							closeStatus:       WebSocketCloseStatus.NormalClosure,
							statusDescription: string.Empty,
							cancellationToken: CancellationToken.None
						);
						
						this.ConnectWebSocket();
						break;
					}
				}
			}
		);
	}
    
	private void ParseWebSocketPayload(
		byte[]                 bytes,
		WebSocketReceiveResult result
	)
	{
		var json = Encoding.UTF8.GetString(
			bytes: bytes,
			index: 0,
			count: result.Count
		);
		
		if (
			json.Contains(
				value: "\"op\":2"
			) is true
		)
		{
#if !DEBUG
			this.ChangeScene(
				sceneName: "Main"
			);
#endif
		}
	}
}
