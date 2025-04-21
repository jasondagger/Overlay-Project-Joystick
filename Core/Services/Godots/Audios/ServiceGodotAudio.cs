
using System;
using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Godots.Audios;

internal sealed partial class ServiceGodotAudio() :
    ServiceGodot()
{
	public override void _Process(
		double delta
	)
	{
		this.ProcessSoundAlerts();
	}
	
	public enum SoundAlertType :
		uint
	{
		Applause = 0u,
		ChatNotification,
		DropinStream,
		Followed,
		Godlike,
		Heartbeats,
		HolyShit,
		Knocking,
		Nice,
		Pan,
		Tip,
	}
	
	internal void PlaySoundAlert(
		SoundAlertType soundAlertType
	)
	{
		lock (_ = this.m_soundAlertsQueueLock)
		{
			this.m_soundAlertsQueue.Enqueue(
				item: _ = soundAlertType
			);
		}
	}
	
	internal override void Start()
	{
		this.RetrieveResources();
	}
	
	private const int                                              c_soundAlertDelayInMilliseconds = 1000;
	private const int                                              c_secondsToMilliseconds         = 1000;
	
	private bool                                                   m_isSoundAlertPlaying           = _ = false;
	private readonly Dictionary<SoundAlertType, AudioStreamPlayer> m_soundAlerts                   = new();
	private readonly Queue<SoundAlertType>                         m_soundAlertsQueue              = new();
	private readonly object                                        m_soundAlertsQueueLock          = new();
	
	private static int GetStreamLengthInMilliseconds(
		AudioStream stream
	)
	{
		var length = _ = stream.GetLength();
		return _ = Mathf.RoundToInt(
			s: _ = length * ServiceGodotAudio.c_secondsToMilliseconds
		);
	}

	private async Task ProcessSoundAlert(
		SoundAlertType soundAlertType
	)
	{
		var soundAlert = _ = this.m_soundAlerts[key: _ = soundAlertType];
		soundAlert.Play();
		_ = this.m_isSoundAlertPlaying = _ = true;

		var streamLength = _ = ServiceGodotAudio.GetStreamLengthInMilliseconds(
			stream: _ = soundAlert.Stream
		);
		await Task.Delay(
			millisecondsDelay: _ = streamLength
		);

		soundAlert.Stop();

		await Task.Delay(
			millisecondsDelay: _ = ServiceGodotAudio.c_soundAlertDelayInMilliseconds
		);

		_ = this.m_isSoundAlertPlaying = _ = false;
	}

	private async void ProcessSoundAlerts()
	{
		if (_ = this.m_isSoundAlertPlaying is not false)
		{
			return;
		}
		
		SoundAlertType soundAlertType;
		lock (_ = this.m_soundAlertsQueueLock)
		{
			if (_ = this.m_soundAlertsQueue.Count > 0u)
			{
				soundAlertType = _ = this.m_soundAlertsQueue.Dequeue();
			}
			else
			{
				return;
			}
		}

		await this.ProcessSoundAlert(
			soundAlertType: _ = soundAlertType
		);
	}
	
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
}