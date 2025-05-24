using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Geminis.Payloads;

[Serializable()]
public sealed class ServiceGeminiPayload()
{
    [JsonPropertyName(
        name: $"candidates"
    )]
    public ServiceGeminiPayloadCandidate[]   Candidates    { get; set; } = null;
    
    [JsonPropertyName(
        name: $"usageMetadata"
    )]
    public ServiceGeminiPayloadUsageMetadata UsageMetaData { get; set; } = null;
    
    [JsonPropertyName(
        name: $"modelVersion"
    )]
    public string                            ModelVersion  { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"responseId"
    )]
    public string                            ResponseId    { get; set; } = string.Empty;
}