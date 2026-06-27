
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskQueryStatements
{
    static ServiceDatabaseTaskQueryStatements()
    {
       
    }
    
    internal const string RetrieveAchievementUser               = 
                                                                  $"INSERT INTO AchievementUser ({nameof(ServiceDatabaseAchievementUser.AchievementUser_Username)}) " +
                                                                  $"VALUES (@{nameof(ServiceDatabaseAchievementUser.AchievementUser_Username)}) " +
                                                                  $"ON CONFLICT ({nameof(ServiceDatabaseAchievementUser.AchievementUser_Username)}) " +
                                                                  $"DO UPDATE SET {nameof(ServiceDatabaseAchievementUser.AchievementUser_Username)} = EXCLUDED.{nameof(ServiceDatabaseAchievementUser.AchievementUser_Username)} " +
                                                                  $"RETURNING *;";
    
    internal const string RetrieveBankUser                      = 
                                                                  $"INSERT INTO BankUser (" +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Total)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold)}" +
                                                                  $") VALUES (" +
                                                                  $"@{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, 0, 10, 0, 1000, 0" +
                                                                  $") ON CONFLICT ({nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}) DO NOTHING; " +
                                                                  $"SELECT * FROM BankUser WHERE {nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)} = @{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)};";
    internal const string RetrieveBankUserTimeLimit             = 
                                                                  $"INSERT INTO BankUser (" +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Total)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold)}" +
                                                                  $") VALUES (" +
                                                                  $"@{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, 0, 10, 0, 1000, 0" +
                                                                  $") ON CONFLICT ({nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}) DO NOTHING; " +
                                                                  $"SELECT " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes)} " +
                                                                  $"FROM BankUser WHERE {nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)} = @{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)};";
    internal const string RetrieveBankUserTip                   = 
                                                                  $"INSERT INTO BankUser (" +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Total)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold)}" +
                                                                  $") VALUES (" +
                                                                  $"@{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, 0, 10, 0, 1000, 0" +
                                                                  $") ON CONFLICT ({nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}) DO NOTHING; " +
                                                                  $"SELECT " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Total)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Tip_Threshold_For_Gush_Control_Link_Minute)}, " +
                                                                  $"{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Current_Tip_Total_For_Tip_Threshold)} " +
                                                                  $"FROM BankUser WHERE {nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)} = @{nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username)};";
    
    internal const string RetrieveGoogleData                    = $"SELECT * FROM GoogleData LIMIT 1";
    
    internal const string RetrieveGoveeData                     = $"SELECT * FROM GoveeData LIMIT 1";
    internal const string RetrieveListGoveeLights               = $"SELECT * FROM GoveeLights";
    
    internal const string RetrieveJoystickData                  = $"SELECT * FROM JoystickData LIMIT 1";
    internal const string RetrieveListJoystickLatestFollowers   = $"SELECT * FROM JoystickLatestFollower";
    internal const string RetrieveListJoystickLatestSubscribers = $"SELECT * FROM JoystickLatestSubscriber";
    internal const string RetrieveListJoystickLatestTippers     = $"SELECT * FROM JoystickLatestTipper";
    
    internal const string RetrieveListUserBadgeColors           = $"SELECT * FROM UserBadgeColors";
    internal const string RetrieveListUserNameColors            = $"SELECT * FROM UserNameColors";
    
    internal const string RetrieveListUserUnlockColors          = $"SELECT " +
                                                                  $"@usr AS {nameof(ServiceDatabaseUserUnlocks.Username)}, " +
                                                                  $"ARRAY(SELECT {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} FROM userunlockcolors WHERE {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} = @usr ORDER BY 1 ASC) AS {nameof(ServiceDatabaseUserUnlocks.UnlockIds)};";

    internal const string RetrieveListUserUnlockEffects         = $"SELECT " +
                                                                  $"@usr AS {nameof(ServiceDatabaseUserUnlocks.Username)}, " +
                                                                  $"ARRAY(SELECT {nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)} FROM userunlockeffects WHERE {nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} = @usr ORDER BY 1 ASC) AS {nameof(ServiceDatabaseUserUnlocks.UnlockIds)};";

    internal const string RetrieveListUserUnlockModels          = $"SELECT " +
                                                                  $"@usr AS {nameof(ServiceDatabaseUserUnlocks.Username)}, " +
                                                                  $"ARRAY(SELECT {nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)} FROM userunlockmodels WHERE {nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} = @usr ORDER BY 1 ASC) AS {nameof(ServiceDatabaseUserUnlocks.UnlockIds)};";
    
    internal const string RetrieveListUserUnlockTitles          = $"SELECT " +
                                                                  $"@usr AS {nameof(ServiceDatabaseUserUnlocks.Username)}, " +
                                                                  $"ARRAY(SELECT {nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)} FROM userunlocktitles WHERE {nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)} = @usr ORDER BY 1 ASC) AS {nameof(ServiceDatabaseUserUnlocks.UnlockIds)};";
    
    internal const string RetrieveLovenseData                   = $"SELECT * FROM LovenseData LIMIT 1";
    
    internal const string RetrieveSpotifyData                   = $"SELECT * FROM SpotifyData LIMIT 1";
    
    internal const string RetrieveSteamUser                     = $"""
                                                                  WITH inserted AS (
                                                                      INSERT INTO SteamUser (
                                                                          {nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}, 
                                                                          {nameof(ServiceDatabaseSteamUser.SteamUser_Steam_Username)}
                                                                      )
                                                                      VALUES (
                                                                          @{nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}, 
                                                                          ''
                                                                      )
                                                                      ON CONFLICT ({nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}) DO NOTHING
                                                                      RETURNING *
                                                                  )
                                                                  SELECT * FROM inserted
                                                                  UNION ALL
                                                                  SELECT * FROM SteamUser 
                                                                  WHERE {nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)} = @{nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}
                                                                  AND NOT EXISTS (SELECT 1 FROM inserted);
                                                                  """;
    
    internal const string RetrieveUserAvatarSettings            = // 1. Ensure the user has their default model unlocked
                                                                  $"INSERT INTO userunlockmodels ({nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)}, {nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)}) " +
                                                                  $"SELECT @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, 0 " +
                                                                  $"WHERE NOT EXISTS (SELECT 1 FROM userunlockmodels WHERE {nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} = @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)} AND {nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)} = 0); " +

                                                                  // 2. Ensure the user has their default shader unlocked
                                                                  $"INSERT INTO userunlockeffects ({nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)}, {nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)}) " +
                                                                  $"SELECT @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, 0 " +
                                                                  $"WHERE NOT EXISTS (SELECT 1 FROM userunlockeffects WHERE {nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} = @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)} AND {nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)} = 0); " +
                                                                  
                                                                  // 3. Ensure default colors are unlocked
                                                                  $"INSERT INTO userunlockcolors ({nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)}, {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}) " +
                                                                  $"SELECT @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, 21 " +
                                                                  $"WHERE NOT EXISTS (SELECT 1 FROM userunlockcolors WHERE {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} = @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)} AND {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} = 21); " +

                                                                  $"INSERT INTO userunlockcolors ({nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)}, {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}) " +
                                                                  $"SELECT @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, 34 " +
                                                                  $"WHERE NOT EXISTS (SELECT 1 FROM userunlockcolors WHERE {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} = @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)} AND {nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} = 34); " +

                                                                  // 4. Ensure the main settings record exists
                                                                  $"INSERT INTO UserAvatarSettings (" +
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
                                                                  $") SELECT @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}, 21, 34, 0, 21, 21, 21, 21, 0, 0, 0, 0 " +
                                                                  $"WHERE NOT EXISTS (SELECT 1 FROM UserAvatarSettings WHERE {nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)} = @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)}); " +

                                                                  // 5. Select the record
                                                                  $"SELECT * FROM UserAvatarSettings WHERE {nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)} = @{nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username)};";
    
    internal const string RetrieveUserTitle                     = $"SELECT * FROM UserTitles WHERE {nameof(ServiceDatabaseUserTitle.UserTitles_Username)} = @{nameof(ServiceDatabaseUserTitle.UserTitles_Username)};";
    
    internal const string ValidateUserBuyColor                  = $"SELECT " +
                                                                  $"@{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} AS {nameof(ServiceDatabaseValidateUserUnlock.Username)}, " +
                                                                  $"@{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} AS {nameof(ServiceDatabaseValidateUserUnlock.Id)}, " +
                                                                  $"NOT EXISTS(SELECT 1 FROM userunlockcolors WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} = @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} = @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserUnlock.IsValid)};";

    internal const string ValidateUserBuyEffect                 = $"SELECT " +
                                                                  $"@{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} AS {nameof(ServiceDatabaseValidateUserUnlock.Username)}, " +
                                                                  $"@{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)} AS {nameof(ServiceDatabaseValidateUserUnlock.Id)}, " +
                                                                  $"NOT EXISTS(SELECT 1 FROM userunlockeffects WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} = @{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)} = @{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)}) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserUnlock.IsValid)};";

    internal const string ValidateUserBuyModel                  = $"SELECT " +
                                                                  $"@{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} AS {nameof(ServiceDatabaseValidateUserUnlock.Username)}, " +
                                                                  $"@{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)} AS {nameof(ServiceDatabaseValidateUserUnlock.Id)}, " +
                                                                  $"NOT EXISTS(SELECT 1 FROM userunlockmodels WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} = @{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)} = @{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)}) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserUnlock.IsValid)};";
    
    internal const string ValidateUserUnlockColor               = $"SELECT @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} AS {nameof(ServiceDatabaseValidateUserHas.Username)}, " +
                                                                  $"EXISTS(SELECT 1 FROM userunlockcolors WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} = @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} = @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserHas.IsValid)};";

    internal const string ValidateUserUnlockEffectAndColor      = $"SELECT @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} AS {nameof(ServiceDatabaseValidateUserHas.Username)}, " +
                                                                  $"(EXISTS(SELECT 1 FROM userunlockcolors WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} = @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)} = @{nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id)}) " +
                                                                  $"AND EXISTS(SELECT 1 FROM userunlockeffects WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} = @{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)} = @{nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id)})) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserHas.IsValid)};";

    internal const string ValidateUserUnlockModel               = $"SELECT @{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} AS {nameof(ServiceDatabaseValidateUserHas.Username)}, " +
                                                                  $"EXISTS(SELECT 1 FROM userunlockmodels WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} = @{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)} = @{nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id)}) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserHas.IsValid)};";
    
    internal const string ValidateUserUnlockTitle               = $"SELECT @{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)} AS {nameof(ServiceDatabaseValidateUserHas.Username)}, " +
                                                                  $"EXISTS(SELECT 1 FROM userunlocktitles WHERE " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)} = @{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username)} AND " +
                                                                  $"{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)} = @{nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id)}) " +
                                                                  $"AS {nameof(ServiceDatabaseValidateUserHas.IsValid)};";
}