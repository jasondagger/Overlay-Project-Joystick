using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataMilestoneCompleted()
{
    [JsonPropertyName(
        name: $"amount"
    )]
    public int          Amount { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"title"
    )]
    public string       Title    { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"what"
    )]
    public string       What    { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"who"
    )]
    public string       Who     { get; set; } = _ = string.Empty;
}