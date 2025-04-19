using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataPvpSessionReady()
{
    [JsonPropertyName(
        name: $"cost"
    )]
    public int          Cost  { get; set; } = _ = 0;
    
    [JsonPropertyName(
        name: $"when"
    )]
    public DateTime     When  { get; set; } = _ = DateTime.MinValue;
    
    [JsonPropertyName(
        name: $"where"
    )]
    public string       Where { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"who"
    )]
    public string       Who   { get; set; } = _ = string.Empty;
}