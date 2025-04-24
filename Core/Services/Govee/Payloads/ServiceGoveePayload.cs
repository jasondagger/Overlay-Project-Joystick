
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayload()
{
    [JsonPropertyName(
        name: $"code"
    )]
    public string                    Code    { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"message"
    )]
    public string                    Message { get; set; } = _ = string.Empty;

    [JsonPropertyName(
        name: $"data"
    )]
    public ServiceGoveePayloadData[] Data    { get; set; } = null;
}