using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataCapability()
{
    [JsonPropertyName(
        name: $"type"
    )]
    public string         Type     { get; set; } = _ = "devices.capabilities.color_setting";
    
    [JsonPropertyName(
        name: $"instance"
    )]
    public string         Instance { get; set; } = _ = $"colorRgb";
    
    [JsonPropertyName(
        name: $"value"
    )]
    public int            Value    { get; set; } = _ = 0;
}