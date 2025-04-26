
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayload()
{
    [JsonPropertyName(
        name: $"requestId"
    )]
    public string                  RequestId { get; set; } = _ = "uuid";

    [JsonPropertyName(
        name: $"payload"
    )]
    public ServiceGoveePayloadData Payload   { get; set; } = new();
}