using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayload()
{
    [JsonPropertyName(
        name: $"identifier"
    )]
    public string                                 Identifier { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"message"
    )]
    public ServiceJoystickWebSocketPayloadMessage Message    { get; set; } = null;
    
    [JsonPropertyName(
        name: $"type"
    )]
    public string                                 Type       { get; set; } = _ = string.Empty;
}