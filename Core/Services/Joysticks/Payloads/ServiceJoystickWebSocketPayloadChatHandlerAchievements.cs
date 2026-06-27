
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.Databases.Tasks.Retrieves;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadChatHandlerAchievements
{
    public static void OnRetrievedAchievementUserCheckedBanks(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CheckedBank,
            progressCurrent:              result.AchievementUser_Checked_Banks,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserCheckedUnlocks(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CheckedUnlocks,
            progressCurrent:              result.AchievementUser_Checked_Unlocks,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserClearedNames(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.ClearedName,
            progressCurrent:              result.AchievementUser_Cleared_Names,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserClearedTitles(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.ClearedTitle,
            progressCurrent:              result.AchievementUser_Cleared_Titles,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserCustomizedAvatar(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CustomizedAvatar,
            progressCurrent:              result.AchievementUser_Customized_Avatars,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserCustomizedBadge(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CustomizedBadge,
            progressCurrent:              result.AchievementUser_Customized_Badges,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserCustomizedName(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CustomizedName,
            progressCurrent:              result.AchievementUser_Customized_Names,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserCustomizedTitle(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.CustomizedTitle,
            progressCurrent:              result.AchievementUser_Customized_Titles,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserLinkedSteams(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.LinkedSteam,
            progressCurrent:              result.AchievementUser_Linked_Steams,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserMessagesSent(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.MessagesSent,
            progressCurrent:              result.AchievementUser_Messages_Sent,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserPreviewedUnlocks(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.PreviewedUnlocks,
            progressCurrent:              result.AchievementUser_Previewed_Unlocks,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorLose3InARow(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorLose3InARow,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Lose_3_In_A_Rows,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorLosses(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorLosses,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Losses,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorMatchesPlayed(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorMatchesPlayed,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Matches_Played,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorPapers(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorPapers,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Papers,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorRocks(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorRocks,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Rocks,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorScissors(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorScissors,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Scissors,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorTie3InARow(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorTie3InARow,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Scissors,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorTies(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorTies,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Ties,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorWin3InARow(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorWin3InARow,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Win_3_In_A_Rows,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRockPaperScissorWins(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RockPaperScissorWins,
            progressCurrent:              result.AchievementUser_Rock_Paper_Scissor_Wins,
            progressIncrease:             1
        );
    }

    public static void OnRetrievedAchievementUserRolled1s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled1,
            progressCurrent:              result.AchievementUser_Rolled_1,
            progressIncrease:             1
        );
        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RollsMinimum,
            progressCurrent:              result.AchievementUser_Rolls_Minimum,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled42s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled42,
            progressCurrent:              result.AchievementUser_Rolled_42,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled67s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled67,
            progressCurrent:              result.AchievementUser_Rolled_67,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled69s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled69,
            progressCurrent:              result.AchievementUser_Rolled_69,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled100s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled100,
            progressCurrent:              result.AchievementUser_Rolled_100,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled240s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled240,
            progressCurrent:              result.AchievementUser_Rolled_240,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled256s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled256,
            progressCurrent:              result.AchievementUser_Rolled_256,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled420s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled420,
            progressCurrent:              result.AchievementUser_Rolled_420,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled720s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled720,
            progressCurrent:              result.AchievementUser_Rolled_720,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled1080s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled1080,
            progressCurrent:              result.AchievementUser_Rolled_1080,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled1337s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled1337,
            progressCurrent:              result.AchievementUser_Rolled_1337,
            progressIncrease:             1
        );
    }
        
    public static void OnRetrievedAchievementUserRolled3840s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled3840,
            progressCurrent:              result.AchievementUser_Rolled_3840,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolled100000s(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolled100000,
            progressCurrent:              result.AchievementUser_Rolled_100000,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRolls(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.Rolls,
            progressCurrent:              result.AchievementUser_Rolls,
            progressIncrease:             1
        );
    }
    
    public static void OnRetrievedAchievementUserRollsMaximum(
        ServiceDatabaseTaskRetrievedAchievementUser serviceDatabaseTaskRetrievedAchievementUser
    )
    {
        var result   = serviceDatabaseTaskRetrievedAchievementUser.Result;
        var username = result.AchievementUser_Username;

        ServiceAchievement.UpdateUserTitleTrackProgress(
            username:                     username,
            serviceAchievementTitleTrack: ServiceAchievementTitleTrack.RollsMaximum,
            progressCurrent:              result.AchievementUser_Rolls_Maximum,
            progressIncrease:             1
        );
    }
}