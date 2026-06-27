using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Govee.Payloads;

[Serializable()]
public sealed class ServiceGoveePayloadDataPagination
{
    [JsonPropertyName(
        name: "pageSize"
    )]
    public int            PageSize  { get; set; } = 50;

    [JsonPropertyName(
        name: "pageToken"
    )]
    public string         PageToken { get; set; } = string.Empty;
}