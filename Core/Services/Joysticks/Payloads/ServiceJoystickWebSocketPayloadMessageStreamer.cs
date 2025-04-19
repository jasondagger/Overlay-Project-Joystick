using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageStreamer()
{
    [JsonPropertyName(
        name: $"signedPhotoThumbUrl"
    )]
    public string                    SignedPhotoThumbUrl { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"signedPhotoUrl"
    )]
    public string                    SignedPhotoUrl      { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"slug"
    )]
    public string                    Slug                { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"username"
    )]
    public string                    Username            { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"usernameColor"
    )]
    public string                    UsernameColor       { get; set; } = _ = string.Empty;
}