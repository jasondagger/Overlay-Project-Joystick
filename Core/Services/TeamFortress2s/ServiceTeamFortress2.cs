
using System;
using System.Collections.Generic;
using Overlay.Core.Contents;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Lovenses;
using Overlay.Core.Services.Spotifies;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Contents.UserEvents;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Joysticks.Payloads;
using WebSocketSharp;

namespace Overlay.Core.Services.TeamFortress2s;

internal sealed partial class ServiceTeamFortress2() : 
	IService
{
	Task IService.Setup()
	{
		return Task.CompletedTask;
	}
    
	Task IService.Start()
	{
		this.RetrieveResources();
		this.SubscribeToEvents();
		return Task.CompletedTask;
	}
    
	Task IService.Stop()
	{
		return Task.CompletedTask;
	}

	internal bool IsBhopping      => this.m_isBhopping is true;
	
	internal bool IsKillStreaking => this.m_killStreak >= 5;
	
	internal void Start()
	{
		this.m_cancellationTokenSourceLogFile.Cancel();
		this.m_cancellationTokenSourceLogFile = new CancellationTokenSource();
		var cancellationToken = this.m_cancellationTokenSourceLogFile.Token;
		
		ServiceTeamFortress2.SendDelayedBotMessage(
			message: $"⚔️ Team Fortress 2 mode started."
		);
		
		Task.Run(
			function:          async () =>
			{
				await using var stream = new FileStream(
					path:   ServiceTeamFortress2.c_consoleLogPath, 
					mode:   FileMode.Open, 
					access: FileAccess.Read, 
					share:  FileShare.ReadWrite
				);
				using var reader = new StreamReader(
					stream: stream
				);

				stream.Seek(
					offset: 0,
					origin: SeekOrigin.End
				);
				
				var serviceLovense = Services.GetService<ServiceLovense>();

				using var timer = new PeriodicTimer(
					period: TimeSpan.FromMilliseconds(
						value: c_consoleLogReadDelayInMilliseconds
					)
				);
				while (!cancellationToken.IsCancellationRequested) {
					await timer.WaitForNextTickAsync(
						cancellationToken: cancellationToken
					);
					var line = await reader.ReadLineAsync(
						cancellationToken: cancellationToken
					);
					switch (line)
					{
						case null:
							continue;
						case "[IdleSys] Idle kick in 5 seconds":
							_ = Task.Run(
								function:          async () =>
								{
									ServiceTeamFortress2BindHandler.Explode();
								
									await Task.Delay(
										millisecondsDelay: 200,
										cancellationToken: cancellationToken
									);
								
									ServiceTeamFortress2BindHandler.MoveForward();
									await Task.Delay(
										millisecondsDelay: 50,
										cancellationToken: cancellationToken
									);
								
									ServiceTeamFortress2BindHandler.MoveForward();
									await Task.Delay(
										millisecondsDelay: 50,
										cancellationToken: cancellationToken
									);
								
									ServiceTeamFortress2BindHandler.MoveForward();
									await Task.Delay(
										millisecondsDelay: 5000,
										cancellationToken: cancellationToken
									);
								
									ServiceTeamFortress2BindHandler.OpenEmoteMenu();
									await Task.Delay(
										millisecondsDelay: 5000,
										cancellationToken: cancellationToken
									);
								
									ServiceTeamFortress2BindHandler.SelectTaunt(
										index: 3
									);
								},
								cancellationToken: cancellationToken
							);
							continue;
					}

					var bhopCheckpointRegex = ServiceTeamFortress2.RegexBhopCheckpoint();
					if (
						bhopCheckpointRegex.IsMatch(
							input: line
						) is true
					)
					{
						ServiceTeamFortress2.LogBhopTimeToChat(
							bhopTime: line
						);
						
						var isBetterTime   = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						var timeInSeconds  = isBetterTime ? 10 : 5;

						var serviceGodots = Services.GetService<ServiceGodots>();
						var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceTeamFortress2BhopSoundAlertRandomizer.GetRandomCheckpointReachedSound()
						);
						
						this.StartBhopLightChange(
							timeInSeconds: timeInSeconds,
							lightSceneA:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName(),
							lightSceneB:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName()
						);
						serviceLovense.All(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: timeInSeconds,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}
					
					var bhopStageRegex = ServiceTeamFortress2.RegexBhopStage();
					if (
						bhopStageRegex.IsMatch(
							input: line
						) is true
					)
					{
						ServiceTeamFortress2.LogBhopTimeToChat(
							bhopTime: line
						);
						
						var isBetterTime      = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						var timeInSeconds     = isBetterTime ? 30 : 15;
						
						var serviceGodots     = Services.GetService<ServiceGodots>();
						var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceTeamFortress2BhopSoundAlertRandomizer.GetRandomStageReachedSound()
						);
						
						this.StartBhopLightChange(
							timeInSeconds: timeInSeconds,
							lightSceneA:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName(),
							lightSceneB:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName()
						);
						serviceLovense.All(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: timeInSeconds,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}

					var targetUsernameJoystick = string.Empty;
					var targetUsernameSteam    = string.Empty;
					lock (this.m_lock)
					{
						foreach (var (usernameJoystick, usernameSteam) in this.m_userSteamUsernames)
						{
							if (
								line.Contains(
									value: usernameSteam
								) is true
							)
							{
								targetUsernameJoystick = usernameJoystick;
								targetUsernameSteam	   = usernameSteam;
								break;
							}
						}
					}

					if (targetUsernameSteam != string.Empty)
					{
						if (
							line.Contains(
								value: "Finished the Map"
							) is true
						)
						{
							ServiceTeamFortress2.RequestAchievementUser(
								username:                     targetUsernameJoystick,
								serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCompletedBhopMaps
							);
							continue;
						}
						
						if (
							line.Contains(
								value: "Finished Bonus"
							) is true
						)
						{
							ServiceTeamFortress2.RequestAchievementUser(
								username:                     targetUsernameJoystick,
								serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCompletedBhopBonuses
							);
							continue;
						}


						if (
							line.Contains(
								value: $"{targetUsernameSteam} killed"
							) is true
						)
						{
							var match = ServiceTeamFortress2.s_achievementWeaponMap.FirstOrDefault(
								kvp => kvp.Key.Any(
									predicate: line.Contains
								)
							);
							if (match.Value != default)
							{
								ServiceTeamFortress2.RequestAchievementUser(
									username:                     targetUsernameJoystick, 
									serviceDatabaseTaskQueryType: match.Value
								);
							}
						}
					}

					if (
						line.Contains(
							value: "SmoothDagger"
						) is false
					)
					{
						continue;
					}
					
					if (
						line.Contains(
							value: "lovense"
						) is true
					)
					{
						serviceLovense.All(
							intensity:     20,
							timeInSeconds: 1,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "defended"
						) is true
					)
					{
						var serviceGodots     = Services.GetService<ServiceGodots>();
						var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceGodotAudio.SoundAlertType.Defended
						);
						
						this.StartBhopLightChange(
							timeInSeconds: 5,
							lightSceneA:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName(),
							lightSceneB:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName()
						);
						serviceLovense.All(
							intensity:     20,
							timeInSeconds: 5,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "captured"
						) is true
					)
					{
						var serviceGodots     = Services.GetService<ServiceGodots>();
						var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceGodotAudio.SoundAlertType.Captured
						);
						
						this.StartBhopLightChange(
							timeInSeconds: 5,
							lightSceneA:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName(),
							lightSceneB:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName()
						);
						serviceLovense.All(
							intensity:     20,
							timeInSeconds: 5,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "suicided."
						) is true
					)
					{
						const string killerName = "SmoothDagger";
						var timeInSeconds       = this.GetKillStreakVibrationLengthInSeconds();
						
						this.ResetKillStreak(
							killerName: killerName
						);

						this.StartBhopLightChange(
							timeInSeconds: timeInSeconds,
							lightSceneA:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName(),
							lightSceneB:   ServiceTeamFortress2LightRandomizer.GetRandomLightSceneName()
						);
						serviceLovense.All(
							intensity:     20,
							timeInSeconds: timeInSeconds,
							eventType:     ServiceLovense.EventType.TF2_Death
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "Finished the Map"
						) is true
					)
					{
						ServiceTeamFortress2.LogBhopTimeToChat(
							bhopTime: line
						);
						
						var isBetterTime  = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						var timeInSeconds = isBetterTime ? 60 : 30;
						
						var serviceGodots = Services.GetService<ServiceGodots>();
						var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceGodotAudio.SoundAlertType.BhopMapCompleted
						);
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceTeamFortress2BhopSoundAlertRandomizer.GetRandomFireworkSound()
						);
						
						var swap = Random.Shared.Next(
							maxValue: 2
						) == 0;
						this.StartBhopLightChange(
							timeInSeconds: timeInSeconds,
							lightSceneA:   swap ? ServiceGoveeSceneNames.TF2KillStreak20 : ServiceTeamFortress2LightRandomizer.GetRandomLightSceneNameWithGold(),
							lightSceneB:   swap ? ServiceTeamFortress2LightRandomizer.GetRandomLightSceneNameWithGold() : ServiceGoveeSceneNames.TF2KillStreak20
						);
						serviceLovense.All(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: timeInSeconds,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "Finished Bonus"
						) is true
					)
					{
						ServiceTeamFortress2.LogBhopTimeToChat(
							bhopTime: line
						);
						
						var isBetterTime  = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						var timeInSeconds = isBetterTime ? 30 : 15;
						
						var serviceGodots = Services.GetService<ServiceGodots>();
						var serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceGodotAudio.SoundAlertType.BhopBonusCompleted
						);
						serviceGodotAudio.PlaySoundAlert(
							soundAlertType: ServiceTeamFortress2BhopSoundAlertRandomizer.GetRandomFireworkSound()
						);
						
						var swap = Random.Shared.Next(
							maxValue: 2
						) == 0;
						this.StartBhopLightChange(
							timeInSeconds: timeInSeconds,
							lightSceneA:   swap ? ServiceGoveeSceneNames.TF2KillStreak20 : ServiceTeamFortress2LightRandomizer.GetRandomLightSceneNameWithGold(),
							lightSceneB:   swap ? ServiceTeamFortress2LightRandomizer.GetRandomLightSceneNameWithGold() : ServiceGoveeSceneNames.TF2KillStreak20
						);
						serviceLovense.All(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: timeInSeconds,
							eventType:     ServiceLovense.EventType.None
						);
						continue;
					}
					
					var isCritical = line.Contains(
						value: "(crit)"
					);
					
					if (
						line.Contains(
							value: "SmoothDagger killed"
						) is true
					)
					{
						this.IncreaseKillStreak();
						this.LogKillStreakToChat();

						var eventType = ServiceTeamFortress2.GetEventTypeFromLogLine(
							line: line
						);
						
						serviceLovense.All(
							intensity:     isCritical ? 20 : 15, 
							timeInSeconds: this.GetKillStreakVibrationLengthInSeconds(),
							eventType:     eventType
						);

						if (targetUsernameJoystick != string.Empty)
						{
							ServiceTeamFortress2.RequestAchievementUser(
								username:                     targetUsernameJoystick,
								serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerDeaths
							);
						
							this.m_userDeathStreaks[key: targetUsernameJoystick]++;
							if (this.m_userDeathStreaks[key: targetUsernameJoystick] is 3)
							{
								ServiceTeamFortress2.RequestAchievementUser(
									username:                     targetUsernameJoystick,
									serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerDominatedBys
								);
							}
						
							this.m_userKillStreaks[key: targetUsernameJoystick] = 0;
						}
					}
					else if (
						line.Contains(
							value: "killed SmoothDagger"
						) is true
					)
					{
						var regex      = ServiceTeamFortress2.RegexKiller();
						var parts      = regex.Split(
							input: line,
							count: 2
						);
						var killerName = parts[0];

						var timeInSeconds = this.GetKillStreakVibrationLengthInSeconds();
						
						this.ResetKillStreak(
							killerName: killerName
						);
						
						serviceLovense.All(
							intensity:     isCritical ? 10 : 5, 
							timeInSeconds: timeInSeconds,
							eventType:     ServiceLovense.EventType.TF2_Death
						);

						if (targetUsernameJoystick != string.Empty)
						{
							ServiceTeamFortress2.RequestAchievementUser(
								username:                     targetUsernameJoystick,
								serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerKills
							);
						
							if (this.m_userDeathStreaks[key: targetUsernameJoystick] >= 3)
							{
								ServiceTeamFortress2.RequestAchievementUser(
									username:                     targetUsernameJoystick,
									serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerRevenges
								);
							}
							this.m_userDeathStreaks[key: targetUsernameJoystick] = 0;
						
							this.m_userKillStreaks[key: targetUsernameJoystick]++;
							if (this.m_userKillStreaks[key: targetUsernameJoystick] is 3)
							{
								ServiceTeamFortress2.RequestAchievementUser(
									username:                     targetUsernameJoystick,
									serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerDominations
								);
							}
						}
					}
				}
			},
			cancellationToken: cancellationToken
		);
	}

	internal void Stop()
	{
		ServiceTeamFortress2.SendDelayedBotMessage(
			message: $"⚔️ Team Fortress 2 mode stopped."
		);
		
		this.m_cancellationTokenSourceLogFile.Cancel();
	}

	private const string                                                       c_consoleLogPath                    = "/home/smoothdagger/snap/steam/common/.local/share/Steam/steamapps/common/Team Fortress 2/tf/console.log";
	private const int                                                          c_consoleLogReadDelayInMilliseconds = 100;
	
	private static readonly Dictionary<string[], ServiceDatabaseTaskQueryType> s_achievementWeaponMap              = new()
	{
		{
			[
				"tf_projectile_pipe", 
				"tf_projectile_pipe_remote", 
				"bottle"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedDemomans
		},
		{
			[
				"shotgun_primary", 
				"pistol", 
				"wrench", 
				"obj_sentrygun"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedEngineers
		},
		{
			[
				"minigun", 
				"shotgun_hwg", 
				"fists"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedHeavies
		},
		{ 
			[
				"syringegun_medic", 
				"bonesaw"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedMedics 
		},
		{
			[
				"flamethrower", 
				"shotgun_pyro", 
				"fireaxe"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedPyros
		},
		{
			[
				"scattergun", 
				"pistol_scout", 
				"bat"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedScouts
		},
		{
			[
				"sniperrifle", 
				"smg", 
				"club"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedSnipers
		},
		{
			[
				"tf_projectile_rocket", 
				"shotgun_soldier", 
				"shovel"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedSoldiers
		},
		{
			[
				"knife", 
				"revolver"
			], 
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedSpies
		},
	};
	
	[GeneratedRegex(
		pattern: @"Finished Bonus \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex RegexBhopBonus();
	
	[GeneratedRegex(
		pattern: @"Checkpoint \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex RegexBhopCheckpoint();
	
	[GeneratedRegex(
		pattern: @"Finished the Map\s+([\d:.]+)(?=\s+Rec\.)"
	)]
	private static partial Regex RegexBhopFinished();
	
	[GeneratedRegex(
		pattern: @"(?<=Per\.\s+)([+-][\d:.]+)\b"
	)]
	private static partial Regex RegexBhopPersonal();
	
	[GeneratedRegex(
		pattern: @"Finished Stage \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex RegexBhopStage();
	
	[GeneratedRegex(
		pattern: @" killed "
	)]
	private static partial Regex RegexKiller();
	
	[GeneratedRegex(
		pattern: @"with\s+([\w_]+)\.?\s*(?:\(crit\))?\s*$"
	)]
	private static partial Regex RegexWeapon();
	
	private CancellationTokenSource             m_cancellationTokenSourceLogFile    = new();
	private CancellationTokenSource             m_cancellationTokenSourceBhopLights = new();
	private bool                                m_isBhopping                        = false;
	private int                                 m_killStreak                        = 0;
	private readonly object                     m_lock                              = new();
	private ServiceGodotAudio                   m_serviceGodotAudio                 = null;
	private readonly Dictionary<string, string> m_userSteamUsernames                = new();
	private readonly Dictionary<string, int>    m_userKillStreaks                   = new();
	private readonly Dictionary<string, int>    m_userDeathStreaks                  = new();
	
	private static ServiceLovense.EventType GetEventTypeFromLogLine(
		string line
	)
	{
		var weaponRegex = ServiceTeamFortress2.RegexWeapon();
		var weaponMatch = weaponRegex.Match(
			input: line
		);
		
		var isWeapon = weaponMatch.Success;
		if (isWeapon is false)
		{
			return ServiceLovense.EventType.None;
		}
		
		var weapon = weaponMatch.Groups[1].Value;
		return weapon switch
		{
			"big_earner"                => ServiceLovense.EventType.TF2_BigEarner,
			"diamondback"               => ServiceLovense.EventType.TF2_DiamondBack,
			"fryingpan"                 => ServiceLovense.EventType.TF2_Pan,
			"player"                    => ServiceLovense.EventType.TF2_Death,
			"tf_projectile_pipe"        => ServiceLovense.EventType.TF2_GrenadeLauncher,
			"tf_projectile_pipe_remote" => ServiceLovense.EventType.TF2_StickyLauncher,
			"world"                     => ServiceLovense.EventType.TF2_Death,
			_                           => ServiceLovense.EventType.None
		};
	}
	
	private static bool IsBetterTime(
		string line
	)
	{
		return line.Contains(
			value: '-'
		) is true || 
		line.Contains(
			value: "Per."
		) is false;
	}

	private static void LogBhopTimeToChat(
		string bhopTime
	)
	{
		var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();

		var bhopPersonalRegex = ServiceTeamFortress2.RegexBhopPersonal();
		var bhopPersonalMatch = bhopPersonalRegex.Match(
			input: bhopTime
		);
		var bhopPersonalTimeExists = bhopPersonalMatch.Success;
		var bhopPersonalTime = $"{(bhopPersonalTimeExists ? $" PR: {bhopPersonalMatch.Value}" : string.Empty)}";
		
		var bhopCheckpointRegex = ServiceTeamFortress2.RegexBhopCheckpoint();
		var bhopCheckpointMatch = bhopCheckpointRegex.Match(
			input: bhopTime
		);
		
		var isCheckpoint = bhopCheckpointMatch.Success;
		if (isCheckpoint is true)
		{
			ServiceTeamFortress2.QueueSkipTrack();
			serviceJoystickBot.SendChatMessageSilently(
				message: $"🏁 SmoothDagger Reached {bhopCheckpointMatch.Groups[0].Value} in {bhopCheckpointMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopStageRegex = ServiceTeamFortress2.RegexBhopStage();
		var bhopStageMatch = bhopStageRegex.Match(
			input: bhopTime
		);
		
		var isStage = bhopStageMatch.Success;
		if (isStage is true)
		{
			ServiceTeamFortress2.QueueSkipTrack();
			serviceJoystickBot.SendChatMessageSilently(
				message: $"🏁 SmoothDagger {bhopStageMatch.Groups[0].Value} in {bhopStageMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopFinishedRegex = ServiceTeamFortress2.RegexBhopFinished();
		var bhopFinishedMatch = bhopFinishedRegex.Match(
			input: bhopTime
		);
		
		var isFinished = bhopFinishedMatch.Success;
		if (isFinished is true)
		{
			ServiceTeamFortress2.QueueKillStreakTrack(
				searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak25()
			);
			serviceJoystickBot.SendChatMessageSilently(
				message: $"🏁 SmoothDagger Finished the Map in {bhopFinishedMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopBonusRegex = ServiceTeamFortress2.RegexBhopBonus();
		var bhopBonusMatch = bhopBonusRegex.Match(
			input: bhopTime
		);
		
		var isBonus = bhopBonusMatch.Success;
		if (isBonus is true)
		{
			ServiceTeamFortress2.QueueKillStreakTrack(
				searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak25()
			);
			serviceJoystickBot.SendChatMessageSilently(
				message: $"🏁 SmoothDagger {bhopBonusMatch.Groups[0].Value} in {bhopBonusMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
		}
	}
	
	private static void QueueKillStreakTrack(
		string searchParameters
	)
	{
		var serviceSpotify = Services.GetService<ServiceSpotify>();
		serviceSpotify.RequestTrackQueueBySearchTermsWithNoNotification(
			searchParameters: searchParameters
		);
	}
	
	private static void QueueSkipTrack()
	{
		var serviceSpotify = Services.GetService<ServiceSpotify>();
		serviceSpotify.RequestSkipToNextTrackWithNoNotification();
	}
	
	private static void RequestAchievementUser(
		string                       username,
		ServiceDatabaseTaskQueryType serviceDatabaseTaskQueryType
	)
	{
		var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
			new(
				parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username), 
				value:         username
			)
		};
        
		ServiceDatabase.ExecuteTaskQuery(
			serviceDatabaseTaskQueryType:        serviceDatabaseTaskQueryType, 
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
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
                
				var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
				serviceJoystickBot.SendChatMessageSilently(
					message: message
				);
			}
		);
	}
	
	private static void SetGoveeLightColors(
		string lightSceneA,
		string lightSceneB
	)
	{
		GoveeLightControllerCeiling.Instance.SetLightScene(
			sceneName: lightSceneA
		);
		GoveeLightControllerStanding.Instance.SetLightScene(
			sceneName: lightSceneB
		);
	}

	private int GetKillStreakVibrationLengthInSeconds()
	{
		return this.m_killStreak switch
		{
			>= 5  and < 10 => 2,
			>= 10 and < 15 => 3,
			>= 15 and < 20 => 4,
			>= 20 and < 25 => 5,
			>= 25          => 6,
			_              => 1
		};
	}

	private void IncreaseKillStreak()
	{
		this.m_killStreak++;	
	}

	private void LogKillStreakToChat()
	{
		var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
		
		switch (this.m_killStreak)
		{
			case 5:
				serviceJoystickBot.SendChatMessageSilently(
					message: $"⚔️ SmoothDagger is on a KILLING SPREE 5"
				);
				ServiceTeamFortress2.QueueKillStreakTrack(
					searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak5()
				);
				ServiceTeamFortress2.SetGoveeLightColors(
					lightSceneA: ServiceGoveeSceneNames.TF2KillStreak5,
					lightSceneB: ServiceGoveeSceneNames.TF2KillStreak5
				);
				break;
			
			case 10:
				serviceJoystickBot.SendChatMessageSilently(
					message: $"⚔️ SmoothDagger is UNSTOPPABLE 10"
				);
				ServiceTeamFortress2.QueueKillStreakTrack(
					searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak10()
				);
				ServiceTeamFortress2.SetGoveeLightColors(
					lightSceneA: ServiceGoveeSceneNames.TF2KillStreak10,
					lightSceneB: ServiceGoveeSceneNames.TF2KillStreak10
				);
				break;
			
			case 15:
				serviceJoystickBot.SendChatMessageSilently(
					message: $"⚔️ SmoothDagger is on a RAMPAGE 15"
				);
				ServiceTeamFortress2.QueueKillStreakTrack(
					searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak15()
				);
				ServiceTeamFortress2.SetGoveeLightColors(
					lightSceneA: ServiceGoveeSceneNames.TF2KillStreak15,
					lightSceneB: ServiceGoveeSceneNames.TF2KillStreak15
				);
				break;
			
			case 20:
				serviceJoystickBot.SendChatMessageSilently(
					message: $"⚔️ SmoothDagger is GOD-LIKE 20"
				);
				ServiceTeamFortress2.QueueKillStreakTrack(
					searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak20()
				);
				ServiceTeamFortress2.SetGoveeLightColors(
					lightSceneA: ServiceGoveeSceneNames.TF2KillStreak20,
					lightSceneB: ServiceGoveeSceneNames.TF2KillStreak20
				);
				break;
			
			case >= 25:
				if (this.m_killStreak % 5 == 0)
				{
					serviceJoystickBot.SendChatMessageSilently(
						message: $"⚔️ SmoothDagger is still GOD-LIKE {this.m_killStreak}"
					);
					ServiceTeamFortress2.QueueKillStreakTrack(
						searchParameters: ServiceTeamFortress2KillStreakTracks.GetRandomTrackKillStreak25()
					);
					
					var swap = Random.Shared.Next(
						maxValue: 2
					) == 0;
					ServiceTeamFortress2.SetGoveeLightColors(
						lightSceneA: swap ? ServiceGoveeSceneNames.TF2KillStreak20 : ServiceTeamFortress2LightRandomizer.GetRandomLightSceneNameWithGold(),
						lightSceneB: swap ? ServiceTeamFortress2LightRandomizer.GetRandomLightSceneNameWithGold() : ServiceGoveeSceneNames.TF2KillStreak20
					);
				}
				break;
			
			default:
				return;
		}
	}

	private void OnSteamUserAddedOrUpdated(
		string usernameJoystick,
		string usernameSteam
	)
	{
		lock (this.m_lock)
		{
			this.m_userSteamUsernames[key: usernameJoystick] =  usernameSteam;
			this.m_userDeathStreaks[key: usernameJoystick]   = 0;
			this.m_userKillStreaks[key: usernameJoystick]    = 0;
		}
	}

	private void OnSteamUserRemoved(
		string usernameJoystick
	)
	{
		lock (this.m_lock)
		{
			this.m_userSteamUsernames.Remove(
				key: usernameJoystick
			);
			this.m_userDeathStreaks.Remove(
				key: usernameJoystick
			);
			this.m_userKillStreaks.Remove(
				key: usernameJoystick
			);
		}
	}
	
	private void ResetKillStreak(
		string killerName
	)
	{
		this.m_serviceGodotAudio.PlaySoundAlert(
			soundAlertType: ServiceGodotAudio.SoundAlertType.Death
		);
		
		if (this.m_killStreak >= 10)
		{
			var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
			serviceJoystickBot.SendChatMessageSilently(
				message: killerName == "SmoothDagger"
					? $"☠️ SmoothDagger ended their own KILLSTREAK {this.m_killStreak}"
					: $"☠️ {killerName} ended SmoothDagger's KILLSTREAK {this.m_killStreak}"
			);
		}
		
		this.m_killStreak = 0;
		
		this.m_cancellationTokenSourceBhopLights.Cancel();
		this.m_cancellationTokenSourceBhopLights = new CancellationTokenSource();
		
		GoveeLightControllers.Reset();
		
		this.m_isBhopping = false;
	}

	private void RetrieveResources()
	{
		var serviceGodots        = Services.GetService<ServiceGodots>();
		this.m_serviceGodotAudio = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
	}

	private void StartBhopLightChange(
		int    timeInSeconds,
		string lightSceneA,
		string lightSceneB
	)
	{
		this.m_isBhopping = false;
		
		this.m_cancellationTokenSourceBhopLights.Cancel();
		this.m_cancellationTokenSourceBhopLights = new CancellationTokenSource();
		var cancellationTokenSource              = this.m_cancellationTokenSourceBhopLights;
		var cancellationToken                    = this.m_cancellationTokenSourceBhopLights.Token;

		Task.Run(
			function: async () =>
			{
				try
				{
					this.m_isBhopping = true;
					ServiceTeamFortress2.SetGoveeLightColors(
						lightSceneA: lightSceneA,
						lightSceneB: lightSceneB
					);
				
					var timeInMilliseconds = timeInSeconds * 1000;
					await Task.Delay(
						millisecondsDelay: timeInMilliseconds,
						cancellationToken: cancellationToken
					);

					if (this.m_cancellationTokenSourceBhopLights == cancellationTokenSource)
					{
						GoveeLightControllers.Reset();
					}
				}
				finally
				{
					if (this.m_cancellationTokenSourceBhopLights == cancellationTokenSource)
					{
						this.m_isBhopping = false;
					}
				}
			},
			cancellationToken: cancellationToken
		);
	}

	private void SubscribeToEvents()
	{
		ServiceDatabaseTaskEvents.RetrievedAchievementUserCompletedBhopBonuses     += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserCompletedBhopBonuses;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserCompletedBhopMaps        += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserCompletedBhopMaps;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedDemomans           += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedDemomans;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedEngineers          += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedEngineers;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedHeavies            += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedHeavies;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedMedics             += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedMedics;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedPyros              += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedPyros;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedScouts             += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedScouts;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedSnipers            += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedSnipers;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedSoldiers           += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedSoldiers;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserPlayedSpies              += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserPlayedSpies;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerDeaths       += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserSmoothDaggerDeaths;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerDominatedBys += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserSmoothDaggerDominatedBys;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerDominations  += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserSmoothDaggerDominations;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerKills        += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserSmoothDaggerKills;
		ServiceDatabaseTaskEvents.RetrievedAchievementUserSmoothDaggerRevenges     += ServiceTeamFortress2AchievementHandler.OnRetrievedAchievementUserSmoothDaggerRevenges;
		
		UserPresenceEventsController.SteamUserAdded                                += this.OnSteamUserAddedOrUpdated;
		UserPresenceEventsController.SteamUserRemoved                              += this.OnSteamUserRemoved;
		ServiceJoystickWebSocketPayloadChatHandler.SteamUserUpdated                += this.OnSteamUserAddedOrUpdated;
	}
}
