
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Stretches;

internal sealed class ServiceStretch :
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
    
    internal static void DisplayStretchAndSwapLayoutToMe()
    {
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: ServiceStretch.c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  0,
            timerDelayInMilliseconds:  12000,
            startMessage:              $"🤸 Stretch for 10 seconds.",
            endMessage:                $"🤸 Stretch timer has ended.",
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    internal void Start()
    {
        this.m_cancellationTokenSource.Cancel();
        this.m_cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken          = this.m_cancellationTokenSource.Token;
        
        ServiceStretch.SendDelayedBotMessage(
            message: $"🤸 Stretch mode started."
        );
        
        Task.Run(
            function: async () =>
            {
                var delayInMilliseconds = Random.Shared.Next(
                    minValue: ServiceStretch.c_delayInitialMinimumForStartStrechInMilliseconds, 
                    maxValue: ServiceStretch.c_delayInitialMaximumForStartStretchInMilliseconds
                );
                
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds, 
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
        
                ServiceStretch.DisplayStretchAndSwapLayoutToMe();
                
                while (cancellationToken.IsCancellationRequested is false)
                {
                    delayInMilliseconds = Random.Shared.Next(
                        minValue: ServiceStretch.c_delayMinimumForNextStretchRoutineInMilliseconds, 
                        maxValue: ServiceStretch.c_delayMaximumForNextStretchRoutineInMilliseconds
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: delayInMilliseconds, 
                        cancellationToken: cancellationToken
                    );

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    ServiceStretch.DisplayStretchAndSwapLayoutToMe();
                }
            },
            cancellationToken: cancellationToken
        );
    }

    internal void Stop()
    {
        ServiceStretch.SendDelayedBotMessage(
            message: $"🤸 Stretch mode stopped."
        );
        this.m_cancellationTokenSource.Cancel();
    }
    
    private const int               c_delayInitialMaximumForStartStretchInMilliseconds = 2700000;
    private const int               c_delayInitialMinimumForStartStrechInMilliseconds  = 1800000;
    private const int               c_delayMaximumForNextStretchRoutineInMilliseconds  = 2700000;
    private const int               c_delayMinimumForNextStretchRoutineInMilliseconds  = 1800000;
    private const float             c_layoutMeDurationInSeconds                        = 14f;
    
	private CancellationTokenSource m_cancellationTokenSource                          = new();
    
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