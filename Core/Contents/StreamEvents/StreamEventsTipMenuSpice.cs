
using Overlay.Core.Tools;

namespace Overlay.Core.Contents;

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
                StreamEventsTipMenuSpice.NipplePinch();          
                break;
            case "Show Butthole":
                StreamEventsTipMenuSpice.ShowButthole();              
                break;
            case "Show Feet":
                StreamEventsTipMenuSpice.ShowFeet();
                break;
            case "Titty Jiggle":
                StreamEventsTipMenuSpice.TittyJiggle();
                break;
            case "Toy In - 5 Minute(s)":
                StreamEventsTipMenuSpice.ToyIn(
                    lengthInMinutes: _ = 5
                );
                break;
            case "Toy In - 10 Minute(s)":
                StreamEventsTipMenuSpice.ToyIn(
                    lengthInMinutes: _ = 10
                );
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
        switch (_ = lengthInMinutes)
        {
            case 1:
            case 2:
                break;
        }
    }

    private static void HandleCum()
    {
        
    }

    private static void HandleCumTaste()
    {
        
    }

    private static void NipplePinch()
    {
        
    }

    private static void ShowButthole()
    {
        
    }

    private static void ShowFeet()
    {
        
    }

    private static void TittyJiggle()
    {
        
    }

    private static void ToyIn(
        int lengthInMinutes
    )
    {
        switch (_ = lengthInMinutes)
        {
            case 5:
            case 10:
                break;
        }
    }
}