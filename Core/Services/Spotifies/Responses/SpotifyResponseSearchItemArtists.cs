
using System;
using System.Text.Json.Serialization;
    
namespace Overlay.Core.Services.Spotifies.Responses;

[Serializable]
public sealed class SpotifyResponseSearchItemArtists
{
    [JsonPropertyName(name: "access_token")]
    public string AccessToken { get; set; } = string.Empty;
}