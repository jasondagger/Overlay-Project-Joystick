using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorizationResult()
{
    [JsonPropertyName(
        name: $"code"
    )]
    public int                                     Code    { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"message"
    )]
    public string                                  Message { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"data"
    )]
    public ServiceLovensePayloadAuthorizationToken Data    { get; set; } = null;
}