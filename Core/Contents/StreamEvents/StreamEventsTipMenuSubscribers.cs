
using Godot;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.OBS;
using Overlay.Core.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuSubscribers
{
    internal const string c_tipReward_SubscriberFlashAssFor10Seconds                       = "Subscriber - Flash Ass for 10 Seconds";
    internal const string c_tipReward_SubscriberFlashCockFor10Seconds                      = "Subscriber - Flash Cock for 10 Seconds";
    internal const string c_tipReward_SubscriberPinchNipsFor10Seconds                      = "Subscriber - Pinch Nips for 10 Seconds";
    internal const string c_tipReward_SubscriberSniffPitsFor10Seconds                      = "Subscriber - Sniff Pits for 10 Seconds";
    internal const string c_tipReward_SubscriberGushControlLinkFor1Minute                  = "Subscriber - Gush Control Link for 1 Minute";
    internal const string c_tipReward_SubscriberUnlockAnAvatarColor                        = "Subscriber - Unlock an Avatar Color";
    internal const string c_tipReward_SubscriberSpinForARandomGushControlLinkOfXMinutes    = "Subscriber - Spin for a Random Gush Control Link of X Minutes";
    internal const string c_tipReward_SubscriberGushControlLinkFor3Minutes                 = "Subscriber - Gush Control Link for 3 Minutes";
    internal const string c_tipReward_SubscriberFlashAssholeFor10Seconds                   = "Subscriber - Flash Asshole for 10 Seconds";
    internal const string c_tipReward_SubscriberGushControlLinkFor5Minutes                 = "Subscriber - Gush Control Link for 5 Minutes";
    internal const string c_tipReward_SubscriberUnlockAnAvatarEffect                       = "Subscriber - Unlock an Avatar Effect";
    internal const string c_tipReward_SubscriberGetNakedFor15Minutes                       = "Subscriber - Get Naked for 15 Minutes";
    internal const string c_tipReward_SubscriberUnlockAnAvatarModel                        = "Subscriber - Unlock an Avatar Model";
    internal const string c_tipReward_SubscriberIncreaseGushControlLinkTimeLimitBy5Minutes = "Subscriber - Increase Gush Control Link Time Limit by 5 Minutes";
    internal const string c_tipReward_SubscriberPayAttentionToMeFor5Minutes                = "Subscriber - Pay Attention to Me for 5 Minutes";
    internal const string c_tipReward_SubscriberPutEdgeInFor15Minutes                      = "Subscriber - Put Edge In for 15 Minutes";
    internal const string c_tipReward_SubscriberGetNakedUntilEndOfStream                   = "Subscriber - Get Naked Until End of Stream";
    internal const string c_tipReward_SubscriberPutEdgeInUntilEndOfStream                  = "Subscriber - Put Edge In Until End of Stream";
    internal const string c_tipReward_SubscriberEndStream                                  = "Subscriber - End Stream";
    
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = messageMetadata.TipMenuItem;
        switch (tipMenuItem)
        {
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberFlashAssFor10Seconds:
                StreamEventsTipMenuSubscribers.HandleSubscriberFlashAssFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberFlashCockFor10Seconds:
                StreamEventsTipMenuSubscribers.HandleSubscriberFlashCockFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPinchNipsFor10Seconds:
                StreamEventsTipMenuSubscribers.HandleSubscriberPinchNipsFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberSniffPitsFor10Seconds:
                StreamEventsTipMenuSubscribers.HandleSubscriberSniffPitsFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGushControlLinkFor1Minute:
                StreamEventsTipMenuSubscribers.HandleSubscriberGushControlLinkFor1Minute(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberUnlockAnAvatarColor:
                StreamEventsTipMenuSubscribers.HandleSubscriberUnlockAnAvatarColor(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberSpinForARandomGushControlLinkOfXMinutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberSpinForARandomGushControlLinkOfXMinutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGushControlLinkFor3Minutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberGushControlLinkFor3Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberFlashAssholeFor10Seconds:
                StreamEventsTipMenuSubscribers.HandleSubscriberFlashAssholeFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGushControlLinkFor5Minutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberGushControlLinkFor5Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberUnlockAnAvatarEffect:
                StreamEventsTipMenuSubscribers.HandleSubscriberUnlockAnAvatarEffect(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGetNakedFor15Minutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberGetNakedFor15Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberUnlockAnAvatarModel:
                StreamEventsTipMenuSubscribers.HandleSubscriberUnlockAnAvatarModel(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberIncreaseGushControlLinkTimeLimitBy5Minutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberIncreaseGushControlLinkTimeLimitBy5Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPayAttentionToMeFor5Minutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberPayAttentionToMeFor5Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPutEdgeInFor15Minutes:
                StreamEventsTipMenuSubscribers.HandleSubscriberPutEdgeInFor15Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGetNakedUntilEndOfStream:
                StreamEventsTipMenuSubscribers.HandleSubscriberGetNakedUntilEndOfStream(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPutEdgeInUntilEndOfStream:
                StreamEventsTipMenuSubscribers.HandleSubscriberPutEdgeInUntilEndOfStream(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberEndStream:
                StreamEventsTipMenuSubscribers.HandleSubscriberEndStream(
                    messageMetadata: messageMetadata
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: 
                        $"EXCEPTION: " +
                        $"{nameof(StreamEventsTipMenuSubscribers)}." +
                        $"{nameof(StreamEventsTipMenuSubscribers.HandleTipMenuItem)}() - " +
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
    
    private static void HandleSubscriberFlashAssFor10Seconds(
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
    
    private static void HandleSubscriberFlashCockFor10Seconds(
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
    
    private static void HandleSubscriberPinchNipsFor10Seconds(
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
    
    private static void HandleSubscriberSniffPitsFor10Seconds(
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

    private static void HandleSubscriberGushControlLinkFor1Minute(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuSubscribers.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     1
        );
    }
    
    private static void HandleSubscriberUnlockAnAvatarColor(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForUnlockColorRequests(
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
                    message:  $"👾 @{username} - You can now unlock an avatar color! Type !unlock color [color]. Check the bio if you need assistance!"
                );
            }
        );
    }
    
    private static void HandleSubscriberSpinForARandomGushControlLinkOfXMinutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForGushControlLinkRollRequests(
            username: username
        );
        
        StreamEventsHelper.SendBotMessage(
            message: $"🎲 Spin for a Random Gush Control Link is ready! Type !spin in chat, {username}!"
        );
    }
    
    private static void HandleSubscriberGushControlLinkFor3Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuSubscribers.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     3
        );
    }
    
    private static void HandleSubscriberFlashAssholeFor10Seconds(
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
    
    private static void HandleSubscriberGushControlLinkFor5Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuSubscribers.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     5
        );
    }
    
    private static void HandleSubscriberUnlockAnAvatarEffect(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForUnlockEffectRequests(
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
                    message:  $"👾 @{username} - You can now unlock an avatar effect! Type !unlock effect [effect]. Check the bio if you need assistance!"
                );
            }
        );
    }
    
    private static void HandleSubscriberGetNakedFor15Minutes(
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
    
    private static void HandleSubscriberUnlockAnAvatarModel(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForUnlockModelRequests(
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
                    message:  $"👾 @{username} - You can now unlock an avatar model! Type !unlock model [model]. Check the bio if you need assistance!"
                );
            }
        );
    }
    
    private static void HandleSubscriberIncreaseGushControlLinkTimeLimitBy5Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuSubscribers.ExecuteTimeLimitIncrease(
            targetUser: messageMetadata.Who
        );
    }

    private static void HandleSubscriberPayAttentionToMeFor5Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float                         c_layoutMeDurationInSeconds = 304f;
        const int                           c_startDelayInMilliseconds  = 2200;
        const int                           c_timerDelayInMilliseconds  = 300000;
        const string                        c_endMessage                = $"🔔 Pay attention for 5 minutes timer has ended.";
        const SceneController.AttentionMode c_attentionMode             = SceneController.AttentionMode.Me;
        const bool                          c_useAdditiveTime           = false;
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger pay attention to them for 5 minutes! Timer started!";
        
        StreamEventsHelper.SetMeTimerWithNotifications(
            layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
            startDelayInMilliseconds:  c_startDelayInMilliseconds,
            timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
            startMessage:              startMessage,
            endMessage:                c_endMessage,
            attentionMode:             c_attentionMode,
            useAdditiveTime:           c_useAdditiveTime,
            soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
            soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
        );
    }
    
    private static void HandleSubscriberPutEdgeInFor15Minutes(
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
    
    private static void HandleSubscriberGetNakedUntilEndOfStream(
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
    
    private static void HandleSubscriberPutEdgeInUntilEndOfStream(
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
    
    private static void HandleSubscriberEndStream(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.StopStream();
        
        var sceneTree = (SceneTree) Engine.GetMainLoop();
        sceneTree.Quit();
    }
}