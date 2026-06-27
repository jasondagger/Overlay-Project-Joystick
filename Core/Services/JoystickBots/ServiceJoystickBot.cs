
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots.TextToSpeeches;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Joysticks.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Services.JoystickBots;

internal sealed class ServiceJoystickBot() :
    IService
{
    Task IService.Setup()
    {
        this.RegisterForRetrievedJoystickData();
        return Task.CompletedTask;
    }

    Task IService.Start()
    {
        this.StartSendingNotificationMessages();
        return Task.CompletedTask;
    }

    Task IService.Stop()
    {
        return Task.CompletedTask;
    }

    internal void SendChatMessage(
        string message
    )
    {
        var serviceJoystick        = Services.GetService<ServiceJoystick>();
        var serviceJoystickRequest = ServiceJoystickRequest.CreateServiceJoystickRequestSendMessage(
            text:      message,
            channelId: this.m_joystickChannelId
        );
        
        serviceJoystick.SendRequest(
            serviceJoystickRequest: serviceJoystickRequest
        );
        
        Chat.AddChatMessageToInstances(
            username:            $"{ServiceJoystickBot.c_username}",
            hasCustomBadgeColor: true,
            customBadgeColor:    ServiceColorInterpolatorColorMode.White,
            hasCustomNameColor:  true,
            customNameColor:     ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20,
            message:             message,
            chatMessageEmotes:   null,
            isModerator:         false,
            isStreamer:          false,
            isSubscriber:        false,
            isBot:               true
        );
        
        ServiceGodotTextToSpeech.Speak(
            message: $"{ServiceJoystickBot.c_usernameTTS} says... {message}"
        );
    }
    
    internal void SendChatMessageSilently(
        string message
    )
    {
        var serviceJoystick        = Services.GetService<ServiceJoystick>();
        var serviceJoystickRequest = ServiceJoystickRequest.CreateServiceJoystickRequestSendMessage(
            text:      message,
            channelId: this.m_joystickChannelId
        );
        
        serviceJoystick.SendRequest(
            serviceJoystickRequest: serviceJoystickRequest
        );
        
        Chat.AddChatMessageToInstances(
            username:            $"{ServiceJoystickBot.c_username}",
            hasCustomBadgeColor: true,
            customBadgeColor:    ServiceColorInterpolatorColorMode.White,
            hasCustomNameColor:  true,
            customNameColor:     ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20,
            message:             message,
            chatMessageEmotes:   null,
            isModerator:         false,
            isStreamer:          false,
            isSubscriber:        false,
            isBot:               true
        );
    }
    
    internal void SendWhisper(
        string username,
        string message
    )
    {
        var serviceJoystick        = Services.GetService<ServiceJoystick>();
        var serviceJoystickRequest = ServiceJoystickRequest.CreateServiceJoystickRequestSendWhisper(
            username:  username,
            text:      message,
            channelId: this.m_joystickChannelId
        );
        
        serviceJoystick.SendRequest(
            serviceJoystickRequest: serviceJoystickRequest
        );
    }

    private const int                c_notificationDelayInMilliseconds = 300000;
    private const string             c_username                        = "SmoothBot";
    private const string             c_usernameTTS                     = "Smooth Bot";
    private const string             c_topJoystickTipperReplacement    = "{TopJoystickTipper}";

    private static readonly string[] s_notificationMessages            =
    [
        $"📣 Customize your avatar! Check the bio for commands on how to make yourself unique! There's over 100 quadrillion possibilities!",
        $"📣 Get 5 free Gush Control Link minutes when you subscribe!",
        $"📣 Get 1 free Gush Control Link minute every 1000 tokens you spend; 500 tokens for subscribers!",
        $"📣 Gush Control Links also control the Edge if SmoothDagger is using it!",
        $"📣 Influence the games we play using the 🕹️ Gaming tip menu!",
        $"📣 Lewd up the stream using the 🥵 NSFW tip menu!",
        $"📣 Make sure you're following the rules! Check the bio below for more details.",
        $"📣 New followers & subscribers get their choice of a cock flash, ass flash, or nipple pinch for 10 seconds using the !claim command once per stream!",
        $"📣 Not digging the lights? Subscribers get access to one free !lights command per stream!",
        $"📣 Play Team Fortress 2 & have a Lovense toy? Download the LovenseFortress app for free - link in bio!",
        $"📣 Make some noise with sound effects! Subscribers get access to the !sfx command once per stream.",
        //$"📣 Shoutout to the all-time highest tipper, {ServiceJoystickBot.c_topJoystickTipperReplacement}! Thank you so much for the support!",
        $"📣 Take control of the stream! Interact with SmoothDagger using the ☺️ IRL tip menu!",
        $"📣 Team Fortress 2 is free to play on Steam! Come join in on the fun!",
        $"📣 Test your luck using the !rtd & !rock, !paper, !scissor commands!",
        $"📣 Tip rewards may be delayed while SmoothDagger is walking to prevent injuries.",
        $"📣 Type !bank check to see how many Gush Control Link minutes you have!",
        $"📣 Type !bank withdraw to use your banked Gush Control Link minutes!",
        $"📣 Want access to exclusive savings? Subscribers get 50% off using the 🌟 Subscriber tip menu!",
        $"📣 Want free Gush Control Link minutes? Subscribers get a free Gush Control Link minute each stream they watch!",
        $"📣 Want free Gush Control Link minutes? New followers get a free Gush Control Link minute!",
        $"📣 Want to control the audio & visuals of the stream? Check out the ✨ Avatars, Lights, & Music tip menu!",
        $"📣 Want to play with us? Add SmoothDagger on Steam!",
        $"📣 Want to vibe to your own music? Subscribers get access to one free !song request & !song skip command per stream!",
    ];

    private string                   m_joystickChannelId               = string.Empty;
    
    private static string ReplaceTopTipperText(
        string message, 
        string topJoystickTipper
    )
    {
        return message.Replace(
            oldValue: ServiceJoystickBot.c_topJoystickTipperReplacement, 
            newValue: message
        );
    }
    
    private void HandleRetrievedJoystickData(
        ServiceDatabaseTaskRetrievedJoystickData retrievedJoystickData
    )
    {
        var result = retrievedJoystickData.Result;
        
        this.m_joystickChannelId = result.JoystickData_Channel_Id;

        Task.Run(
            function:
            async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 4000
                );
                this.SendChatMessage(
                    message: $"📣 SmoothDagger is now live!"
                );
            }
        );
    }
    
    private void RegisterForRetrievedJoystickData()
    {
        ServiceDatabaseTaskEvents.RetrievedJoystickData += this.HandleRetrievedJoystickData;
    }

    private void StartSendingNotificationMessages()
    {
        Task.Run(
            function: async () =>
            {
                var random    = new Random();
                var messages  = ServiceJoystickBot.s_notificationMessages.ToList();
                var lastIndex = -1;

                while (true)
                {
                    for (var i = messages.Count - 1; i > 0; i--)
                    {
                        var j = random.Next(
                            maxValue: i + 1
                        );
                        (messages[i], messages[j]) = (messages[j], messages[i]);
                    }
            
                    if (
                        lastIndex != -1 && 
                        ServiceJoystickBot.s_notificationMessages[lastIndex] == messages[0]
                    )
                    {
                        (messages[0], messages[^1]) = (messages[^1], messages[0]);
                    }
            
                    foreach (var message in messages)
                    {
                        await Task.Delay(
                            millisecondsDelay: ServiceJoystickBot.c_notificationDelayInMilliseconds
                        );

                        var topJoystickTipper = "";
                        var dynamicMessage    = ServiceJoystickBot.ReplaceTopTipperText(
                            message:           message,
                            topJoystickTipper: topJoystickTipper
                        );
                        this.SendChatMessageSilently(
                            message: message
                        );

                        lastIndex = Array.IndexOf(
                            array: ServiceJoystickBot.s_notificationMessages, 
                            value: message
                        );
                    }
                }
            }
        );
    }
}