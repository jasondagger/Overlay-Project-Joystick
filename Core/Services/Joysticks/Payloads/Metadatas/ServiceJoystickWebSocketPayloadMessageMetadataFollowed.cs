using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataFollowed()
{
    [JsonPropertyName(
        name: $"what"
    )]
    public string      What { get; set; } = _ = string.Empty;

    [JsonPropertyName(
        name: $"who"
    )]
    public string      Who  { get; set; } = _ = string.Empty;
}