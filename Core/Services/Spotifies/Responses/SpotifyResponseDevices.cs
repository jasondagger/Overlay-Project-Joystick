
using System;
using System.Text.Json.Serialization;
    
namespace Overlay.Core.Services.Spotifies.Responses;

[Serializable]
public sealed class SpotifyResponseDevices
{
    [JsonPropertyName(name: "devices")]
    public SpotifyResponseDevice[] Devices { get; set; } = null;
}