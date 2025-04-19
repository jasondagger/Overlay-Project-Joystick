using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataTipGoalDeleted()
{
    [JsonPropertyName(
        name: $"amount"
    )]
    public int          Amount { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"title"
    )]
    public string       Title  { get; set; } = _ = string.Empty;
}