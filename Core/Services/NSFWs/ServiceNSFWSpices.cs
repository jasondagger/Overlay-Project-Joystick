
using System;

namespace Overlay.Core.Services.NSFWs;

internal static class ServiceNSFWSpices
{
    internal static string GetRandomNSFWSpice()
    {
        var index = Random.Shared.Next(
            minValue: 0, 
            maxValue: ServiceNSFWSpices.s_nsfwSpices.Length
        );
        return ServiceNSFWSpices.s_nsfwSpices[index];
    }
    
    private static readonly string[] s_nsfwSpices =
    [
        $"Flash Ass for 10 Seconds",
        $"Flash Cock for 10 Seconds",
        $"Pinch Nipples for 10 Seconds",
    ];
}