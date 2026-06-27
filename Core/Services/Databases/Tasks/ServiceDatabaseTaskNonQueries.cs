
using Overlay.Core.Services.Databases.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskNonQueries
{
    static ServiceDatabaseTaskNonQueries()
    {
        
    }

    internal static async Task ExecuteAsyncNonQuery(
        ServiceDatabaseTaskNonQueryType          serviceDatabaseTaskNonQueryType,
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskSqlParameters
	)
	{
        await ServiceDatabaseTaskNonQueries.s_taskNonQueries[
            key: serviceDatabaseTaskNonQueryType
        ].Invoke(
            arg: serviceDatabaseTaskSqlParameters
        );
    }

    private static readonly Dictionary<ServiceDatabaseTaskNonQueryType, Func<List<ServiceDatabaseTaskNpgsqlParameter>, Task>> s_taskNonQueries = new()
    {
        {
            ServiceDatabaseTaskNonQueryType.AddJoystickLatestFollower, 
            ServiceDatabaseTaskNonQueries.AddJoystickLatestFollowerAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.AddJoystickLatestSubscriber, 
            ServiceDatabaseTaskNonQueries.AddJoystickLatestSubscriberAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.AddJoystickLatestTipper, 
            ServiceDatabaseTaskNonQueries.AddJoystickLatestTipperAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.BuyUserColor, 
            ServiceDatabaseTaskNonQueries.BuyUserColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.BuyUserEffect, 
            ServiceDatabaseTaskNonQueries.BuyUserEffectAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.BuyUserModel, 
            ServiceDatabaseTaskNonQueries.BuyUserModelAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.DepositTimeForBankUser, 
            ServiceDatabaseTaskNonQueries.DepositTimeForBankUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.DepositTipForBankUser, 
            ServiceDatabaseTaskNonQueries.DepositTipForBankUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncreaseTimeLimitForBankUser, 
            ServiceDatabaseTaskNonQueries.IncreaseTimeLimitForBankUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementBuyMeABananaShakes,
            ServiceDatabaseTaskNonQueries.IncrementAchievementBuyMeABananaShakesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementBuyMeADinners,
            ServiceDatabaseTaskNonQueries.IncrementAchievementBuyMeADinnersAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementBuyMeAPoweradeSlushies,
            ServiceDatabaseTaskNonQueries.IncrementAchievementBuyMeAPoweradeSlushiesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCheckedBanks,
            ServiceDatabaseTaskNonQueries.IncrementAchievementCheckedBanksAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCheckedUnlocks,
            ServiceDatabaseTaskNonQueries.IncrementAchievementCheckedUnlocksAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementClearedNames,
            ServiceDatabaseTaskNonQueries.IncrementAchievementClearedNamesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementClearedTitles,
            ServiceDatabaseTaskNonQueries.IncrementAchievementClearedTitlesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementColorsUnlocked,
            ServiceDatabaseTaskNonQueries.IncrementAchievementColorsUnlockedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCompliments,
            ServiceDatabaseTaskNonQueries.IncrementAchievementComplimentsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedAvatars,
            ServiceDatabaseTaskNonQueries.IncrementAchievementCustomizedAvatarsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedBadges,
            ServiceDatabaseTaskNonQueries.IncrementAchievementCustomizedBadgesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedNames,
            ServiceDatabaseTaskNonQueries.IncrementAchievementCustomizedNamesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementCustomizedTitles,
            ServiceDatabaseTaskNonQueries.IncrementAchievementCustomizedTitlesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementDropins,
            ServiceDatabaseTaskNonQueries.IncrementAchievementDropinsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementEffectsUnlocked,
            ServiceDatabaseTaskNonQueries.IncrementAchievementEffectsUnlockedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementExercises,
            ServiceDatabaseTaskNonQueries.IncrementAchievementExercisesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementFaps,
            ServiceDatabaseTaskNonQueries.IncrementAchievementFapsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementFirsts,
            ServiceDatabaseTaskNonQueries.IncrementAchievementFirstsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementGiftedSubs,
            ServiceDatabaseTaskNonQueries.IncrementAchievementGiftedSubsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementGushControlLinkMinutesUsed,
            ServiceDatabaseTaskNonQueries.IncrementAchievementGushControlLinkMinutesUsedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementHellos,
            ServiceDatabaseTaskNonQueries.IncrementAchievementHellosAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementHydrates,
            ServiceDatabaseTaskNonQueries.IncrementAchievementHydratesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementInteractions,
            ServiceDatabaseTaskNonQueries.IncrementAchievementInteractionsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementLightsChanged,
            ServiceDatabaseTaskNonQueries.IncrementAchievementLightsChangedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementLinkedSteams,
            ServiceDatabaseTaskNonQueries.IncrementAchievementLinkedSteamsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementLurks,
            ServiceDatabaseTaskNonQueries.IncrementAchievementLurksAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementMessagesSent,
            ServiceDatabaseTaskNonQueries.IncrementAchievementMessagesSentAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementMinutesWatched,
            ServiceDatabaseTaskNonQueries.IncrementAchievementMinutesWatchedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementModelsUnlocked,
            ServiceDatabaseTaskNonQueries.IncrementAchievementModelsUnlockedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementNsfwClaims,
            ServiceDatabaseTaskNonQueries.IncrementAchievementNsfwClaimsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementPayMyBills,
            ServiceDatabaseTaskNonQueries.IncrementAchievementPayMyBillsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementPreviewedUnlocks,
            ServiceDatabaseTaskNonQueries.IncrementAchievementPreviewedUnlocksAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementQuestionsAsked,
            ServiceDatabaseTaskNonQueries.IncrementAchievementQuestionsAskedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorLose3InARows,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorLose3InARowsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorLosses,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorLossesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorMatchesPlayed,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorMatchesPlayedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorPapers,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorPapersAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorScissors,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorScissorsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorRocks,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorRocksAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorTie3InARows,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorTie3InARowsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorTies,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorTiesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorWin3InARows,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorWin3InARowsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRockPaperScissorWins,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRockPaperScissorWinsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled1,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled1Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled42,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled42Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled67,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled67Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled69,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled69Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled100,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled100Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled240,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled240Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled256,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled256Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled420,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled420Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled720,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled720Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled1080,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled1080Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled1337,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled1337Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled3840,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled3840Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolled100000,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRolled100000Async
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRolls,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRollsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRollsMaximum,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRollsMaximumAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementRollsMinimum,
            ServiceDatabaseTaskNonQueries.IncrementAchievementRollsMinimumAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementSfxUsed,
            ServiceDatabaseTaskNonQueries.IncrementAchievementSfxUsedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementShowinSomeLoves,
            ServiceDatabaseTaskNonQueries.IncrementAchievementShowinSomeLovesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementSongsRequested,
            ServiceDatabaseTaskNonQueries.IncrementAchievementSongsRequestedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementSongsSkipped,
            ServiceDatabaseTaskNonQueries.IncrementAchievementSongsSkippedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementSubs,
            ServiceDatabaseTaskNonQueries.IncrementAchievementSubsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2Explodes,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2ExplodesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2Kills,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2KillsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2CompletedBhopBonuses,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2CompletedBhopBonusesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2CompletedBhopMaps,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2CompletedBhopMapsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedDemomans,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedDemomansAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedEngineers,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedEngineersAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedHeavies,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedHeaviesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedMedics,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedMedicsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedPyros,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedPyrosAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedScouts,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedScoutsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedSnipers,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedSnipersAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedSoldiers,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedSoldiersAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2PlayedSpies,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2PlayedSpiesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerDeaths,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2SmoothDaggerDeathsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerDominatedBys,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2SmoothDaggerDominatedBysAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerDominations,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2SmoothDaggerDominationsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerKills,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2SmoothDaggerKillsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTf2SmoothDaggerRevenges,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTf2SmoothDaggerRevengesAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementThanks,
            ServiceDatabaseTaskNonQueries.IncrementAchievementThanksAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTimesViewed,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTimesViewedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTitlesUnlocked,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTitlesUnlockedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTokeUps,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTokeUpsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTotalTips,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTotalTipsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.IncrementAchievementTracksCompleted,
            ServiceDatabaseTaskNonQueries.IncrementAchievementTracksCompletedAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.RemoveUserBadgeColor, 
            ServiceDatabaseTaskNonQueries.RemoveUserBadgeColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.RemoveUserNameColor, 
            ServiceDatabaseTaskNonQueries.RemoveUserNameColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.RemoveUserTitle, 
            ServiceDatabaseTaskNonQueries.RemoveUserTitleAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.ResetTipLimitForBankUsers, 
            ServiceDatabaseTaskNonQueries.ResetTipLimitForBankUsersAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.ResetUserAvatarSettings, 
            ServiceDatabaseTaskNonQueries.ResetUserAvatarSettingsAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.SetTimeForBankUser, 
            ServiceDatabaseTaskNonQueries.SetTimeForBankUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UnlockUserTitleWithMeta, 
            ServiceDatabaseTaskNonQueries.UnlockUserTitleWithMetaAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UnlockUserTitleWithNoMeta, 
            ServiceDatabaseTaskNonQueries.UnlockUserTitleWithNoMetaAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateAchievementUserLastViewDate, 
            ServiceDatabaseTaskNonQueries.UpdateAchievementUserLastViewDateAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateCurrentTipForBankUser, 
            ServiceDatabaseTaskNonQueries.UpdateCurrentTipForBankUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateSteamUser, 
            ServiceDatabaseTaskNonQueries.UpdateSteamUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateTipLimitForBankUser, 
            ServiceDatabaseTaskNonQueries.UpdateTipLimitForBankUserAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarBase, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarBaseAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarModel, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarModelAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarOutline, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarOutlineAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader0EffectAndColor, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarShader0EffectAndColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader1EffectAndColor, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarShader1EffectAndColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader2EffectAndColor, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarShader2EffectAndColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader3EffectAndColor, 
            ServiceDatabaseTaskNonQueries.UpdateUserAvatarShader3EffectAndColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserBadgeColor, 
            ServiceDatabaseTaskNonQueries.UpdateUserBadgeColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserNameColor, 
            ServiceDatabaseTaskNonQueries.UpdateUserNameColorAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.UpdateUserTitle, 
            ServiceDatabaseTaskNonQueries.UpdateUserTitleAsync
        },
        {
            ServiceDatabaseTaskNonQueryType.WithdrawTimeForBankUser, 
            ServiceDatabaseTaskNonQueries.WithdrawTimeForBankUserAsync
        },
    };
    
    private static async Task AddJoystickLatestFollowerAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO JoystickLatestFollower (" +
            $"{nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task AddJoystickLatestSubscriberAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO JoystickLatestSubscriber (" +
            $"{nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task AddJoystickLatestTipperAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO JoystickLatestTipper (" +
            $"{nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task BuyUserColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO UserUnlockColors (" +
            $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)}, " +
            $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)}, " +
            $"@{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task BuyUserEffectAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO UserUnlockEffects (" +
            $"{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)}, " +
            $"{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)}, " +
            $"@{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task BuyUserModelAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO UserUnlockModels (" +
            $"{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)}, " +
            $"{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)}, " +
            $"@{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task DepositTimeForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    ) => await ServiceDatabaseTaskNonQueries.UpdateBankTimeAsync(
        serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters, 
        mathOperator:                        $"+"
    );
    
    private static async Task DepositTipForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table         = "BankUser";
        const string c_columnUser    = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username);
        const string c_columnCurrent = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold);
        const string c_columnTotal   = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Total);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnCurrent}, {c_columnTotal}) " +
            $"VALUES (@{c_columnUser}, @{c_columnCurrent}, @{c_columnCurrent}) " +
            $"ON CONFLICT ({c_columnUser}) " +
            $"DO UPDATE SET " +
            $"{c_columnCurrent} = COALESCE({c_table}.{c_columnCurrent}, 0) + EXCLUDED.{c_columnCurrent}, " +
            $"{c_columnTotal} = COALESCE({c_table}.{c_columnTotal}, 0) + EXCLUDED.{c_columnCurrent};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveBankUserTip,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncreaseTimeLimitForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table                      = "BankUser";
        const string c_columnUser                 = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username);
        const string c_columnLimit                = nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes);
        const string c_columnTime                 = nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes);
        const int    c_limitIncreaseRateInMinutes = 5;
        const int    c_startingLimit              = 10;

        var npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnLimit}, {c_columnTime}) " +
            $"VALUES (@{c_columnUser}, {c_startingLimit + c_limitIncreaseRateInMinutes}, 0) " +
            $"ON CONFLICT ({c_columnUser}) " +
            $"DO UPDATE SET {c_columnLimit} = COALESCE({c_table}.{c_columnLimit}, {c_startingLimit}) + {c_limitIncreaseRateInMinutes};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveBankUserTimeLimit,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementAsync(
        string                                   columnName,
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table      = "AchievementUser";
        const string c_columnUser = nameof(ServiceDatabaseAchievementUser.AchievementUser_Username);

        var npgsqlStatement =
            $"INSERT INTO {c_table} ({c_columnUser}, {columnName}) " +
            $"VALUES (@{c_columnUser}, @{columnName}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{columnName} = {c_table}.{columnName} + EXCLUDED.{columnName}";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementBuyMeABananaShakesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Buy_Me_A_Banana_Shakes),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementBuyMeADinnersAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Buy_Me_A_Dinners),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementBuyMeAPoweradeSlushiesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Buy_Me_A_Powerade_Slushies),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementCheckedBanksAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Checked_Banks),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementCheckedUnlocksAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Checked_Unlocks),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementClearedNamesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Cleared_Names),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementClearedTitlesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Cleared_Titles),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementColorsUnlockedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Colors_Unlocked),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementComplimentsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Compliments),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementCustomizedAvatarsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Avatars),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementCustomizedBadgesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Badges),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementCustomizedNamesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Names),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementCustomizedTitlesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Customized_Titles),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementDropinsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Dropins),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementEffectsUnlockedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Effects_Unlocked),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementExercisesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Exercises),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementFapsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Faps),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementFirstsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Firsts),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementGiftedSubsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Gifted_Subs),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementGushControlLinkMinutesUsedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Gush_Control_Link_Minutes_Used),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementHellosAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Hellos),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementHydratesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Hydrates),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementInteractionsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Interactions),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementLightsChangedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Lights_Changed),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementLinkedSteamsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Linked_Steams),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementLurksAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Lurks),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementMessagesSentAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Messages_Sent),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementMinutesWatchedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Minutes_Watched),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementModelsUnlockedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Models_Unlocked),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementNsfwClaimsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Nsfw_Claims),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementPayMyBillsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Pay_My_Bills),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementPreviewedUnlocksAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Previewed_Unlocks),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementQuestionsAskedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Questions_Asked),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRockPaperScissorLose3InARowsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Lose_3_In_A_Rows),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRockPaperScissorLossesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Losses),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRockPaperScissorMatchesPlayedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Matches_Played),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRockPaperScissorPapersAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Papers),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRockPaperScissorRocksAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Rocks),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRockPaperScissorScissorsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Scissors),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRockPaperScissorTie3InARowsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Tie_3_In_A_Rows),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRockPaperScissorTiesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Ties),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRockPaperScissorWin3InARowsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Win_3_In_A_Rows),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRockPaperScissorWinsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Wins),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled1Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_1),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled100Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_100),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled100000Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_100000),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled1080Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_1080),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRolled1337Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_1337),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled240Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_240),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled256Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_256),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled3840Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_3840),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled42Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_42),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled420Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_420),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled67Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_67),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled69Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_69),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRolled720Async(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolled_720),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementRollsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolls),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRollsMaximumAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolls_Maximum),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementRollsMinimumAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Rolls_Minimum),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementSfxUsedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Sfx_Used),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementShowinSomeLovesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Showin_Some_Loves),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementSongsRequestedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Songs_Requested),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementSongsSkippedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Songs_Skipped),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementSubsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Subs),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2ExplodesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Explodes),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2KillsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Kills),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2CompletedBhopBonusesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Completed_Bhop_Bonuses),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2CompletedBhopMapsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Completed_Bhop_Maps),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedDemomansAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Demomans),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedEngineersAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Engineers),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedHeaviesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Heavies),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedMedicsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Medics),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedPyrosAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Pyros),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedScoutsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Scouts),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedSnipersAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Snipers),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedSoldiersAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Soldiers),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task IncrementAchievementTf2PlayedSpiesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Spies),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2SmoothDaggerDeathsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Deaths),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2SmoothDaggerDominatedBysAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Dominated_Bys),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2SmoothDaggerDominationsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Dominations),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2SmoothDaggerKillsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Kills),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTf2SmoothDaggerRevengesAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Revenges),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementThanksAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Thanks),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTimesViewedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Times_Viewed),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTitlesUnlockedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Titles_Unlocked),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTokeUpsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Toke_Ups),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTotalTipsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Total_Tips),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task IncrementAchievementTracksCompletedAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskNonQueries.IncrementAchievementAsync(
            columnName:                          nameof(ServiceDatabaseAchievementUser.AchievementUser_Tracks_Completed),
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task RemoveUserBadgeColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table         = "UserBadgeColors";
        const string c_columnUser    = nameof(ServiceDatabaseUserBadgeColor.UserBadgeColors_Username);

        const string npgsqlStatement = $"DELETE FROM {c_table} " +
                                       $"WHERE {c_columnUser} = (@{c_columnUser});";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task RemoveUserNameColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table         = "UserNameColors";
        const string c_columnUser    = nameof(ServiceDatabaseUserNameColor.UserNameColors_Username);

        const string npgsqlStatement = $"DELETE FROM {c_table} " + 
                                       $"WHERE {c_columnUser} = (@{c_columnUser});";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task RemoveUserTitleAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table         = "UserTitles";
        const string c_columnUser    = nameof(ServiceDatabaseUserTitle.UserTitles_Username);

        const string npgsqlStatement = $"DELETE FROM {c_table} " + 
                                       $"WHERE {c_columnUser} = (@{c_columnUser});";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task ResetTipLimitForBankUsersAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = $"UPDATE BankUser SET {nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute)} = 1000;";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task ResetUserAvatarSettingsAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table             = "UserAvatarSettings";

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id)}, " +
            $"@{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id)}" +
            $") ON CONFLICT ({nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}) DO UPDATE SET " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id)}, " +
            $"{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id)} = EXCLUDED.{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id)};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task SetTimeForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table      = "BankUser";
        const string c_columnUser = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username);
        const string c_columnTime = nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUser}, {c_columnTime}" +
            $") VALUES (" +
            $"@{c_columnUser}, " +
            $"@{c_columnTime}" +
            $") ON CONFLICT (" +
            $"{c_columnUser}" +
            $") DO UPDATE SET {c_columnTime} = EXCLUDED.{c_columnTime};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UnlockUserTitleWithMetaAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO UserUnlockTitles (" +
            $"{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)}, " +
            $"{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)}, " +
            $"@{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );

        serviceDatabaseTaskNpgsqlParameters = [
            new ServiceDatabaseTaskNpgsqlParameter(
                parameterName: nameof(ServiceDatabaseAchievementUser.AchievementUser_Username),
                value:         serviceDatabaseTaskNpgsqlParameters[0].Value
            ),
        ];

        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveAchievementUserTitlesUnlocked,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UnlockUserTitleWithNoMetaAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_npgsqlStatement = 
            $"INSERT INTO UserUnlockTitles (" +
            $"{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)}, " +
            $"{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)}" +
            $") VALUES (" +
            $"@{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)}, " +
            $"@{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)}" +
            $")";
        
        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement,
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateAchievementUserLastViewDateAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table          = "AchievementUser";
        const string c_columnUser     = nameof(ServiceDatabaseAchievementUser.AchievementUser_Username);
        const string c_columnViewDate = nameof(ServiceDatabaseAchievementUser.AchievementUser_Last_View_Date);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUser}, {c_columnViewDate}" +
            $") VALUES (" +
            $"@{c_columnUser}, " +
            $"@{c_columnViewDate}" +
            $") ON CONFLICT (" +
            $"{c_columnUser}" +
            $") DO UPDATE SET {c_columnViewDate} = EXCLUDED.{c_columnViewDate};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateBankTimeAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters,
        string                                   mathOperator
    )
    {
        const string c_table      = "BankUser";
        const string c_columnUser = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username);
        const string c_columnTime = nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes);

        var npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUser}, {c_columnTime}" +
            $") VALUES (" +
            $"@{c_columnUser}, " +
            $"@{c_columnTime}" +
            $") ON CONFLICT (" +
            $"{c_columnUser}" +
            $") DO UPDATE SET {c_columnTime} = {c_table}.{c_columnTime} {mathOperator} EXCLUDED.{c_columnTime};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateCurrentTipForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table         = "BankUser";
        const string c_columnUser    = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username);
        const string c_columnCurrent = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUser}, {c_columnCurrent}" +
            $") VALUES (" +
            $"@{c_columnUser}, " +
            $"@{c_columnCurrent}" +
            $") ON CONFLICT (" +
            $"{c_columnUser}" +
            $") DO UPDATE SET {c_columnCurrent} = EXCLUDED.{c_columnCurrent};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateSteamUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table                  = "SteamUser";
        const string c_columnJoystickUsername = nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username);
        const string c_columnSteamUsername    = nameof(ServiceDatabaseSteamUser.SteamUser_Steam_Username);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnJoystickUsername}, {c_columnSteamUsername}" +
            $") VALUES (" +
            $"@{c_columnJoystickUsername}, " +
            $"@{c_columnSteamUsername}" +
            $") ON CONFLICT (" +
            $"{c_columnJoystickUsername}" +
            $") DO UPDATE SET {c_columnSteamUsername} = EXCLUDED.{c_columnSteamUsername};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateTipLimitForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table       = "BankUser";
        const string c_columnUser  = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username);
        const string c_columnLimit = nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUser}, {c_columnLimit}" +
            $") VALUES (" +
            $"@{c_columnUser}, " +
            $"@{c_columnLimit}" +
            $") ON CONFLICT (" +
            $"{c_columnUser}" +
            $") DO UPDATE SET {c_columnLimit} = EXCLUDED.{c_columnLimit};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserAvatarBaseAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table             = "UserAvatarSettings";
        const string c_columnUsername    = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnBaseColorId = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUsername}, {c_columnBaseColorId}" +
            $") VALUES (" +
            $"@{c_columnUsername}, @{c_columnBaseColorId}" +
            $") ON CONFLICT ({c_columnUsername}) " +
            $"DO UPDATE SET {c_columnBaseColorId} = EXCLUDED.{c_columnBaseColorId};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserAvatarModelAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table          = "UserAvatarSettings";
        const string c_columnUsername = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnModelId  = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUsername}, {c_columnModelId}" +
            $") VALUES (" +
            $"@{c_columnUsername}, @{c_columnModelId}" +
            $") ON CONFLICT ({c_columnUsername}) " +
            $"DO UPDATE SET {c_columnModelId} = EXCLUDED.{c_columnModelId};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserAvatarOutlineAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table                = "UserAvatarSettings";
        const string c_columnUsername       = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnOutlineColorId = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} (" +
            $"{c_columnUsername}, {c_columnOutlineColorId}" +
            $") VALUES (" +
            $"@{c_columnUsername}, @{c_columnOutlineColorId}" +
            $") ON CONFLICT ({c_columnUsername}) " +
            $"DO UPDATE SET {c_columnOutlineColorId} = EXCLUDED.{c_columnOutlineColorId};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserAvatarShader0EffectAndColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table        = "UserAvatarSettings";
        const string c_columnUser   = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnColor  = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id);
        const string c_columnEffect = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnColor}, {c_columnEffect}) " +
            $"VALUES (@{c_columnUser}, @{c_columnColor}, @{c_columnEffect}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnColor} = EXCLUDED.{c_columnColor}, " +
            $"{c_columnEffect} = EXCLUDED.{c_columnEffect};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task UpdateUserAvatarShader1EffectAndColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table        = "UserAvatarSettings";
        const string c_columnUser   = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnColor  = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id);
        const string c_columnEffect = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnColor}, {c_columnEffect}) " +
            $"VALUES (@{c_columnUser}, @{c_columnColor}, @{c_columnEffect}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnColor} = EXCLUDED.{c_columnColor}, " +
            $"{c_columnEffect} = EXCLUDED.{c_columnEffect};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task UpdateUserAvatarShader2EffectAndColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table        = "UserAvatarSettings";
        const string c_columnUser   = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnColor  = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id);
        const string c_columnEffect = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnColor}, {c_columnEffect}) " +
            $"VALUES (@{c_columnUser}, @{c_columnColor}, @{c_columnEffect}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnColor} = EXCLUDED.{c_columnColor}, " +
            $"{c_columnEffect} = EXCLUDED.{c_columnEffect};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static async Task UpdateUserAvatarShader3EffectAndColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table        = "UserAvatarSettings";
        const string c_columnUser   = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username);
        const string c_columnColor  = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id);
        const string c_columnEffect = nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnColor}, {c_columnEffect}) " +
            $"VALUES (@{c_columnUser}, @{c_columnColor}, @{c_columnEffect}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnColor} = EXCLUDED.{c_columnColor}, " +
            $"{c_columnEffect} = EXCLUDED.{c_columnEffect};";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserBadgeColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table        = "UserBadgeColors";
        const string c_columnUser  = nameof(ServiceDatabaseUserBadgeColor.UserBadgeColors_Username);
        const string c_columnColor = nameof(ServiceDatabaseUserBadgeColor.UserBadgeColors_Color_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnColor}) " +
            $"VALUES (@{c_columnUser}, @{c_columnColor}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnColor} = EXCLUDED.{c_columnColor}";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserNameColorAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table       = "UserNameColors";
        const string c_columnUser  = nameof(ServiceDatabaseUserNameColor.UserNameColors_Username);
        const string c_columnColor = nameof(ServiceDatabaseUserNameColor.UserNameColors_Color_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnColor}) " +
            $"VALUES (@{c_columnUser}, @{c_columnColor}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnColor} = EXCLUDED.{c_columnColor}";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task UpdateUserTitleAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        const string c_table       = "UserTitles";
        const string c_columnUser  = nameof(ServiceDatabaseUserTitle.UserTitles_Username);
        const string c_columnTitle = nameof(ServiceDatabaseUserTitle.UserTitles_Title_Id);

        const string c_npgsqlStatement = 
            $"INSERT INTO {c_table} ({c_columnUser}, {c_columnTitle}) " +
            $"VALUES (@{c_columnUser}, @{c_columnTitle}) " +
            $"ON CONFLICT ({c_columnUser}) DO UPDATE SET " +
            $"{c_columnTitle} = EXCLUDED.{c_columnTitle}";

        await ServiceDatabaseTaskLogic.ExecuteNonQueryAsync(
            npgsqlStatement:                     c_npgsqlStatement, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static async Task WithdrawTimeForBankUserAsync(
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    ) => await ServiceDatabaseTaskNonQueries.UpdateBankTimeAsync(
        serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters, 
        mathOperator:                        $"-"
    );
}