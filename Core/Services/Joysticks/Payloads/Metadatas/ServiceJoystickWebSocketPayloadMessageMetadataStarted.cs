using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataStarted()
{
    [JsonPropertyName(
        name: $"who"
    )]
    public string Who { get; set; } = _ = string.Empty;
}