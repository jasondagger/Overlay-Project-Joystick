
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.Databases.Tasks.Retrieves;

namespace Overlay.Core.Services.TeamFortress2s;

internal static class ServiceTeamFortress2AchievementHandler
{
    internal static void OnRetrievedAchievementUserCompletedBhopBonuses(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CompletedBhopBonus,
            progressCurrent:              result.AchievementUser_Tf2_Completed_Bhop_Bonuses,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserCompletedBhopMaps(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CompletedBhopMap,
            progressCurrent:              result.AchievementUser_Tf2_Completed_Bhop_Maps,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedDemomans(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedDemoman,
            progressCurrent:              result.AchievementUser_Tf2_Played_Demomans,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedEngineers(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedEngineer,
            progressCurrent:              result.AchievementUser_Tf2_Played_Engineers,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedHeavies(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedHeavy,
            progressCurrent:              result.AchievementUser_Tf2_Played_Heavies,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedMedics(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedMedic,
            progressCurrent:              result.AchievementUser_Tf2_Played_Medics,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedPyros(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedPyro,
            progressCurrent:              result.AchievementUser_Tf2_Played_Pyros,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedScouts(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedScout,
            progressCurrent:              result.AchievementUser_Tf2_Played_Scouts,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedSnipers(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedSniper,
            progressCurrent:              result.AchievementUser_Tf2_Played_Snipers,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedSoldiers(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedSoldier,
            progressCurrent:              result.AchievementUser_Tf2_Played_Soldiers,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserPlayedSpies(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PlayedSpy,
            progressCurrent:              result.AchievementUser_Tf2_Played_Spies,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserSmoothDaggerDeaths(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.SmoothDaggerDeaths,
            progressCurrent:              result.AchievementUser_Tf2_SmoothDagger_Deaths,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserSmoothDaggerDominatedBys(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.SmoothDaggerDominatedBys,
            progressCurrent:              result.AchievementUser_Tf2_SmoothDagger_Dominated_Bys,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserSmoothDaggerDominations(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.SmoothDaggerDominations,
            progressCurrent:              result.AchievementUser_Tf2_SmoothDagger_Dominations,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserSmoothDaggerKills(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.SmoothDaggerKills,
            progressCurrent:              result.AchievementUser_Tf2_SmoothDagger_Kills,
            progressIncrease:             1
        );
    }
    
    internal static void OnRetrievedAchievementUserSmoothDaggerRevenges(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.SmoothDaggerRevenges,
            progressCurrent:              result.AchievementUser_Tf2_SmoothDagger_Revenges,
            progressIncrease:             1
        );
    }
}