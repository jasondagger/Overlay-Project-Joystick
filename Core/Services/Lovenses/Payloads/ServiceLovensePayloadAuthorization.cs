
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorization()
{
    [JsonPropertyName(
        name: $"token"
    )]
    public string       Token     { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"uid"
    )]
    public string       UserId    { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"uname"
    )]
    public string       Username  { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"utoken"
    )]
    public string       UserToken { get; set; } = string.Empty;
}