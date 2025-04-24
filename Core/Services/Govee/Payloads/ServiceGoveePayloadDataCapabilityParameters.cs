using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataCapabilityParameters()
{
    [JsonPropertyName(
        name: $"dataType"
    )]
    public string                                              DataType { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"fields"
    )]
    public ServiceGoveePayloadDataCapabilityParametersField[]  Fields   { get; set; } = null;
    
    [JsonPropertyName(
        name: $"options"
    )]
    public ServiceGoveePayloadDataCapabilityParametersOption[] Options  { get; set; } = null;
    
    [JsonPropertyName(
        name: $"range"
    )]
    public ServiceGoveePayloadDataCapabilityParametersRange    Range    { get; set; } = null;
    
    [JsonPropertyName(
        name: $"unit"
    )]
    public string                                              Unit     { get; set; } = _ = string.Empty;
}