
using System.Collections.Generic;
using System.Threading.Tasks;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;
using EffectBackgroundSnow = Overlay.Core.Contents.Effects.Backgrounds.EffectBackgroundSnow;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuIRL
{
    internal const string c_tipReward_First                  = "First!";
    internal const string c_tipReward_Ohai                   = "Ohai";
    internal const string c_tipReward_ThankYou               = "Thank You!";
    internal const string c_tipReward_YouLookGreat           = "You Look Great!";
    internal const string c_tipReward_DoOneMore              = "Do One More!";
    internal const string c_tipReward_Hydrate                = "Hydrate";
    internal const string c_tipReward_JustLurkin             = "Just Lurkin'";
    internal const string c_tipReward_BellyRubFor10Seconds   = "Belly Rub for 10 Seconds";
    internal const string c_tipReward_FlexFor10Seconds       = "Flex for 10 Seconds";
    internal const string c_tipReward_HeadpatsFor10Seconds   = "Headpats for 10 Seconds";
    internal const string c_tipReward_StreeeetchFor10Seconds = "Streeeetch for 10 Seconds";
    internal const string c_tipReward_Do10JumpingJacks       = "Do 10 Jumping Jacks";
    internal const string c_tipReward_Do10Situps             = "Do 10 Sit-ups";
    internal const string c_tipReward_Do10Squats             = "Do 10 Squats";
    internal const string c_tipReward_Do10Burpees            = "Do 10 Burpees";
    internal const string c_tipReward_Do10Pushups            = "Do 10 Push-ups";
    internal const string c_tipReward_Do10BicepCurls         = "Do 10 Bicep Curls";
    internal const string c_tipReward_Ddo10LateralRaises     = "Do 10 Lateral Raises";
    internal const string c_tipReward_Do10OverheadPresses    = "Do 10 Overhead Presses";
    internal const string c_tipReward_ShowinSomeLove         = "Showin' Some Love";
    internal const string c_tipReward_TokeUp                 = "Toke Up";
    internal const string c_tipReward_BuyMeAPoweradeSlushie  = "Buy Me a Powerade Slushie";
    internal const string c_tipReward_BuyMeABananaShake      = "Buy Me a Banana Shake";
    internal const string c_tipReward_BuyMeDinner            = "Buy Me Dinner";
    internal const string c_tipReward_PayMyBills             = "Pay My Bills";
    
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = messageMetadata.TipMenuItem;
        switch (tipMenuItem)
        {
            case StreamEventsTipMenuIRL.c_tipReward_First:
                StreamEventsTipMenuIRL.HandleFirst(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Ohai:
                StreamEventsTipMenuIRL.HandleOhai(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_ThankYou:
                StreamEventsTipMenuIRL.HandleThankYou(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_YouLookGreat:
                StreamEventsTipMenuIRL.HandleYouLookGreat(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_DoOneMore:
                StreamEventsTipMenuIRL.HandleDoOneMore(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Hydrate:
                StreamEventsTipMenuIRL.HandleHydrate(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_JustLurkin:
                StreamEventsTipMenuIRL.HandleJustLurkin(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_BellyRubFor10Seconds:
                StreamEventsTipMenuIRL.HandleBellyRubFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_FlexFor10Seconds:
                StreamEventsTipMenuIRL.HandleFlexFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_HeadpatsFor10Seconds:
                StreamEventsTipMenuIRL.HandleHeadpatsFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_StreeeetchFor10Seconds:
                StreamEventsTipMenuIRL.HandleStreeeetchFor10Seconds(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10JumpingJacks:
                StreamEventsTipMenuIRL.HandleDo10JumpingJacks(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10Situps:
                StreamEventsTipMenuIRL.HandleDo10Situps(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10Squats:
                StreamEventsTipMenuIRL.HandleDo10Squats(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10Burpees:
                StreamEventsTipMenuIRL.HandleDo10Burpees(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10Pushups:
                StreamEventsTipMenuIRL.HandleDo10Pushups(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10BicepCurls:
                StreamEventsTipMenuIRL.HandleDo10BicepCurls(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Ddo10LateralRaises:
                StreamEventsTipMenuIRL.HandleDo10LateralRaises(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_Do10OverheadPresses:
                StreamEventsTipMenuIRL.HandleDo10OverheadPresses(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_ShowinSomeLove:
                StreamEventsTipMenuIRL.HandleShowinSomeLove(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_TokeUp:
                StreamEventsTipMenuIRL.HandleTokeUp(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_BuyMeAPoweradeSlushie:
                StreamEventsTipMenuIRL.HandleBuyMeAPoweradeSlushie(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_BuyMeABananaShake:
                StreamEventsTipMenuIRL.HandleBuyMeABananaShake(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_BuyMeDinner:
                StreamEventsTipMenuIRL.HandleBuyMeDinner(
                    messageMetadata: messageMetadata
                );
                break;
            case StreamEventsTipMenuIRL.c_tipReward_PayMyBills:
                StreamEventsTipMenuIRL.HandlePayMyBills(
                    messageMetadata: messageMetadata
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: 
                        $"EXCEPTION: " +
                        $"{nameof(StreamEventsTipMenuIRL)}." +
                        $"{nameof(StreamEventsTipMenuIRL.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }
    
    private static void ExecuteDeposit(
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
    
    private static void HandleFirst(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsTipMenuIRL.ExecuteDeposit(
            targetUser: messageMetadata.Who
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🥇 {messageMetadata.Who} was first! You earned a free Gush Control Link minute! Type !bank check to see how many minutes you have!"
        );
    }
    
    private static void HandleOhai(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsHelper.SendBotMessage(
            message: $"👋 Howdy, {messageMetadata.Who}!"
        );
    }
    
    private static void HandleThankYou(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🙏 You're welcome, {messageMetadata.Who}!"
        );
    }
    
    private static void HandleYouLookGreat(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsHelper.SendBotMessage(
            message: $"👑 Right back at you, {messageMetadata.Who}!"
        );
    }
    
    private static void HandleDoOneMore(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🔁 {messageMetadata.Who} requested SmoothDagger do one more! Get to it!"
        );
    }
    
    private static void HandleHydrate(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🥤 {messageMetadata.Who} requested SmoothDagger to get hydrated! Thanks for the hydration reminder!"
        );
    }
    
    private static void HandleJustLurkin(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🥷 Thanks for lurkin', {messageMetadata.Who}! Lurk away!"
        );
    }
    
    private static void HandleBellyRubFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Belly rub for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger rub their belly for 10 seconds! Timer started!";
        
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
    
    private static void HandleFlexFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Flex for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger flex for 10 seconds! Timer started!";
        
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
    
    private static void HandleHeadpatsFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Headpats for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger give headpats for 10 seconds! Timer started!";
        
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
    
    private static void HandleStreeeetchFor10Seconds(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 14f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 12000;
        const string c_endMessage                = $"🔔 Streeeetch for 10 seconds timer has ended.";
        
        var startMessage = $"🔔 {messageMetadata.Who} requested SmoothDagger streeeetch for 10 seconds! Timer started!";
        
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
    
    private static void HandleDo10JumpingJacks(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 jumping jacks! Work out time!";
        
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
    
    private static void HandleDo10Situps(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 sit-ups! Work out time!";
        
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
    
    private static void HandleDo10Squats(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 squats! Work out time!";
        
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
    
    private static void HandleDo10Burpees(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 burpees! Work out time!";
        
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
    
    private static void HandleDo10Pushups(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 push-ups! Work out time!";
        
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
    
    private static void HandleDo10BicepCurls(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 bicep curls! Work out time!";
        
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
    
    private static void HandleDo10LateralRaises(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 lateral raises! Work out time!";
        
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
    
    private static void HandleDo10OverheadPresses(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        const float  c_layoutMeDurationInSeconds = 42f;
        const int    c_startDelayInMilliseconds  = 2200;
        const int    c_timerDelayInMilliseconds  = 40000;
        const string c_endMessage                = $"";
        
        var startMessage = $"💪 {messageMetadata.Who} requested SmoothDagger do 10 overhead presses! Work out time!";
        
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
    
    private static void HandleShowinSomeLove(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        GoveeLightControllers.SetGoveeLightControllers(
            lightsCeiling:  (null, ServiceGoveeSceneNames.ShowinSomeLove),
            lightsStanding: (null, ServiceGoveeSceneNames.ShowinSomeLove)
        );
        EffectBackgroundSnow.Instance.SetStoreAndStartResetTimerForParticleTexture(
            particleTexture: EffectBackgroundSnow.ParticleTexture.Heart
        );
        StreamEventsHelper.SendBotMessage(
            message: $"❤️‍ Thanks for showin' some love, {messageMetadata.Who}!"
        );
    }
    
    private static void HandleTokeUp(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        GoveeLightControllers.SetGoveeLightControllers(
            lightsCeiling:  (null, ServiceGoveeSceneNames.TokeUp),
            lightsStanding: (null, ServiceGoveeSceneNames.TokeUp)
        );
        EffectBackgroundSnow.Instance.SetStoreAndStartResetTimerForParticleTexture(
            particleTexture: EffectBackgroundSnow.ParticleTexture.MarijuanaLeaf
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🌲 {messageMetadata.Who} requested SmoothDagger to toke up! Blaze it up!"
        );
    }
    
    private static void HandleBuyMeAPoweradeSlushie(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        GoveeLightControllers.SetGoveeLightControllers(
            lightsCeiling:  (null, ServiceGoveeSceneNames.PoweradeSlushie),
            lightsStanding: (null, ServiceGoveeSceneNames.PoweradeSlushie)
        );
        EffectBackgroundSnow.Instance.SetStoreAndStartResetTimerForParticleTexture(
            particleTexture: EffectBackgroundSnow.ParticleTexture.Drink
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🍺 {messageMetadata.Who} just bought SmoothDagger a Powerade slushie! Thank you!"
        );
    }
    
    private static void HandleBuyMeABananaShake(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Banana
        );
        GoveeLightControllers.SetGoveeLightControllers(
            lightsCeiling:  (null, ServiceGoveeSceneNames.BananaShake),
            lightsStanding: (null, ServiceGoveeSceneNames.BananaShake)
        );
        EffectBackgroundSnow.Instance.SetStoreAndStartResetTimerForParticleTexture(
            particleTexture: EffectBackgroundSnow.ParticleTexture.Banana
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🍺 {messageMetadata.Who} just bought SmoothDagger a banana shake! Thank you!"
        );
    }
    
    private static void HandleBuyMeDinner(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        GoveeLightControllers.SetGoveeLightControllers(
            lightsCeiling:  (null, ServiceGoveeSceneNames.Dinner),
            lightsStanding: (null, ServiceGoveeSceneNames.Dinner)
        );
        EffectBackgroundSnow.Instance.SetStoreAndStartResetTimerForParticleTexture(
            particleTexture: EffectBackgroundSnow.ParticleTexture.Turkey
        );
        StreamEventsHelper.SendBotMessage(
            message: $"🍗 {messageMetadata.Who} just bought SmoothDagger dinner! Thank you!"
        );
    }
    
    private static void HandlePayMyBills(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        EffectBackgroundSnow.Instance.SetStoreAndStartResetTimerForParticleTexture(
            particleTexture: EffectBackgroundSnow.ParticleTexture.MoneyBag
        );
        StreamEventsHelper.SendBotMessage(
            message: $"💸 {messageMetadata.Who} just paid SmoothDagger's bills! Thank you!"
        );
    }
}