using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataViewerCountUpdated()
{
    [JsonPropertyName(
        name: $"number_of_viewers"
    )]
    public int NumberOfViewers { get; set; } = _ = 0;
}