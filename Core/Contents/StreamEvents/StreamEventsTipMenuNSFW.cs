
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuNSFW
{
    internal const string c_tipReward_FlashAssFor10Seconds                       = "Flash Ass for 10 Seconds";
    internal const string c_tipReward_FlashCockFor10Seconds                      = "Flash Cock for 10 Seconds";
    internal const string c_tipReward_PinchNipsFor10Seconds                      = "Pinch Nips for 10 Seconds";
    internal const string c_tipReward_SniffPitsFor10Seconds                      = "Sniff Pits for 10 Seconds";
    internal const string c_tipReward_GushControlLinkFor1Minute                  = "Gush Control Link for 1 Minute";
    internal const string c_tipReward_SpinForARandomGushControlLinkOfXMinutes    = "Spin for a Random Gush Control Link of X Minutes";
    internal const string c_tipReward_GushControlLinkFor3Minutes                 = "Gush Control Link for 3 Minutes";
    internal const string c_tipReward_FlashAssholeFor10Seconds                   = "Flash Asshole for 10 Seconds";
    internal const string c_tipReward_GushControlLinkFor5Minutes                 = "Gush Control Link for 5 Minutes";
    internal const string c_tipReward_GetNakedFor15Minutes                       = "Get Naked for 15 Minutes";
    internal const string c_tipReward_IncreaseGushControlLinkTimeLimitBy5Minutes = "Increase Gush Control Link Time Limit by 5 Minutes";
    internal const string c_tipReward_PutEdgeInFor15Minutes                      = "Put Edge In for 15 Minutes";
    internal const string c_tipReward_GetNakedUntilEndOfStream                   = "Get Naked Until End of Stream";
    internal const string c_tipReward_PutEdgeInUntilEndOfStream                  = "Put Edge In Until End of Stream";
    
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = messageMetadata.TipMenuItem;
        switch (tipMenuItem)
        {
            case StreamEventsTipMenuNSFW.c_tipReward_FlashAssFor10Seconds:
                StreamEventsTipMenuNSFW.HandleFlashAssFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_FlashCockFor10Seconds:
                StreamEventsTipMenuNSFW.HandleFlashCockFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_PinchNipsFor10Seconds:
                StreamEventsTipMenuNSFW.HandlePinchNipsFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_SniffPitsFor10Seconds:
                StreamEventsTipMenuNSFW.HandleSniffPitsFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_GushControlLinkFor1Minute:
                StreamEventsTipMenuNSFW.HandleGushControlLinkFor1Minute(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_SpinForARandomGushControlLinkOfXMinutes:
                StreamEventsTipMenuNSFW.HandleSpinForARandomGushControlLinkOfXMinutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_GushControlLinkFor3Minutes:
                StreamEventsTipMenuNSFW.HandleGushControlLinkFor3Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_FlashAssholeFor10Seconds:
                StreamEventsTipMenuNSFW.HandleFlashAssholeFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_GushControlLinkFor5Minutes:
                StreamEventsTipMenuNSFW.HandleGushControlLinkFor5Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_GetNakedFor15Minutes:
                StreamEventsTipMenuNSFW.HandleGetNakedFor15Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_IncreaseGushControlLinkTimeLimitBy5Minutes:
                StreamEventsTipMenuNSFW.HandleIncreaseGushControlLinkTimeLimitBy5Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_PutEdgeInFor15Minutes:
                StreamEventsTipMenuNSFW.HandlePutEdgeInFor15Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_GetNakedUntilEndOfStream:
                StreamEventsTipMenuNSFW.HandleGetNakedUntilEndOfStream(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuNSFW.c_tipReward_PutEdgeInUntilEndOfStream:
                StreamEventsTipMenuNSFW.HandlePutEdgeInUntilEndOfStream(
                    messageMetadata: messageMetadata
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: 
                        $"EXCEPTION: " +
                        $"{nameof(StreamEventsTipMenuNSFW)}." +
                        $"{nameof(StreamEventsTipMenuNSFW.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
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
                
                StreamEventsHelper.SendBotMessage(
                    message: $"🏦 Deposited {amount} minute{(amount > 1 ? "s" : string.Empty)} for {targetUser}. Type !bank withdraw {amount} if you would like to use it now!"
                );
            }
        );
    }

    private static void ExecuteTimeLimitIncrease(
        string targetUser
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.IncreaseTimeLimitForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void HandleFlashAssFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Flash ass for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger flash their ass for 10 seconds! Timer started!";
        
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  c_startDelayInMilliseconds,
            timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
            startMessage:              startMessage,
            endMessage:                c_endMessage,
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    private static void HandleFlashCockFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Flash cock for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger flash their cock for 10 seconds! Timer started!";
        
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  c_startDelayInMilliseconds,
            timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
            startMessage:              startMessage,
            endMessage:                c_endMessage,
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    private static void HandlePinchNipsFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Pinch nips for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger pinch their nips for 10 seconds! Timer started!";
        
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  c_startDelayInMilliseconds,
            timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
            startMessage:              startMessage,
            endMessage:                c_endMessage,
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    private static void HandleSniffPitsFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Sniff pits for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger sniff their pits for 10 seconds! Timer started!";
        
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  c_startDelayInMilliseconds,
            timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
            startMessage:              startMessage,
            endMessage:                c_endMessage,
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }

    private static void HandleGushControlLinkFor1Minute(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuNSFW.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     1
        );
    }
    
    private static void HandleSpinForARandomGushControlLinkOfXMinutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForGushControlLinkRollRequests(
            username: username
        );

        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 200
                );
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"🎲 Spin for a Random Gush Control Link is ready! Type !spin in chat, {username}!"
                );
            }
        );
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleGushControlLinkFor3Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuNSFW.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     3
        );
    }
    
    private static void HandleFlashAssholeFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Flash asshole for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger flash their asshole for 10 seconds! Timer started!";
        
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  c_startDelayInMilliseconds,
            timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
            startMessage:              startMessage,
            endMessage:                c_endMessage,
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    private static void HandleGushControlLinkFor5Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuNSFW.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     5
        );
    }
    
    private static void HandleGetNakedFor15Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ActivityDing
        );

        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 2200
                );
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"🔔 {messageMetadata.Who} requested SmoothDagger to get naked for 15 minutes! Timer started!"
                );
                
                await Task.Delay(
                    millisecondsDelay: 900000
                );
                
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"🔔 Get naked for 15 minutes timer has ended."
                );
            }
        );
    }
    
    private static void HandleIncreaseGushControlLinkTimeLimitBy5Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuNSFW.ExecuteTimeLimitIncrease(
            targetUser: messageMetadata.Who
        );
    }
    
    private static void HandlePutEdgeInFor15Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ActivityDing
        );

        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 2200
                );
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"🔔 {messageMetadata.Who} requested SmoothDagger to put their Edge in for 15 minutes! Timer started!"
                );
                
                await Task.Delay(
                    millisecondsDelay: 900000
                );
                
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"🔔 Put Edge In for 15 minutes timer has ended."
                );
            }
        );
    }
    
    private static void HandleGetNakedUntilEndOfStream(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ActivityDing
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🥵 {messageMetadata.Who} requested SmoothDagger to get naked until end of stream!"
        );
    }
    
    private static void HandlePutEdgeInUntilEndOfStream(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ActivityDing
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🥵 {messageMetadata.Who} requested SmoothDagger to put their Edge in until end of stream!"
        );
    }
}