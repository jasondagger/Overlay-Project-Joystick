using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataCapability()
{
    [JsonPropertyName(
        name: $"type"
    )]
    public string                                      Type       { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"instance"
    )]
    public string                                      Instance   { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"parameters"
    )]
    public ServiceGoveePayloadDataCapabilityParameters Parameters { get; set; } = null;
}