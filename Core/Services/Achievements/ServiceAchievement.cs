
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Achievements;

internal sealed class ServiceAchievement() :
    IService
{
    Task IService.Setup()
    {
        ServiceAchievement.SubscribeToEvents();
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

    internal static string GetTitleNameFromAchievementTitle(
        ServiceAchievementTitle serviceAchievementTitle
    )
    {
        return serviceAchievementTitle switch
        {
            // Meta Achievements
            ServiceAchievementTitle.CheckedBank_1                       => $"Interesting",
            ServiceAchievementTitle.CheckedUnlocks_1                    => $"Unlocked",
            ServiceAchievementTitle.ClearedName_1                       => $"Unnamed",
            ServiceAchievementTitle.ClearedTitle_1                      => $"No Title",
            ServiceAchievementTitle.CompletedBhopBonus_1                => $"Bonus",
            ServiceAchievementTitle.CompletedBhopMap_1                  => $"Bunny Hopper",
            ServiceAchievementTitle.CustomizedAvatar_1                  => $"Custom Built",
            ServiceAchievementTitle.CustomizedBadge_1                   => $"Papers, Please",
            ServiceAchievementTitle.CustomizedName_1                    => $"I Have A Name",
            ServiceAchievementTitle.CustomizedTitle_1                   => $"Title & Registration",
            ServiceAchievementTitle.LinkedSteam_1                       => $"Steam Linked",
            ServiceAchievementTitle.MessagesSent_1                      => $"I Talked",
            ServiceAchievementTitle.MessagesSent_10                     => $"I Chatted",
            ServiceAchievementTitle.MessagesSent_100                    => $"I Expressed",
            ServiceAchievementTitle.MessagesSent_1000                   => $"I Voiced",
            ServiceAchievementTitle.MessagesSent_10000                  => $"I Proclaimed",
            ServiceAchievementTitle.MessagesSent_100000                 => $"I Talked... A Lot",
            ServiceAchievementTitle.MinutesWatched_1                    => $"The Spectator",
            ServiceAchievementTitle.MinutesWatched_10                   => $"The Observer",
            ServiceAchievementTitle.MinutesWatched_100                  => $"The Watcher",
            ServiceAchievementTitle.MinutesWatched_1000                 => $"The Regular",
            ServiceAchievementTitle.MinutesWatched_10000                => $"The Fixture",
            ServiceAchievementTitle.MinutesWatched_100000               => $"The Monument",
            ServiceAchievementTitle.PlayedDemoman_1                     => $"The Demoman",
            ServiceAchievementTitle.PlayedEngineer_1                    => $"The Engineer",
            ServiceAchievementTitle.PlayedHeavy_1                       => $"The Heavy",
            ServiceAchievementTitle.PlayedMedic_1                       => $"The Medic",
            ServiceAchievementTitle.PlayedPyro_1                        => $"The Pyro",
            ServiceAchievementTitle.PlayedScout_1                       => $"The Scout",
            ServiceAchievementTitle.PlayedSniper_1                      => $"The Sniper",
            ServiceAchievementTitle.PlayedSoldier_1                     => $"The Soldier",
            ServiceAchievementTitle.PlayedSpy_1                         => $"The Spy",
            ServiceAchievementTitle.PreviewedUnlocks_1                  => $"Eye Candy",
            ServiceAchievementTitle.QuestionsAsked_1                    => $"Just A Little Curious",
            ServiceAchievementTitle.QuestionsAsked_10                   => $"Asking The Tough Questions",
            ServiceAchievementTitle.QuestionsAsked_100                  => $"Questionable",
            ServiceAchievementTitle.QuestionsAsked_1000                 => $"I'm Done Asking",
            ServiceAchievementTitle.RockPaperScissorLose3InARow_1       => $"Unlucky",
            ServiceAchievementTitle.RockPaperScissorLosses_1            => $"I Lost",
            ServiceAchievementTitle.RockPaperScissorLosses_10           => $"Skill Issue",
            ServiceAchievementTitle.RockPaperScissorLosses_100          => $";_;",
            ServiceAchievementTitle.RockPaperScissorLosses_1000         => $"Loser - Beck",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1     => $"Challenger",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10    => $"Contender",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_100   => $"Experienced",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1000  => $"Veteran",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10000 => $"Traumatized",
            ServiceAchievementTitle.RockPaperScissorPapers_1            => $"Paper",
            ServiceAchievementTitle.RockPaperScissorPapers_10           => $"Flat Hand",
            ServiceAchievementTitle.RockPaperScissorPapers_100          => $"Loose Leaf",
            ServiceAchievementTitle.RockPaperScissorPapers_1000         => $"Hole Punched",
            ServiceAchievementTitle.RockPaperScissorRocks_1             => $"Rock",
            ServiceAchievementTitle.RockPaperScissorRocks_10            => $"Fisticuffs",
            ServiceAchievementTitle.RockPaperScissorRocks_100           => $"Rock Hard",
            ServiceAchievementTitle.RockPaperScissorRocks_1000          => $"Stoned",
            ServiceAchievementTitle.RockPaperScissorScissors_1          => $"Scissors",
            ServiceAchievementTitle.RockPaperScissorScissors_10         => $"A Cut Above",
            ServiceAchievementTitle.RockPaperScissorScissors_100        => $"Sliced",
            ServiceAchievementTitle.RockPaperScissorScissors_1000       => $"Straight Edge",
            ServiceAchievementTitle.RockPaperScissorTie3InARow_1        => $"Average",
            ServiceAchievementTitle.RockPaperScissorTies_1              => $"I Tied",
            ServiceAchievementTitle.RockPaperScissorTies_10             => $"Mirrored",
            ServiceAchievementTitle.RockPaperScissorTies_100            => $"All Tied Up",
            ServiceAchievementTitle.RockPaperScissorTies_1000           => $"Deadlock",
            ServiceAchievementTitle.RockPaperScissorWin3InARow_1        => $"Lucky",
            ServiceAchievementTitle.RockPaperScissorWins_1              => $"I Won",
            ServiceAchievementTitle.RockPaperScissorWins_10             => $"Hot Hands",
            ServiceAchievementTitle.RockPaperScissorWins_100            => $"^_^",
            ServiceAchievementTitle.RockPaperScissorWins_1000           => $"Professional",
            ServiceAchievementTitle.Rolled1_1                           => $"1",
            ServiceAchievementTitle.Rolled42_1                          => $"Answered",
            ServiceAchievementTitle.Rolled67_1                          => $"6-7",
            ServiceAchievementTitle.Rolled69_1                          => $"Nice",
            ServiceAchievementTitle.Rolled100_1                         => $"A+",
            ServiceAchievementTitle.Rolls_1                             => $"Roll'd",
            ServiceAchievementTitle.Rolls_10                            => $"Roller",
            ServiceAchievementTitle.Rolls_100                           => $"High Roller",
            ServiceAchievementTitle.Rolls_1000                          => $"Diced",
            ServiceAchievementTitle.Rolls_10000                         => $"Gambler",
            ServiceAchievementTitle.RollsMaximum_1                      => $"MAX",
            ServiceAchievementTitle.RollsMaximum_1000                   => $"MAXIMUM",
            ServiceAchievementTitle.RollsMinimum_1                      => $"min",
            ServiceAchievementTitle.RollsMinimum_1000                   => $"minimum",
            ServiceAchievementTitle.SmoothDaggerDeaths_1                => $"I Died To SmoothDagger",
            ServiceAchievementTitle.SmoothDaggerDeaths_10               => $"Uninstalling",
            ServiceAchievementTitle.SmoothDaggerDeaths_100              => $"I Quit",
            ServiceAchievementTitle.SmoothDaggerDeaths_1000             => $"TF2 Sucks",
            ServiceAchievementTitle.SmoothDaggerDominatedBys_1          => $"Owned",
            ServiceAchievementTitle.SmoothDaggerDominatedBys_10         => $"I'm Done",
            ServiceAchievementTitle.SmoothDaggerDominations_1           => $"DOMINATED",
            ServiceAchievementTitle.SmoothDaggerDominations_10          => $"lol",
            ServiceAchievementTitle.SmoothDaggerKills_1                 => $"First Of Many",
            ServiceAchievementTitle.SmoothDaggerKills_10                => $"Target Practice",
            ServiceAchievementTitle.SmoothDaggerKills_100               => $"Kingslayer",
            ServiceAchievementTitle.SmoothDaggerKills_1000              => $"I Killed SmoothDagger",
            ServiceAchievementTitle.SmoothDaggerRevenges_1              => $"A Dish Served Cold",
            ServiceAchievementTitle.SmoothDaggerRevenges_10             => $"REVENGE",
            ServiceAchievementTitle.TimesViewed_1                       => $"New Hire",
            ServiceAchievementTitle.TimesViewed_10                      => $"Enjoying The View",
            ServiceAchievementTitle.TimesViewed_100                     => $"Lurkin' Around",
            ServiceAchievementTitle.TimesViewed_1000                    => $"Gooner",
            ServiceAchievementTitle.TimesViewed_10000                   => $"Cannot Unsee",
            
            // Non-Meta Achievements
            ServiceAchievementTitle.Rolled240_1                         => $"240p",
            ServiceAchievementTitle.Rolled256_4                         => $"0xFFFFFFFF",
            ServiceAchievementTitle.Rolled420_1                         => $"#YOLOswag420",
            ServiceAchievementTitle.Rolled720_1                         => $"720p",
            ServiceAchievementTitle.Rolled1080_1                        => $"1080p",
            ServiceAchievementTitle.Rolled1337_1                        => $"I Speak L33T",
            ServiceAchievementTitle.Rolled3840_1                        => $"Ultra-Wide 4K",
            ServiceAchievementTitle.Rolled100000_1                      => $"0.0001%",
            ServiceAchievementTitle.TitlesUnlocked_1                    => $"I Did It!",
            ServiceAchievementTitle.TitlesUnlocked_10                   => $"Title Grinder",
            ServiceAchievementTitle.TitlesUnlocked_25                   => $"The Collector",
            ServiceAchievementTitle.TitlesUnlocked_50                   => $"Legacy Bearer",
            ServiceAchievementTitle.TitlesUnlocked_75                   => $"Basically A Pokemon Trainer",
            ServiceAchievementTitle.TitlesUnlocked_101                  => $"Got'em ALL",
            ServiceAchievementTitle.TracksCompleted_1                   => $"Tutorial Completed",
            ServiceAchievementTitle.TracksCompleted_5                   => $"On Track",
            ServiceAchievementTitle.TracksCompleted_10                  => $"Paving The Way",
            ServiceAchievementTitle.TracksCompleted_20                  => $"Trailblazer",
            ServiceAchievementTitle.TracksCompleted_30                  => $"Completionist",
            ServiceAchievementTitle.TracksCompleted_40                  => $"God Walking Amongst Mere Mortals",
            ServiceAchievementTitle.TracksCompleted_48                  => $"I Just Work Here",
            
            // Only Given Achievements
            ServiceAchievementTitle.Dev                                 => $"Dev",
            _                                                           => throw new NotImplementedException(),
        };
    }

    internal static void UpdateUserTitleTrackProgress(
        string                       username,
        ServiceAchievementTitleTrack serviceAchievementTitleTrack,
        int                          progressCurrent,
        int                          progressIncrease
    )
    {
        ServiceAchievement.CheckAndGrantMilestoneTitles(
            username:                     username,
            serviceAchievementTitleTrack: serviceAchievementTitleTrack,
            progressCurrent:              progressCurrent,
            progressIncrease:             progressIncrease
        );
        ServiceAchievement.ExecuteUpdateAchievementUserProgress(
            username:                     username,
            serviceAchievementTitleTrack: serviceAchievementTitleTrack,
            progressIncrease:             progressIncrease
        );
    }

    private static void CheckAndGrantMilestoneTitles(
        string                       username,
        ServiceAchievementTitleTrack serviceAchievementTitleTrack,
        int                          progressCurrent,
        int                          progressIncrease
    )
    {
        var progressNew = progressCurrent + progressIncrease;
        var thresholds  = ServiceAchievement.GetMilestoneThresholdsFromAchievementTitleTrack(
            serviceAchievementTitleTrack: serviceAchievementTitleTrack
        );

        foreach (var threshold in thresholds)
        {
            if (
                progressCurrent <  threshold && 
                progressNew     >= threshold
            )
            {
                var serviceAchievementTitle = ServiceAchievement.GetAchievementTitleFromAchievementTitleTrackAndThreshold(
                    serviceAchievementTitleTrack: serviceAchievementTitleTrack,
                    threshold:                    threshold
                );
                var titleName               = ServiceAchievement.GetTitleNameFromAchievementTitle(
                    serviceAchievementTitle: serviceAchievementTitle
                );
                
                ServiceAchievement.SendDelayedBotMessage(
                    message: $"🏆 @{username} unlocked the {titleName} title for {ServiceAchievement.GetAchievementDescriptionFromAchievementTitle(serviceAchievementTitle)}! Type !title {titleName} to set it now! Type !titles to see all of your titles!"
                );
                
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Achievement
                );

                if (
                    ServiceAchievement.IsMetaAchievement(
                        serviceAchievementTitleTrack: serviceAchievementTitleTrack
                    ) is true
                )
                {
                    ServiceAchievement.ExecuteUnlockAchievementTitleWithMeta(
                        username: username,
                        serviceAchievementTitle: serviceAchievementTitle
                    );

                    if (threshold == thresholds[^1])
                    {
                        ServiceAchievement.RetrieveAchievementUserTracksCompleted(
                            username: username
                        );
                    }
                }
                else
                {
                    ServiceAchievement.ExecuteUnlockAchievementTitleWithNoMeta(
                        username:                username,
                        serviceAchievementTitle: serviceAchievementTitle
                    );
                }
            }
        }
    }

    private static void ExecuteUnlockAchievementTitleWithMeta(
        string                  username,
        ServiceAchievementTitle serviceAchievementTitle
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username),
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id),
                value:         (int) serviceAchievementTitle
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UnlockUserTitleWithMeta, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void ExecuteUnlockAchievementTitleWithNoMeta(
        string                  username,
        ServiceAchievementTitle serviceAchievementTitle
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username),
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id),
                value:         (int) serviceAchievementTitle
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UnlockUserTitleWithNoMeta, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void ExecuteUpdateAchievementUserProgress(
        string                       username,
        ServiceAchievementTitleTrack serviceAchievementTitleTrack,
        int                          progressIncrease
    )
    {
        var achievementName                 = ServiceAchievement.GetDatabaseColumnNameFromAchievementTitleTrack(
            serviceAchievementTitleTrack: serviceAchievementTitleTrack
        );
        var serviceDatabaseTaskNonQueryType = ServiceAchievement.GetDatabaseQueryTypeFromAchievementTitleTrack(
            serviceAchievementTitleTrack: serviceAchievementTitleTrack
        );
        
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username), 
                value:         username
            ),
            new(
                parameterName: achievementName, 
                value:         progressIncrease
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  serviceDatabaseTaskNonQueryType, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private static ServiceAchievementTitle GetAchievementTitleFromAchievementTitleTrackAndThreshold(
        ServiceAchievementTitleTrack serviceAchievementTitleTrack,
        int                          threshold
    )
    {
        return (serviceAchievementTitleTrack, threshold) switch
        {
            // Meta Achievements
            ( ServiceAchievementTitleTrack.CheckedBank,                   1      ) => ServiceAchievementTitle.CheckedBank_1,
            ( ServiceAchievementTitleTrack.CheckedUnlocks,                1      ) => ServiceAchievementTitle.CheckedUnlocks_1,
            ( ServiceAchievementTitleTrack.ClearedName,                   1      ) => ServiceAchievementTitle.ClearedName_1,
            ( ServiceAchievementTitleTrack.ClearedTitle,                  1      ) => ServiceAchievementTitle.ClearedTitle_1,
            ( ServiceAchievementTitleTrack.CompletedBhopBonus,            1      ) => ServiceAchievementTitle.CompletedBhopBonus_1,
            ( ServiceAchievementTitleTrack.CompletedBhopMap,              1      ) => ServiceAchievementTitle.CompletedBhopMap_1,
            ( ServiceAchievementTitleTrack.CustomizedAvatar,              1      ) => ServiceAchievementTitle.CustomizedAvatar_1,
            ( ServiceAchievementTitleTrack.CustomizedBadge,               1      ) => ServiceAchievementTitle.CustomizedBadge_1,
            ( ServiceAchievementTitleTrack.CustomizedName,                1      ) => ServiceAchievementTitle.CustomizedName_1,
            ( ServiceAchievementTitleTrack.CustomizedTitle,               1      ) => ServiceAchievementTitle.CustomizedTitle_1,
            ( ServiceAchievementTitleTrack.LinkedSteam,                   1      ) => ServiceAchievementTitle.LinkedSteam_1,
            ( ServiceAchievementTitleTrack.MessagesSent,                  1      ) => ServiceAchievementTitle.MessagesSent_1,
            ( ServiceAchievementTitleTrack.MessagesSent,                  10     ) => ServiceAchievementTitle.MessagesSent_10,
            ( ServiceAchievementTitleTrack.MessagesSent,                  100    ) => ServiceAchievementTitle.MessagesSent_100,
            ( ServiceAchievementTitleTrack.MessagesSent,                  1000   ) => ServiceAchievementTitle.MessagesSent_1000,
            ( ServiceAchievementTitleTrack.MessagesSent,                  10000  ) => ServiceAchievementTitle.MessagesSent_10000,
            ( ServiceAchievementTitleTrack.MessagesSent,                  100000 ) => ServiceAchievementTitle.MessagesSent_100000,
            ( ServiceAchievementTitleTrack.MinutesWatched,                1      ) => ServiceAchievementTitle.MinutesWatched_1,
            ( ServiceAchievementTitleTrack.MinutesWatched,                10     ) => ServiceAchievementTitle.MinutesWatched_10,
            ( ServiceAchievementTitleTrack.MinutesWatched,                100    ) => ServiceAchievementTitle.MinutesWatched_100,
            ( ServiceAchievementTitleTrack.MinutesWatched,                1000   ) => ServiceAchievementTitle.MinutesWatched_1000,
            ( ServiceAchievementTitleTrack.MinutesWatched,                10000  ) => ServiceAchievementTitle.MinutesWatched_10000,
            ( ServiceAchievementTitleTrack.MinutesWatched,                100000 ) => ServiceAchievementTitle.MinutesWatched_100000,
            ( ServiceAchievementTitleTrack.PlayedDemoman,                 1      ) => ServiceAchievementTitle.PlayedDemoman_1,
            ( ServiceAchievementTitleTrack.PlayedEngineer,                1      ) => ServiceAchievementTitle.PlayedEngineer_1,
            ( ServiceAchievementTitleTrack.PlayedHeavy,                   1      ) => ServiceAchievementTitle.PlayedHeavy_1,
            ( ServiceAchievementTitleTrack.PlayedMedic,                   1      ) => ServiceAchievementTitle.PlayedMedic_1,
            ( ServiceAchievementTitleTrack.PlayedPyro,                    1      ) => ServiceAchievementTitle.PlayedPyro_1,
            ( ServiceAchievementTitleTrack.PlayedScout,                   1      ) => ServiceAchievementTitle.PlayedScout_1,
            ( ServiceAchievementTitleTrack.PlayedSniper,                  1      ) => ServiceAchievementTitle.PlayedSniper_1,
            ( ServiceAchievementTitleTrack.PlayedSoldier,                 1      ) => ServiceAchievementTitle.PlayedSoldier_1,
            ( ServiceAchievementTitleTrack.PlayedSpy,                     1      ) => ServiceAchievementTitle.PlayedSpy_1,
            ( ServiceAchievementTitleTrack.PreviewedUnlocks,              1      ) => ServiceAchievementTitle.PreviewedUnlocks_1,
            ( ServiceAchievementTitleTrack.QuestionsAsked,                1      ) => ServiceAchievementTitle.QuestionsAsked_1,
            ( ServiceAchievementTitleTrack.QuestionsAsked,                10     ) => ServiceAchievementTitle.QuestionsAsked_10,
            ( ServiceAchievementTitleTrack.QuestionsAsked,                100    ) => ServiceAchievementTitle.QuestionsAsked_100,
            ( ServiceAchievementTitleTrack.QuestionsAsked,                1000   ) => ServiceAchievementTitle.QuestionsAsked_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorLose3InARow,   1      ) => ServiceAchievementTitle.RockPaperScissorLose3InARow_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorLosses,        1      ) => ServiceAchievementTitle.RockPaperScissorLosses_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorLosses,        10     ) => ServiceAchievementTitle.RockPaperScissorLosses_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorLosses,        100    ) => ServiceAchievementTitle.RockPaperScissorLosses_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorLosses,        1000   ) => ServiceAchievementTitle.RockPaperScissorLosses_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed, 1      ) => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed, 10     ) => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed, 100    ) => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed, 1000   ) => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed, 10000  ) => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10000,
            ( ServiceAchievementTitleTrack.RockPaperScissorPapers,        1      ) => ServiceAchievementTitle.RockPaperScissorPapers_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorPapers,        10     ) => ServiceAchievementTitle.RockPaperScissorPapers_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorPapers,        100    ) => ServiceAchievementTitle.RockPaperScissorPapers_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorPapers,        1000   ) => ServiceAchievementTitle.RockPaperScissorPapers_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorRocks,         1      ) => ServiceAchievementTitle.RockPaperScissorRocks_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorRocks,         10     ) => ServiceAchievementTitle.RockPaperScissorRocks_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorRocks,         100    ) => ServiceAchievementTitle.RockPaperScissorRocks_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorRocks,         1000   ) => ServiceAchievementTitle.RockPaperScissorRocks_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorScissors,      1      ) => ServiceAchievementTitle.RockPaperScissorScissors_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorScissors,      10     ) => ServiceAchievementTitle.RockPaperScissorScissors_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorScissors,      100    ) => ServiceAchievementTitle.RockPaperScissorScissors_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorScissors,      1000   ) => ServiceAchievementTitle.RockPaperScissorScissors_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorTie3InARow,    1      ) => ServiceAchievementTitle.RockPaperScissorTie3InARow_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorTies,          1      ) => ServiceAchievementTitle.RockPaperScissorTies_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorTies,          10     ) => ServiceAchievementTitle.RockPaperScissorTies_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorTies,          100    ) => ServiceAchievementTitle.RockPaperScissorTies_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorTies,          1000   ) => ServiceAchievementTitle.RockPaperScissorTies_1000,
            ( ServiceAchievementTitleTrack.RockPaperScissorWin3InARow,    1      ) => ServiceAchievementTitle.RockPaperScissorWin3InARow_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorWins,          1      ) => ServiceAchievementTitle.RockPaperScissorWins_1,
            ( ServiceAchievementTitleTrack.RockPaperScissorWins,          10     ) => ServiceAchievementTitle.RockPaperScissorWins_10,
            ( ServiceAchievementTitleTrack.RockPaperScissorWins,          100    ) => ServiceAchievementTitle.RockPaperScissorWins_100,
            ( ServiceAchievementTitleTrack.RockPaperScissorWins,          1000   ) => ServiceAchievementTitle.RockPaperScissorWins_1000,
            ( ServiceAchievementTitleTrack.Rolled1,                       1      ) => ServiceAchievementTitle.Rolled1_1,
            ( ServiceAchievementTitleTrack.Rolled42,                      1      ) => ServiceAchievementTitle.Rolled42_1,
            ( ServiceAchievementTitleTrack.Rolled67,                      1      ) => ServiceAchievementTitle.Rolled67_1,
            ( ServiceAchievementTitleTrack.Rolled69,                      1      ) => ServiceAchievementTitle.Rolled69_1,
            ( ServiceAchievementTitleTrack.Rolled100,                     1      ) => ServiceAchievementTitle.Rolled100_1,
            ( ServiceAchievementTitleTrack.Rolls,                         1      ) => ServiceAchievementTitle.Rolls_1,
            ( ServiceAchievementTitleTrack.Rolls,                         10     ) => ServiceAchievementTitle.Rolls_10,
            ( ServiceAchievementTitleTrack.Rolls,                         100    ) => ServiceAchievementTitle.Rolls_100,
            ( ServiceAchievementTitleTrack.Rolls,                         1000   ) => ServiceAchievementTitle.Rolls_1000,
            ( ServiceAchievementTitleTrack.Rolls,                         10000  ) => ServiceAchievementTitle.Rolls_10000,
            ( ServiceAchievementTitleTrack.RollsMaximum,                  1      ) => ServiceAchievementTitle.RollsMaximum_1,
            ( ServiceAchievementTitleTrack.RollsMaximum,                  1000   ) => ServiceAchievementTitle.RollsMaximum_1000,
            ( ServiceAchievementTitleTrack.RollsMinimum,                  1      ) => ServiceAchievementTitle.RollsMinimum_1,
            ( ServiceAchievementTitleTrack.RollsMinimum,                  1000   ) => ServiceAchievementTitle.RollsMinimum_1000,
            ( ServiceAchievementTitleTrack.SmoothDaggerDeaths,            1      ) => ServiceAchievementTitle.SmoothDaggerDeaths_1,
            ( ServiceAchievementTitleTrack.SmoothDaggerDeaths,            10     ) => ServiceAchievementTitle.SmoothDaggerDeaths_10,
            ( ServiceAchievementTitleTrack.SmoothDaggerDeaths,            100    ) => ServiceAchievementTitle.SmoothDaggerDeaths_100,
            ( ServiceAchievementTitleTrack.SmoothDaggerDeaths,            1000   ) => ServiceAchievementTitle.SmoothDaggerDeaths_1000,
            ( ServiceAchievementTitleTrack.SmoothDaggerDominatedBys,      1      ) => ServiceAchievementTitle.SmoothDaggerDominatedBys_1,
            ( ServiceAchievementTitleTrack.SmoothDaggerDominatedBys,      10     ) => ServiceAchievementTitle.SmoothDaggerDominatedBys_10,
            ( ServiceAchievementTitleTrack.SmoothDaggerDominations,       1      ) => ServiceAchievementTitle.SmoothDaggerDominations_1,
            ( ServiceAchievementTitleTrack.SmoothDaggerDominations,       10     ) => ServiceAchievementTitle.SmoothDaggerDominations_10,
            ( ServiceAchievementTitleTrack.SmoothDaggerKills,             1      ) => ServiceAchievementTitle.SmoothDaggerKills_1,
            ( ServiceAchievementTitleTrack.SmoothDaggerKills,             10     ) => ServiceAchievementTitle.SmoothDaggerKills_10,
            ( ServiceAchievementTitleTrack.SmoothDaggerKills,             100    ) => ServiceAchievementTitle.SmoothDaggerKills_100,
            ( ServiceAchievementTitleTrack.SmoothDaggerKills,             1000   ) => ServiceAchievementTitle.SmoothDaggerKills_1000,
            ( ServiceAchievementTitleTrack.SmoothDaggerRevenges,          1      ) => ServiceAchievementTitle.SmoothDaggerRevenges_1,
            ( ServiceAchievementTitleTrack.SmoothDaggerRevenges,          10     ) => ServiceAchievementTitle.SmoothDaggerRevenges_10,
            ( ServiceAchievementTitleTrack.TimesViewed,                   1      ) => ServiceAchievementTitle.TimesViewed_1,
            ( ServiceAchievementTitleTrack.TimesViewed,                   10     ) => ServiceAchievementTitle.TimesViewed_10,
            ( ServiceAchievementTitleTrack.TimesViewed,                   100    ) => ServiceAchievementTitle.TimesViewed_100,
            ( ServiceAchievementTitleTrack.TimesViewed,                   1000   ) => ServiceAchievementTitle.TimesViewed_1000,
            ( ServiceAchievementTitleTrack.TimesViewed,                   10000  ) => ServiceAchievementTitle.TimesViewed_10000,
            
            // Non-Meta Achievements
            ( ServiceAchievementTitleTrack.Rolled240,                     1      ) => ServiceAchievementTitle.Rolled240_1,
            ( ServiceAchievementTitleTrack.Rolled256,                     4      ) => ServiceAchievementTitle.Rolled256_4,
            ( ServiceAchievementTitleTrack.Rolled420,                     1      ) => ServiceAchievementTitle.Rolled420_1,
            ( ServiceAchievementTitleTrack.Rolled720,                     1      ) => ServiceAchievementTitle.Rolled720_1,
            ( ServiceAchievementTitleTrack.Rolled1080,                    1      ) => ServiceAchievementTitle.Rolled1080_1,
            ( ServiceAchievementTitleTrack.Rolled1337,                    1      ) => ServiceAchievementTitle.Rolled1337_1,
            ( ServiceAchievementTitleTrack.Rolled3840,                    1      ) => ServiceAchievementTitle.Rolled3840_1,
            ( ServiceAchievementTitleTrack.Rolled100000,                  1      ) => ServiceAchievementTitle.Rolled100000_1,
            ( ServiceAchievementTitleTrack.TitlesUnlocked,                1      ) => ServiceAchievementTitle.TitlesUnlocked_1,
            ( ServiceAchievementTitleTrack.TitlesUnlocked,                10     ) => ServiceAchievementTitle.TitlesUnlocked_10,
            ( ServiceAchievementTitleTrack.TitlesUnlocked,                25     ) => ServiceAchievementTitle.TitlesUnlocked_25,
            ( ServiceAchievementTitleTrack.TitlesUnlocked,                50     ) => ServiceAchievementTitle.TitlesUnlocked_50,
            ( ServiceAchievementTitleTrack.TitlesUnlocked,                75     ) => ServiceAchievementTitle.TitlesUnlocked_75,
            ( ServiceAchievementTitleTrack.TitlesUnlocked,                101    ) => ServiceAchievementTitle.TitlesUnlocked_101,
            ( ServiceAchievementTitleTrack.TracksCompleted,               1      ) => ServiceAchievementTitle.TracksCompleted_1,
            ( ServiceAchievementTitleTrack.TracksCompleted,               5      ) => ServiceAchievementTitle.TracksCompleted_5,
            ( ServiceAchievementTitleTrack.TracksCompleted,               10     ) => ServiceAchievementTitle.TracksCompleted_10,
            ( ServiceAchievementTitleTrack.TracksCompleted,               20     ) => ServiceAchievementTitle.TracksCompleted_20,
            ( ServiceAchievementTitleTrack.TracksCompleted,               30     ) => ServiceAchievementTitle.TracksCompleted_30,
            ( ServiceAchievementTitleTrack.TracksCompleted,               40     ) => ServiceAchievementTitle.TracksCompleted_40,
            ( ServiceAchievementTitleTrack.TracksCompleted,               48     ) => ServiceAchievementTitle.TracksCompleted_48,
            _                                                                      => throw new NotImplementedException(),
        };
    }

    private static string GetAchievementDescriptionFromAchievementTitle(
        ServiceAchievementTitle serviceAchievementTitle
    )
    {
        return serviceAchievementTitle switch
        {
            // Meta Achievements
            ServiceAchievementTitle.CheckedBank_1                       => $"checking your bank",
            ServiceAchievementTitle.CheckedUnlocks_1                    => $"checking your unlocks",
            ServiceAchievementTitle.ClearedName_1                       => $"clearing your name",
            ServiceAchievementTitle.ClearedTitle_1                      => $"clearing your title",
            ServiceAchievementTitle.CompletedBhopMap_1                  => $"completing a bunny hop map",
            ServiceAchievementTitle.CompletedBhopBonus_1                => $"completing a bunny hop bonus",
            ServiceAchievementTitle.CustomizedAvatar_1                  => $"customizing your avatar",
            ServiceAchievementTitle.CustomizedBadge_1                   => $"customizing your badge color",
            ServiceAchievementTitle.CustomizedName_1                    => $"customizing your name color",
            ServiceAchievementTitle.CustomizedTitle_1                   => $"customizing your title",
            ServiceAchievementTitle.LinkedSteam_1                       => $"linking your Steam username",
            ServiceAchievementTitle.MessagesSent_1                      => $"sending 1 message",
            ServiceAchievementTitle.MessagesSent_10                     => $"sending 10 messages",
            ServiceAchievementTitle.MessagesSent_100                    => $"sending 100 messages",
            ServiceAchievementTitle.MessagesSent_1000                   => $"sending 1000 messages",
            ServiceAchievementTitle.MessagesSent_10000                  => $"sending 10000 messages",
            ServiceAchievementTitle.MessagesSent_100000                 => $"sending 100000 messages",
            ServiceAchievementTitle.MinutesWatched_1                    => $"watching the stream for 1 minute",
            ServiceAchievementTitle.MinutesWatched_10                   => $"watching the stream for 10 minutes",
            ServiceAchievementTitle.MinutesWatched_100                  => $"watching the stream for 100 minutes",
            ServiceAchievementTitle.MinutesWatched_1000                 => $"watching the stream for 1000 minutes",
            ServiceAchievementTitle.MinutesWatched_10000                => $"watching the stream for 10000 minutes",
            ServiceAchievementTitle.MinutesWatched_100000               => $"watching the stream for 100000 minutes",
            ServiceAchievementTitle.PlayedDemoman_1                     => $"getting a kill with the Demoman's Stickybomb Launcher",
            ServiceAchievementTitle.PlayedEngineer_1                    => $"getting a kill with the Engineer's Wrench",
            ServiceAchievementTitle.PlayedHeavy_1                       => $"getting a kill with the Heavy's Minigun",
            ServiceAchievementTitle.PlayedMedic_1                       => $"getting a kill with the Medic's Syringe Gun",
            ServiceAchievementTitle.PlayedPyro_1                        => $"getting a kill with the Pyro's Flame Thrower",
            ServiceAchievementTitle.PlayedScout_1                       => $"getting a kill with the Scout's Scattergun",
            ServiceAchievementTitle.PlayedSniper_1                      => $"getting a kill with the Sniper's Sniper Rifle",
            ServiceAchievementTitle.PlayedSoldier_1                     => $"getting a kill with the Soldier's Rocket Launcher",
            ServiceAchievementTitle.PlayedSpy_1                         => $"getting a kill with the Spy's Knife",
            ServiceAchievementTitle.PreviewedUnlocks_1                  => $"previewing avatar unlocks",
            ServiceAchievementTitle.QuestionsAsked_1                    => $"asking SmoothBot 1 question",
            ServiceAchievementTitle.QuestionsAsked_10                   => $"asking SmoothBot 10 questions",
            ServiceAchievementTitle.QuestionsAsked_100                  => $"asking SmoothBot 100 questions",
            ServiceAchievementTitle.QuestionsAsked_1000                 => $"asking SmoothBot 1000 questions",
            ServiceAchievementTitle.RockPaperScissorLose3InARow_1       => $"losing rock paper scissor 3 times in a row",
            ServiceAchievementTitle.RockPaperScissorLosses_1            => $"losing rock paper scissor 1 time",
            ServiceAchievementTitle.RockPaperScissorLosses_10           => $"losing rock paper scissor 10 times",
            ServiceAchievementTitle.RockPaperScissorLosses_100          => $"losing rock paper scissor 100 times",
            ServiceAchievementTitle.RockPaperScissorLosses_1000         => $"losing rock paper scissor 1000 times",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1     => $"playing rock paper scissor 1 time",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10    => $"playing rock paper scissor 10 times",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_100   => $"playing rock paper scissor 100 times",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1000  => $"playing rock paper scissor 1000 times",
            ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10000 => $"playing rock paper scissor 10000 times",
            ServiceAchievementTitle.RockPaperScissorPapers_1            => $"using paper 1 time",
            ServiceAchievementTitle.RockPaperScissorPapers_10           => $"using paper 10 times",
            ServiceAchievementTitle.RockPaperScissorPapers_100          => $"using paper 100 times",
            ServiceAchievementTitle.RockPaperScissorPapers_1000         => $"using paper 1000 times",
            ServiceAchievementTitle.RockPaperScissorRocks_1             => $"using rock 1 time",
            ServiceAchievementTitle.RockPaperScissorRocks_10            => $"using rock 10 times",
            ServiceAchievementTitle.RockPaperScissorRocks_100           => $"using rock 100 times",
            ServiceAchievementTitle.RockPaperScissorRocks_1000          => $"using rock 1000 times",
            ServiceAchievementTitle.RockPaperScissorScissors_1          => $"using scissor 1 time",
            ServiceAchievementTitle.RockPaperScissorScissors_10         => $"using scissor 10 times",
            ServiceAchievementTitle.RockPaperScissorScissors_100        => $"using scissor 100 times",
            ServiceAchievementTitle.RockPaperScissorScissors_1000       => $"using scissor 1000 times",
            ServiceAchievementTitle.RockPaperScissorTie3InARow_1        => $"tying rock paper scissor 3 times in a row",
            ServiceAchievementTitle.RockPaperScissorTies_1              => $"tying rock paper scissor 1 time",
            ServiceAchievementTitle.RockPaperScissorTies_10             => $"tying rock paper scissor 10 times",
            ServiceAchievementTitle.RockPaperScissorTies_100            => $"tying rock paper scissor 100 times",
            ServiceAchievementTitle.RockPaperScissorTies_1000           => $"tying rock paper scissor 1000 times",
            ServiceAchievementTitle.RockPaperScissorWin3InARow_1        => $"winning rock paper scissor 3 times in a row",
            ServiceAchievementTitle.RockPaperScissorWins_1              => $"winning rock paper scissor 1 time",
            ServiceAchievementTitle.RockPaperScissorWins_10             => $"winning rock paper scissor 10 times",
            ServiceAchievementTitle.RockPaperScissorWins_100            => $"winning rock paper scissor 100 times",
            ServiceAchievementTitle.RockPaperScissorWins_1000           => $"winning rock paper scissor 1000 times",
            ServiceAchievementTitle.Rolled1_1                           => $"rolling a 1",
            ServiceAchievementTitle.Rolled42_1                          => $"rolling a 42",
            ServiceAchievementTitle.Rolled67_1                          => $"rolling a 67",
            ServiceAchievementTitle.Rolled69_1                          => $"rolling a 69",
            ServiceAchievementTitle.Rolled100_1                         => $"rolling a natural 100",
            ServiceAchievementTitle.Rolls_1                             => $"rolling the dice 1 time",
            ServiceAchievementTitle.Rolls_10                            => $"rolling the dice 10 times",
            ServiceAchievementTitle.Rolls_100                           => $"rolling the dice 100 times",
            ServiceAchievementTitle.Rolls_1000                          => $"rolling the dice 1000 times",
            ServiceAchievementTitle.Rolls_10000                         => $"rolling the dice 10000 times",
            ServiceAchievementTitle.RollsMaximum_1                      => $"rolling the maximum value 1 time",
            ServiceAchievementTitle.RollsMaximum_1000                   => $"rolling the maximum value 1000 times",
            ServiceAchievementTitle.RollsMinimum_1                      => $"rolling the minimum value 1 time",
            ServiceAchievementTitle.RollsMinimum_1000                   => $"rolling the minimum value 1000 times",
            ServiceAchievementTitle.SmoothDaggerDeaths_1                => $"dying to SmoothDagger 1 time",
            ServiceAchievementTitle.SmoothDaggerDeaths_10               => $"dying to SmoothDagger 10 times",
            ServiceAchievementTitle.SmoothDaggerDeaths_100              => $"dying to SmoothDagger 100 times",
            ServiceAchievementTitle.SmoothDaggerDeaths_1000             => $"dying to SmoothDagger 1000 times",
            ServiceAchievementTitle.SmoothDaggerDominatedBys_1          => $"being dominated by SmoothDagger 1 time",
            ServiceAchievementTitle.SmoothDaggerDominatedBys_10         => $"being dominated by SmoothDagger 10 times",
            ServiceAchievementTitle.SmoothDaggerDominations_1           => $"dominating SmoothDagger 1 time",
            ServiceAchievementTitle.SmoothDaggerDominations_10          => $"dominating SmoothDagger 10 times",
            ServiceAchievementTitle.SmoothDaggerKills_1                 => $"killing SmoothDagger 1 time",
            ServiceAchievementTitle.SmoothDaggerKills_10                => $"killing SmoothDagger 10 times",
            ServiceAchievementTitle.SmoothDaggerKills_100               => $"killing SmoothDagger 100 times",
            ServiceAchievementTitle.SmoothDaggerKills_1000              => $"killing SmoothDagger 1000 times",
            ServiceAchievementTitle.SmoothDaggerRevenges_1              => $"getting revenge on SmoothDagger 1 time",
            ServiceAchievementTitle.SmoothDaggerRevenges_10             => $"getting revenge on SmoothDagger 10 times",
            ServiceAchievementTitle.TimesViewed_1                       => $"watching the stream 1 time",
            ServiceAchievementTitle.TimesViewed_10                      => $"watching the stream 10 times",
            ServiceAchievementTitle.TimesViewed_100                     => $"watching the stream 100 times",
            ServiceAchievementTitle.TimesViewed_1000                    => $"watching the stream 1000 times",
            ServiceAchievementTitle.TimesViewed_10000                   => $"watching the stream 10000 times",

            // Non-Meta Achievements
            ServiceAchievementTitle.Rolled240_1                         => $"rolling a 240",
            ServiceAchievementTitle.Rolled256_4                         => $"rolling a 256 4 times",
            ServiceAchievementTitle.Rolled420_1                         => $"rolling a 420",
            ServiceAchievementTitle.Rolled720_1                         => $"rolling a 720",
            ServiceAchievementTitle.Rolled1080_1                        => $"rolling a 1080",
            ServiceAchievementTitle.Rolled1337_1                        => $"rolling a 1337",
            ServiceAchievementTitle.Rolled3840_1                        => $"rolling a 3840",
            ServiceAchievementTitle.Rolled100000_1                      => $"rolling a 100000",
            ServiceAchievementTitle.TitlesUnlocked_1                    => $"unlocking 1 title",
            ServiceAchievementTitle.TitlesUnlocked_10                   => $"unlocking 10 titles",
            ServiceAchievementTitle.TitlesUnlocked_25                   => $"unlocking 25 titles",
            ServiceAchievementTitle.TitlesUnlocked_50                   => $"unlocking 50 titles",
            ServiceAchievementTitle.TitlesUnlocked_75                   => $"unlocking 75 titles",
            ServiceAchievementTitle.TitlesUnlocked_101                  => $"unlocking 101 titles",
            ServiceAchievementTitle.TracksCompleted_1                   => $"completing 1 title track",
            ServiceAchievementTitle.TracksCompleted_5                   => $"completing 5 title tracks",
            ServiceAchievementTitle.TracksCompleted_10                  => $"completing 10 title tracks",
            ServiceAchievementTitle.TracksCompleted_20                  => $"completing 20 title tracks",
            ServiceAchievementTitle.TracksCompleted_30                  => $"completing 30 title tracks",
            ServiceAchievementTitle.TracksCompleted_40                  => $"completing 40 title tracks",
            ServiceAchievementTitle.TracksCompleted_48                  => $"completing 48 title tracks",
            
            // Only Given Achievements
            ServiceAchievementTitle.Dev                                 => $"being a developer",
            
            _                                                           => throw new NotImplementedException(),
        };
    }

    private static string GetDatabaseColumnNameFromAchievementTitleTrack(
        ServiceAchievementTitleTrack serviceAchievementTitleTrack
    )
    {
        return serviceAchievementTitleTrack switch
        {
            // Meta Achievements
            ServiceAchievementTitleTrack.CheckedBank                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Checked_Banks),
            ServiceAchievementTitleTrack.CheckedUnlocks                => nameof(ServiceDatabaseAchievementUser.AchievementUser_Checked_Unlocks),
            ServiceAchievementTitleTrack.ClearedName                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Cleared_Names),
            ServiceAchievementTitleTrack.ClearedTitle                  => nameof(ServiceDatabaseAchievementUser.AchievementUser_Cleared_Titles),
            ServiceAchievementTitleTrack.CompletedBhopBonus            => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Completed_Bhop_Bonuses),
            ServiceAchievementTitleTrack.CompletedBhopMap              => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Completed_Bhop_Maps),
            ServiceAchievementTitleTrack.CustomizedAvatar              => nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Avatars),
            ServiceAchievementTitleTrack.CustomizedBadge               => nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Badges),
            ServiceAchievementTitleTrack.CustomizedName                => nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Names),
            ServiceAchievementTitleTrack.LinkedSteam                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Linked_Steams),
            ServiceAchievementTitleTrack.CustomizedTitle               => nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Titles),
            ServiceAchievementTitleTrack.MessagesSent                  => nameof(ServiceDatabaseAchievementUser.AchievementUser_Messages_Sent),
            ServiceAchievementTitleTrack.MinutesWatched                => nameof(ServiceDatabaseAchievementUser.AchievementUser_Minutes_Watched),
            ServiceAchievementTitleTrack.PreviewedUnlocks              => nameof(ServiceDatabaseAchievementUser.AchievementUser_Previewed_Unlocks),
            ServiceAchievementTitleTrack.QuestionsAsked                => nameof(ServiceDatabaseAchievementUser.AchievementUser_Questions_Asked),
            ServiceAchievementTitleTrack.RockPaperScissorLose3InARow   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Lose_3_In_A_Rows),
            ServiceAchievementTitleTrack.RockPaperScissorLosses        => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Losses),
            ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Matches_Played),
            ServiceAchievementTitleTrack.RockPaperScissorPapers        => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Papers),
            ServiceAchievementTitleTrack.RockPaperScissorRocks         => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Rocks),
            ServiceAchievementTitleTrack.RockPaperScissorScissors      => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Scissors),
            ServiceAchievementTitleTrack.RockPaperScissorTie3InARow    => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Tie_3_In_A_Rows),
            ServiceAchievementTitleTrack.RockPaperScissorTies          => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Ties),
            ServiceAchievementTitleTrack.RockPaperScissorWin3InARow    => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Win_3_In_A_Rows),
            ServiceAchievementTitleTrack.RockPaperScissorWins          => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Wins),
            ServiceAchievementTitleTrack.Rolled1                       => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_1),
            ServiceAchievementTitleTrack.Rolled42                      => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_42),
            ServiceAchievementTitleTrack.Rolled67                      => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_67),
            ServiceAchievementTitleTrack.Rolled69                      => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_69),
            ServiceAchievementTitleTrack.Rolled100                     => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_100),
            ServiceAchievementTitleTrack.Rolls                         => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolls),
            ServiceAchievementTitleTrack.RollsMaximum                  => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolls_Maximum),
            ServiceAchievementTitleTrack.RollsMinimum                  => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolls_Minimum),
            ServiceAchievementTitleTrack.PlayedDemoman                 => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Demomans),
            ServiceAchievementTitleTrack.PlayedEngineer                => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Engineers),
            ServiceAchievementTitleTrack.PlayedHeavy                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Heavies),
            ServiceAchievementTitleTrack.PlayedMedic                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Medics),
            ServiceAchievementTitleTrack.PlayedPyro                    => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Pyros),
            ServiceAchievementTitleTrack.PlayedScout                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Scouts),
            ServiceAchievementTitleTrack.PlayedSniper                  => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Snipers),
            ServiceAchievementTitleTrack.PlayedSoldier                 => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Soldiers),
            ServiceAchievementTitleTrack.PlayedSpy                     => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Spies),
            ServiceAchievementTitleTrack.SmoothDaggerDeaths            => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Deaths),
            ServiceAchievementTitleTrack.SmoothDaggerDominatedBys      => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Dominated_Bys),
            ServiceAchievementTitleTrack.SmoothDaggerDominations       => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Dominations),
            ServiceAchievementTitleTrack.SmoothDaggerKills             => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Kills),
            ServiceAchievementTitleTrack.SmoothDaggerRevenges          => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Revenges),
            ServiceAchievementTitleTrack.TimesViewed                   => nameof(ServiceDatabaseAchievementUser.AchievementUser_Times_Viewed),
            
            // Non-Meta Achievements
            ServiceAchievementTitleTrack.Rolled240                     => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_240),
            ServiceAchievementTitleTrack.Rolled256                     => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_256),
            ServiceAchievementTitleTrack.Rolled420                     => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_420),
            ServiceAchievementTitleTrack.Rolled720                     => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_720),
            ServiceAchievementTitleTrack.Rolled1080                    => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_1080),
            ServiceAchievementTitleTrack.Rolled1337                    => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_1337),
            ServiceAchievementTitleTrack.Rolled3840                    => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_3840),
            ServiceAchievementTitleTrack.Rolled100000                  => nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_100000),
            ServiceAchievementTitleTrack.TitlesUnlocked                => nameof(ServiceDatabaseAchievementUser.AchievementUser_Titles_Unlocked),
            ServiceAchievementTitleTrack.TracksCompleted               => nameof(ServiceDatabaseAchievementUser.AchievementUser_Tracks_Completed),
            _                                                          => throw new NotImplementedException(),
        };
    }
    
    private static ServiceDatabaseTaskNonQueryType GetDatabaseQueryTypeFromAchievementTitleTrack(
        ServiceAchievementTitleTrack serviceAchievementTitleTrack
    )
    {
        return serviceAchievementTitleTrack switch
        {
            // Meta Achievements
            ServiceAchievementTitleTrack.CheckedBank                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementCheckedBanks,
            ServiceAchievementTitleTrack.CheckedUnlocks                => ServiceDatabaseTaskNonQueryType.IncrementAchievementCheckedUnlocks,
            ServiceAchievementTitleTrack.ClearedName                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementClearedNames,
            ServiceAchievementTitleTrack.ClearedTitle                  => ServiceDatabaseTaskNonQueryType.IncrementAchievementClearedTitles,
            ServiceAchievementTitleTrack.CompletedBhopBonus            => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2CompletedBhopBonuses,
            ServiceAchievementTitleTrack.CompletedBhopMap              => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2CompletedBhopMaps,
            ServiceAchievementTitleTrack.CustomizedAvatar              => ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedAvatars,
            ServiceAchievementTitleTrack.CustomizedBadge               => ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedBadges,
            ServiceAchievementTitleTrack.CustomizedName                => ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedNames,
            ServiceAchievementTitleTrack.CustomizedTitle               => ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedTitles,
            ServiceAchievementTitleTrack.LinkedSteam                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementLinkedSteams,
            ServiceAchievementTitleTrack.MessagesSent                  => ServiceDatabaseTaskNonQueryType.IncrementAchievementMessagesSent,
            ServiceAchievementTitleTrack.MinutesWatched                => ServiceDatabaseTaskNonQueryType.IncrementAchievementMinutesWatched,
            ServiceAchievementTitleTrack.PreviewedUnlocks              => ServiceDatabaseTaskNonQueryType.IncrementAchievementPreviewedUnlocks,
            ServiceAchievementTitleTrack.QuestionsAsked                => ServiceDatabaseTaskNonQueryType.IncrementAchievementQuestionsAsked,
            ServiceAchievementTitleTrack.RockPaperScissorLose3InARow   => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorLose3InARows,
            ServiceAchievementTitleTrack.RockPaperScissorLosses        => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorLosses,
            ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorMatchesPlayed,
            ServiceAchievementTitleTrack.RockPaperScissorPapers        => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorPapers,
            ServiceAchievementTitleTrack.RockPaperScissorRocks         => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorRocks,
            ServiceAchievementTitleTrack.RockPaperScissorScissors      => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorScissors,
            ServiceAchievementTitleTrack.RockPaperScissorTie3InARow    => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorTie3InARows,
            ServiceAchievementTitleTrack.RockPaperScissorTies          => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorTies,
            ServiceAchievementTitleTrack.RockPaperScissorWin3InARow    => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorWin3InARows,
            ServiceAchievementTitleTrack.RockPaperScissorWins          => ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorWins,
            ServiceAchievementTitleTrack.Rolled1                       => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled1,
            ServiceAchievementTitleTrack.Rolled42                      => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled42,
            ServiceAchievementTitleTrack.Rolled67                      => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled67,
            ServiceAchievementTitleTrack.Rolled69                      => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled69,
            ServiceAchievementTitleTrack.Rolled100                     => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled100,
            ServiceAchievementTitleTrack.Rolls                         => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolls,
            ServiceAchievementTitleTrack.RollsMaximum                  => ServiceDatabaseTaskNonQueryType.IncrementAchievementRollsMaximum,
            ServiceAchievementTitleTrack.RollsMinimum                  => ServiceDatabaseTaskNonQueryType.IncrementAchievementRollsMinimum,
            ServiceAchievementTitleTrack.PlayedDemoman                 => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedDemomans,
            ServiceAchievementTitleTrack.PlayedEngineer                => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedEngineers,
            ServiceAchievementTitleTrack.PlayedHeavy                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedHeavies,
            ServiceAchievementTitleTrack.PlayedMedic                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedMedics,
            ServiceAchievementTitleTrack.PlayedPyro                    => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedPyros,
            ServiceAchievementTitleTrack.PlayedScout                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedScouts,
            ServiceAchievementTitleTrack.PlayedSniper                  => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedSnipers,
            ServiceAchievementTitleTrack.PlayedSoldier                 => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedSoldiers,
            ServiceAchievementTitleTrack.PlayedSpy                     => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedSpies,
            ServiceAchievementTitleTrack.SmoothDaggerDeaths            => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerDeaths,
            ServiceAchievementTitleTrack.SmoothDaggerDominatedBys      => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerDominatedBys,
            ServiceAchievementTitleTrack.SmoothDaggerDominations       => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerDominations,
            ServiceAchievementTitleTrack.SmoothDaggerKills             => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerKills,
            ServiceAchievementTitleTrack.SmoothDaggerRevenges          => ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerRevenges,
            ServiceAchievementTitleTrack.TimesViewed                   => ServiceDatabaseTaskNonQueryType.IncrementAchievementTimesViewed,
            
            // Non-Meta Achievements
            ServiceAchievementTitleTrack.Rolled240                     => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled240,
            ServiceAchievementTitleTrack.Rolled256                     => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled256,
            ServiceAchievementTitleTrack.Rolled420                     => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled420,
            ServiceAchievementTitleTrack.Rolled720                     => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled720,
            ServiceAchievementTitleTrack.Rolled1080                    => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled1080,
            ServiceAchievementTitleTrack.Rolled1337                    => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled1337,
            ServiceAchievementTitleTrack.Rolled3840                    => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled3840,
            ServiceAchievementTitleTrack.Rolled100000                  => ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled100000,
            ServiceAchievementTitleTrack.TitlesUnlocked                => ServiceDatabaseTaskNonQueryType.IncrementAchievementTitlesUnlocked,
            ServiceAchievementTitleTrack.TracksCompleted               => ServiceDatabaseTaskNonQueryType.IncrementAchievementTracksCompleted,
            _                                                          => throw new NotImplementedException(),
        };
    }

    private static int[] GetMilestoneThresholdsFromAchievementTitleTrack(
        ServiceAchievementTitleTrack serviceAchievementTitleTrack
    )
    {
        return serviceAchievementTitleTrack switch
        {
            // Meta Achievements
            ServiceAchievementTitleTrack.CheckedBank                   => [ 1 ],
            ServiceAchievementTitleTrack.CheckedUnlocks                => [ 1 ],
            ServiceAchievementTitleTrack.ClearedName                   => [ 1 ],
            ServiceAchievementTitleTrack.ClearedTitle                  => [ 1 ],
            ServiceAchievementTitleTrack.CompletedBhopBonus            => [ 1 ],
            ServiceAchievementTitleTrack.CompletedBhopMap              => [ 1 ],
            ServiceAchievementTitleTrack.CustomizedAvatar              => [ 1 ],
            ServiceAchievementTitleTrack.CustomizedBadge               => [ 1 ],
            ServiceAchievementTitleTrack.CustomizedName                => [ 1 ],
            ServiceAchievementTitleTrack.CustomizedTitle               => [ 1 ],
            ServiceAchievementTitleTrack.LinkedSteam                   => [ 1 ],
            ServiceAchievementTitleTrack.MessagesSent                  => [ 1, 10, 100, 1000, 10000, 100000 ],
            ServiceAchievementTitleTrack.MinutesWatched                => [ 1, 10, 100, 1000, 10000, 100000 ],
            ServiceAchievementTitleTrack.PlayedDemoman                 => [ 1 ],
            ServiceAchievementTitleTrack.PlayedEngineer                => [ 1 ],
            ServiceAchievementTitleTrack.PlayedHeavy                   => [ 1 ],
            ServiceAchievementTitleTrack.PlayedMedic                   => [ 1 ],
            ServiceAchievementTitleTrack.PlayedPyro                    => [ 1 ],
            ServiceAchievementTitleTrack.PlayedScout                   => [ 1 ],
            ServiceAchievementTitleTrack.PlayedSniper                  => [ 1 ],
            ServiceAchievementTitleTrack.PlayedSoldier                 => [ 1 ],
            ServiceAchievementTitleTrack.PlayedSpy                     => [ 1 ],
            ServiceAchievementTitleTrack.PreviewedUnlocks              => [ 1 ],
            ServiceAchievementTitleTrack.QuestionsAsked                => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.RockPaperScissorLose3InARow   => [ 1 ],
            ServiceAchievementTitleTrack.RockPaperScissorLosses        => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed => [ 1, 10, 100, 1000, 10000 ],
            ServiceAchievementTitleTrack.RockPaperScissorPapers        => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.RockPaperScissorRocks         => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.RockPaperScissorScissors      => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.RockPaperScissorTie3InARow    => [ 1 ],
            ServiceAchievementTitleTrack.RockPaperScissorTies          => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.RockPaperScissorWin3InARow    => [ 1 ],
            ServiceAchievementTitleTrack.RockPaperScissorWins          => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.Rolled1                       => [ 1 ],
            ServiceAchievementTitleTrack.Rolled42                      => [ 1 ],
            ServiceAchievementTitleTrack.Rolled67                      => [ 1 ],
            ServiceAchievementTitleTrack.Rolled69                      => [ 1 ],
            ServiceAchievementTitleTrack.Rolled100                     => [ 1 ],
            ServiceAchievementTitleTrack.Rolls                         => [ 1, 10, 100, 1000, 10000 ],
            ServiceAchievementTitleTrack.RollsMaximum                  => [ 1, 1000 ],
            ServiceAchievementTitleTrack.RollsMinimum                  => [ 1, 1000 ],
            ServiceAchievementTitleTrack.SmoothDaggerDeaths            => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.SmoothDaggerDominatedBys      => [ 1, 10 ],
            ServiceAchievementTitleTrack.SmoothDaggerDominations       => [ 1, 10 ],
            ServiceAchievementTitleTrack.SmoothDaggerKills             => [ 1, 10, 100, 1000 ],
            ServiceAchievementTitleTrack.SmoothDaggerRevenges          => [ 1, 10 ],
            ServiceAchievementTitleTrack.TimesViewed                   => [ 1, 10, 100, 1000, 10000 ],
            
            // Non-Meta Achievements
            ServiceAchievementTitleTrack.Rolled240                     => [ 1 ],
            ServiceAchievementTitleTrack.Rolled256                     => [ 4 ],
            ServiceAchievementTitleTrack.Rolled420                     => [ 1 ],
            ServiceAchievementTitleTrack.Rolled720                     => [ 1 ],
            ServiceAchievementTitleTrack.Rolled1080                    => [ 1 ],
            ServiceAchievementTitleTrack.Rolled1337                    => [ 1 ],
            ServiceAchievementTitleTrack.Rolled3840                    => [ 1 ],
            ServiceAchievementTitleTrack.Rolled100000                  => [ 1 ],
            ServiceAchievementTitleTrack.TitlesUnlocked                => [ 1, 10, 25, 50, 75, 101 ],
            ServiceAchievementTitleTrack.TracksCompleted               => [ 1, 5, 10, 20, 30, 40, 48 ],
            
            _                                                          => throw new NotImplementedException(),
        };
    }

    private static bool IsMetaAchievement(
        ServiceAchievementTitleTrack serviceAchievementTitleTrack
    )
    {
        return serviceAchievementTitleTrack switch
        {
            // Meta Achievements
            ServiceAchievementTitleTrack.CheckedBank                   => true,
            ServiceAchievementTitleTrack.CheckedUnlocks                => true,
            ServiceAchievementTitleTrack.ClearedName                   => true,
            ServiceAchievementTitleTrack.ClearedTitle                  => true,
            ServiceAchievementTitleTrack.CompletedBhopBonus            => true,
            ServiceAchievementTitleTrack.CompletedBhopMap              => true,
            ServiceAchievementTitleTrack.CustomizedAvatar              => true,
            ServiceAchievementTitleTrack.CustomizedBadge               => true,
            ServiceAchievementTitleTrack.CustomizedName                => true,
            ServiceAchievementTitleTrack.CustomizedTitle               => true,
            ServiceAchievementTitleTrack.LinkedSteam                   => true,
            ServiceAchievementTitleTrack.MessagesSent                  => true,
            ServiceAchievementTitleTrack.MinutesWatched                => true,
            ServiceAchievementTitleTrack.PlayedDemoman                 => true,
            ServiceAchievementTitleTrack.PlayedEngineer                => true,
            ServiceAchievementTitleTrack.PlayedHeavy                   => true,
            ServiceAchievementTitleTrack.PlayedMedic                   => true,
            ServiceAchievementTitleTrack.PlayedPyro                    => true,
            ServiceAchievementTitleTrack.PlayedScout                   => true,
            ServiceAchievementTitleTrack.PlayedSniper                  => true,
            ServiceAchievementTitleTrack.PlayedSoldier                 => true,
            ServiceAchievementTitleTrack.PlayedSpy                     => true,
            ServiceAchievementTitleTrack.PreviewedUnlocks              => true,
            ServiceAchievementTitleTrack.QuestionsAsked                => true,
            ServiceAchievementTitleTrack.RockPaperScissorLose3InARow   => true,
            ServiceAchievementTitleTrack.RockPaperScissorLosses        => true,
            ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed => true,
            ServiceAchievementTitleTrack.RockPaperScissorPapers        => true,
            ServiceAchievementTitleTrack.RockPaperScissorRocks         => true,
            ServiceAchievementTitleTrack.RockPaperScissorScissors      => true,
            ServiceAchievementTitleTrack.RockPaperScissorTie3InARow    => true,
            ServiceAchievementTitleTrack.RockPaperScissorTies          => true,
            ServiceAchievementTitleTrack.RockPaperScissorWin3InARow    => true,
            ServiceAchievementTitleTrack.RockPaperScissorWins          => true,
            ServiceAchievementTitleTrack.Rolled1                       => true,
            ServiceAchievementTitleTrack.Rolled42                      => true,
            ServiceAchievementTitleTrack.Rolled67                      => true,
            ServiceAchievementTitleTrack.Rolled69                      => true,
            ServiceAchievementTitleTrack.Rolled100                     => true,
            ServiceAchievementTitleTrack.Rolls                         => true,
            ServiceAchievementTitleTrack.RollsMaximum                  => true,
            ServiceAchievementTitleTrack.RollsMinimum                  => true,
            ServiceAchievementTitleTrack.SmoothDaggerDeaths            => true,
            ServiceAchievementTitleTrack.SmoothDaggerDominatedBys      => true,
            ServiceAchievementTitleTrack.SmoothDaggerDominations       => true,
            ServiceAchievementTitleTrack.SmoothDaggerKills             => true,
            ServiceAchievementTitleTrack.SmoothDaggerRevenges          => true,
            ServiceAchievementTitleTrack.TimesViewed                   => true,
            
            // Non Meta Achievements
            _                                                          => false
        };
    }

    private static void OnRetrievedAchievementUserTitlesUnlocked(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        const int c_titleIncreaseCount = 1;
        
        var result          = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username        = result.AchievementUser_Username;
        var progressCurrent = result.AchievementUser_Titles_Unlocked;
        
        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.TitlesUnlocked,
            progressCurrent:              progressCurrent,
            progressIncrease:             c_titleIncreaseCount
        );
    }
    
    private static void OnRetrievedAchievementUserTracksCompleted(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        const int c_trackIncreaseCount = 1;
        
        var result          = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username        = result.AchievementUser_Username;
        var progressCurrent = result.AchievementUser_Tracks_Completed;
        
        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.TracksCompleted,
            progressCurrent:              progressCurrent,
            progressIncrease:             c_trackIncreaseCount
        );
    }

    private static void RetrieveAchievementUserTracksCompleted(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username),
                value:         username
            ),
        };
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveAchievementUserTracksCompleted,
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

    private static void SubscribeToEvents()
    {
        ServiceDatabaseTaskEvents.RetrievedAchievementUserTitlesUnlocked  += ServiceAchievement.OnRetrievedAchievementUserTitlesUnlocked;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserTracksCompleted += ServiceAchievement.OnRetrievedAchievementUserTracksCompleted;
    }
}