
using System.Threading.Tasks;
using Godot;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.Joysticks.Requests;

namespace Overlay.Core.Services.JoystickBots;

public sealed class ServiceJoystickBot() :
    IService
{
    Task IService.Setup()
    {
        this.RegisterForRetrievedJoystickData();
        this.RegisterForJoystickStreamEvents();
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
        
        Chat.Instance.AddChatMessage(
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

    private readonly RandomNumberGenerator m_random            = new();
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
                    millisecondsDelay: _ = 3000
                );
                this.SendChatMessage(
                    message:   _ = $"SmoothDagger is now LIVE!"
                );
            }
        );
    }

    private void HandleStreamEventStreamDroppedIn(
        ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn metadataDropinStream
    )
    {
        string[] messages =
        [
            $"Welcome in, {_ = metadataDropinStream.Who} & friends! Feel free to lurk or chat :)",
            $"{_ = metadataDropinStream.Who} DROP IN DETECTED! TRIGGERING ALARMS!",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed metadataFollowed
    )
    {
        string[] messages =
        [
            $"Thank you for following, {_ = metadataFollowed.Who}!",
            $"A new follower has appeared! Welcome, {_ = metadataFollowed.Who}!",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed metadataSubscribed
    )
    {
        string[] messages =
        [
            $"Thank you for subscribing, {_ = metadataSubscribed.Who}!",
            $"The MYTH, the LEGEND! {_ = metadataSubscribed.Who} just subscribed!",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped metadataTipped
    )
    {
        string[] messages =
        [
            $"Cha-CHING! Thank you!",
            $"Thank you for keeping my circuits running, {_ = metadataTipped.Who}!",
        ];
        var index = _ = m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }

    private void RegisterForJoystickStreamEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.StreamDroppedIn += this.HandleStreamEventStreamDroppedIn;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed        += this.HandleStreamEventFollowed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Subscribed      += this.HandleStreamEventSubscribed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped          += this.HandleStreamEventTipped;
    }
    
    private void RegisterForRetrievedJoystickData()
    {
        _ = ServiceDatabaseTaskEvents.RetrievedJoystickData += this.HandleRetrievedJoystickData;
    }
}