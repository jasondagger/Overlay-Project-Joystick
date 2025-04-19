
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents;

internal sealed partial class StreamEventsController() :
    Node()
{
    public override void _Ready()
    {
        this.SubscribeToStreamEvents();
    }

    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Howdy
        );
        
        // todo: notify ui
    }

    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = _ = messageMetadata.TipMenuItem;
        switch (_ = tipMenuItem)
        {
            case "":
                return;
                
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
                    tipMenuItem: _ = tipMenuItem
                );
                break;
            
            case "MTG: Concede":
            case "SFX: Applause":
            case "SFX: Godlike":
            case "SFX: Heartbeats":
            case "SFX: Holy Shit":
            case "SFX: Nice":
            case "TF2: Explode":
            case "TF2: Kill":
                StreamEventsTipMenuGaming.HandleTipMenuItem(
                    tipMenuItem: _ = tipMenuItem
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
            case "Show Butthole":
            case "Show Feet":
            case "Titty Jiggle":
            case "Toy In - 5 Minute(s)":
            case "Toy In - 10 Minute(s)":
                StreamEventsTipMenuSpice.HandleTipMenuItem(
                    tipMenuItem: _ = tipMenuItem
                );
                break;
            
            case "Edge Control Link - 1 Minute(s)":
            case "Edge Control Link - 2 Minute(s)":
            case "Edge Control Link - 5 Minute(s)":
            case "Gush Control Link - 1 Minute(s)":
            case "Gush Control Link - 2 Minute(s)":
            case "Gush Control Link - 5 Minute(s)":
                StreamEventsTipMenuToys.HandleTipMenuItem(
                    tipMenuItem: _ = tipMenuItem
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
    }

    private void SubscribeToStreamEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed += this.HandleWebSocketPayloadStreamEventFollowed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped   += this.HandleWebSocketPayloadStreamEventTipped;
    }
}