using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataTipGoalIncreased()
{
    [JsonPropertyName(
        name: $"amount"
    )]
    public int            Amount   { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"by_user"
    )]
    public string         ByUser   { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"current"
    )]
    public int            Current  { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"previous"
    )]
    public int            Previous { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"what"
    )]
    public string         What     { get; set; } = _ = string.Empty;
}