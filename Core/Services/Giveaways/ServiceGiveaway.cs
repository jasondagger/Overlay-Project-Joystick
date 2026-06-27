
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Contents.UserEvents;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.TeamFortress2s;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Services.Godots.Audios;


namespace Overlay.Core.Services.Giveaways;

internal sealed class ServiceGiveaway :
    IService
{
    Task IService.Setup()
    {
        return Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        this.RetrieveResources();
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
        
        ServiceGiveaway.SendDelayedBotMessage(
            message: $"🎁 Giveaway mode started."
        );
        
        Task.Run(
            function: async () =>
            {
                var delayInMilliseconds = Random.Shared.Next(
                    minValue: ServiceGiveaway.c_delayInitialMinimumForStartGiveawayInMilliseconds, 
                    maxValue: ServiceGiveaway.c_delayInitialMaximumForStartGiveawayInMilliseconds
                );
                
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds, 
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
        
                this.DisplayGiveaway();
                
                while (cancellationToken.IsCancellationRequested is false)
                {
                    delayInMilliseconds = Random.Shared.Next(
                        minValue: ServiceGiveaway.c_delayMinimumForNextGiveawayRoutineInMilliseconds, 
                        maxValue: ServiceGiveaway.c_delayMaximumForNextGiveawayRoutineInMilliseconds
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: delayInMilliseconds, 
                        cancellationToken: cancellationToken
                    );

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    this.DisplayGiveaway();
                }
            },
            cancellationToken: cancellationToken
        );
    }

    internal void Stop()
    {
        ServiceGiveaway.SendDelayedBotMessage(
            message: $"🎁 Giveaway mode stopped."
        );
        this.m_cancellationTokenSource.Cancel();
    }
    
    private const int                    c_oneHourInMilliseconds                             = 3600000;
    private const int                    c_threeHoursInMilliseconds                          = 10800000;

    private const int                    c_delayInitialMaximumForStartGiveawayInMilliseconds = ServiceGiveaway.c_threeHoursInMilliseconds;
    private const int                    c_delayInitialMinimumForStartGiveawayInMilliseconds = ServiceGiveaway.c_oneHourInMilliseconds;
    private const int                    c_delayMaximumForNextGiveawayRoutineInMilliseconds  = ServiceGiveaway.c_threeHoursInMilliseconds;
    private const int                    c_delayMinimumForNextGiveawayRoutineInMilliseconds  = ServiceGiveaway.c_oneHourInMilliseconds;

	private CancellationTokenSource      m_cancellationTokenSource                           = new();
    private UserPresenceEventsController m_userPresenceEventsController                      = null;

    private void DisplayGiveaway()
    {
        if (this.m_userPresenceEventsController is null)
        {
            this.RetrieveResources();
            if (this.m_userPresenceEventsController is null)
            {
                return;
            }
        }
        
        var users = this.m_userPresenceEventsController.GetUsersInChat() ?? [];
        if (users.Count is 0)
        {
            return;
        }
        
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.MilestoneCompleted
        );
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceTeamFortress2BhopSoundAlertRandomizer.GetRandomFireworkSound()
        );
        
        string[] userArray = [.. users];
        var winner         = userArray[
            Random.Shared.Next(
                maxValue: userArray.Length
            )
        ];
        var roll           = Random.Shared.Next(
            minValue: 1,
            maxValue: 1001
        );
        var minutes = roll switch
        {
            1000  => 10,
            > 949 => 5,
            > 849 => 3,
            _     => 1
        };
        
        ServiceGiveaway.ExecuteDeposit(
            targetUser: winner,
            amount:     minutes
        );
        
        ServiceGiveaway.SendDelayedBotMessage(
            message: $"🎁 GIVEAWAY: @{winner} just won {minutes} Gush Control Link minute{(minutes is 1 ? string.Empty : "s")}! Type !bank check to see how many minutes you have! Thanks for watching!"
        );
    }
    
    private static void ExecuteDeposit(
        string targetUser,
        int    amount
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
            ),
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes), 
                value:         amount
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.DepositTimeForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private void RetrieveResources()
    {
        this.m_userPresenceEventsController = UserPresenceEventsController.Instance;
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