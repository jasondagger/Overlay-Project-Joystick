
using System;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Joysticks.Requests;
using System.Threading.Tasks;

namespace Overlay.Core.Services.JoystickBots;

public sealed class ServiceJoystickBot() :
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
        var serviceJoystickRequest = ServiceJoystickRequest.CreateServiceJoystickRequestChatMessage(
            text:      message,
            channelId: this.m_joystickChannelId
        );
        
        serviceJoystick.SendRequest(
            serviceJoystickRequest: serviceJoystickRequest
        );
        
        Chat.AddChatMessageToInstances(
            username:          $"{ServiceJoystickBot.c_username}",
            usernameColor:     string.Empty,
            message:           message,
            chatMessageEmotes: null,
            isModerator:       true,
            isStreamer:        false,
            isSubscriber:      true
        );
    }

    private const string             c_username             = "SmoothBot";

    private static readonly string[] s_notificationMessages =
    [
        "Want to request a song? Subscribers get one free song request per stream using the !songrequest command! Check the description for more information.",
        "Not digging the lights? Subscribers can type !lights with a color to change the lights once per stream & more!",
        "Consider subscribing for access to a suite of commands that control the lights & sound of the stream!",
        "Want to play with us? Add SmoothDagger on Steam to join in on the fun!",
    ];

    private string                   m_joystickChannelId     = string.Empty;
    private int                      m_lastNotificationIndex = -1;
    
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
                    message: $"SmoothDagger is now live!"
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
            function: 
            async () =>
            {
                var random = new Random();
                while (true)
                {
                    await Task.Delay(
                        millisecondsDelay: 600000
                    );

                    var index = random.Next(
                        minValue: 0,
                        maxValue: ServiceJoystickBot.s_notificationMessages.Length
                    );
                    while (index == this.m_lastNotificationIndex)
                    {
                        index = random.Next(
                            minValue: 0,
                            maxValue: ServiceJoystickBot.s_notificationMessages.Length
                        );
                    }
                    
                    this.SendChatMessage(
                        message: ServiceJoystickBot.s_notificationMessages[index]
                    );
                    this.m_lastNotificationIndex = index;
                }
            }
        );
    }
}