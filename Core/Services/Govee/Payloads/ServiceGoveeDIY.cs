
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveeDIY()
{
    [JsonPropertyName(
        name: $"requestId"
    )]
    public string              RequestId { get; set; } = _ = "uuid";
    
    [JsonPropertyName(
        name: $"message"
    )]
    public string              Message   { get; set; } = _ = "success";
    
    [JsonPropertyName(
        name: $"code"
    )]
    public int                 Code      { get; set; } = _ = 0;

    [JsonPropertyName(
        name: $"payload"
    )]
    public ServiceGoveeDIYData Payload   { get; set; } = new();
}