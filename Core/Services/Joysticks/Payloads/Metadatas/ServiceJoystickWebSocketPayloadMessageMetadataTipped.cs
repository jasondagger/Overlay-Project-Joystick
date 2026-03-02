
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataTipped()
{
    [JsonPropertyName(
        name: $"how_much"
    )]
    public int                 HowMuch     { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"tip_menu_item"
    )]
    public string              TipMenuItem { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"what"
    )]
    public string              What        { get; set; } = string.Empty;

    [JsonPropertyName(
        name: $"who"
    )]
    public string              Who         { get; set; } = string.Empty;
}