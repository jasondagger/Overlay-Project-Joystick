
using System.Threading.Tasks;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.TeamFortress2s;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuGaming
{
    internal const string c_tipReward_TF2TriggerAnAction          = "TF2: Trigger an Action";
    internal const string c_tipReward_TF2SwitchToBunnyHopping     = "TF2: Switch to Bunny Hopping";
    internal const string c_tipReward_TF2SwitchToCasualQueue      = "TF2: Switch to Casual Queue";
    internal const string c_tipReward_TF2SwitchToCompetitiveQueue = "TF2: Switch to Competitive Queue";
    internal const string c_tipReward_TF2SwitchToMannVsMachine    = "TF2: Switch to Mann Vs Machine";
    internal const string c_tipReward_TF2SwitchToSurfing          = "TF2: Switch to Surfing";
    internal const string c_tipReward_TF2BuyMeADuelingMiniGame    = "TF2: Buy Me a Dueling Mini-Game";
    internal const string c_tipReward_TF2BuyMeATourOfDutyTicket   = "TF2: Buy Me a Tour Of Duty Ticket";
    internal const string c_tipReward_TF2BuyMeACaseKey            = "TF2: Buy Me a Case Key";
    internal const string c_tipReward_TF2CloseTheGame             = "TF2: Close the Game";
    internal const string c_tipReward_TF2BuyMeAGoldenPan          = "TF2: Buy Me a Golden Pan";
    
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = messageMetadata.TipMenuItem;
        switch (tipMenuItem)
        {
            case StreamEventsTipMenuGaming.c_tipReward_TF2TriggerAnAction:
                StreamEventsTipMenuGaming.HandleTF2TriggerAnAction(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToBunnyHopping:
                StreamEventsTipMenuGaming.HandleTF2SwitchToBunnyHopping();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToCasualQueue:
                StreamEventsTipMenuGaming.HandleTF2SwitchToCasualQueue();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToCompetitiveQueue:
                StreamEventsTipMenuGaming.HandleTF2SwitchToCompetitiveQueue();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToMannVsMachine:
                StreamEventsTipMenuGaming.HandleTF2SwitchToMannVsMachine();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2SwitchToSurfing:
                StreamEventsTipMenuGaming.HandleTF2SwitchToSurfing();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeADuelingMiniGame:
                StreamEventsTipMenuGaming.HandleTF2TF2BuyMeADuelingMiniGame();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeATourOfDutyTicket:
                StreamEventsTipMenuGaming.HandleTF2BuyMeATourOfDutyTicket();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeACaseKey:
                StreamEventsTipMenuGaming.HandleTF2BuyMeACaseKey();
                break;
            
            case StreamEventsTipMenuGaming.c_tipReward_TF2CloseTheGame:
                StreamEventsTipMenuGaming.HandleTF2CloseTheGame();
                break;
          
            case StreamEventsTipMenuGaming.c_tipReward_TF2BuyMeAGoldenPan:
                StreamEventsTipMenuGaming.HandleTF2BuyMeAGoldenPan();
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: 
                        $"EXCEPTION: " +
                        $"{nameof(StreamEventsTipMenuGaming)}." +
                        $"{nameof(StreamEventsTipMenuGaming.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }
    
    private static void HandleTF2TriggerAnAction(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForTF2TriggerAnActionRequests(
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
                    message:  $"⚔️ @{username} - TF2: Trigger an Action is ready! Type !tf2 [action] in chat! Actions can be found in the bio if you need help!"
                );
            }
        );
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2SwitchToBunnyHopping()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2SwitchToCasualQueue()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2SwitchToCompetitiveQueue()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2SwitchToMannVsMachine()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2SwitchToSurfing()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2TF2BuyMeADuelingMiniGame()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2BuyMeATourOfDutyTicket()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2BuyMeACaseKey()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleTF2CloseTheGame()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
        ServiceTeamFortress2BindHandler.CloseGame();
    }
    
    private static void HandleTF2BuyMeAGoldenPan()
    {
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
}