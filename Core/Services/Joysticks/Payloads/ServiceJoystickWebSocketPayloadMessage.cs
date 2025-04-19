using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessage()
{
    [JsonPropertyName(
        name: $"author"
    )]
    public ServiceJoystickWebSocketPayloadMessageAuthor   Author            { get; set; } = null;
    
    [JsonPropertyName(
        name: $"botCommand"
    )]
    public string                                         BotCommand        { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"botCommandArg"
    )]
    public string                                         BotCommandArg     { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"channelId"
    )]
    public string                                         ChannelId         { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"createdAt"
    )]
    public string                                         CreatedAt         { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"emotesUsed"
    )]
    public ServiceJoystickWebSocketPayloadMessageEmote[]  EmotesUsed        { get; set; } = null;
    
    [JsonPropertyName(
        name: $"event"
    )]
    public string                                         Event             { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"highlight"
    )]
    public bool                                           Highlight         { get; set; } = _ = false;
    
    [JsonPropertyName(
        name: $"id"
    )]
    public string                                         Id                { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"mention"
    )]
    public bool                                           Mention           { get; set; } = _ = false;
    
    [JsonPropertyName(
        name: $"mentionedUsername"
    )]
    public string                                         MentionedUsername { get; set; } = null;
    
    [JsonPropertyName(
        name: $"messageId"
    )]
    public string                                         MessageId         { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"metadata"
    )]
    public string                                         Metadata          { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"streamer"
    )]
    public ServiceJoystickWebSocketPayloadMessageStreamer Streamer          { get; set; } = null;
    
    [JsonPropertyName(
        name: $"text"
    )]
    public string                                         Text              { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"type"
    )]
    public string                                         Type              { get; set; } = _ = string.Empty;
    
    [JsonPropertyName(
        name: $"visibility"
    )]
    public string                                         Visibility        { get; set; } = _ = string.Empty;
}