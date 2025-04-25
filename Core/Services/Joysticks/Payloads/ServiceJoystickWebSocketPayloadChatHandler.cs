
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Godot;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.JoystickBots;
using RandomNumberGenerator = Godot.RandomNumberGenerator;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadChatHandler
{
    internal static void HandleWebSocketPayloadChat(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.AddChatMessage(
            payloadMessage: _ = payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.PlayChatNotificationSoundEffect(
            payloadMessage: _ = payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.HandleStreamerShoutout(
            payloadMessage: _ = payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommands(
            payloadMessage: _ = payloadMessage
        );
    }
    
    private const string                    c_joystickUserStreamLinkPrefix       = "https://joystick.tv/u/";
    private const string                    c_streamerUsername                   = $"SmoothDagger";
    private const string                    c_tipCommand                         = $"!tip";
    private const int                       c_commandRollTheDiceDefaultParameter = 100;

    private static readonly HashSet<string> s_streamersShoutedOut                = [
        _ = ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername
    ];
    
    private static void AddChatMessage(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var author            = _ = payloadMessage.Author;
        var message           = _ = payloadMessage.Text;
        
        var username          = _ = author.Username;
        var usernameColor     = _ = author.UsernameColor;
        var isModerator       = _ = author.IsModerator;
        var isStreamer        = _ = author.IsStreamer;
        var isSubscriber      = _ = author.IsSubscriber;
        
        var emotes            = _ = payloadMessage.EmotesUsed;
        var numberOfEmotes    = _ = emotes.Length;
        var chatMessageEmotes = _ = new ChatMessageEmote[_ = numberOfEmotes];
        for (var i = _ = 0U; _ = i < numberOfEmotes; _ = i++)
        {
            var emote                = _ = emotes[i];
            _ = chatMessageEmotes[i] = _ = new ChatMessageEmote(
                code: _ = emote.Code,
                url:  _ = emote.SignedUrl
            );
        }
        
        Chat.Instance.AddChatMessage(
            username:          _ = username,
            usernameColor:     _ = usernameColor,
            message:           _ = message,
            chatMessageEmotes: _ = chatMessageEmotes,
            isModerator:       _ = isModerator,
            isStreamer:        _ = isStreamer,
            isSubscriber:      _ = isSubscriber
        );
    }

    private static void HandleBotCommandRollTheDice(
        string username,
        string parameters
    )
    {
        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
        
        var commandSplit = _ = parameters.Split(
            separator: _ = ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: _ = $"Invalid !rtd parameter - !rtd must be in the following format: !rtd 69."
            );
            return;
        }
        
        var hasValue = _ = long.TryParse(
            s:      _ = commandSplit[0],
            result: out var value
        );
        var hasParameters = _ = string.IsNullOrEmpty(
            value: _ = parameters
        ) is false;
        
        if (
            _ = 
            hasParameters is true &&
            (
                hasValue is false ||
                value <= 0
            )
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: _ = $"Invalid !rtd parameter - !rtd parameter must be empty or a whole number greater than 0."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: _ = parameters
            ) is true
        )
        {
            _ = value = _ = ServiceJoystickWebSocketPayloadChatHandler.c_commandRollTheDiceDefaultParameter;
        }
        
        var random      = _ = new Random();
        var randomValue = _ = random.NextInt64() % value + 1;

        serviceJoystickBot.SendChatMessage(
            message: _ = $"🎲 {_ = username} rolled a {_ = randomValue} out of {_ = value}! 🎲"
        );
    }
    
    private static void HandleBotCommands(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = _ = payloadMessage.Text;
        if (
            message.StartsWith(
                value: _ = '!'
            ) is false
        )
        {
            return;
        }
        
        var author   = _ =  payloadMessage.Author;
        var username = _ =  author.Username;

        var commandSplit = _ = message.Split(
            separator: _ = ' ',
            count:     _ = 2
        );
        var command    = _ = commandSplit[0].ToLower();
        var parameters = _ = commandSplit.Length > 1 ? commandSplit[1].ToLower() : string.Empty;
        switch (_ = command)
        {
            case "!dice":
            case "!roll":
            case "!rollthedice":
            case "!rtd":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandRollTheDice(
                    username:   _ = username,
                    parameters: _ = parameters
                );
                break;
            
            default:
                return;
        }
    }

    private static void HandleStreamerShoutout(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var author   = _ = payloadMessage.Author;
        var username = _ = author.Username;
        if (
            _ = ServiceJoystickWebSocketPayloadChatHandler.s_streamersShoutedOut.Contains(
                username
            ) is true
        )
        {
            return;
        }
        
        var serviceGodots    = _ = Services.GetService<ServiceGodots>();
        var serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
        
        serviceGodotHttp.SendHttpRequest(
            url:                     _ = $"{_ = ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{_ = username}",
            headers:                 [],
            method:                  _ = HttpClient.Method.Get,
            json:                    _ = string.Empty,
            requestCompletedHandler: (
                long     result,
                long     responseCode,
                string[] headers,
                byte[]   body
            ) =>
            {
                var bodyAsString = _ = Encoding.UTF8.GetString(
                    bytes: _ = body
                );

                if (
                    _ = bodyAsString.Contains(
                        value: _ = $"<title>{username}"
                    ) is false
                )
                {
                    return;
                }

                ServiceJoystickWebSocketPayloadChatHandler.s_streamersShoutedOut.Add(
                    item: _ = username
                );
                
                string[] messages =
                [
                    $"Oh shit, it's {_ = username}! Check out their streams: {_ = ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{_ = username}",
                    $"Holy fuck, a wild {_ = username} appeared! Go catch their streams: {_ = ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{_ = username}",
                    $"Shoutout to {_ = username}! Check out their streams: {_ = ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{_ = username}",
                ];
                var random = _ = new RandomNumberGenerator();
                var index  = _ = random.RandiRange(
                    from: _ = 0,
                    to:   _ = messages.Length - 1
                );

                var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessage(
                    message: _ = messages[index]
                );
            }
        );
    }
    
    private static void PlayChatNotificationSoundEffect(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = _ = payloadMessage.Text;
        if (
            _ = message.StartsWith(
                value: _ = ServiceJoystickWebSocketPayloadChatHandler.c_tipCommand
            ) is true
        )
        {
            var remainingMessage = _ = message.Replace(
                oldValue: _ = $"{_ = ServiceJoystickWebSocketPayloadChatHandler.c_tipCommand}",
                newValue: _ = string.Empty
            );

            var isValidTip = _ = true;
            for (var index = _ = 0; _ = index < remainingMessage.Length; _ = index++)
            {
                var character = _ = remainingMessage[index];
                
                var isCharacterASpace = _ = character is ' ';
                if (_ = isCharacterASpace)
                {
                    continue;
                }

                var isCharacterANumber = _ = char.IsNumber(
                    c: _ = character
                );
                if (_ = isCharacterANumber)
                {
                    break;
                }

                _ = isValidTip = _ = false;
                break;
            }

            if (_ = isValidTip is true)
            {
                return;
            }
        }
        
        var serviceGodots     = _ = Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.ChatNotification
        );
    }
}