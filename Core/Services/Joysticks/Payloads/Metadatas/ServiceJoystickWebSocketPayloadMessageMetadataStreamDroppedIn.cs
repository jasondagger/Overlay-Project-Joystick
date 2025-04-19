using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn()
{
    [JsonPropertyName(
        name: $"number_of_viewers"
    )]
    public int         NumberOfViewers { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"who"
    )]
    public string      Who             { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"what"
    )]
    public string      What            { get; set; } = _ = string.Empty;
}