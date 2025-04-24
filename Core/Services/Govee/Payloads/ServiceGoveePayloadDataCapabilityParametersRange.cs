using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataCapabilityParametersRange()
{
    [JsonPropertyName(
        name: $"max"
    )]
    public int             Max       { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"min"
    )]
    public int             Min       { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"precision"
    )]
    public int             Precision { get; set; } = _ = 0;
}