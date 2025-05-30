
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuToys
{
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = _ = messageMetadata.TipMenuItem;
        switch (_ = tipMenuItem)
        {
            case "Edge Control Link - 1 Minute(s)":
                StreamEventsTipMenuToys.HandleControlLinkEdge(
                    lengthInMinutes: _ = 1
                );
                break;
            case "Edge Control Link - 2 Minute(s)":
                StreamEventsTipMenuToys.HandleControlLinkEdge(
                    lengthInMinutes: _ = 2
                );
                break;
            case "Edge Control Link - 5 Minute(s)":
                StreamEventsTipMenuToys.HandleControlLinkEdge(
                    lengthInMinutes: _ = 5
                );
                break;
            case "Gush Control Link - 1 Minute(s)":
                StreamEventsTipMenuToys.HandleControlLinkGush(
                    lengthInMinutes: _ = 1
                );
                break;
            case "Gush Control Link - 2 Minute(s)":
                StreamEventsTipMenuToys.HandleControlLinkGush(
                    lengthInMinutes: _ = 2
                );
                break;
            case "Gush Control Link - 5 Minute(s)":
                StreamEventsTipMenuToys.HandleControlLinkGush(
                    lengthInMinutes: _ = 5
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuToys)}." +
                        $"{_ = nameof(StreamEventsTipMenuToys.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }
    
    private static void HandleControlLinkEdge(
        int lengthInMinutes
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
        
        switch (_ = lengthInMinutes)
        {
            case 1:
                break;
            case 2:
                break;
            case 5:
                break;
        }
    }
    
    private static void HandleControlLinkGush(
        int lengthInMinutes
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
        
        switch (_ = lengthInMinutes)
        {
            case 1:
                break;
            case 2:
                break;
            case 5:
                break;
        }
    }
}