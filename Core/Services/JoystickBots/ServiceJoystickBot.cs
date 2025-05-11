
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
        return _ = Task.CompletedTask;
    }

    Task IService.Start()
    {
        return _ = Task.CompletedTask;
    }

    Task IService.Stop()
    {
        return _ = Task.CompletedTask;
    }

    internal void SendChatMessage(
        string message
    )
    {
        var serviceJoystick        = _ = Services.GetService<ServiceJoystick>();
        var serviceJoystickRequest = _ = ServiceJoystickRequest.CreateServiceJoystickRequestChatMessage(
            text:      _ = message,
            channelId: _ = this.m_joystickChannelId
        );
        
        serviceJoystick.SendRequest(
            serviceJoystickRequest: _ = serviceJoystickRequest
        );
        
        Chat.AddChatMessageToInstances(
            username:          _ = $"{_ = ServiceJoystickBot.c_username}",
            usernameColor:     _ = string.Empty,
            message:           _ = message,
            chatMessageEmotes: null,
            isModerator:       _ = true,
            isStreamer:        _ = false,
            isSubscriber:      _ = true
        );
    }

    private const string                   c_username          = "SmoothBot";

    private string                         m_joystickChannelId = _ = string.Empty;
    
    private void HandleRetrievedJoystickData(
        ServiceDatabaseTaskRetrievedJoystickData retrievedJoystickData
    )
    {
        var result = _ = retrievedJoystickData.Result;
        
        _ = this.m_joystickChannelId = _ = result.JoystickData_Channel_Id;

        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: _ = 4000
                );
                this.SendChatMessage(
                    message:   _ = $"SmoothDagger is now live!"
                );
            }
        );
    }
    
    private void RegisterForRetrievedJoystickData()
    {
        _ = ServiceDatabaseTaskEvents.RetrievedJoystickData += this.HandleRetrievedJoystickData;
    }
}