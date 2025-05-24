using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Geminis.Payloads;

[Serializable()]
public sealed class ServiceGeminiPayloadCandidateContentParts()
{
    [JsonPropertyName(
        name: $"text"
    )]
    public string Text { get; set; } = _ = string.Empty;
}