
using System.Collections.Generic;
using Godot;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Lovenses;

namespace Overlay.Core.Contents.UserEvents;

internal partial class UserPresenceEventsController() : 
	Node()
{
	public override void _Ready()
	{
		this.SubscribeToUserEvents();
	}
	
	private const double                   c_welcomeInVibrationTime = 3d;
	
    private readonly RandomNumberGenerator m_random                 = new();
    private readonly HashSet<string>       m_usersWelcomedIn        = [ "SmoothDagger" ];

	private void HandleWebSocketPayloadUserPresenceEventUserEntered(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		var username = payloadMessage.Text;
		
		if (
			this.m_usersWelcomedIn.Add(
				item: username
			) is false
		)
		{
			return;
		}

		var serviceGodots     = Services.Services.GetService<ServiceGodots>();
		var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();

		serviceGodotAudio.PlaySoundAlert(
			soundAlertType: ServiceGodotAudio.SoundAlertType.ChatNotification
		);
		
		var serviceLovense = Services.Services.GetService<ServiceLovense>();
		serviceLovense.Vibrate(
			intensity:     20,
			timeInSeconds: UserPresenceEventsController.c_welcomeInVibrationTime
		);
        
		string[] messages =
		[
			$"Welcome in, @{username}!",
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
	
	private void HandleWebSocketPayloadUserPresenceEventUserLeft(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		
	}

	private void SubscribeToUserEvents()
	{
		ServiceJoystickWebSocketPayloadUserPresenceEvents.UserEntered += HandleWebSocketPayloadUserPresenceEventUserEntered;
		ServiceJoystickWebSocketPayloadUserPresenceEvents.UserLeft    += HandleWebSocketPayloadUserPresenceEventUserLeft;
	}
}
