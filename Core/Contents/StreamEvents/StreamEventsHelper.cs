
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.StreamEvents;

internal static class StreamEventsHelper 
{
	internal static void PlaySoundAlert(
		ServiceGodotAudio.SoundAlertType soundAlertType
	)
	{
		var serviceGodots     = Services.Services.GetService<ServiceGodots>();
		var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
            
		serviceGodotAudio.PlaySoundAlert(
			soundAlertType: soundAlertType
		);
	}

	internal static void SendBotMessage(
		string message
	)
	{
		Task.Run(
			function: async () =>
			{
				await Task.Delay(
					millisecondsDelay: 40
				);
                
				var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
				serviceJoystickBot.SendChatMessageSilently(
					message: message
				);
			}
		);
	}

	internal static void SetMeTimerWithNotifications(
		float                             layoutMeDurationInSeconds,
		int                               startDelayInMilliseconds,
		int                               timerDelayInMilliseconds,
		string                            startMessage,
		string                            endMessage,
		SceneController.AttentionMode     attentionMode              = SceneController.AttentionMode.Normal,
		bool                              useAdditiveTime            = true,
		ServiceGodotAudio.SoundAlertType? soundAlertTypeStart        = null,
		ServiceGodotAudio.SoundAlertType? soundAlertTypeEnd          = null
	)
	{
		Task.Run(
			function: async () =>
			{
				var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
				var serviceGodots      = Services.Services.GetService<ServiceGodots>();
				var serviceGodotAudio  = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
				
				var sceneAttentionMode = SceneController.Instance.GetAttentionMode();
				if (sceneAttentionMode is SceneController.AttentionMode.Me)
				{
					var remainingNormalTimeInMilliseconds = SceneController.Instance.GetLayoutMeRemainingNormalDurationInMilliseconds();
					
					await Task.Delay(
						millisecondsDelay: startDelayInMilliseconds + remainingNormalTimeInMilliseconds
					);

					await StreamEventsHelper.WaitForLayoutMeNormalToFinish();
					
					SceneController.Instance.AddToLayoutMeRemainingNormalDurationInSeconds(
						seconds: layoutMeDurationInSeconds
					);
				
					if (startMessage != string.Empty)
					{
						serviceJoystickBot.SendChatMessageSilently(
							message: startMessage
						);
					}

					if (soundAlertTypeStart is not null)
					{
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: soundAlertTypeStart.Value
						);
					}
				}
				else if (useAdditiveTime is true)
				{
					var remainingMilliseconds = SceneController.Instance.GetLayoutMeRemainingDurationInMilliseconds();
					var delayInMilliseconds   = remainingMilliseconds > 0 ? remainingMilliseconds : startDelayInMilliseconds;
					
					await Task.Delay(
						millisecondsDelay: delayInMilliseconds
					);

					await StreamEventsHelper.WaitForLayoutMeToFinish();
				
					if (startMessage != string.Empty)
					{
						serviceJoystickBot.SendChatMessageSilently(
							message: startMessage
						);
					}
					
					if (soundAlertTypeStart is not null)
					{
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: soundAlertTypeStart.Value
						);
					}
					
					SceneController.Instance.SetAttentionModeAndLayoutToMeForDuration(
						attentionMode:     attentionMode,
						durationInSeconds: layoutMeDurationInSeconds
					);
				}
				else
				{
					await Task.Delay(
						millisecondsDelay: startDelayInMilliseconds
					);

					if (startMessage != string.Empty)
					{
						serviceJoystickBot.SendChatMessageSilently(
							message: startMessage
						);
					}
					
					if (soundAlertTypeStart is not null)
					{
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: soundAlertTypeStart.Value
						);
					}
					
					SceneController.Instance.SetAttentionModeAndLayoutToMeForDuration(
						attentionMode:     attentionMode,
						durationInSeconds: layoutMeDurationInSeconds
					);
				}

				await Task.Delay(
					millisecondsDelay: timerDelayInMilliseconds
				);
				
				if (endMessage != string.Empty)
				{
					serviceJoystickBot.SendChatMessageSilently(
						message: endMessage
					);
				}
				
				if (soundAlertTypeEnd is not null)
				{
					serviceGodotAudio.PlaySoundAlert(
						soundAlertType: soundAlertTypeEnd.Value
					);
				}
			}
		);
	}

	private static async Task WaitForLayoutMeToFinish()
	{
		var remainingMilliseconds = SceneController.Instance.GetLayoutMeRemainingDurationInMilliseconds();
		while (remainingMilliseconds > 0)
		{
			await Task.Delay(
				millisecondsDelay: remainingMilliseconds
			);

			remainingMilliseconds = SceneController.Instance.GetLayoutMeRemainingDurationInMilliseconds();
		}
	}
	
	private static async Task WaitForLayoutMeNormalToFinish()
	{
		var remainingMilliseconds = SceneController.Instance.GetLayoutMeRemainingNormalDurationInMilliseconds();
		while (remainingMilliseconds > 0)
		{
			await Task.Delay(
				millisecondsDelay: remainingMilliseconds
			);

			remainingMilliseconds = SceneController.Instance.GetLayoutMeRemainingNormalDurationInMilliseconds();
		}
	}
}