
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal sealed partial class StreamEventsController() :
    Node()
{
    public override void _Ready()
    {
        this.SubscribeToStreamEvents();
    }
    
    private readonly RandomNumberGenerator m_random = new();
    
    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Followed
        );
        
        string[] messages =
        [
            $"A new follower has appeared! Welcome, {_ = messageMetadata.Who}!",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventGiftedSubscriptions(
        ServiceJoystickWebSocketPayloadMessageMetadataGiftedSubscriptions messageMetadata
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.GiftedSubscriptions
        );
        
        string[] messages =
        [
            $"{_ = messageMetadata.Who} gifted {_ = messageMetadata.HowMuch} sub{_ = (messageMetadata.HowMuch > 1 ? "s" : string.Empty)}! Thank you so much!",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventStreamDroppedIn(
        ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn messageMetadata
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.StreamDroppedIn
        );
        
        string[] messages =
        [
            $"Welcome in, {_ = messageMetadata.Who} & friends! Feel free to lurk or chat :)",
            $"Hello there, {_ = messageMetadata.Who} & friends! Feel free to lurk or chat :)",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Subscribed
        );
        
        string[] messages =
        [
            $"The MYTH, the LEGEND! {_ = messageMetadata.Who} just subscribed!",
            $"{_ = messageMetadata.Who} just subscribed! Thank you so much for keeping my circuits running!",
        ];
        var index = _ = this.m_random.RandiRange(
            0,
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem   = _ = messageMetadata.TipMenuItem;
        switch (_ = tipMenuItem)
        {
            case "":
                StreamEventsController.PlayTipSoundEffect();
                break;
                
            case "Ohai":
            case "Nice Smile!":
            case "Thank You":
            case "You Look Great!":
            case "Headpats":
            case "Belly Rub":
            case "Hydrate":
            case "Toke Up":
            case "Do One More!":
            case "Streeeetch!":
                StreamEventsTipMenuIRL.HandleTipMenuItem(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            case "MTG: Concede":
            case "SFX: Applause":
            case "SFX: Godlike":
            case "SFX: Heartbeats":
            case "SFX: Holy Shit":
            case "SFX: Knocking":
            case "SFX: Nice":
            case "SFX: Pan":
            case "TF2: Explode":
            case "TF2: Kill":
                StreamEventsTipMenuGaming.HandleTipMenuItem(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            case "Ass Slap":
            case "Ass Slap x5":
            case "Ass Slap x10":
            case "Balls Foreplay - 1 Minute(s)":
            case "Balls Foreplay - 2 Minute(s)":
            case "Belly Button Fingering - 1 Minute(s)":
            case "Belly Button Fingering - 2 Minute(s)":
            case "Cock Out - 1 Minute(s)":
            case "Cock Out - 5 Minute(s)":
            case "Cock Out - 10 Minute(s)":
            case "Cock Foreplay - 1 Minute(s)":
            case "Cock Foreplay - 2 Minute(s)":
            case "Cum":
            case "Cum Taste":
            case "Nipple Pinch":
            case "No Underwear & Pants - 10 Minute(s)":
            case "Show Butthole":
            case "Show Feet":
            case "Titty Jiggle":
            case "Toy In - 5 Minute(s)":
            case "Toy In - 10 Minute(s)":
            case "Turn On Ring Light":
                StreamEventsTipMenuSpice.HandleTipMenuItem(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            case "Skip Song":
            case "Song Request":
                StreamEventsTipMenuSpotify.HandleTipMenuItem(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            case "Set Light Color Blue":
            case "Set Light Color Cyan":
            case "Set Light Color Green":
            case "Set Light Color Magenta":
            case "Set Light Color Red":
            case "Set Light Color White":
            case "Set Light Color Yellow":
            case "Set Light Scene Heatwave":
            case "Set Light Scene Icy":
            case "Set Light Scene Rainbow":
            case "Turn Off Lights":
                StreamEventsTipMenuLights.HandleTipMenuItem(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            case "Edge Control Link - 1 Minute(s)":
            case "Edge Control Link - 2 Minute(s)":
            case "Edge Control Link - 5 Minute(s)":
            case "Gush Control Link - 1 Minute(s)":
            case "Gush Control Link - 2 Minute(s)":
            case "Gush Control Link - 5 Minute(s)":
                StreamEventsTipMenuToys.HandleTipMenuItem(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsController)}." +
                        $"{_ = nameof(StreamEventsController.HandleWebSocketPayloadStreamEventTipped)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
        
        string[] messages =
        [
            $"Cha-CHING! Thank you!",
            $"Thank you for keeping my circuits running, {_ = messageMetadata.Who}!",
        ];
        var index = _ = this.m_random.RandiRange(
            0, 
            messages.Length - 1
        );

        var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = messages[index]
        );
    }
    
    private static void PlayTipSoundEffect()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private void SubscribeToStreamEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed            += this.HandleWebSocketPayloadStreamEventFollowed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.GiftedSubscriptions += this.HandleWebSocketPayloadStreamEventGiftedSubscriptions;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.StreamDroppedIn     += this.HandleWebSocketPayloadStreamEventStreamDroppedIn;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Subscribed          += this.HandleWebSocketPayloadStreamEventSubscribed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped              += this.HandleWebSocketPayloadStreamEventTipped;
    }
}