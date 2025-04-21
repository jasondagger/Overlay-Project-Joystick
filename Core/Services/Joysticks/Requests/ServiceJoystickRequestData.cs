
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Requests;

[Serializable]
public sealed class ServiceJoystickRequestData
{
    [JsonPropertyName(
        name: "action"
    )]
    public string         Action    { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: "channelId"
    )]
    public string         ChannelId { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: "messageId"
    )]
    public string         MessageId { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: "text"
    )]
    public string         Text      { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: "username"
    )]
    public string         Username  { get; set; } = _ = string.Empty;
}