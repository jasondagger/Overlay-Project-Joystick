
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Hydrations;

internal sealed class ServiceHydrate :
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
        
        ServiceHydrate.SendDelayedBotMessage(
            message: $"🥤 Hydration mode started."
        );
        
        Task.Run(
            function: async () =>
            {
                var delayInMilliseconds = Random.Shared.Next(
                    minValue: ServiceHydrate.c_delayInitialMinimumForStartHydrateInMilliseconds, 
                    maxValue: ServiceHydrate.c_delayInitialMaximumForStartHydrateInMilliseconds
                );
                
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds, 
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
        
                ServiceHydrate.DisplayHydrationReminder();
                
                while (!cancellationToken.IsCancellationRequested)
                {
                    delayInMilliseconds = Random.Shared.Next(
                        minValue: ServiceHydrate.c_delayMinimumForNextHydrateRoutineInMilliseconds, 
                        maxValue: ServiceHydrate.c_delayMaximumForNextHydrateRoutineInMilliseconds
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: delayInMilliseconds, 
                        cancellationToken: cancellationToken
                    );

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    ServiceHydrate.DisplayHydrationReminder();
                }
            },
            cancellationToken: cancellationToken
        );
    }

    internal void Stop()
    {
        ServiceHydrate.SendDelayedBotMessage(
            message: $"🥤 Hydration mode stopped."
        );
        this.m_cancellationTokenSource.Cancel();
    }
    
    private const int               c_delayInitialMaximumForStartHydrateInMilliseconds = 2700000;
    private const int               c_delayInitialMinimumForStartHydrateInMilliseconds = 1800000;
    private const int               c_delayMaximumForNextHydrateRoutineInMilliseconds  = 2700000;
    private const int               c_delayMinimumForNextHydrateRoutineInMilliseconds  = 1800000;
    
	private CancellationTokenSource m_cancellationTokenSource                          = new();

    private static void DisplayHydrationReminder()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ActivityDing
        );
        
        var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessageSilently(
            message: $"🥤 Hydration reminder. Stay hydrated!"
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