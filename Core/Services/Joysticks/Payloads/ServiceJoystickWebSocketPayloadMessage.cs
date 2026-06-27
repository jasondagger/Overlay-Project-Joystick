using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessage()
{
    [JsonPropertyName(
        name: $"author"
    )]
    public ServiceJoystickWebSocketPayloadMessageAuthor   Author            { get; set; } = new();
    
    [JsonPropertyName(
        name: $"botCommand"
    )]
    public string                                         BotCommand        { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"botCommandArg"
    )]
    public string                                         BotCommandArg     { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"channelId"
    )]
    public string                                         ChannelId         { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"createdAt"
    )]
    public string                                         CreatedAt         { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"emotesUsed"
    )]
    public ServiceJoystickWebSocketPayloadMessageEmote[]  EmotesUsed        { get; set; } = null;
    
    [JsonPropertyName(
        name: $"event"
    )]
    public string                                         Event             { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"highlight"
    )]
    public bool                                           Highlight         { get; set; } = false;
    
    [JsonPropertyName(
        name: $"id"
    )]
    public string                                         Id                { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"mention"
    )]
    public bool                                           Mention           { get; set; } = false;
    
    [JsonPropertyName(
        name: $"mentionedUsername"
    )]
    public string                                         MentionedUsername { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"messageId"
    )]
    public string                                         MessageId         { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"metadata"
    )]
    public string                                         Metadata          { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"streamer"
    )]
    public ServiceJoystickWebSocketPayloadMessageStreamer Streamer          { get; set; } = new();
    
    [JsonPropertyName(
        name: $"text"
    )]
    public string                                         Text              { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"type"
    )]
    public string                                         Type              { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"visibility"
    )]
    public string                                         Visibility        { get; set; } = string.Empty;
}