
using Godot;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.Lovenses;
using Overlay.Core.Services.TeamFortress2s;
using Overlay.Core.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.StreamEvents;

internal sealed partial class StreamEventsController() :
    Node()
{
    public override void _Ready()
    {
        this.SubscribeToStreamEvents();
    }

    private const double                   c_dropInVibrationTime             = 30d;
    private const double                   c_followVibrationTime             = 5d;
    private const double                   c_milestoneCompletedVibrationTime = 20d;
    private const double                   c_subscribeVibrationTime          = 12d;
    
    private readonly RandomNumberGenerator m_random                          = new();

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
                
                await Task.Delay(
                    millisecondsDelay: 200
                );
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: $"🏦 Deposited {amount} Gush Control Link minute{(amount > 1 ? "s" : string.Empty)} for @{targetUser}. Type !bank withdraw {amount} if you would like to use {(amount > 1 ? "them" : "it")} now!"
                );
            }
        );
    }
    
    private static void ExecuteDepositDropin(
        string targetUser
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
            ),
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes), 
                value:         1
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
    
    private static void ExecuteDepositTip(
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
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold), 
                value:         amount
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.DepositTipForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void ExecuteUpdateTipLimitForSubscriber(
        string targetUser
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
            ),
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute), 
                value:         500
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateTipLimitForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static double GetTipVibrationTime(
        int tipAmount
    )
    {
        return tipAmount switch
        {
            <= 4   => 1d,
            <  10  => 2d,
            <  20  => 4d,
            <  35  => 10d,
            <  75  => 20d,
            <  150 => 35d,
            <  350 => 60d,
            <  875 => 135d,
            _      => 300d
        };
    }
    
    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Followed
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_followVibrationTime,
            eventType:     ServiceLovense.EventType.Followed
        );

        var username      = messageMetadata.Who;
        string[] messages =
        [
            $"❤️ A new follower has appeared! Welcome, @{username}! You just earned 1 Gush Control Link minute! Type !bank check to see how many minutes you have & !claim [ass/cock/nipples] for some extra spice! 🥵 Thank you so much!",
        ];
        var index         = this.m_random.RandiRange(
            from: 0,
            to:   messages.Length - 1
        );

        StreamEventsController.SendDelayedBotMessage(
            message: messages[index]
        );
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForClaimRewardRequests(
            username: username
        );
        StreamEventsController.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     1
        );
    }
    
    private void HandleWebSocketPayloadStreamEventGiftedSubscriptions(
        ServiceJoystickWebSocketPayloadMessageMetadataGiftedSubscriptions messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.GiftedSubscriptions
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_subscribeVibrationTime * messageMetadata.HowMuch,
            eventType:     ServiceLovense.EventType.Subscribed
        );
        
        string[] messages =
        [
            $"🌟 @{messageMetadata.Who} gifted {messageMetadata.HowMuch} sub{(messageMetadata.HowMuch > 1 ? "s" : string.Empty)}! Thank you so much!",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        StreamEventsController.SendDelayedBotMessage(
            message: messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventMilestoneCompleted(
        ServiceJoystickWebSocketPayloadMessageMetadataMilestoneCompleted messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.MilestoneCompleted
        );
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceTeamFortress2BhopSoundAlertRandomizer.GetRandomFireworkSound()
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_milestoneCompletedVibrationTime,
            eventType:     ServiceLovense.EventType.MilestoneCompleted
        );

        string[] messages =
        [
            $"🌟 @{messageMetadata.Who} completed the {messageMetadata.Title} milestone!",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        StreamEventsController.SendDelayedBotMessage(
            message: messages[index]
        );
        
        switch (messageMetadata.Title)
        {
            case "1 Minute Gush Control Link":
                StreamEventsController.ExecuteDeposit(
                    targetUser: messageMetadata.Who,
                    amount:     1
                );
                break;
            
            case "3 Minute Gush Control Link":
                StreamEventsController.ExecuteDeposit(
                    targetUser: messageMetadata.Who,
                    amount:     3
                );
                break;
            
            case "5 Minute Gush Control Link":
            case "5 Minute Gush Control Link + Cum Show":
            case "5 Minute Gush Control Link + Cum Show + Naked All Stream":
            case "5 Minute Gush Control Link + Cum Show + Naked All Stream + Edge All Stream":
                StreamEventsController.ExecuteDeposit(
                    targetUser: messageMetadata.Who,
                    amount:     5
                );
                break;
            
            case "5 Minute Gush Control Link + Cum Show + Naked All Stream + Edge All Stream + 15 Minutes of Me":
                StreamEventsController.ExecuteDeposit(
                    targetUser: messageMetadata.Who,
                    amount:     5
                );
                
                const float                         c_layoutMeDurationInSeconds = 1052f;
                const int                           c_startDelayInMilliseconds  = 2200;
                const int                           c_timerDelayInMilliseconds  = 1052000;
                const string                        c_endMessage                = $"";
                const string                        c_startMessage              = $"";
                const SceneController.AttentionMode c_attentionMode             = SceneController.AttentionMode.Me;
                const bool                          c_useAdditiveTime           = false;
        
                StreamEventsHelper.SetMeTimerWithNotifications(
                    layoutMeDurationInSeconds: c_layoutMeDurationInSeconds,
                    startDelayInMilliseconds:  c_startDelayInMilliseconds,
                    timerDelayInMilliseconds:  c_timerDelayInMilliseconds,
                    startMessage:              c_startMessage,
                    endMessage:                c_endMessage,
                    attentionMode:             c_attentionMode,
                    useAdditiveTime:           c_useAdditiveTime
                );
                break;
            
            default:
                StreamEventsController.SendDelayedBotMessage(
                    message: $"@SmoothDagger, you're missing the {messageMetadata.Title} milestone title in {nameof(StreamEventsController)}.{nameof(StreamEventsController.HandleWebSocketPayloadStreamEventMilestoneCompleted)}()."
                );
                break;
        }
    }
    
    private void HandleWebSocketPayloadStreamEventStreamDroppedIn(
        ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.StreamDroppedIn
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_dropInVibrationTime,
            eventType:     ServiceLovense.EventType.DroppedIn
        );
        
        var username      = messageMetadata.Who;
        string[] messages =
        [
            $"👋 Welcome in, @{username} & friends! You earned a free Gush Control Link minute & !claim command reward! Type !bank check to see how many minutes you have & !claim [ass/cock/nipples] for some spice!",
            $"👋 Hello there, @{username} & friends! You earned a free Gush Control Link minute & !claim command reward! Type !bank check to see how many minutes you have & !claim [ass/cock/nipples] for some spice!",
        ];
        var index = this.m_random.RandiRange(
            from: 0,
            to:   messages.Length - 1
        );

        ServiceJoystickWebSocketPayloadChatHandler.AddUserForClaimRewardRequests(
            username: username
        );
        StreamEventsController.ExecuteDepositDropin(
            targetUser: username
        );
        StreamEventsController.SendDelayedBotMessage(
            message: messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Subscribed
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_subscribeVibrationTime,
            eventType:     ServiceLovense.EventType.Subscribed
        );
        
        string[] messages =
        [
            $"🌟 The MYTH, the LEGEND! @{messageMetadata.Who} just subscribed & earned 5 Gush Control Link minutes! Check the bio & tip menu below for new commands & rewards!",
            $"🌟 @{messageMetadata.Who} just subscribed & earned 5 Gush Control Link minutes! Thank you so much for keeping my circuits running! Check the bio & tip menu below for new commands & rewards!",
        ];
        var index = this.m_random.RandiRange(
            from: 0,
            to:   messages.Length - 1
        );

        StreamEventsController.SendDelayedBotMessage(
            message: messages[index]
        );
        
        StreamEventsController.ExecuteDeposit(
            targetUser: messageMetadata.Who,
            amount:     5
        );
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = messageMetadata.TipMenuItem;
        if (tipMenuItem is not null)
        {
            switch (tipMenuItem)
            {
                case "":
                    StreamEventsController.PlayTipSoundEffect();
                    break;
                    
                case StreamEventsTipMenuIRL.c_tipReward_First:
                case StreamEventsTipMenuIRL.c_tipReward_Ohai:
                case StreamEventsTipMenuIRL.c_tipReward_ThankYou:
                case StreamEventsTipMenuIRL.c_tipReward_YouLookGreat:
                case StreamEventsTipMenuIRL.c_tipReward_DoOneMore:
                case StreamEventsTipMenuIRL.c_tipReward_Hydrate:
                case StreamEventsTipMenuIRL.c_tipReward_JustLurkin:
                case StreamEventsTipMenuIRL.c_tipReward_BellyRubFor10Seconds:
                case StreamEventsTipMenuIRL.c_tipReward_FlexFor10Seconds:
                case StreamEventsTipMenuIRL.c_tipReward_HeadpatsFor10Seconds:
                case StreamEventsTipMenuIRL.c_tipReward_StreeeetchFor10Seconds:
                case StreamEventsTipMenuIRL.c_tipReward_Do10JumpingJacks:
                case StreamEventsTipMenuIRL.c_tipReward_Do10Situps:
                case StreamEventsTipMenuIRL.c_tipReward_Do10Squats:
                case StreamEventsTipMenuIRL.c_tipReward_Do10Burpees:
                case StreamEventsTipMenuIRL.c_tipReward_Do10Pushups:
                case StreamEventsTipMenuIRL.c_tipReward_Do10BicepCurls:
                case StreamEventsTipMenuIRL.c_tipReward_Ddo10LateralRaises:
                case StreamEventsTipMenuIRL.c_tipReward_Do10OverheadPresses:
                case StreamEventsTipMenuIRL.c_tipReward_ShowinSomeLove:
                case StreamEventsTipMenuIRL.c_tipReward_TokeUp:
                case StreamEventsTipMenuIRL.c_tipReward_BuyMeAPoweradeSlushie:
                case StreamEventsTipMenuIRL.c_tipReward_BuyMeABananaShake:
                case StreamEventsTipMenuIRL.c_tipReward_BuyMeDinner:
                case StreamEventsTipMenuIRL.c_tipReward_PayMyBills:
                    StreamEventsTipMenuIRL.HandleTipMenuItem(
                        messageMetadata: messageMetadata
                    );
                    break;
                
                case StreamEventsTipMenuGaming.c_tipReward_TF2TriggerAnAction:
                case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToBunnyHopping:
                case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToCasualQueue:
                case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToCompetitiveQueue:
                case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToMannVsMachine:
                case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToSurfing:
                case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeADuelingMiniGame:
                case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeATourOfDutyTicket:
                case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeACaseKey:
                case StreamEventsTipMenuGaming.c_tipReward_TF2CloseTheGame:
                case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeAGoldenPan:
                    StreamEventsTipMenuGaming.HandleTipMenuItem(
                        messageMetadata: messageMetadata
                    );
                    break;
                
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_PlaySoundEffect:
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_SpotifySongRequest:
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_SpotifySongSkip:
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_SetOverlayThemeAndBackgroundLightsFor15Minutes:
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_UnlockAnAvatarColor:
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_UnlockAnAvatarEffect:
                case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_UnlockAnAvatarModel:
                    StreamEventsTipMenuAvatarsLightsAndSounds.HandleTipMenuItem(
                        messageMetadata: messageMetadata
                    );
                    break;
                
                case StreamEventsTipMenuNSFW.c_tipReward_FlashAssFor10Seconds:
                case StreamEventsTipMenuNSFW.c_tipReward_FlashCockFor10Seconds:
                case StreamEventsTipMenuNSFW.c_tipReward_PinchNipsFor10Seconds:
                case StreamEventsTipMenuNSFW.c_tipReward_SniffPitsFor10Seconds:
                case StreamEventsTipMenuNSFW.c_tipReward_GushControlLinkFor1Minute:
                case StreamEventsTipMenuNSFW.c_tipReward_SpinForARandomGushControlLinkOfXMinutes:
                case StreamEventsTipMenuNSFW.c_tipReward_GushControlLinkFor3Minutes:
                case StreamEventsTipMenuNSFW.c_tipReward_FlashAssholeFor10Seconds:
                case StreamEventsTipMenuNSFW.c_tipReward_GushControlLinkFor5Minutes:
                case StreamEventsTipMenuNSFW.c_tipReward_GetNakedFor15Minutes:
                case StreamEventsTipMenuNSFW.c_tipReward_IncreaseGushControlLinkTimeLimitBy5Minutes:
                case StreamEventsTipMenuNSFW.c_tipReward_PutEdgeInFor15Minutes:
                case StreamEventsTipMenuNSFW.c_tipReward_GetNakedUntilEndOfStream:
                case StreamEventsTipMenuNSFW.c_tipReward_PutEdgeInUntilEndOfStream:
                    StreamEventsTipMenuNSFW.HandleTipMenuItem(
                        messageMetadata: messageMetadata
                    );
                    break;
                
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberFlashAssFor10Seconds:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberFlashCockFor10Seconds:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPinchNipsFor10Seconds:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberSniffPitsFor10Seconds:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGushControlLinkFor1Minute:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberUnlockAnAvatarColor:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberSpinForARandomGushControlLinkOfXMinutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGushControlLinkFor3Minutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberFlashAssholeFor10Seconds:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGushControlLinkFor5Minutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberUnlockAnAvatarEffect:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGetNakedFor15Minutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberUnlockAnAvatarModel:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberIncreaseGushControlLinkTimeLimitBy5Minutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPayAttentionToMeFor5Minutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPutEdgeInFor15Minutes:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberGetNakedUntilEndOfStream:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberPutEdgeInUntilEndOfStream:
                case StreamEventsTipMenuSubscribers.c_tipReward_SubscriberEndStream:
                    StreamEventsTipMenuSubscribers.HandleTipMenuItem(
                        messageMetadata: messageMetadata
                    );
                    break;
                
                default:
                    ConsoleLogger.LogMessage(
                        message: 
                            $"EXCEPTION: " +
                            $"{nameof(StreamEventsController)}." +
                            $"{nameof(StreamEventsController.HandleWebSocketPayloadStreamEventTipped)}() - " +
                            $"Missing tip menu item \"{tipMenuItem}\"."
                    );
                    return;
            }
        }
        else
        {
            StreamEventsController.PlayTipSoundEffect();
        }
        
        string[] messages =
        [
            $"💸 Cha-CHING! Thank you, @{messageMetadata.Who}!",
            $"💸 Thank you for keeping my circuits running, @{messageMetadata.Who}!",
            $"💸 @{messageMetadata.Who} tipped {messageMetadata.HowMuch} tokens! Thank you!"
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );
        
        StreamEventsController.ExecuteDepositTip(
            targetUser: messageMetadata.Who,
            amount:     messageMetadata.HowMuch
        );
        
        StreamEventsController.SendDelayedBotMessage(
            message: messages[index]
        );
        
        var vibrationTimeInSeconds = StreamEventsController.GetTipVibrationTime(
            tipAmount: messageMetadata.HowMuch
        );
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: vibrationTimeInSeconds,
            eventType:     ServiceLovense.EventType.Tipped
        );
    }
    
    private static void PlayTipSoundEffect()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
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
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: message
                );
            }
        );
    }
    
    private void SubscribeToStreamEvents()
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Followed            += this.HandleWebSocketPayloadStreamEventFollowed;
        ServiceJoystickWebSocketPayloadStreamEvents.GiftedSubscriptions += this.HandleWebSocketPayloadStreamEventGiftedSubscriptions;
        ServiceJoystickWebSocketPayloadStreamEvents.MilestoneCompleted  += this.HandleWebSocketPayloadStreamEventMilestoneCompleted;
        ServiceJoystickWebSocketPayloadStreamEvents.StreamDroppedIn     += this.HandleWebSocketPayloadStreamEventStreamDroppedIn;
        ServiceJoystickWebSocketPayloadStreamEvents.Subscribed          += this.HandleWebSocketPayloadStreamEventSubscribed;
        ServiceJoystickWebSocketPayloadStreamEvents.Tipped              += this.HandleWebSocketPayloadStreamEventTipped;
    }
}