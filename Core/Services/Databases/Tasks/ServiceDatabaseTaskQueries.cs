
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueries
{
    static ServiceDatabaseTaskQueries()
    {
		
    }

    internal static async Task ExecuteAsyncQuery(
        ServiceDatabaseTaskQueryType             serviceDatabaseTaskQueryType,
        List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
        await ServiceDatabaseTaskQueries.s_taskQueries[
			key: serviceDatabaseTaskQueryType
		].Invoke(
	        arg: serviceDatabaseTaskNpgsqlParameters
	    );
    }

    private static readonly Dictionary<ServiceDatabaseTaskQueryType, Func<List<ServiceDatabaseTaskNpgsqlParameter>, Task>> s_taskQueries = new()
    {
        {
			ServiceDatabaseTaskQueryType.Start,
			_ => ServiceDatabaseTaskQueries.Start()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedBanks,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCheckedBanks
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedUnlocks,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCheckedUnlocks
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserClearedNames,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserClearedNames
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserClearedTitles,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserClearedTitles
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCompletedBhopBonuses,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCompletedBhopBonuses
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCompletedBhopMaps,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCompletedBhopMaps
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedAvatar,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCustomizedAvatar
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedBadge,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCustomizedBadge
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedName,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCustomizedName
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedTitle,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserCustomizedTitle
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserLinkedSteams,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserLinkedSteams
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserMessagesSent,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserMessagesSent
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserMinutesWatched,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserMinutesWatched
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedDemomans,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedDemomans
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedEngineers,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedEngineers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedHeavies,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedHeavies
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedMedics,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedMedics
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedPyros,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedPyros
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedScouts,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedScouts
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedSnipers,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedSnipers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedSoldiers,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedSoldiers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPlayedSpies,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPlayedSpies
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserPreviewedUnlocks,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserPreviewedUnlocks
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserQuestionsAsked,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserQuestionsAsked
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorLose3InARow,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorLose3InARow
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorLosses,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorLosses
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorMatchesPlayed,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorMatchesPlayed
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorPapers,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorPapers
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorRocks,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorRocks
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorScissors,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorScissors
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorTie3InARow,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorTie3InARow
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorTies,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorTies
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorWin3InARow,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorWin3InARow
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorWins,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRockPaperScissorWins
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled1s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled1s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled42s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled42s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled67s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled67s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled69s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled69s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled100s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled100s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled240s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled240s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled256s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled256s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled420s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled420s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled720s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled720s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled1080s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled1080s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled1337s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled1337s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled3840s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled3840s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled100000s,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolled100000s
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolls,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRolls
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserRollsMaximum,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserRollsMaximum
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerDeaths,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserSmoothDaggerDeaths
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerDominatedBys,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserSmoothDaggerDominatedBys
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerDominations,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserSmoothDaggerDominations
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerKills,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserSmoothDaggerKills
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserSmoothDaggerRevenges,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserSmoothDaggerRevenges
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserTimesViewed,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserTimesViewed
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserTitlesUnlocked,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserTitlesUnlocked
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveAchievementUserTracksCompleted,
			ServiceDatabaseTaskQueries.RetrieveAsyncAchievementUserTracksCompleted
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveBankUser,
			ServiceDatabaseTaskQueries.RetrieveAsyncBankUser
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveBankUserTimeLimit,
			ServiceDatabaseTaskQueries.RetrieveAsyncBankUserTimeLimit
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveBankUserTip,
			ServiceDatabaseTaskQueries.RetrieveAsyncBankUserTip
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveGoogleData,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncGoogleData()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveGoveeData,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListGoveeLights,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveJoystickData,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestFollowers,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestFollowers()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestSubscribers,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestSubscribers()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestTippers,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncListJoystickLatestTippers()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListUserBadgeColors,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncListUserBadgeColors()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveListUserNameColors,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncListUserNameColors()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveLovenseData,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncLovenseData()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveSpotifyData,
			_ => ServiceDatabaseTaskQueries.RetrieveAsyncSpotifyData()
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveSteamUser,
			ServiceDatabaseTaskQueries.RetrieveAsyncSteamUser
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserAvatarSettings,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserAvatarSettings
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserEnteredSteamUsername,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserEnteredSteamUsername
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserExitedSteamUsername,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserExitedSteamUsername
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserTitle,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserTitle
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserHasColor_Avatar,
			ServiceDatabaseTaskQueries.ValidateAsyncUserUnlockColor_Avatar
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserHasColor_Badge,
			ServiceDatabaseTaskQueries.ValidateAsyncUserUnlockColor_Badge
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserHasColor_Name,
			ServiceDatabaseTaskQueries.ValidateAsyncUserUnlockColor_Name
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserHasEffectAndColor,
			ServiceDatabaseTaskQueries.ValidateAsyncUserUnlockEffectAndColor
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserHasModel,
			ServiceDatabaseTaskQueries.ValidateAsyncUserUnlockModel
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserHasTitle,
			ServiceDatabaseTaskQueries.ValidateAsyncUserUnlockTitle
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserUnlockColors,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserUnlockColors
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserUnlockEffects,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserUnlockEffects
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserUnlockModels,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserUnlockModels
		},
		{
			ServiceDatabaseTaskQueryType.RetrieveUserUnlockTitles,
			ServiceDatabaseTaskQueries.RetrieveAsyncUserUnlockTitles
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserUnlockColor,
			ServiceDatabaseTaskQueries.ValidateAsyncUserBuyColor
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserUnlockEffect,
			ServiceDatabaseTaskQueries.ValidateAsyncUserBuyEffect
		},
		{
			ServiceDatabaseTaskQueryType.ValidateUserUnlockModel,
			ServiceDatabaseTaskQueries.ValidateAsyncUserBuyModel
		},
	};
    
    private static async Task RetrieveAsyncAchievementUserCheckedBanks(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCheckedBanks,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCheckedUnlocks(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCheckedUnlocks,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserClearedNames(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserClearedNames,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserClearedTitles(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserClearedTitles,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCompletedBhopBonuses(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCompletedBhopBonuses,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCompletedBhopMaps(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCompletedBhopMaps,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCustomizedAvatar(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedAvatar,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCustomizedBadge(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedBadge,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCustomizedName(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedName,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserCustomizedTitle(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserCustomizedTitle,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserLinkedSteams(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserLinkedSteams,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserMessagesSent(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserMessagesSent,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserMinutesWatched(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserMinutesWatched,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedDemomans(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedDemomans,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedEngineers(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedEngineers,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedHeavies(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedHeavies,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedMedics(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedMedics,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedPyros(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedPyros,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedScouts(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedScouts,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedSnipers(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSnipers,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedSoldiers(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSoldiers,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPlayedSpies(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPlayedSpies,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserPreviewedUnlocks(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserPreviewedUnlocks,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserQuestionsAsked(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserQuestionsAsked,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorLose3InARow(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorLose3InARow,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorLosses(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorLosses,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorMatchesPlayed(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorMatchesPlayed,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorPapers(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorPapers,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorRocks(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorRocks,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorScissors(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorScissors,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorTie3InARow(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorTie3InARow,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorTies(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorTies,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorWin3InARow(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorWin3InARow,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRockPaperScissorWins(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRockPaperScissorWins,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled1s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled1s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled42s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled42s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled67s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled67s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled69s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled69s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled100s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled100s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled240s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled240s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled256s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled256s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled420s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled420s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled720s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled720s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled1080s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled1080s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled1337s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled1337s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled3840s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled3840s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolled100000s(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolled100000s,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRolls(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRolls,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserRollsMaximum(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserRollsMaximum,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserSmoothDaggerDeaths(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDeaths,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserSmoothDaggerDominatedBys(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDominatedBys,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserSmoothDaggerDominations(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerDominations,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserSmoothDaggerKills(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerKills,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserSmoothDaggerRevenges(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserSmoothDaggerRevenges,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserTimesViewed(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserTimesViewed,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserTitlesUnlocked(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserTitlesUnlocked,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncAchievementUserTracksCompleted(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveAchievementUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedAchievementUserTracksCompleted,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncBankUser(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveBankUser;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedBankUser,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncBankUserTimeLimit(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveBankUserTimeLimit;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedBankUserTimeLimit,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncBankUserTip(
	    List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
    )
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveBankUserTip;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		             c_npgsqlStatement,
		    executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedBankUserTip,
		    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
	    );
    }
    
    private static async Task RetrieveAsyncGoogleData()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveGoogleData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoogleData
	    );
    }
    
    private static async Task RetrieveAsyncGoveeData()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveGoveeData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedGoveeData
	    );
    }
    
    private static async Task RetrieveAsyncJoystickData()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveJoystickData;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedJoystickData
	    );
    }
    
    private static async Task RetrieveAsyncListGoveeLights()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListGoveeLights;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListGoveeLights
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestFollowers()
    {
	    const string npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestFollowers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestFollowers
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestSubscribers()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestSubscribers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestSubscribers
	    );
    }
    
    private static async Task RetrieveAsyncListJoystickLatestTippers()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListJoystickLatestTippers;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListJoystickLatestTippers
	    );
    }
    
    private static async Task RetrieveAsyncListUserBadgeColors()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListUserBadgeColors;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListUserBadgeColors
	    );
    }
    
    private static async Task RetrieveAsyncListUserNameColors()
    {
	    const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListUserNameColors;
	    await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
		    npgsqlStatement:		  c_npgsqlStatement,
		    executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedListUserNameColors
	    );
    }
	
	private static async Task RetrieveAsyncLovenseData()
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveLovenseData;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  c_npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedLovenseData
		);
	}
	
	private static async Task RetrieveAsyncSpotifyData()
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveSpotifyData;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		  c_npgsqlStatement,
			executeQueryAsyncHandler: ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedSpotifyData
		);
	}
	
	private static async Task RetrieveAsyncSteamUser(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveSteamUser;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedSteamUser,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task RetrieveAsyncUserAvatarSettings(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveUserAvatarSettings;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserAvatarSettings,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task RetrieveAsyncUserEnteredSteamUsername(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveSteamUser;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserEnteredSteamUsername,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task RetrieveAsyncUserExitedSteamUsername(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveSteamUser;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserExitedSteamUsername,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task RetrieveAsyncUserTitle(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveUserTitle;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserTitle,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task RetrieveAsyncUserUnlockColors(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListUserUnlockColors;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserUnlockColors,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
    
	private static async Task RetrieveAsyncUserUnlockEffects(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListUserUnlockEffects;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserUnlockEffects,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
    
	private static async Task RetrieveAsyncUserUnlockModels(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListUserUnlockModels;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserUnlockModels,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task RetrieveAsyncUserUnlockTitles(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.RetrieveListUserUnlockTitles;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncRetrievedUserUnlockTitles,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}

	private static async Task Start()
	{
		await ServiceDatabaseTaskQueries.RetrieveAsyncGoogleData();
		
		await ServiceDatabaseTaskQueries.RetrieveAsyncGoveeData();
		await ServiceDatabaseTaskQueries.RetrieveAsyncListGoveeLights();
		
	    await ServiceDatabaseTaskQueries.RetrieveAsyncJoystickData();

	    await ServiceDatabaseTaskQueries.RetrieveAsyncListUserBadgeColors();
	    await ServiceDatabaseTaskQueries.RetrieveAsyncListUserNameColors();

	    await ServiceDatabaseTaskQueries.RetrieveAsyncLovenseData();
	}
	
	private static async Task ValidateAsyncUserBuyColor(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserBuyColor;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserUnlockColor,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserBuyEffect(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserBuyEffect;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserUnlockEffect,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserBuyModel(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserBuyModel;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserUnlockModel,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}

	private static async Task ValidateAsyncUserUnlockColor_Avatar(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserUnlockColor;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Avatar,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserUnlockColor_Badge(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserUnlockColor;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Badge,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserUnlockColor_Name(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserUnlockColor;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasColor_Name,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserUnlockEffectAndColor(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserUnlockEffectAndColor;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasEffectAndColor,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserUnlockModel(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserUnlockModel;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasModel,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
	
	private static async Task ValidateAsyncUserUnlockTitle(
		List<ServiceDatabaseTaskNpgsqlParameter> serviceDatabaseTaskNpgsqlParameters
	)
	{
		const string c_npgsqlStatement = ServiceDatabaseTaskQueryStatements.ValidateUserUnlockTitle;
		await ServiceDatabaseTaskLogic.ExecuteQueryAsync(
			npgsqlStatement:		             c_npgsqlStatement,
			executeQueryAsyncHandler:            ServiceDatabaseTaskQueryHandlers.HandleExecuteQueryAsyncValidatedUserHasTitle,
			serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
		);
	}
};