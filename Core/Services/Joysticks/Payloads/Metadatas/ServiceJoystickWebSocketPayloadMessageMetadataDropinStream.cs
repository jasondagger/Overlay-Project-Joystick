using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataDropinStream()
{
    [JsonPropertyName(
        name: $"destination"
    )]
    public string                     Destination         { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"destination_username"
    )]
    public string                     DestinationUsername { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"number_of_viewers"
    )]
    public int                        NumberOfViewers     { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"origin"
    )]
    public string                     Origin              { get; set; } = string.Empty;
}