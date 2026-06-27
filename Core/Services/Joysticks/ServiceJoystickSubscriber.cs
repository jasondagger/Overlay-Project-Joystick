
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks;

[Serializable]
public sealed class ServiceJoystickSubscriber
{
    [JsonPropertyName(
        name: "username"
    )]
    public string                Username        { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: "expires_at"
    )]
    public string                ExpiresAt       { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: "renewal"
    )]
    public bool                  Renewal         { get; set; } = false;
    
    [JsonPropertyName(
        name: "streak"
    )]
    public int                   Streak          { get; set; } = 0;

    [JsonPropertyName(
        name: "total_subscribed"
    )]
    public int                   TotalSubscribed { get; set; } = 0;
    
    [JsonPropertyName(
        name: "nickname"
    )]
    public string                Nickname        { get; set; } = string.Empty;

    [JsonPropertyName(
        name: "username_color"
    )]
    public string                UsernameColor   { get; set; } = string.Empty;
}