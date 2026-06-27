
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Breaks;

internal sealed class ServiceBreak :
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
        
        ServiceBreak.SendDelayedBotMessage(
            message: $"🏝️ Break mode started."
        );
        
        Task.Run(
            function: async () =>
            {
                var delayInMilliseconds = Random.Shared.Next(
                    minValue: ServiceBreak.c_delayInitialMinimumForStartBreakInMilliseconds, 
                    maxValue: ServiceBreak.c_delayInitialMaximumForStartBreakInMilliseconds
                );
                
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds, 
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
        
                ServiceBreak.DisplayBreakAndSwapLayoutToMe();
                
                while (cancellationToken.IsCancellationRequested is false)
                {
                    delayInMilliseconds = Random.Shared.Next(
                        minValue: ServiceBreak.c_delayMinimumForNextBreakRoutineInMilliseconds, 
                        maxValue: ServiceBreak.c_delayMaximumForNextBreakRoutineInMilliseconds
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: delayInMilliseconds, 
                        cancellationToken: cancellationToken
                    );

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    ServiceBreak.DisplayBreakAndSwapLayoutToMe();
                }
            },
            cancellationToken: cancellationToken
        );
    }

    internal void Stop()
    {
        ServiceBreak.SendDelayedBotMessage(
            message: $"🏝️ Break mode stopped."
        );
        this.m_cancellationTokenSource.Cancel();
    }
    
    private const int               c_delayInitialMaximumForStartBreakInMilliseconds = 2700000;
    private const int               c_delayInitialMinimumForStartBreakInMilliseconds = 1800000;
    private const int               c_delayMaximumForNextBreakRoutineInMilliseconds  = 2700000;
    private const int               c_delayMinimumForNextBreakRoutineInMilliseconds  = 1800000;
    private const float             c_layoutMeDurationInSeconds                      = 14f;
    
	private CancellationTokenSource m_cancellationTokenSource                        = new();

    private static void DisplayBreakAndSwapLayoutToMe()
    {
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: ServiceBreak.c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  0,
            timerDelayInMilliseconds:  12000,
            startMessage:              $"🏝️ Break for 10 seconds.",
            endMessage:                $"🏝️ Break timer has ended.",
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