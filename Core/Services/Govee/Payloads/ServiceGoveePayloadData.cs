
#nullable enable
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadData()
{
    [JsonPropertyName(
        name: $"sku"
    )]
    public string                             Sku        { get; set; } = "H607C";
    
    [JsonPropertyName(
        name: $"device"
    )]
    public string                             Device     { get; set; } = string.Empty;

    [JsonPropertyName(
        name: "pagination"
    )]
    public ServiceGoveePayloadDataPagination? Pagination { get; set; } = null;
    
    [JsonPropertyName(
        name: $"capability"
    )]
    public ServiceGoveePayloadDataCapability  Capability { get; set; } = new();
}