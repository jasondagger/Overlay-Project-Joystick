
namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadUserPresenceHandler
{
    internal static void HandleWebSocketPayloadUserPresence(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var payloadMessageType = payloadMessage.Type;
        switch (payloadMessageType)
        {
            case "enter_stream":
                ServiceJoystickWebSocketPayloadUserPresenceHandler.HandleWebSocketPayloadUserPresenceEnterStream(
                    payloadMessage: payloadMessage
                );
                break;
            
            case "leave_stream":
                ServiceJoystickWebSocketPayloadUserPresenceHandler.HandleWebSocketPayloadUserPresenceLeaveStream(
                    payloadMessage: payloadMessage
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
        ServiceJoystickWebSocketPayloadUserPresenceEvents.UserEntered?.Invoke(
            obj: payloadMessage
        );
    }
    
    private static void HandleWebSocketPayloadUserPresenceLeaveStream(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadUserPresenceEvents.UserLeft?.Invoke(
            obj: payloadMessage
        );
    }
}