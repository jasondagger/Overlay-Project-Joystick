
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Lovenses;

namespace Overlay.Core.Services.TeamFortress2s;

public sealed partial class ServiceTeamFortress2() : 
	IService
{
	Task IService.Setup()
	{
		return Task.CompletedTask;
	}
    
	Task IService.Start()
	{
		return Task.CompletedTask;
	}
    
	Task IService.Stop()
	{
		return Task.CompletedTask;
	}

	internal void StartReadingConsoleLog()
	{
		this.m_cancellationTokenSource.Cancel();
		this.m_cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken = this.m_cancellationTokenSource.Token;
		
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

				while (!cancellationToken.IsCancellationRequested) {
					var line = await reader.ReadLineAsync(
						cancellationToken: cancellationToken
					);
					if (line is null)
					{
						await Task.Delay(
							millisecondsDelay: ServiceTeamFortress2.c_consoleLogReadDelay, 
							cancellationToken: cancellationToken
						);
						continue;
					}
					
					var bhopCheckpointRegex = ServiceTeamFortress2.BhopRegexCheckpoint();
					if (
						bhopCheckpointRegex.IsMatch(
							input: line
						) is true
					)
					{
						ServiceTeamFortress2.LogBhopTimeToChat(
							bhopTime: line
						);
						
						var isBetterTime = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						serviceLovense.Vibrate(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: isBetterTime ? 10 : 5
						);
						continue;
					}
					
					var bhopStageRegex = ServiceTeamFortress2.BhopRegexStage();
					if (
						bhopStageRegex.IsMatch(
							input: line
						) is true
					)
					{
						ServiceTeamFortress2.LogBhopTimeToChat(
							bhopTime: line
						);
						
						var isBetterTime = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						serviceLovense.Vibrate(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: isBetterTime ? 30 : 15
						);
						continue;
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
						serviceLovense.Vibrate(
							intensity:     20,
							timeInSeconds: 1
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "defended"
						) is true ||
						line.Contains(
							value: "captured"
						) is true
					)
					{
						serviceLovense.Vibrate(
							intensity:     20,
							timeInSeconds: 5
						);
						continue;
					}
					
					if (
						line.Contains(
							value: "suicided."
						) is true
					)
					{
						this.ResetKillStreak();
						
						serviceLovense.Vibrate(
							intensity:     20,
							timeInSeconds: this.GetKillStreakVibrationLengthInSeconds()
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
						
						var isBetterTime = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						serviceLovense.Vibrate(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: isBetterTime ? 60 : 30
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
						
						var isBetterTime = ServiceTeamFortress2.IsBetterTime(
							line: line
						);
						serviceLovense.Vibrate(
							intensity:     isBetterTime ? 20 : 10,
							timeInSeconds: isBetterTime ? 30 : 15
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
						
						serviceLovense.Vibrate(
							intensity:     isCritical ? 20 : 15, 
							timeInSeconds: this.GetKillStreakVibrationLengthInSeconds()
						);
					}
					else if (
						line.Contains(
							value: "killed SmoothDagger"
						) is true
					)
					{
						this.ResetKillStreak();
						
						serviceLovense.Vibrate(
							intensity:     isCritical ? 10 : 5, 
							timeInSeconds: this.GetKillStreakVibrationLengthInSeconds()
						);
					}
				}
			},
			cancellationToken: cancellationToken
		);
	}

	internal void StopReadingConsoleLog()
	{
		this.m_cancellationTokenSource.Cancel();
	}

	private const string c_consoleLogPath      = "/home/smoothdagger/snap/steam/common/.local/share/Steam/steamapps/common/Team Fortress 2/tf/console.log";
	private const int    c_consoleLogReadDelay = 5;
	
	[GeneratedRegex(
		pattern: @"Finished Bonus \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex BhopRegexBonus();
	
	[GeneratedRegex(
		pattern: @"Checkpoint \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex BhopRegexCheckpoint();
	
	[GeneratedRegex(
		pattern: @"Finished the Map\s+([\d:.]+)(?=\s+Rec\.)"
	)]
	private static partial Regex BhopRegexFinished();
	
	[GeneratedRegex(
		pattern: @"(?<=Per\.\s+)([+-][\d:.]+)\b"
	)]
	private static partial Regex BhopRegexPersonal();
	
	[GeneratedRegex(
		pattern: @"Finished Stage \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex BhopRegexStage();
	
	private CancellationTokenSource m_cancellationTokenSource = new();
	private int                     m_killStreak              = 0;

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

		var bhopPersonalRegex = ServiceTeamFortress2.BhopRegexPersonal();
		var bhopPersonalMatch = bhopPersonalRegex.Match(
			input: bhopTime
		);
		var bhopPersonalTimeExists = bhopPersonalMatch.Success;
		var bhopPersonalTime = $"{(bhopPersonalTimeExists ? $" PR: {bhopPersonalMatch.Value}" : string.Empty)}";
		
		var bhopCheckpointRegex = ServiceTeamFortress2.BhopRegexCheckpoint();
		var bhopCheckpointMatch = bhopCheckpointRegex.Match(
			input: bhopTime
		);
		
		var isCheckpoint = bhopCheckpointMatch.Success;
		if (isCheckpoint is true)
		{
			serviceJoystickBot.SendChatMessage(
				message: $"SmoothDagger Reached {bhopCheckpointMatch.Groups[0].Value} in {bhopCheckpointMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopStageRegex = ServiceTeamFortress2.BhopRegexStage();
		var bhopStageMatch = bhopStageRegex.Match(
			input: bhopTime
		);
		
		var isStage = bhopStageMatch.Success;
		if (isStage is true)
		{
			serviceJoystickBot.SendChatMessage(
				message: $"SmoothDagger {bhopStageMatch.Groups[0].Value} in {bhopStageMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopFinishedRegex = ServiceTeamFortress2.BhopRegexFinished();
		var bhopFinishedMatch = bhopFinishedRegex.Match(
			input: bhopTime
		);
		
		var isFinished = bhopFinishedMatch.Success;
		if (isFinished is true)
		{
			serviceJoystickBot.SendChatMessage(
				message: $"SmoothDagger Finished the Map in {bhopFinishedMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopBonusRegex = ServiceTeamFortress2.BhopRegexBonus();
		var bhopBonusMatch = bhopBonusRegex.Match(
			input: bhopTime
		);
		
		var isBonus = bhopBonusMatch.Success;
		if (isBonus is true)
		{
			serviceJoystickBot.SendChatMessage(
				message: $"SmoothDagger {bhopBonusMatch.Groups[0].Value} in {bhopBonusMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
		}
	}

	private void LogKillStreakToChat()
	{
		var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
		switch (this.m_killStreak)
		{
			case 5:
				serviceJoystickBot.SendChatMessage(
					message: $"SmoothDagger is on a KILLING SPREE 5"
				);
				break;
			
			case 10:
				serviceJoystickBot.SendChatMessage(
					message: $"SmoothDagger is UNSTOPPABLE 10"
				);
				break;
			
			case 15:
				serviceJoystickBot.SendChatMessage(
					message: $"SmoothDagger is on a RAMPAGE 15"
				);
				break;
			
			case 20:
				serviceJoystickBot.SendChatMessage(
					message: $"SmoothDagger is GOD-LIKE 20"
				);
				break;
			
			case >= 25:
				if (this.m_killStreak % 5 == 0)
				{
					serviceJoystickBot.SendChatMessage(
						message: $"SmoothDagger is still GOD-LIKE {this.m_killStreak}"
					);
				}

				break;
			
			default:
				return;
		}
	}
	
	private void ResetKillStreak()
	{
		this.m_killStreak = 0;
	}
}
