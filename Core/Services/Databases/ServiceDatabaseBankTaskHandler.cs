
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.JoystickBots;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases;

internal static class ServiceDatabaseBankTaskHandler
{
    internal static void Start()
    {
        ServiceDatabaseTaskEvents.RetrievedBankUserTip += ServiceDatabaseBankTaskHandler.OnRetrievedBankUserTimeLimit;
    }
    
    private static void ExecuteDepositTime(
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
                
                ServiceDatabaseBankTaskHandler.SendDelayedBotMessage(
                    message: $"🏦 {targetUser}, you just earned {amount} Gush Control Link minute{(amount > 1 ? "s" : string.Empty)}. Type !bank withdraw {amount} if you would like to use it now!"
                );
            }
        );
    }

    private static void ExecuteUpdateTip(
        string targetUser, 
        int    amount,
        int    threshold
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
            ),
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold), 
                value:         amount
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateCurrentTipForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );

                var remainingTokens = threshold - amount;
                ServiceDatabaseBankTaskHandler.SendDelayedBotMessage(
                    message: $"🏦 {targetUser}, you'll earn a free Gush Control Link minute in {remainingTokens} more token{(remainingTokens > 1 ? "s" : string.Empty)}!"
                );
            }
        );
    }
    
    private static void OnRetrievedBankUserTimeLimit(
        ServiceDatabaseTaskRetrievedBankUserTip serviceDatabaseTaskRetrievedBankUserTip
    )
    {
        var user      = serviceDatabaseTaskRetrievedBankUserTip.Result;
        var username  = user.BankUser_Joystick_Username;
        var threshold = user.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute;
        var current   = user.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold;

        var minutesEarned = 0;
        while (current >= threshold)
        {
            minutesEarned++;
            current -= threshold;
        }

        if (minutesEarned <= 0)
        {
            var remainingTokens = threshold - current;
            ServiceDatabaseBankTaskHandler.SendDelayedBotMessage(
                message: $"🏦 {username}, you'll earn a free Gush Control Link minute in {remainingTokens} more token{(remainingTokens > 1 ? "s" : string.Empty)}!"
            );
            return;
        }
        
        ServiceDatabaseBankTaskHandler.ExecuteDepositTime(
            targetUser: username,
            amount:     minutesEarned
        );
        ServiceDatabaseBankTaskHandler.ExecuteUpdateTip(
            targetUser: username,
            amount:     current,
            threshold:  threshold
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