
using System;
using Godot;
using System.Collections.Generic;

namespace Overlay.Core.Services.Godots.Audios;

internal sealed partial class ServiceGodotAudio() :
    ServiceGodot()
{
	public enum SoundAlertType :
		uint
	{
		Applause = 0u,
		Ass,
		BallsOfSteel,
		ChatNotification,
		Followed,
		GiftedSubscriptions,
		Godlike,
		Heartbeats,
		HolyShit,
		Knocking,
		Nice,
		Pan,
		StreamDroppedIn,
		Subscribed,
		Tip,
	}
	
	internal void PlaySoundAlert(
		SoundAlertType soundAlertType
	)
	{
		var soundAlert = this.m_soundAlerts[key: soundAlertType];
		this.CallDeferred(
			method: $"{nameof(ServiceGodotAudio.PlayAudio)}",
			args:  [
				soundAlert,
			]
		);
	}
	
	internal override void Start()
	{
		this.RetrieveResources();
	}
	
	private const int                                              c_soundAlertDelayInMilliseconds = 1000;
	private const int                                              c_secondsToMilliseconds         = 1000;
	
	private readonly Dictionary<SoundAlertType, AudioStreamPlayer> m_soundAlerts                   = new();
	
	private void RetrieveResources()
	{
		var soundAlertTypes = Enum.GetValues<SoundAlertType>();
		foreach (var soundAlertType in soundAlertTypes)
		{
			var audioStreamPlayer = this.GetChild<AudioStreamPlayer>(
				idx: (int) soundAlertType
			);
			this.m_soundAlerts.Add(
				key:   soundAlertType,
				value: audioStreamPlayer
			);
		}
	}

	private static void PlayAudio(
		AudioStreamPlayer audioStreamPlayer
	)
	{
		audioStreamPlayer.Play();
	}
}