
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadQRCode()
{
    [JsonPropertyName(
        name: $"result"
    )]
    public bool       Result     { get; set; } = true;
    
    [JsonPropertyName(
        name: $"uid"
    )]
    public int          Code    { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"message"
    )]
    public string       Message  { get; set; } = string.Empty;
}