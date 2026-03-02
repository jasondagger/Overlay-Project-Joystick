
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovenses;

[Serializable()]
public sealed class ServiceLovenseCommand(
    string action,
    double timeInSeconds
)
{
    [JsonPropertyName(
        name: $"action"
    )]
    public string Action        { get; set; } = action;
    
    [JsonPropertyName(
        name: $"apiVer"
    )]
    public int    ApiVersion    { get; set; } = 1;
    
    [JsonPropertyName(
        name: $"command"
    )]
    public string Command       { get; set; } = "Function";
    
    [JsonPropertyName(
        name: $"timeSec"
    )]
    public string TimeInSeconds { get; set; } = $"{timeInSeconds}";
}