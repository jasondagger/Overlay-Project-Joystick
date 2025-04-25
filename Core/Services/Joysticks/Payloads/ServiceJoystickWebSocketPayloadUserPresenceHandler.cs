
namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadUserPresenceHandler
{
    internal static void HandleWebSocketPayloadUserPresence(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var payloadMessageType = _ = payloadMessage.Type;
        switch (_ = payloadMessageType)
        {
            case "enter_stream":
                ServiceJoystickWebSocketPayloadUserPresenceHandler.HandleWebSocketPayloadUserPresenceEnterStream(
                    payloadMessage: _ = payloadMessage
                );
                break;
            
            case "leave_stream":
                ServiceJoystickWebSocketPayloadUserPresenceHandler.HandleWebSocketPayloadUserPresenceLeaveStream(
                    payloadMessage: _ = payloadMessage
                );
                break;
			
            default:
                return;
        }
    }
    
	private static void HandleWebSocketPayloadUserPresenceEnterStream(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        
    }
    
    private static void HandleWebSocketPayloadUserPresenceLeaveStream(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        
    }
}