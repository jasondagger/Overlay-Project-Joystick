
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovense.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorization()
{
    [JsonPropertyName(
        name: $"token"
    )]
    public string       Token     { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"uid"
    )]
    public string       UserId    { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"uname"
    )]
    public string       Username  { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"utoken"
    )]
    public string       UserToken { get; set; } = _ = string.Empty;
}