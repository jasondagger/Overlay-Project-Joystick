
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.Lovenses;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal sealed partial class StreamEventsController() :
    Node()
{
    public override void _Ready()
    {
        this.SubscribeToStreamEvents();
    }

    private const double                   c_dropInVibrationTime    = 30d;
    private const double                   c_followVibrationTime    = 5d;
    private const double                   c_subscribeVibrationTime = 12d;
    
    private readonly RandomNumberGenerator m_random                 = new();

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
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Followed
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.Vibrate(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_followVibrationTime
        );
        
        string[] messages =
        [
            $"A new follower has appeared! Welcome, @{messageMetadata.Who}!",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventGiftedSubscriptions(
        ServiceJoystickWebSocketPayloadMessageMetadataGiftedSubscriptions messageMetadata
    )
    {
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.GiftedSubscriptions
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.Vibrate(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_subscribeVibrationTime * messageMetadata.HowMuch
        );
        
        string[] messages =
        [
            $"{messageMetadata.Who} gifted {messageMetadata.HowMuch} sub{(messageMetadata.HowMuch > 1 ? "s" : string.Empty)}! Thank you so much!",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventStreamDroppedIn(
        ServiceJoystickWebSocketPayloadMessageMetadataStreamDroppedIn messageMetadata
    )
    {
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.StreamDroppedIn
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.Vibrate(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_dropInVibrationTime
        );
        
        string[] messages =
        [
            $"Welcome in, {messageMetadata.Who} & friends! Feel free to lurk or chat :)",
            $"Hello there, {messageMetadata.Who} & friends! Feel free to lurk or chat :)",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: messages[index]
        );
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Subscribed
        );
        
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.Vibrate(
            intensity:     20,
            timeInSeconds: StreamEventsController.c_subscribeVibrationTime
        );
        
        string[] messages =
        [
            $"The MYTH, the LEGEND! {messageMetadata.Who} just subscribed!",
            $"{messageMetadata.Who} just subscribed! Thank you so much for keeping my circuits running!",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: messages[index]
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
                    
                case "Ohai":
                case "Nice Smile!":
                case "Thank You":
                case "You Look Great!":
                case "Headpats":
                case "Belly Rub":
                case "Streeeetch!":
                case "Do One More!":
                case "Hydrate":
                case "Just Lurkin'":
                case "Toke Up":
                    StreamEventsTipMenuIRL.HandleTipMenuItem(
                        messageMetadata: messageMetadata
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
                        messageMetadata: messageMetadata
                    );
                    break;
                
                case "Skip Song":
                case "Song Request":
                    StreamEventsTipMenuSpotify.HandleTipMenuItem(
                        messageMetadata: messageMetadata
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
                case "Set Light Scene Toxic":
                case "Turn Off Lights":
                    StreamEventsTipMenuLights.HandleTipMenuItem(
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
            $"Cha-CHING! Thank you!",
            $"Thank you for keeping my circuits running, {messageMetadata.Who}!",
        ];
        var index = this.m_random.RandiRange(
            from: 0, 
            to:   messages.Length - 1
        );

        var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: messages[index]
        );

        var vibrationTimeInSeconds = StreamEventsController.GetTipVibrationTime(
            tipAmount: messageMetadata.HowMuch
        );
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.Vibrate(
            intensity:     20,
            timeInSeconds: vibrationTimeInSeconds
        );
    }
    
    private static void PlayTipSoundEffect()
    {
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private void SubscribeToStreamEvents()
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Followed            += this.HandleWebSocketPayloadStreamEventFollowed;
        ServiceJoystickWebSocketPayloadStreamEvents.GiftedSubscriptions += this.HandleWebSocketPayloadStreamEventGiftedSubscriptions;
        ServiceJoystickWebSocketPayloadStreamEvents.StreamDroppedIn     += this.HandleWebSocketPayloadStreamEventStreamDroppedIn;
        ServiceJoystickWebSocketPayloadStreamEvents.Subscribed          += this.HandleWebSocketPayloadStreamEventSubscribed;
        ServiceJoystickWebSocketPayloadStreamEvents.Tipped              += this.HandleWebSocketPayloadStreamEventTipped;
    }
}