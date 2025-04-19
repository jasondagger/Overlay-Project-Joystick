using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadType()
{
    [JsonPropertyName(
        name: $"type"
    )]
    public string Type { get; set; } = _ = string.Empty;
}