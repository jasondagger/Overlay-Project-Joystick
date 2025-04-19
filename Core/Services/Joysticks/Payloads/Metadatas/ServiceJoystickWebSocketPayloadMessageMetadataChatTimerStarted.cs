using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataChatTimerStarted()
{
    [JsonPropertyName(
        name: $"username"
    )]
    public string       Name   { get; set; } = _ = string.Empty;

    [JsonPropertyName(
        name: $"endsAt"
    )]
    public DateTime     EndsAt { get; set; } = _ = DateTime.MinValue;
}