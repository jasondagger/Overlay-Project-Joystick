
namespace Overlay.Core.Tools;

using Godot;
using System;

internal static class ConsoleLogger
{
    internal static void LogMessage(
        string message    
    )
    {
        lock (_ = ConsoleLogger.s_lock)
        {
            var timeStampSystem = _ = ConsoleLogger.GetTimeStampSystem();
            GD.Print(
                what: _ = $"{_ = timeStampSystem} {_ = message}"
            );
        }
    }

    internal static void LogMessageError(
        string messageError    
    )
    {
        lock (_ = ConsoleLogger.s_lock)
        {
            var timeStampSystem = _ = ConsoleLogger.GetTimeStampSystem();
            GD.PrintErr(
                what: _ = $"{_ = timeStampSystem} {_ = messageError}"
            );
        }
    }

    private static readonly object s_lock = new();

    private static string GetTimeStampSystem()
    {
        var dateTime = _ = DateTime.Now;
        return _ = $"[{_ = dateTime:yyyy-MM-dd} {_ = dateTime:HH:mm:ss.fff}]";
    }
}