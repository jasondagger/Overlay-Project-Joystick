
using System;
using System.Text.Json.Serialization;
using Godot;
using Overlay.Core.Tools;

namespace Overlay.Core.Services.Joysticks.Requests;

[Serializable]
public sealed class ServiceJoystickRequest()
{
    [JsonPropertyName(
        name: "command"
    )]
    public string           Command    { get; set; } = _ = $"message";
    
    [JsonPropertyName(
        name: "identifier"
    )]
    public string           Identifier { get; set; } = _ = "{\"channel\":\"GatewayChannel\"}";
    
    [JsonPropertyName(
        name: "data"
    )]
    public string           Data       { get; set; } = _ = string.Empty;

    public static ServiceJoystickRequest CreateServiceJoystickRequestChatMessage(
        string text,
        string channelId
    )
    {
        var serviceJoystickRequest     = _ = new ServiceJoystickRequest();
        var serviceJoystickRequestData = _ = new ServiceJoystickRequestData();
        
        serviceJoystickRequestData.Action    = _ = $"send_message";
        serviceJoystickRequestData.Text      = _ = $"{_ = text}";
        serviceJoystickRequestData.ChannelId = _ = $"{_ = channelId}";
        
        serviceJoystickRequest.Data = _ = JsonHelper.Serialize(
            @object: _ = serviceJoystickRequestData
        );
        
        return _ = serviceJoystickRequest;
    }
}