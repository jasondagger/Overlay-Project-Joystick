
using Overlay.Core.Tools;

namespace Overlay.Core.Contents;

internal static class StreamEventsTipMenuToys
{
    internal static void HandleTipMenuItem(
        string tipMenuItem
    )
    {
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