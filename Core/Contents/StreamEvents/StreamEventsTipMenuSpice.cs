
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuSpice
{
    internal static void HandleTipMenuItem(
        string tipMenuItem
    )
    {
        switch (_ = tipMenuItem)
        {
            case "Ass Slap":
                StreamEventsTipMenuSpice.HandleAssSlap(
                    count: _ = 1
                );
                break;
            case "Ass Slap x5":
                StreamEventsTipMenuSpice.HandleAssSlap(
                    count: _ = 5
                );
                break;
            case "Ass Slap x10":
                StreamEventsTipMenuSpice.HandleAssSlap(
                    count: _ = 10
                );
                break;
            case "Balls Foreplay - 1 Minute(s)":
                StreamEventsTipMenuSpice.HandleBallForeplay(
                    lengthInMinutes: _ = 1
                );
                break;
            case "Balls Foreplay - 2 Minute(s)":
                StreamEventsTipMenuSpice.HandleBallForeplay(
                    lengthInMinutes: _ = 2
                );               
                break;
            case "Belly Button Fingering - 1 Minute(s)":
                StreamEventsTipMenuSpice.HandleBellyButtonFingering(
                    lengthInMinutes: _ = 1
                );               
                break;
            case "Belly Button Fingering - 2 Minute(s)":
                StreamEventsTipMenuSpice.HandleBellyButtonFingering(
                    lengthInMinutes: _ = 2
                );             
                break;
            case "Cock Out - 1 Minute(s)":
                StreamEventsTipMenuSpice.HandleCockOut(
                    lengthInMinutes: _ = 1
                );              
                break;
            case "Cock Out - 5 Minute(s)":
                StreamEventsTipMenuSpice.HandleCockOut(
                    lengthInMinutes: _ = 5
                );                
                break;
            case "Cock Out - 10 Minute(s)":
                StreamEventsTipMenuSpice.HandleCockOut(
                    lengthInMinutes: _ = 10
                );              
                break;
            case "Cock Foreplay - 1 Minute(s)":
                StreamEventsTipMenuSpice.HandleCockForeplay(
                    lengthInMinutes: _ = 1
                );              
                break;
            case "Cock Foreplay - 2 Minute(s)":
                StreamEventsTipMenuSpice.HandleCockForeplay(
                    lengthInMinutes: _ = 2
                );              
                break;
            case "Cum":
                StreamEventsTipMenuSpice.HandleCum();              
                break;
            case "Cum Taste":
                StreamEventsTipMenuSpice.HandleCumTaste();             
                break;
            case "Nipple Pinch":
                StreamEventsTipMenuSpice.HandleNipplePinch();          
                break;
            case "No Underwear & Pants - 10 Minute(s)":
                StreamEventsTipMenuSpice.HandleNoUnderwearAndPants();
                break;
            case "Show Butthole":
                StreamEventsTipMenuSpice.HandleShowButthole();              
                break;
            case "Show Feet":
                StreamEventsTipMenuSpice.HandleShowFeet();
                break;
            case "Titty Jiggle":
                StreamEventsTipMenuSpice.HandleTittyJiggle();
                break;
            case "Toy In - 5 Minute(s)":
                StreamEventsTipMenuSpice.HandleToyIn(
                    lengthInMinutes: _ = 5
                );
                break;
            case "Toy In - 10 Minute(s)":
                StreamEventsTipMenuSpice.HandleToyIn(
                    lengthInMinutes: _ = 10
                );
                break;
            case "Turn On Ring Light":
                StreamEventsTipMenuSpice.HandleTurnOnRingLight();
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuSpice)}." +
                        $"{_ = nameof(StreamEventsTipMenuSpice.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }

    private static void HandleAssSlap(
        int count
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
        
        switch (_ = count)
        {
            case 1:
            case 5:
            case 10:
                break;
        }
    }

    private static void HandleBallForeplay(
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
            case 2:
                break;
        }
    }
    
    private static void HandleBellyButtonFingering(
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
            case 2:
                break;
        }
    }
    
    private static void HandleCockOut(
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
            case 5:
            case 10:
                break;
        }
    }
    
    private static void HandleCockForeplay(
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
            case 2:
                break;
        }
    }

    private static void HandleCum()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleCumTaste()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleNipplePinch()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleShowButthole()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleShowFeet()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleNoUnderwearAndPants()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleTittyJiggle()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleToyIn(
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
            case 5:
            case 10:
                break;
        }
    }

    private static void HandleTurnOnRingLight()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }
}