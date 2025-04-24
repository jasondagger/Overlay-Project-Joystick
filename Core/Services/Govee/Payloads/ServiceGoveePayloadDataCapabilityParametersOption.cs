using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataCapabilityParametersOption()
{
    [JsonPropertyName(
        name: $"name"
    )]
    public string       Name  { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"value"
    )]
    public object       Value { get; set; } = null;
}