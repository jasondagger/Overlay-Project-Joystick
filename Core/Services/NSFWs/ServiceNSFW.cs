
using Overlay.Core.Contents.StreamEvents;
using System;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;

namespace Overlay.Core.Services.NSFWs;

internal sealed class ServiceNSFW :
    IService
{
    Task IService.Setup()
    {
        return Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        return Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        return Task.CompletedTask;
    }
    
    internal void Start()
    {
        this.m_cancellationTokenSource.Cancel();
        this.m_cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken          = this.m_cancellationTokenSource.Token;
        
        ServiceNSFW.SendDelayedBotMessage(
            message: $"🥵 NSFW mode started."
        );
        
        Task.Run(
            function: async () =>
            {
                var delayInMilliseconds = Random.Shared.Next(
                    minValue: ServiceNSFW.c_delayInitialMinimumForStartNSFWInMilliseconds, 
                    maxValue: ServiceNSFW.c_delayInitialMaximumForStartNSFWInMilliseconds
                );
                
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds, 
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
        
                ServiceNSFW.DisplayNSFWSpiceAndSwapLayoutToMe();
                
                while (!cancellationToken.IsCancellationRequested)
                {
                    delayInMilliseconds = Random.Shared.Next(
                        minValue: ServiceNSFW.c_delayMinimumForNextNSFWRoutineInMilliseconds, 
                        maxValue: ServiceNSFW.c_delayMaximumForNextNSFWRoutineInMilliseconds
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: delayInMilliseconds, 
                        cancellationToken: cancellationToken
                    );

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    ServiceNSFW.DisplayNSFWSpiceAndSwapLayoutToMe();
                }
            },
            cancellationToken: cancellationToken
        );
    }

    internal void Stop()
    {
        ServiceNSFW.SendDelayedBotMessage(
            message: $"🥵 NSFW mode stopped."
        );
        this.m_cancellationTokenSource.Cancel();
    }
    
    private const int               c_delayInitialMaximumForStartNSFWInMilliseconds = 1800000;
    private const int               c_delayInitialMinimumForStartNSFWInMilliseconds = 1200000;
    private const int               c_delayMaximumForNextNSFWRoutineInMilliseconds  = 2700000;
    private const int               c_delayMinimumForNextNSFWRoutineInMilliseconds  = 1800000;
    private const float             c_layoutMeDurationInSeconds                     = 14f;
    
	private CancellationTokenSource m_cancellationTokenSource                       = new();

    private static void DisplayNSFWSpiceAndSwapLayoutToMe()
    {
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: ServiceNSFW.c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  0,
            timerDelayInMilliseconds:  12000,
            startMessage:              $"🥵 NSFW Spice: {ServiceNSFWSpices.GetRandomNSFWSpice()}",
            endMessage:                $"🥵 NSFW Spice: timer has ended.",
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    private static void SendDelayedBotMessage(
        string message
    )
    {
        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 200
                );
                
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: message
                );
            }
        );
    }
}