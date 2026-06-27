
using System;

namespace Overlay.Core.Services.WorkOuts;

internal static class ServiceWorkOutTasks
{
    internal static string GetRandomWorkOutTask()
    {
        var index = Random.Shared.Next(
            minValue: 0, 
            maxValue: ServiceWorkOutTasks.s_workOutTasks.Length
        );
        return ServiceWorkOutTasks.s_workOutTasks[index];
    }
    
    private static readonly string[] s_workOutTasks =
    [
        $"Do 10 Jumping Jacks",
        $"Do 10 Sit-ups",
        $"Do 10 Squats",
        $"Do 10 Burpees",
        $"Do 10 Push-ups",
        $"Do 10 Bicep Curls",
        $"Do 10 Lateral Raises",
        $"Do 10 Overhead Presses",
    ];
}