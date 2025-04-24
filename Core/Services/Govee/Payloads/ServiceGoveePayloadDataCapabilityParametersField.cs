using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataCapabilityParametersField()
{
    [JsonPropertyName(
        name: $"dataType"
    )]
    public string                                              DataType  { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"fieldName"
    )]
    public string                                              FieldName { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"options"
    )]
    public ServiceGoveePayloadDataCapabilityParametersOption[] Options   { get; set; } = null;
    
    [JsonPropertyName(
        name: $"range"
    )]
    public ServiceGoveePayloadDataCapabilityParametersRange    Range     { get; set; } = null;
    
    [JsonPropertyName(
        name: $"required"
    )]
    public bool                                                Required  { get; set; } = _ = false;
}