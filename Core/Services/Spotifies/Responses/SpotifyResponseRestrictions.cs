
using System;
using System.Text.Json.Serialization;
    
namespace Overlay.Core.Services.Spotifies.Responses;

[Serializable]
public sealed class SpotifyResponseRestrictions
{
    [JsonPropertyName(name: "reason")]
    public string Reason { get; set; } = string.Empty;
}