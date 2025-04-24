
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.OBS;

[Serializable()]
public sealed class ServiceOBSRequestSceneChange(
    string requestType,
    string sceneName
)
{
    [JsonPropertyName(
        name: $"request-type"
    )]
    public string             RequestType { get; set; } = _ = requestType;
    
    [JsonPropertyName(
        name: $"scene-name"
    )]
    public string             SceneName   { get; set; } = _ = sceneName;
}