using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Geminis.Payloads;

[Serializable()]
public sealed class ServiceGeminiPayloadCandidate()
{
    [JsonPropertyName(
        name: $"content"
    )]
    public ServiceGeminiPayloadCandidateContent Content      { get; set; } = null;
    
    [JsonPropertyName(
        name: $"finishReason"
    )]
    public string                               FinishReason { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"avgLogprobs"
    )]
    public double                               AvgLogprobs  { get; set; } = 0d;
}