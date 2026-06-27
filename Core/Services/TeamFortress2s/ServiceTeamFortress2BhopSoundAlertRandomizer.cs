
using System;
using Overlay.Core.Services.Godots.Audios;

namespace Overlay.Core.Services.TeamFortress2s;

internal static class ServiceTeamFortress2BhopSoundAlertRandomizer
{
    internal static ServiceGodotAudio.SoundAlertType GetRandomCheckpointReachedSound()
    {
        var index = ServiceTeamFortress2BhopSoundAlertRandomizer.s_random.Next(
            ServiceTeamFortress2BhopSoundAlertRandomizer.s_checkpointReachedSounds.Length
        );
        return ServiceTeamFortress2BhopSoundAlertRandomizer.s_checkpointReachedSounds[index];
    }
    
    internal static ServiceGodotAudio.SoundAlertType GetRandomFireworkSound()
    {
        var index = ServiceTeamFortress2BhopSoundAlertRandomizer.s_random.Next(
            ServiceTeamFortress2BhopSoundAlertRandomizer.s_fireworkSounds.Length
        );
        return ServiceTeamFortress2BhopSoundAlertRandomizer.s_fireworkSounds[index];
    }

    internal static ServiceGodotAudio.SoundAlertType GetRandomStageReachedSound()
    {
        var index = ServiceTeamFortress2BhopSoundAlertRandomizer.s_random.Next(
            ServiceTeamFortress2BhopSoundAlertRandomizer.s_stageReachedSounds.Length
        );
        return ServiceTeamFortress2BhopSoundAlertRandomizer.s_stageReachedSounds[index];
    }
    
    private static readonly Random                             s_random                  = new();
    private static readonly ServiceGodotAudio.SoundAlertType[] s_checkpointReachedSounds =
    [
        ServiceGodotAudio.SoundAlertType.BhopCheckpointReached1,
        ServiceGodotAudio.SoundAlertType.BhopCheckpointReached2,
        ServiceGodotAudio.SoundAlertType.BhopCheckpointReached3
    ];
    private static readonly ServiceGodotAudio.SoundAlertType[] s_fireworkSounds          =
    [
        ServiceGodotAudio.SoundAlertType.BhopGenericCompletedFireworks1,
        ServiceGodotAudio.SoundAlertType.BhopGenericCompletedFireworks2,
        ServiceGodotAudio.SoundAlertType.BhopGenericCompletedFireworks3,
        ServiceGodotAudio.SoundAlertType.BhopGenericCompletedFireworks4
    ];
    private static readonly ServiceGodotAudio.SoundAlertType[] s_stageReachedSounds      =
    [
        ServiceGodotAudio.SoundAlertType.BhopStageReached1,
        ServiceGodotAudio.SoundAlertType.BhopStageReached2,
        ServiceGodotAudio.SoundAlertType.BhopStageReached3,
        ServiceGodotAudio.SoundAlertType.BhopStageReached4,
        ServiceGodotAudio.SoundAlertType.BhopStageReached5
    ];
}
