using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataMilestoneCompleted()
{
    [JsonPropertyName(
        name: $"amount"
    )]
    public int          Amount { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"title"
    )]
    public string       Title    { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"what"
    )]
    public string       What    { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"who"
    )]
    public string       Who     { get; set; } = string.Empty;
}