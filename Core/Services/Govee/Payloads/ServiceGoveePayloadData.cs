using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadData()
{
    [JsonPropertyName(
        name: $"sku"
    )]
    public string                              Sku          { get; set; } = _ = "H607C";
    
    [JsonPropertyName(
        name: $"device"
    )]
    public string                              Device       { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"capability"
    )]
    public ServiceGoveePayloadDataCapability Capability { get; set; } = new();
}