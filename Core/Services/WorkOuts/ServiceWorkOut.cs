
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Services.WorkOuts;

internal sealed class ServiceWorkOut :
    IService
{
    Task IService.Setup()
    {
        return Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        ServiceWorkOut.StartWalkReminder();
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
        var cancellationToken = this.m_cancellationTokenSource.Token;
        
        ServiceWorkOut.SendDelayedBotMessage(
            message: $"💪 Work out mode started."
        );
        
        Task.Run(
            function: async () =>
            {
                var delayInMilliseconds = Random.Shared.Next(
                    minValue: ServiceWorkOut.c_initialDelayMinimumForStartWorkOutInMilliseconds, 
                    maxValue: ServiceWorkOut.c_initialDelayMaximumForStartWorkOutInMilliseconds
                );
                
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds, 
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
        
                ServiceWorkOut.DisplayWorkoutTaskAndSwapLayoutToMe();
                
                while (!cancellationToken.IsCancellationRequested)
                {
                    delayInMilliseconds = Random.Shared.Next(
                        minValue: ServiceWorkOut.c_delayMinimumForNextWorkOutRoutineInMilliseconds, 
                        maxValue: ServiceWorkOut.c_delayMaximumForNextWorkOutRoutineInMilliseconds
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: delayInMilliseconds, 
                        cancellationToken: cancellationToken
                    );

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    ServiceWorkOut.DisplayWorkoutTaskAndSwapLayoutToMe();
                }
            },
            cancellationToken: cancellationToken
        );
    }

    internal void Stop()
    {
        ServiceWorkOut.SendDelayedBotMessage(
            message: $"💪 Work out mode stopped."
        );
        
        this.m_cancellationTokenSource.Cancel();
    }
    
    private const int               c_initialDelayMaximumForStartWorkOutInMilliseconds = 900000;
    private const int               c_initialDelayMinimumForStartWorkOutInMilliseconds = 300000;
    private const int               c_delayMaximumForNextWorkOutRoutineInMilliseconds  = 2700000;
    private const int               c_delayMinimumForNextWorkOutRoutineInMilliseconds  = 1800000;
    private const float             c_layoutMeDurationInSeconds                        = 42f;

    private const int               c_walkReminderTimeHours                            = 18;
    private const int               c_walkReminderTimeMinutes                          = 50;
    private const int               c_walkReminderTimeInSeconds                        = 0;
    private const int               c_walkReminderTimeCheckDelayInMilliseconds         = 1000;
    
	private CancellationTokenSource m_cancellationTokenSource                          = new();

    private static void DisplayWorkoutTaskAndSwapLayoutToMe()
    {
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: ServiceWorkOut.c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  0,
            timerDelayInMilliseconds:  0,
            startMessage:              $"💪 Work out task: {ServiceWorkOutTasks.GetRandomWorkOutTask()}",
            endMessage:                string.Empty,
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

    private static void StartWalkReminder()
    {
        Task.Run(
            function: async () =>
            {
                var targetTime = new TimeSpan(
                    hours:   ServiceWorkOut.c_walkReminderTimeHours,
                    minutes: ServiceWorkOut.c_walkReminderTimeMinutes,
                    seconds: ServiceWorkOut.c_walkReminderTimeInSeconds
                );

                if (DateTime.Now.TimeOfDay >= targetTime)
                {
                    return;
                }
                
                while (DateTime.Now.TimeOfDay < targetTime)
                {
                    await Task.Delay(
                        millisecondsDelay: ServiceWorkOut.c_walkReminderTimeCheckDelayInMilliseconds  
                    );
                }

                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"👟 Reminder: Walk at 7pm PST."
                );
            }
        );
    }
}