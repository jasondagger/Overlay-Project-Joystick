
using Godot;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Overlay.Core.Tools;

namespace Overlay.Core.Services.Godots.Audios;

internal sealed partial class ServiceGodotAudio() :
    ServiceGodot()
{
	public enum SoundAlertType :
		uint
	{
		Achievement = 0u,
		ActivityDing,
		Airhorn,
		Applause,
		Ass,
		BallsOfSteel,
		Banana,
		BhopBonusCompleted,
		BhopCheckpointReached1,
		BhopCheckpointReached2,
		BhopCheckpointReached3,
		BhopGenericCompletedFireworks1,
		BhopGenericCompletedFireworks2,
		BhopGenericCompletedFireworks3,
		BhopGenericCompletedFireworks4,
		BhopMapCompleted,
		BhopStageReached1,
		BhopStageReached2,
		BhopStageReached3,
		BhopStageReached4,
		BhopStageReached5,
		Blink,
		Boing,
		Bonk,
		Bruh,
		Buzzer,
		Captured,
		Censor,
		ChatNotification,
		CommandAccept,
		CommandDeny,
		CriticalHit,
		Death,
		Defended,
		Discord,
		DunDun,
		Fart,
		Followed,
		GiftedSubscriptions,
		Godlike,
		GoldenPan,
		Grindr,
		Hammer,
		Heartbeats,
		HelloThere,
		HolyShit,
		InstantTransmission,
		Interesting,
		Jeopardy,
		Knocking,
		Lovense,
		MarioJump,
		MarioPowerUp,
		MilestoneCompleted,
		Nice,
		Nope,
		Nut,
		OhMyGod,
		Pan,
		Pop,
		Quack,
		Rizz,
		RubberDucky,
		SadTrombone,
		Scored,
		Shocking,
		Startle,
		StreamDroppedIn,
		Subscribed,
		TacoBell,
		Tip,
		Uwu,
		Whip,
		Wow,
		Yay,
	}

	public override void _ExitTree()
	{
		this.m_shutdown = true;
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
		this.StartRandomizedSoundEffects();
	}
	
	private const int                                              c_timeDelayForAutoTriggerInMillisecondsMinimum = 20000;
	private const int                                              c_timeDelayForAutoTriggerInMillisecondsMaximum = 300000;
	
	private static readonly IReadOnlyList<SoundAlertType>          s_soundAlertsAvailableForRandomizedPlay        =
	[
		SoundAlertType.Airhorn,
		SoundAlertType.Applause,
		SoundAlertType.Ass,
		SoundAlertType.BallsOfSteel,
		SoundAlertType.Blink,
		SoundAlertType.Boing,
		SoundAlertType.Bonk,
		SoundAlertType.Bruh,
		SoundAlertType.Buzzer,
		SoundAlertType.Censor,
		SoundAlertType.CriticalHit,
		SoundAlertType.Discord,
		SoundAlertType.DunDun,
		SoundAlertType.Fart,
		SoundAlertType.Godlike,
		SoundAlertType.GoldenPan,
		SoundAlertType.Grindr,
		SoundAlertType.Hammer,
		SoundAlertType.Heartbeats,
		SoundAlertType.HelloThere,
		SoundAlertType.HolyShit,
		SoundAlertType.InstantTransmission,
		SoundAlertType.Interesting,
		SoundAlertType.Jeopardy,
		SoundAlertType.Knocking,
		SoundAlertType.MarioJump,
		SoundAlertType.MarioPowerUp,
		SoundAlertType.Nice,
		SoundAlertType.Nut,
		SoundAlertType.OhMyGod,
		SoundAlertType.Pan,
		SoundAlertType.Pop,
		SoundAlertType.Quack,
		SoundAlertType.Rizz,
		SoundAlertType.RubberDucky,
		SoundAlertType.SadTrombone,
		SoundAlertType.Scored,
		SoundAlertType.Shocking,
		SoundAlertType.Startle,
		SoundAlertType.TacoBell,
		SoundAlertType.Uwu,
		SoundAlertType.Whip,
		SoundAlertType.Wow,
		SoundAlertType.Yay,
	];
	
    private readonly RandomNumberGenerator                         m_random                                       = new();
	private readonly Dictionary<SoundAlertType, AudioStreamPlayer> m_soundAlerts                                  = new();
    private bool                                                   m_shutdown                                     = false;
	
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
	
	private void StartRandomizedSoundEffects()
	{
		Task.Run(
			function: async () =>
			{
                var serviceJoystickBot                   = Services.GetService<ServiceJoystickBot>();
				var numberOfSoundAlertsForRandomizedPlay = ServiceGodotAudio.s_soundAlertsAvailableForRandomizedPlay.Count - 1;
				while (this.m_shutdown is false)
				{
					var timeDelayInMilliseconds = this.m_random.RandiRange(
						from: ServiceGodotAudio.c_timeDelayForAutoTriggerInMillisecondsMinimum,
						to:   ServiceGodotAudio.c_timeDelayForAutoTriggerInMillisecondsMaximum
					);
					await Task.Delay(
						millisecondsDelay: timeDelayInMilliseconds
					);
					
					var randomSoundIndex = this.m_random.RandiRange(
						from: 0,
						to:   numberOfSoundAlertsForRandomizedPlay
					);
					var soundAlertType   = ServiceGodotAudio.s_soundAlertsAvailableForRandomizedPlay[index: randomSoundIndex];
					this.PlaySoundAlert(
						soundAlertType: soundAlertType
					);
					serviceJoystickBot.SendChatMessageSilently(
						message: $"🔊 {EnumHelper.ToSpacedPascalCase(enumValue: soundAlertType)} played."
					);
				}
			}
		);
	}

	private static void PlayAudio(
		AudioStreamPlayer audioStreamPlayer
	)
	{
		audioStreamPlayer.Play();
	}
}