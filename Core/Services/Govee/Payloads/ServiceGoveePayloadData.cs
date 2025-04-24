using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadData()
{
    [JsonPropertyName(
        name: $"sku"
    )]
    public string                              Sku          { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"device"
    )]
    public string                              Device       { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"deviceName"
    )]
    public string                              DeviceName   { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"capabilities"
    )]
    public ServiceGoveePayloadDataCapability[] Capabilities { get; set; } = null;
}