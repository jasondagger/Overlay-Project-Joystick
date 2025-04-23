
using System;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;

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
        ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommands(
            payloadMessage: _ = payloadMessage
        );
    }
    
    private const string c_tipCommand                         = $"!tip";
    private const int    c_commandRollTheDiceDefaultParameter = 100;

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
        var hasValue = _ = int.TryParse(
            s:      _ = parameters, 
            result: out var value
        );
        if (
            _ = hasValue is false ||
            string.IsNullOrEmpty(
                value: _ = parameters
            ) is true
        )
        {
            _ = value = _ = ServiceJoystickWebSocketPayloadChatHandler.c_commandRollTheDiceDefaultParameter;
        }
        
        var random      = _ = new Random();
        var randomValue = _ = random.Next() % value + 1;
        
        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
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