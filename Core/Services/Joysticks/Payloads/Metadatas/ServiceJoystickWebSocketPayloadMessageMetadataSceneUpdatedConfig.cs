using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataSceneUpdatedConfig()
{
    [JsonPropertyName(
        name: $"completedColor"
    )]
    public string               CompletedColor { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"fontSize"
    )]
    public string               FontSize       { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"progressColor"
    )]
    public string               ProgressColor  { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"titleColor"
    )]
    public string               TitleColor     { get; set; } = _ = string.Empty;
}