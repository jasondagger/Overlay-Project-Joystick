
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
    public string           Command    { get; set; } = $"message";
    
    [JsonPropertyName(
        name: "identifier"
    )]
    public string           Identifier { get; set; } = "{\"channel\":\"GatewayChannel\"}";
    
    [JsonPropertyName(
        name: "data"
    )]
    public string           Data       { get; set; } = string.Empty;

    public static ServiceJoystickRequest CreateServiceJoystickRequestSendMessage(
        string text,
        string channelId
    )
    {
        var serviceJoystickRequest     = new ServiceJoystickRequest();
        var serviceJoystickRequestData = new ServiceJoystickRequestData
        {
            Action    = $"send_message",
            Text      = $"{text}",
            ChannelId = $"{channelId}"
        };

        serviceJoystickRequest.Data = JsonHelper.Serialize(
            @object: serviceJoystickRequestData
        );
        
        return serviceJoystickRequest;
    }
    
    public static ServiceJoystickRequest CreateServiceJoystickRequestSendWhisper(
        string username,
        string text,
        string channelId
    )
    {
        var serviceJoystickRequest     = new ServiceJoystickRequest();
        var serviceJoystickRequestData = new ServiceJoystickRequestData
        {
            Action    = $"send_whisper",
            Username  = $"{username}",
            Text      = $"{text}",
            ChannelId = $"{channelId}"
        };

        serviceJoystickRequest.Data = JsonHelper.Serialize(
            @object: serviceJoystickRequestData
        );
        
        return serviceJoystickRequest;
    }
}