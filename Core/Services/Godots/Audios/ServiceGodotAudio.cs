
using System;
using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Godots.Audios;

internal sealed partial class ServiceGodotAudio() :
    ServiceGodot()
{
	public enum SoundAlertType :
		uint
	{
		Applause = 0u,
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
		var soundAlert = _ = this.m_soundAlerts[key: _ = soundAlertType];
		this.CallDeferred(
			method: _ = $"{_ = nameof(ServiceGodotAudio.PlayAudio)}",
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
		var soundAlertTypes = _ = Enum.GetValues<SoundAlertType>();
		foreach (var soundAlertType in soundAlertTypes)
		{
			var audioStreamPlayer = _ = this.GetChild<AudioStreamPlayer>(
				idx: _ = (int) soundAlertType
			);
			this.m_soundAlerts.Add(
				key:   _ = soundAlertType,
				value: _ = audioStreamPlayer
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