
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadChatHandler
{
    internal static void HandleWebSocketPayloadChat(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.AddChatMessage(
            payloadMessage: _ = payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.PlaySoundEffect();
    }

    private static void AddChatMessage(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var author        = _ = payloadMessage.Author;
        var message       = _ = payloadMessage.Text;

        var username      = _ = author.Username;
        var usernameColor = _ = author.UsernameColor;
        var isModerator   = _ = author.IsModerator;
        var isStreamer    = _ = author.IsStreamer;
        var isSubscriber  = _ = author.IsSubscriber;
        
        Chat.Instance.AddChatMessage(
            username:      _ = username,
            usernameColor: _ = usernameColor,
            message:       _ = message,
            isModerator:   _ = isModerator,
            isStreamer:    _ = isStreamer,
            isSubscriber:  _ = isSubscriber
        );
    }
    
    private static void PlaySoundEffect()
    {
        var serviceGodots     = _ = Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.ChatNotification
        );
    }
}