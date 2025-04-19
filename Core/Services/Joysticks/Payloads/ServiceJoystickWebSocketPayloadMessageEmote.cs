
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageEmote()
{
    [JsonPropertyName(
        name: $"code"
    )]
    public string                   Code               { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"signedThumbnailUrl"
    )]
    public string                   SignedThumbnailUrl { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"signedUrl"
    )]
    public string                   SignedUrl          { get; set; } = _ = string.Empty;
}