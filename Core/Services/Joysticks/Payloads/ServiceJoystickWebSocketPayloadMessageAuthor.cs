using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageAuthor()
{
    [JsonPropertyName(
        name: $"displayNameWithFlair"
    )]
    public string                     DisplayNameWithFlair { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"isModerator"
    )]
    public bool                       IsModerator          { get; set; } = _ = false;
    
    [JsonPropertyName(
        name: $"isSiteStreamer"
    )]
    public bool                       IsSiteStreamer       { get; set; } = _ = false;
    
    [JsonPropertyName(
        name: $"isStreamer"
    )]
    public bool                       IsStreamer           { get; set; } = _ = false;
    
    [JsonPropertyName(
        name: $"isSubscriber"
    )]
    public bool                       IsSubscriber         { get; set; } = false;
    
    [JsonPropertyName(
        name: $"signedPhotoThumbUrl"
    )]
    public string                     SignedPhotoThumbUrl  { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"signedPhotoUrl"
    )]
    public string                     SignedPhotoUrl       { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"slug"
    )]
    public string                     Slug                 { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"username"
    )]
    public string                     Username             { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"usernameColor"
    )]
    public string                     UsernameColor        { get; set; } = null;
}