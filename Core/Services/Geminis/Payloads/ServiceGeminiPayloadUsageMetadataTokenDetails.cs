using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Geminis.Payloads;

[Serializable()]
public sealed class ServiceGeminiPayloadUsageMetadataTokenDetails()
{
    [JsonPropertyName(
        name: $"modality"
    )]
    public string           Modality   { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"tokenCount"
    )]
    public int              TokenCount { get; set; } = 0;
}