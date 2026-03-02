
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorizationValidationResult()
{
    [JsonPropertyName(
        name: $"code"
    )]
    public int                                                    Code    { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"message"
    )]
    public string                                                 Message { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"data"
    )]
    public ServiceLovensePayloadAuthorizationValidationResultData Data    { get; set; } = null;
}