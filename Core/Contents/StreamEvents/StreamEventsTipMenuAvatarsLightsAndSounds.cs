
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Spotifies;
using Overlay.Core.Tools;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsTipMenuAvatarsLightsAndSounds
{
    internal const string c_tipReward_PlaySoundEffect                                = "Play Sound Effect";
    internal const string c_tipReward_SetOverlayThemeAndBackgroundLightsFor15Minutes = "Set Overlay Theme & Background Lights for 15 Minutes";
    internal const string c_tipReward_SpotifySongRequest                             = "Spotify Song Request";
    internal const string c_tipReward_SpotifySongSkip                                = "Spotify Song Skip";
    internal const string c_tipReward_UnlockAnAvatarColor                            = "Unlock an Avatar Color";
    internal const string c_tipReward_UnlockAnAvatarEffect                           = "Unlock an Avatar Effect";
    internal const string c_tipReward_UnlockAnAvatarModel                            = "Unlock an Avatar Model";
    
    internal static void HandleTipMenuItem(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var tipMenuItem = messageMetadata.TipMenuItem;
        switch (tipMenuItem)
        {
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_PlaySoundEffect:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandlePlaySoundEffect(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_SetOverlayThemeAndBackgroundLightsFor15Minutes:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandleSetOverlayThemeAndBackgroundLightsFor15Minutes(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_SpotifySongRequest:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandleSongRequest(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_SpotifySongSkip:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandleSongSkip();
                break;
            
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_UnlockAnAvatarColor:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandleUnlockAnAvatarColor(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_UnlockAnAvatarEffect:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandleUnlockAnAvatarEffect(
                    messageMetadata: messageMetadata
                );
                break;
            
            case StreamEventsTipMenuAvatarsLightsAndSounds.c_tipReward_UnlockAnAvatarModel:
                StreamEventsTipMenuAvatarsLightsAndSounds.HandleUnlockAnAvatarModel(
                    messageMetadata: messageMetadata
                );
                break;
            
            default:
                ConsoleLogger.LogMessage(
                    message: 
                        $"EXCEPTION: " +
                        $"{nameof(StreamEventsTipMenuAvatarsLightsAndSounds)}." +
                        $"{nameof(StreamEventsTipMenuAvatarsLightsAndSounds.HandleTipMenuItem)}() - " +
                        $"Missing tip menu item \"{tipMenuItem}\"."
                );
                return;
        }
        
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandlePlaySoundEffect(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForSFXRequests(
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
                    message: $"🎵 @{username} - SFX are ready! Type !sfx with your choice in chat! Sound effects can be found in the bio if you need help!"
                );
            }
        );
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleSetOverlayThemeAndBackgroundLightsFor15Minutes(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForLightRequests(
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
                    message: $"💡 @{username} - Lights are ready! Type !lights with your combination of colors & scenes in chat! Light colors & scenes can be found in the bio if you need help!"
                );
            }
        );
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleSongRequest(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var username = messageMetadata.Who;
        ServiceJoystickWebSocketPayloadChatHandler.AddUserForSongRequests(
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
                    message: $"🎵 @{username} - Song request is ready! Type !song request [song] - [artist] in chat!"
                );
            }
        );
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }
    
    private static void HandleSongSkip()
    {
        var serviceSpotify = Services.Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestSkipToNextTrack();
        
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
        
        serviceGodotAudio.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.Tip
        );
    }

    private static void HandleUnlockAnAvatarColor(
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
    
    private static void HandleUnlockAnAvatarEffect(
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
    
    private static void HandleUnlockAnAvatarModel(
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
}