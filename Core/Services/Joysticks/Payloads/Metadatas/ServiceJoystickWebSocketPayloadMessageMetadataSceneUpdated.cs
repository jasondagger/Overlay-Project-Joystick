using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataSceneUpdated()
{
    [JsonPropertyName(
        name: $"config"
    )]
    public ServiceJoystickWebSocketPayloadMessageMetadataSceneUpdatedConfig Config { get; set; } = null;
    
    [JsonPropertyName(
        name: $"username"
    )]
    public string                                                           Name   { get; set; } = _ = string.Empty;
}