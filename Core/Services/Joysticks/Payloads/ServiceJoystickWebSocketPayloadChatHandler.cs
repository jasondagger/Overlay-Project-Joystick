
using Overlay.Core.Contents;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Services.Spotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using Overlay.Core.Services.Geminis;
using Overlay.Core.Services.Godots.TextToSpeeches;
using RandomNumberGenerator = Godot.RandomNumberGenerator;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadChatHandler
{
    internal static void HandleWebSocketPayloadChat(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.AddChatMessage(
            payloadMessage: payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.PlayChatNotificationSoundEffect(
            payloadMessage: payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.HandleStreamerShoutout(
            payloadMessage: payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommands(
            payloadMessage: payloadMessage
        );
        
        ServiceGodotTextToSpeech.Speak(
            message: $"{payloadMessage.Author.Username} says... {payloadMessage.Text}"
        );
    }

    internal static void AddUserForSongRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Add(
            item: username
        );
    }

    internal static void ProcessSongRequest(
        bool succeeded
    )
    {
        var actioner = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Dequeue();
        if (succeeded is true)
        {
            return;
        }
        
        if (actioner.IsSubscriber is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedSongRequestCommand.Remove(
                item: actioner.Name
            );
        }
        else if (actioner.IsTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Add(
                item: actioner.Name
            );
        }
    }

    private class SpotifySongActioner
    {
        internal string Name         = string.Empty;
        internal bool   IsSubscriber = false;
        internal bool   IsTipper     = false;
    }
    
    private const string                               c_joystickUserStreamLinkPrefix         = "https://joystick.tv/u/";
    private const string                               c_streamerUsername                     = $"SmoothDagger";
    private const string                               c_tipCommand                           = $"!tip";
    private const int                                  c_commandRollTheDiceDefaultParameter   = 100;

    private static readonly Queue<SpotifySongActioner> s_pendingSongRequesters                = [];
    private static readonly HashSet<string>            s_pendingSongRequestTippers            = [];
    private static readonly HashSet<string>            s_subscribersWhoUsedLightCommand       = [];
    private static readonly HashSet<string>            s_subscribersWhoUsedSongRequestCommand = [];
    private static readonly HashSet<string>            s_subscribersWhoUsedSongSkipCommand    = [];
    private static readonly HashSet<string>            s_streamersShoutedOut                  = [
        ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername
    ];
    
    private static void AddChatMessage(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var author            = payloadMessage.Author;
        var message           = payloadMessage.Text;
        
        var username          = author.Username;
        var usernameColor     = author.UsernameColor;
        var isModerator       = author.IsModerator;
        var isStreamer        = author.IsStreamer;
        var isSubscriber      = author.IsSubscriber;
        
        var emotes            = payloadMessage.EmotesUsed;
        var numberOfEmotes    = emotes.Length;
        var chatMessageEmotes = new ChatMessageEmote[numberOfEmotes];
        for (var i = 0U; i < numberOfEmotes; i++)
        {
            var emote                = emotes[i];
            chatMessageEmotes[i] = new ChatMessageEmote(
                code: emote.Code,
                url:  emote.SignedUrl
            );
        }
        
        Chat.AddChatMessageToInstances(
            username:          username,
            usernameColor:     usernameColor,
            message:           message,
            chatMessageEmotes: chatMessageEmotes,
            isModerator:       isModerator,
            isStreamer:        isStreamer,
            isSubscriber:      isSubscriber
        );
    }
    
    private static void HandleBotCommandAsk(
        string message
    )
    {
        var serviceGemini = Services.GetService<ServiceGemini>();
        serviceGemini.Ask(
            message: message
        );
    }
    
    private static void HandleBotCommandBallsOfSteel()
    {
        var serviceGodots     = Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.BallsOfSteel
        );
    }

    private static void HandleBotCommandLights(
        string username,
        string parameters,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();

        switch (isStreamer)
        {
            case false when
                isSubscriber is false:
                serviceJoystickBot.SendChatMessage(
                    message: $"Invalid !lights user - only subscribers & SmoothDagger have access to this command."
                );
                return;
            
            case false when
                ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedLightCommand.Contains(
                    item: username
                ):
                serviceJoystickBot.SendChatMessage(
                    message: $"Invalid !lights usage - subscribers can use this only once per stream."
                );
                return;
        }

        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !lights parameter - the following parameters are valid: [color/off]."
            );
            return;
        }

        var command = parameters.ToLower();
        switch (command)
        {
            case "off":
                GoveeLightController.Instance.TurnOffLights();
                break;
            
            case "heatwave":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Heatwave"
                );
                break;
            
            case "icy":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Icy"
                );
                break;

            case "rainbow":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Rainbow"
                );
                break;
            
            case "toxic":
                GoveeLightController.Instance.SetLightScene(
                    sceneName: "Toxic"
                );
                break;
            
            case "red":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Red
                );
                break;
            
            case "yellow":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Yellow
                );
                break;

            case "green":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Green
                );
                break;

            case "cyan":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Cyan
                );
                break;

            case "blue":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Blue
                );
                break;

            case "magenta":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.Magenta
                );
                break;

            case "white":
                GoveeLightController.Instance.SetLightColor(
                    colorType: ServicePastelInterpolator.ColorType.White
                );
                break;
                
            default:
                serviceJoystickBot.SendChatMessage(
                    message: $"Invalid !lights parameter - the color or scene is invalid."
                );
                return;
        }

        ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedLightCommand.Add(
            item: username
        );
    }
    
    private static void HandleBotCommandRockPaperScissors(
        string command,
        string parameters
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is false
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid {command} parameter - {command} has no parameters."
            );
            return;
        }
        
        string[] icons =
        [
            "📄",
            "🗿",
            "✂️"
        ];
        
        var random = new RandomNumberGenerator();
        var index  = random.RandiRange(
            from: 0,
            to:   icons.Length - 1
        );
        
        serviceJoystickBot.SendChatMessage(
            message: $"{icons[index]}"
        );
    }
    
    private static void HandleBotCommandRollTheDice(
        string username,
        string parameters
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !rtd parameter - !rtd must be in the following format: !rtd 69."
            );
            return;
        }
        
        var hasValue = long.TryParse(
            s:      commandSplit[0],
            result: out var value
        );
        var hasParameters = string.IsNullOrEmpty(
            value: parameters
        ) is false;
        
        if (
            
            hasParameters is true &&
            (
                hasValue is false ||
                value <= 0
            )
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !rtd parameter - !rtd parameter must be empty or a whole number greater than 0."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            value = ServiceJoystickWebSocketPayloadChatHandler.c_commandRollTheDiceDefaultParameter;
        }
        
        var random      = new Random();
        var randomValue = random.NextInt64() % value + 1;

        serviceJoystickBot.SendChatMessage(
            message: $"🎲 {username} rolled a {randomValue} out of {value}! 🎲"
        );
    }

    private static void HandleBotCommandSongRequest(
        string username,
        string parameters,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();

        if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Any(pendingSongRequester => pendingSongRequester.Name == username))
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Spotify is still processing your song request. Please wait before making another request."
            );
            return;
        }
        
        var isTipper = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Contains(
            item: username
        );
        
        if (
            isStreamer is false &&
            isSubscriber is false && 
            isTipper is false
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !songrequest user - only subscribers, users who claimed the song request token reward, & SmoothDagger have access to this command."
            );
            return;
        }
        
        var hasSubscriberUsedCommand = ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedSongRequestCommand.Contains(
            item: username
        );
        
        if (
            isStreamer is false &&
            isTipper is false &&
            hasSubscriberUsedCommand is true
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !songrequest usage - subscribers can use this only once per stream."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !songrequest parameter - the following parameters are valid: [search parameters]."
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Enqueue(
            item: new SpotifySongActioner
            {
                Name         = username,
                IsSubscriber = isSubscriber,
                IsTipper     = isTipper
            }
        );

        if (isTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Remove(
                item: username
            );
        }
        else if (isSubscriber is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedSongRequestCommand.Add(
                item: username
            );
        }

        var serviceSpotify = Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestTrackQueueBySearchTerms(
            searchParameters: parameters
        );
    }
    
    private static void HandleBotCommandSongSkip(
        string username,
        string parameters,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();

        switch (isStreamer)
        {
            case false when
                isSubscriber is false:
                serviceJoystickBot.SendChatMessage(
                    message: $"Invalid !songskip user - only subscribers & SmoothDagger have access to this command."
                );
                return;
            
            case false when
                ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedSongSkipCommand.Contains(
                    item: username
                ):
                serviceJoystickBot.SendChatMessage(
                    message: $"Invalid !songskip usage - subscribers can use this only once per stream."
                );
                return;
        }

        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is false
        )
        {
            serviceJoystickBot.SendChatMessage(
                message: $"Invalid !songskip parameter - there are no parameters available."
            );
            return;
        }

        var serviceSpotify = Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestSkipToNextTrack();
        
        ServiceJoystickWebSocketPayloadChatHandler.s_subscribersWhoUsedSongSkipCommand.Add(
            item: username
        );
    }
    
    private static void HandleBotCommands(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = payloadMessage.Text;
        if (
            message.StartsWith(
                value: '!'
            ) is false
        )
        {
            return;
        }
        
        var author       = payloadMessage.Author;
        var username     = author.Username;
        var isSubscriber = author.IsSubscriber;
        var isStreamer   = author.IsStreamer;

        var commandSplit = message.Split(
            separator: ' ',
            count:     2
        );
        var command    = commandSplit[0].ToLower();
        var parameters = commandSplit.Length > 1 ? commandSplit[1].ToLower() : string.Empty;
        switch (command)
        {
            case "!ask":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAsk(
                    message: parameters
                );
                break;
            
            case "!balls":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandBallsOfSteel();
                break;
            
            case "!lights":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandLights(
                    username:     username,
                    parameters:   parameters,
                    isStreamer:   isStreamer,
                    isSubscriber: isSubscriber
                );
                break;
            
            case "!paper":
            case "!rock":
            case "!scissors":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandRockPaperScissors(
                    command:    command,
                    parameters: parameters
                );
                break;
            
            case "!dice":
            case "!roll":
            case "!rollthedice":
            case "!rtd":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandRollTheDice(
                    username:   username,
                    parameters: parameters
                );
                break;
            
            case "!request":
            case "!songrequest":
            case "!sr":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSongRequest(
                    username:     username,
                    parameters:   parameters,
                    isStreamer:   isStreamer,
                    isSubscriber: isSubscriber
                );
                break;
            
            case "!skip":
            case "!skipsong":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSongSkip(
                    username:     username,
                    parameters:   parameters,
                    isStreamer:   isStreamer,
                    isSubscriber: isSubscriber
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
        var author   = payloadMessage.Author;
        var username = author.Username;
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_streamersShoutedOut.Contains(
                username
            ) is true
        )
        {
            return;
        }

        var isContentCreator = author.IsContentCreator;
        if (isContentCreator is false)
        {
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_streamersShoutedOut.Add(
            item: username
        );
                
        string[] messages =
        [
            $"Oh shit, it's {username}! Check out their streams: {ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{username}",
            $"Holy fuck, a wild {username} appeared! Go catch their streams: {ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{username}",
            $"Shoutout to {username}! Check out their streams: {ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{username}",
        ];
        var random = new RandomNumberGenerator();
        var index  = random.RandiRange(
            from: 0,
            to:   messages.Length - 1
        );
        
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: messages[index]
        );
    }
    
    private static void PlayChatNotificationSoundEffect(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = payloadMessage.Text;
        if (
            message.StartsWith(
                value: ServiceJoystickWebSocketPayloadChatHandler.c_tipCommand
            ) is true
        )
        {
            var remainingMessage = message.Replace(
                oldValue: $"{ServiceJoystickWebSocketPayloadChatHandler.c_tipCommand}",
                newValue: string.Empty
            );

            var isValidTip = true;
            foreach (var character in remainingMessage)
            {
                var isCharacterASpace = character is ' ';
                if (isCharacterASpace)
                {
                    continue;
                }

                var isCharacterANumber = char.IsNumber(
                    c: character
                );
                if (isCharacterANumber)
                {
                    break;
                }

                isValidTip = false;
                break;
            }

            if (isValidTip is true)
            {
                return;
            }
        }
        
        var serviceGodots     = Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ChatNotification
        );
    }
}