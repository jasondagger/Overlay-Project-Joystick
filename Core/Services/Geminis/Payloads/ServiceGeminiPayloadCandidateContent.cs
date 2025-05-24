using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Geminis.Payloads;

[Serializable()]
public sealed class ServiceGeminiPayloadCandidateContent()
{
    [JsonPropertyName(
        name: $"parts"
    )]
    public ServiceGeminiPayloadCandidateContentParts[] Parts { get; set; } = null;
    
    [JsonPropertyName(
        name: $"role"
    )]
    public string                                      Role  { get; set; } = string.Empty;
}