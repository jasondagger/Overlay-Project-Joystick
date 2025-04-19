using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataFollowerCountUpdated()
{
    [JsonPropertyName(
        name: $"number_of_followers"
    )]
    public int NumberOfFollowers { get; set; } = _ = 0;
}