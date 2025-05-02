
using System;
using System.Text.Json.Serialization;
    
namespace Overlay.Core.Services.Spotifies.Responses;

[Serializable]
public sealed class SpotifyResponseExternalUrls
{
    [JsonPropertyName(name: "spotify")]
    public string Spotify { get; set; } = string.Empty;
}