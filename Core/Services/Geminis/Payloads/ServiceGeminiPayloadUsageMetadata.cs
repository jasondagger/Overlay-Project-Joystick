using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Geminis.Payloads;

[Serializable()]
public sealed class ServiceGeminiPayloadUsageMetadata()
{
    [JsonPropertyName(
        name: $"candidatesTokenCount"
    )]
    public int                                             CandidatesTokenCount    { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"candidatesTokensDetails"
    )]
    public ServiceGeminiPayloadUsageMetadataTokenDetails[] CandidatesTokensDetails { get; set; } = null;
    
    [JsonPropertyName(
        name: $"promptTokenCount"
    )]
    public int                                             PromptTokenCount        { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"promptTokensDetails"
    )]
    public ServiceGeminiPayloadUsageMetadataTokenDetails[] PromptTokensDetails     { get; set; } = null;
    
    [JsonPropertyName(
        name: $"totalTokenCount"
    )]
    public int                                             TotalTokenCount         { get; set; } = 0;
}