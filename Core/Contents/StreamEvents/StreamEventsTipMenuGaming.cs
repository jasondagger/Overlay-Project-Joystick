
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents;

internal static class StreamEventsTipMenuGaming
{
    internal static void HandleTipMenuItem(
        string tipMenuItem
    )
    {
        switch (_ = tipMenuItem)
        {
            case "MTG: Concede":
                StreamEventsTipMenuGaming.HandleMTGConcede();
                break;
            case "SFX: Applause":
            case "SFX: Godlike":
            case "SFX: Heartbeats":
            case "SFX: Holy Shit":
            case "SFX: Nice":
                StreamEventsTipMenuGaming.HandleSFX(
                    tipMenuItem: _ = tipMenuItem
                );
                break;
            case "TF2: Explode":
                StreamEventsTipMenuGaming.HandleTF2Explode();
                break;
            case "TF2: Kill":
                StreamEventsTipMenuGaming.HandleTF2Kill();
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuGaming)}." +
                        $"{_ = nameof(StreamEventsTipMenuGaming.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }

    private static void HandleMTGConcede()
    {

    }
    
    private static void HandleSFX(
        string tipMenuItem
    )
    {
        ServiceGodotAudio.SoundAlertType soundAlertType;
        switch (_ = tipMenuItem)
        {
            case "SFX: Applause":
                soundAlertType = _ = ServiceGodotAudio.SoundAlertType.Applause;
                break;
            
            case "SFX: Godlike":
                soundAlertType = _ = ServiceGodotAudio.SoundAlertType.Godlike;
                break;
            
            case "SFX: Heartbeats":
                soundAlertType = _ = ServiceGodotAudio.SoundAlertType.Heartbeats;
                break;
            
            case "SFX: Holy Shit":
                soundAlertType = _ = ServiceGodotAudio.SoundAlertType.HolyShit;
                break;
            
            case "SFX: Nice":
                soundAlertType = _ = ServiceGodotAudio.SoundAlertType.Nice;
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuGaming)}." +
                        $"{_ = nameof(StreamEventsTipMenuGaming.HandleSFX)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }

        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = soundAlertType
        );
    }
    
    private static void HandleTF2Explode()
    {

    }
    
    private static void HandleTF2Kill()
    {

    }
}