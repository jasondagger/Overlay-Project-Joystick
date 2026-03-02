using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveeDIYData()
{
    [JsonPropertyName(
        name: $"sku"
    )]
    public string                              Sku          { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"device"
    )]
    public string                              Device       { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"capabilities"
    )]
    public ServiceGoveePayloadDIYCapability[] Capabilities { get; set; } = [];
}