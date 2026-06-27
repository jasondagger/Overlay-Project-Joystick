
using Godot;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.TextToSpeeches;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Lovenses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.UserEvents;

internal partial class UserPresenceEventsController() : 
	Node()
{
	public override void _EnterTree()
	{
		this.SubscribeToUserEvents();
	}
	
	public override void _Ready()
	{
		this.SetInstance();
		UserPresenceEventsController.RequestUserAvatarSettings(
			username: UserPresenceEventsController.c_streamerUsername
		);
	}
	
	internal static UserPresenceEventsController Instance { get; private set;  } = null;

	internal static Action<string, string>       SteamUserAdded                  = null;
	internal static Action<string>               SteamUserRemoved                = null;

	internal HashSet<string> GetUsersInChat()
	{
		lock (this.m_lock)
		{
			return this.m_usersInChat;
		}
	}
	
	private const int                                            c_minuteWatchedIncrease                      = 1;
	private const string                                         c_streamerUsername                           = "SmoothDagger";
	private const int                                            c_viewCountIncrease                          = 1;
	private const double                                         c_welcomeInVibrationTime                     = 3d;
	
    private readonly object                                      m_lock                                       = new();
    private readonly RandomNumberGenerator                       m_random                                     = new();
    private readonly HashSet<string>                             m_usersWelcomedIn                            = [ UserPresenceEventsController.c_streamerUsername ];
    private readonly HashSet<string>                             m_usersInChat                                = [ ];
    private readonly Dictionary<string, CancellationTokenSource> m_userMinutesWatchedCancellationTokenSources = new();
    
    private static void ExecuteDeposit(
	    string targetUser
    )
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
			    value:         targetUser
		    ),
		    new(
			    parameterName: nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes), 
			    value:         1
		    )
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
				    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.DepositTimeForBankUser, 
				    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }

    private static void ExecuteUpdateAchievementUserLastViewDate(
		string username
    )
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username), 
			    value:         username
		    ),
		    new(
			    parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Last_View_Date), 
			    value:         DateTime.UtcNow
		    ),
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
				    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateAchievementUserLastViewDate, 
				    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }
    
    private static void OnAchievementUserMinutesWatchedRetrieved(
	    ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
	    var result = serviceDatabaseTaskRetrievedAchievementUser.Result;
	    
	    var username        = result.AchievementUser_Username;
	    var progressCurrent = result.AchievementUser_Minutes_Watched;

	    ServiceAchievement.UpdateUserTitleTrackProgress(
		    username:                     username,
		    serviceAchievementTitleTrack: ServiceAchievementTitleTrack.MinutesWatched,
		    progressCurrent:              progressCurrent,
		    progressIncrease:             UserPresenceEventsController.c_minuteWatchedIncrease
	    );
    }
	
    private static void OnAchievementUserTimesViewedRetrieved(
	    ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
	)
    {
	    var result = serviceDatabaseTaskRetrievedAchievementUser.Result;
	    
	    var username        = result.AchievementUser_Username;
	    var progressCurrent = result.AchievementUser_Times_Viewed;

	    ServiceAchievement.UpdateUserTitleTrackProgress(
		    username:                     username,
		    serviceAchievementTitleTrack: ServiceAchievementTitleTrack.TimesViewed,
		    progressCurrent:              progressCurrent,
		    progressIncrease:             UserPresenceEventsController.c_viewCountIncrease
		);
    }

    private static void OnUserAvatarSettingsRetrieved(
	    ServiceDatabaseTaskRetrievedUserAvatarSettings serviceDatabaseTaskRetrievedUserAvatarSettings 
	)
    {
	    var result   = serviceDatabaseTaskRetrievedUserAvatarSettings.Result;
	    var username = result.UserAvatarSettings_Username;
	    
	    var shaderColors  = new Dictionary<EffectBackgroundAvatarShaderSlot, ServiceColorInterpolatorColorMode>()
	    {
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot0,
			    (ServiceColorInterpolatorColorMode) result.UserAvatarSettings_Shader0_Color_Id
		    },
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot1,
			    (ServiceColorInterpolatorColorMode) result.UserAvatarSettings_Shader1_Color_Id
		    },
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot2,
			    (ServiceColorInterpolatorColorMode) result.UserAvatarSettings_Shader2_Color_Id
		    },
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot3,
			    (ServiceColorInterpolatorColorMode) result.UserAvatarSettings_Shader3_Color_Id
		    },
	    };
	    
	    var shaderEffects = new Dictionary<EffectBackgroundAvatarShaderSlot, EffectBackgroundAvatarShaderEffect>()
	    {
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot0,
			    (EffectBackgroundAvatarShaderEffect) result.UserAvatarSettings_Shader0_Effect_Id
		    },
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot1,
			    (EffectBackgroundAvatarShaderEffect) result.UserAvatarSettings_Shader1_Effect_Id
		    },
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot2,
			    (EffectBackgroundAvatarShaderEffect) result.UserAvatarSettings_Shader2_Effect_Id
		    },
		    {
			    EffectBackgroundAvatarShaderSlot.ShaderSlot3,
			    (EffectBackgroundAvatarShaderEffect) result.UserAvatarSettings_Shader3_Effect_Id
		    },
	    };

	    if (BackgroundAvatarsController.Instance is not null)
	    {
		    BackgroundAvatarsController.Instance.AddAvatar(
			    username:               username,
			    colorModeBase:          (ServiceColorInterpolatorColorMode) result.UserAvatarSettings_Base_Color_Id,
			    colorModeOutline:       (ServiceColorInterpolatorColorMode) result.UserAvatarSettings_Outline_Color_Id,
			    shaderEffectColorModes: shaderColors,
			    shaderEffects:          shaderEffects,
			    shaderModel:            (EffectBackgroundAvatarShaderModel) result.UserAvatarSettings_Model_Id
		    );
	    }
    }

    private static void RequestAchievementUserMinutesWatched(
	    string username
	)
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username), 
			    value:         username
		    ),
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
				    serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveAchievementUserMinutesWatched, 
				    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }
    
    private static void RequestAchievementUserTimesViewed(
	    string username
    )
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username), 
			    value:         username
		    ),
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
				    serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveAchievementUserTimesViewed, 
				    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }

    private static void RequestUserAvatarSettings(
	    string username
    )
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username), 
			    value:         username
		    ),
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
				    serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserAvatarSettings, 
				    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }
    
    private static void RequestUserEnteredSteamUsername(
	    string username
    )
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: $"{nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}", 
			    value:         username
		    ),
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
				    serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserEnteredSteamUsername, 
				    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }
    
    private static void RequestUserExitedSteamUsername(
	    string username
    )
    {
	    var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
		    new(
			    parameterName: $"{nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}", 
			    value:         username
		    ),
	    };
        
	    Task.Run(
		    function: async () =>
		    {
			    await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
				    serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserExitedSteamUsername, 
				    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
			    );
		    }
	    );
    }
    
    private static void SendDelayedBotMessage(
	    string message
    )
    {
	    Task.Run(
		    function: async () =>
		    {
			    await Task.Delay(
				    millisecondsDelay: 200
			    );
                
			    var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
			    serviceJoystickBot.SendChatMessageSilently(
				    message: message
			    );
		    }
	    );
    }

	private void HandleWebSocketPayloadUserPresenceEventUserEntered(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		var username = payloadMessage.Text;

		if (username is UserPresenceEventsController.c_streamerUsername)
		{
			return;
		}

		var cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken       = cancellationTokenSource.Token;
		lock (this.m_lock)
		{
			if (
				this.m_usersInChat.Add(
					item: username
				)
			)
			{
				this.m_userMinutesWatchedCancellationTokenSources.Add(
					key:   username,
					value: cancellationTokenSource
				);

				Task.Run(
					function:          async () =>
					{
						while (cancellationToken.IsCancellationRequested is false)
						{
							await Task.Delay(
								millisecondsDelay: 60000,
								cancellationToken: cancellationToken
							);

							if (cancellationToken.IsCancellationRequested)
							{
								return;
							}
						
							UserPresenceEventsController.RequestAchievementUserMinutesWatched(
								username: username
							);
						}
					},
					cancellationToken: cancellationToken
				);
			}
		}
		
		UserPresenceEventsController.RequestUserEnteredSteamUsername(
			username: username	
		);
		
		UserPresenceEventsController.RequestUserAvatarSettings(
			username: username
		);
		
		if (
			this.m_usersWelcomedIn.Add(
				item: username
			) is false
		)
		{
			return;
		}
		
		UserPresenceEventsController.RequestAchievementUserTimesViewed(
			username: username
		);
		
		StreamEventsHelper.PlaySoundAlert(
			soundAlertType: ServiceGodotAudio.SoundAlertType.ChatNotification
		);
		
		var serviceLovense = Services.Services.GetService<ServiceLovense>();
		serviceLovense.All(
			intensity:     20,
			timeInSeconds: UserPresenceEventsController.c_welcomeInVibrationTime,
			eventType:     ServiceLovense.EventType.None
		);

		var serviceJoystick = Services.Services.GetService<ServiceJoystick>();
		var isSubscriber    = serviceJoystick.IsSubscriber(
			username: username
		);
		if (isSubscriber is true)
		{
			string[] messages =
			[
				$"🌟 Welcome in, @{username}! You earned a free Gush Control Link minute & !claim command reward! Type !bank check to see how many minutes you have & !claim [ass/cock/nipples] for some spice! 🥵 Thanks for being a subscriber!",
			];
			var index = this.m_random.RandiRange(
				from: 0,
				to:   messages.Length - 1
			);
        
			ServiceJoystickWebSocketPayloadChatHandler.AddUserForClaimRewardRequests(
				username: username
			);
			UserPresenceEventsController.ExecuteDeposit(
				targetUser: username
			);
			UserPresenceEventsController.SendDelayedBotMessage(
				message: messages[index]
			);
		}
		else
		{
			string[] messages =
			[
				$"👋 Welcome in, @{username}!",
			];
			var index = this.m_random.RandiRange(
				from: 0,
				to:   messages.Length - 1
			);

			UserPresenceEventsController.SendDelayedBotMessage(
				message: messages[index]
			);
		}
		
		ServiceGodotTextToSpeech.SpeakWithInterrupt(
			message: $"Welcome in, {username}!"
		);
	}
	
	private void HandleWebSocketPayloadUserPresenceEventUserLeft(
		ServiceJoystickWebSocketPayloadMessage payloadMessage
	)
	{
		var username = payloadMessage.Text;
		
		UserPresenceEventsController.ExecuteUpdateAchievementUserLastViewDate(
			username: username
		);

		if (username is UserPresenceEventsController.c_streamerUsername)
		{
			return;
		}

		lock (this.m_lock)
		{
			this.m_usersInChat.Remove(
				item: username
			);

			if (
				this.m_userMinutesWatchedCancellationTokenSources.TryGetValue(
					key:   username, 
					value: out var cancellationTokenSource
				) is true
			)
			{
                cancellationTokenSource.Cancel();
			}
			this.m_userMinutesWatchedCancellationTokenSources.Remove(
				key: username
			);
			
			UserPresenceEventsController.SteamUserRemoved?.Invoke(
				obj: username
			);
		}

		if (BackgroundAvatarsController.Instance is not null)
		{
			BackgroundAvatarsController.Instance.RemoveAvatar(
				username: username
			);
		}
	}
	
	private void OnUserEnteredSteamUsernameRetrieved(
		ServiceDatabaseTaskRetrievedSteamUser serviceDatabaseTaskRetrievedSteamUser
	)
	{
		var result           = serviceDatabaseTaskRetrievedSteamUser.Result;
		var usernameJoystick = result.SteamUser_Joystick_Username;
		var usernameSteam    = result.SteamUser_Steam_Username;
		
		lock (this.m_lock)
		{
			if (
				this.m_usersInChat.Contains(
					item: usernameJoystick
				) is true && 
				usernameSteam != string.Empty
			)
			{
				UserPresenceEventsController.SteamUserAdded?.Invoke(
					arg1: usernameJoystick,
					arg2: usernameSteam
				);
			}
		}
	}
	
	private void SetInstance()
	{
		UserPresenceEventsController.Instance = this;
	}

	private void SubscribeToUserEvents()
	{
		ServiceJoystickWebSocketPayloadUserPresenceEvents.UserEntered    += this.HandleWebSocketPayloadUserPresenceEventUserEntered;
		ServiceJoystickWebSocketPayloadUserPresenceEvents.UserLeft       += this.HandleWebSocketPayloadUserPresenceEventUserLeft;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserMinutesWatched += UserPresenceEventsController.OnAchievementUserMinutesWatchedRetrieved;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserTimesViewed    += UserPresenceEventsController.OnAchievementUserTimesViewedRetrieved;
		ServiceDatabaseTaskEvents.RetrievedUserAvatarSettings            += UserPresenceEventsController.OnUserAvatarSettingsRetrieved;
		ServiceDatabaseTaskEvents.RetrievedUserEnteredSteamUsername      += this.OnUserEnteredSteamUsernameRetrieved;
	}
}