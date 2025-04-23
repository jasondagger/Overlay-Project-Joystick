
using Overlay.Core.Contents.Chats;

namespace Overlay.Core.Tools;

using Godot;
using System;

internal static class ConsoleLogger
{
    internal static void LogMessage(
        string message    
    )
    {
        var timeStampSystem = _ = ConsoleLogger.GetTimeStampSystem();
        var fullMessage     = _ = $"{_ = timeStampSystem} {_ = message}";
        
        lock (_ = ConsoleLogger.s_lock)
        {
            GD.Print(
                what: _ = fullMessage
            );
            
            if (Chat.Instance is not null)
            {
                Chat.Instance.AddDebugMessage(
                    message: _ = fullMessage
                );
            }
        }
    }

    internal static void LogMessageError(
        string messageError    
    )
    {
        var timeStampSystem = _ = ConsoleLogger.GetTimeStampSystem();
        var fullMessage     = _ = $"{_ = timeStampSystem} {_ = messageError}";
        lock (_ = ConsoleLogger.s_lock)
        {
            GD.PrintErr(
                what: _ = fullMessage
            );

            if (Chat.Instance is not null)
            {
                Chat.Instance.AddDebugMessage(
                    message: _ = fullMessage
                );
            }
        }
    }

    private static readonly object s_lock = new();

    private static string GetTimeStampSystem()
    {
        var dateTime = _ = DateTime.Now;
        return _ = $"[{_ = dateTime:yyyy-MM-dd} {_ = dateTime:HH:mm:ss.fff}]";
    }
}