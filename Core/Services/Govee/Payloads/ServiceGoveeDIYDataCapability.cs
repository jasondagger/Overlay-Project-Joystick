using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDIYCapability()
{
    [JsonPropertyName(
        name: $"type"
    )]
    public string                                      Type     { get; set; } = _ = "devices.capabilities.color_setting";
    
    [JsonPropertyName(
        name: $"instance"
    )]
    public string                                      Instance { get; set; } = _ = $"colorRgb";
    
    [JsonPropertyName(
        name: $"parameters"
    )]
    public ServiceGoveePayloadDataCapabilityParameters Parameters    { get; set; } = new();
}