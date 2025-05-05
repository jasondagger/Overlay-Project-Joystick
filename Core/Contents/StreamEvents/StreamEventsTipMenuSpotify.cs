
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.Spotifies;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuSpotify
{
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = _ = messageMetadata.TipMenuItem;
        switch (_ = tipMenuItem)
        {
            case "Skip Song":
                StreamEventsTipMenuSpotify.HandleSkipSong();
                break;
            
            case "Song Request":
                StreamEventsTipMenuSpotify.HandleSongRequest(
                    messageMetadata: _ = messageMetadata
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: _ = 
                        $"EXCEPTION: " +
                        $"{_ = nameof(StreamEventsTipMenuSpotify)}." +
                        $"{_ = nameof(StreamEventsTipMenuSpotify.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
    }
    
    private static void HandleSkipSong()
    {
        var serviceSpotify = Services.Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestSkipToNextTrack();
        
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleSongRequest(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var username = _ = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForSongRequests(
            username: _ = username
        );

        var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
        serviceJoystickBot.SendChatMessage(
            message: _ = $"Song request ready! Type !songrequest or !sr with your search parameters in chat, @{username}!"
        );
        
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = _ = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: _ = ServiceGodotAudio.SoundAlertType.Tip
        );
    }
}