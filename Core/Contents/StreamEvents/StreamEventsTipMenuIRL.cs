
using Godot;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents;

internal static class StreamEventsTipMenuIRL
{
    internal static void HandleTipMenuItem(
        string tipMenuItem
    )
    {
        switch (_ = tipMenuItem)
        {
            case "Ohai":
                StreamEventsTipMenuIRL.HandleOhai();
                break;
            case "Nice Smile!":
                StreamEventsTipMenuIRL.HandleNiceSmile();
                break;
            case "Thank You":
                StreamEventsTipMenuIRL.HandleThankYou();
                break;
            case "You Look Great!":
                StreamEventsTipMenuIRL.HandleYouLookGreat();
                break;
            case "Headpats":
                StreamEventsTipMenuIRL.HandleHeadpats();
                break;
            case "Belly Rub":
                StreamEventsTipMenuIRL.HandleBellyRub();
                break;
            case "Hydrate":
                StreamEventsTipMenuIRL.HandleHydrate();
                break;
            case "Toke Up":
                StreamEventsTipMenuIRL.HandleTokeUp();
                break;
            case "Do One More!":
                StreamEventsTipMenuIRL.HandleDoOneMore();
                break;
            case "Streeeetch!":
                StreamEventsTipMenuIRL.HandleStreeeetch();
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuIRL)}." +
                        $"{_ = nameof(StreamEventsTipMenuIRL.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }
    
    private static void HandleOhai()
    {
        
    }
    
    private static void HandleNiceSmile()
    {
        
    }
    
    private static void HandleThankYou()
    {
        
    }
    
    private static void HandleYouLookGreat()
    {
        
    }
    
    private static void HandleHeadpats()
    {
        
    }
    
    private static void HandleBellyRub()
    {
        
    }
    
    private static void HandleHydrate()
    {
        
    }
    
    private static void HandleTokeUp()
    {
        
    }
    
    private static void HandleDoOneMore()
    {
        
    }
    
    private static void HandleStreeeetch()
    {
        
    }
}