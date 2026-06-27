
using Overlay.Core.Contents;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.Effects.Rainbows;
using Overlay.Core.Contents.Nameplates;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Databases.Tasks.Validates;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Lovenses;
using Overlay.Core.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static partial class ServiceJoystickWebSocketPayloadChatHandler
{
    private static void OnRetrievedBankUser(
        ServiceDatabaseTaskRetrievedBankUser serviceDatabaseTaskRetrievedBankUser
    )
    {
        var user     = serviceDatabaseTaskRetrievedBankUser.Result;
        var minutes  = user.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes;
        var limit    = user.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes;
        var username = user.BankUser_Joystick_Username;

        ServiceJoystickWebSocketPayloadChatHandler.s_bankUserLimits[key: username] = limit;
        
        var withdrawalTotalCurrent = ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals.GetValueOrDefault(
            key:          username, 
            defaultValue: 0
        );
        var remainingMinutes       = limit - withdrawalTotalCurrent;
        
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingWithdrawals.Remove(
                key:   username, 
                value: out var amount
            ) is false
        )
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏦 @{username}, you have {minutes} Gush Control Link minute{(minutes != 1 ? "s" : string.Empty)} in the bank & " +
                             $"a maximum limit of {limit} minute{(limit != 1 ? "s" : string.Empty)} available per stream. " +
                             $"{remainingMinutes} minute{(remainingMinutes != 1 ? "s" : string.Empty)} can still be withdrawn this stream."
                );
            }

            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedBanks
            );
            return;
        }
        
        var totalWithdrawnSoFar = ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals.GetValueOrDefault(
            key:          username, 
            defaultValue: 0
        );
        if (totalWithdrawnSoFar + amount > limit)
        {
            var actualAllowed = limit - totalWithdrawnSoFar;
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    $"🛑 @{username} - Your withdrawal exceeds your limit. You only have {actualAllowed} minute{(actualAllowed != 1 ? "s" : string.Empty)} remaining."
                );
            }
            return;
        }

        if (remainingMinutes < amount)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏦 @{username}, you don't have enough remaining minutes. You have {remainingMinutes} minute{(remainingMinutes != 1 ? "s" : string.Empty)} remaining."
                );
            }
            return;
        }

        if (minutes < amount)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏦 @{username}, you don't have enough banked minutes. Your current balance is {minutes} minute{(minutes != 1 ? "s" : string.Empty)}."
                );
            }
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals.TryAdd(
            key:   username, 
            value: 0
        );
        ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals[key: username] += amount;

        var serviceDatabaseTaskSqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Lovense_Gush_Control_Link_Time_In_Minutes), 
                value:         amount
            )
        };

        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.WithdrawTimeForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskSqlParameters
                );

                withdrawalTotalCurrent                 = ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals.GetValueOrDefault(
                    key:          username,
                    defaultValue: 0
                );
                var remainingMinutesBeforeLimitReached = limit - withdrawalTotalCurrent;
                var totalRemainingMinutesInBank        = minutes - amount;
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🏦 @{username} withdrew {amount} minute{(amount != 1 ? "s" : string.Empty)}. " +
                                 $"Your remaining balance is {totalRemainingMinutesInBank} minute{(totalRemainingMinutesInBank != 1 ? "s" : string.Empty)}. " +
                                 $"You now have control of SmoothDagger."
                    );
                }
                
                var gushControlLink = ServiceLovenseGushControlLinkRandomizer.GetRandomGushControlLink();
                var whisper         = $"🎁 Gush Control Link: {gushControlLink}";
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotWhisper(
                    username: username,
                    whisper:  whisper
                );
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotWhisper(
                    username: ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername,
                    whisper:  whisper
                );

                ServiceJoystickWebSocketPayloadChatHandler.s_gushControllerUsername = username;
                
                var minutesInMilliseconds = ServiceJoystickWebSocketPayloadChatHandler.ConvertMinutesToMilliseconds(
                    minutes: amount
                );
                var delayInMilliseconds   = minutesInMilliseconds + ServiceJoystickWebSocketPayloadChatHandler.c_delayGushControlLinkSetupInMilliseconds;
                await Task.Delay(
                    millisecondsDelay: delayInMilliseconds
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.s_gushControllerUsername = string.Empty;
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🏦 {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} is no longer being controlled by {username}. " +
                                 $"You have {remainingMinutesBeforeLimitReached} minute{(remainingMinutesBeforeLimitReached != 1 ? "s" : string.Empty)} left available this stream. " +
                                 $"The !bank withdraw command is now available."
                    );
                }
            }
        );
    }
    
    private static void OnRetrievedBankUserTimeLimit(
        ServiceDatabaseTaskRetrievedBankUserTimeLimit serviceDatabaseTaskRetrievedBankUserTimeLimit
    )
    {
        var user     = serviceDatabaseTaskRetrievedBankUserTimeLimit.Result;
        var limit    = user.BankUser_Lovense_Gush_Control_Link_Time_Limit_In_Minutes;
        var username = user.BankUser_Joystick_Username;

        ServiceJoystickWebSocketPayloadChatHandler.s_bankUserLimits[key: username] = limit;
        
        lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🏦 @{username}, your Gush Control Link time limit has been increased to {limit} minute{(limit != 1 ? "s" : string.Empty)}."
            );
        }
    }

    private static void OnRetrievedSteamUser(
        ServiceDatabaseTaskRetrievedSteamUser serviceDatabaseTaskRetrievedSteamUser
    )
    {
        var result           = serviceDatabaseTaskRetrievedSteamUser.Result;
        var joystickUsername = result.SteamUser_Joystick_Username;
        var steamUsername    = result.SteamUser_Steam_Username;

        if (steamUsername != string.Empty)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"☁️ @{joystickUsername} - your Steam username is currently set to {steamUsername}."
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"☁️ @{joystickUsername} - your Steam username has not been set yet."
                );
            }
        }
    }
    
    private static void OnRetrievedUserBadgeColors(
        ServiceDatabaseTaskRetrievedListUserBadgeColors serviceDatabaseTaskRetrievedListUserBadgeColors
    )
    {
        var result = serviceDatabaseTaskRetrievedListUserBadgeColors.Result;
        foreach (var userBadgeColor in result)
        {
            var username = userBadgeColor.UserBadgeColors_Username;
            var color    = (ServiceColorInterpolatorColorMode) userBadgeColor.UserBadgeColors_Color_Id;
            
            ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors[key: username] = color;

            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                NameplateIcon.SetInitialIconColor(
                    color: color
                );
                EffectBackgroundGeometry.SetInitialGeometryColorInverse(
                    color: color
                );
                EffectRainbowStripe.UpdateGlobalColorInverse(
                    color: color
                );
            }
        }
    }
    
    private static void OnValidatedUserHasColor_Avatar(
        ServiceDatabaseTaskValidatedUserHasColor serviceDatabaseTaskValidatedUserHasColor
    )
    {
        var result   = serviceDatabaseTaskValidatedUserHasColor.Result;
        var username = result.Username;
        var isValid  = result.IsValid;
        
        var avatarUpdateData = ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers[key: username];
        if (isValid is true)
        {
            if (avatarUpdateData.Base is not null)
            {
                BackgroundAvatarsController.Instance.UpdateAvatarBase(
                    username:                          username,
                    serviceColorInterpolatorColorMode: avatarUpdateData.Base.Value
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.UpdateUserAvatarBase(
                    username:                          username,
                    serviceColorInterpolatorColorMode: avatarUpdateData.Base.Value       
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"👾 @{username} - Your avatar base has been updated to {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Base.Value)}."
                    );
                }
                
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedAvatar
                );
            }
            else if (avatarUpdateData.Outline is not null)
            {
                BackgroundAvatarsController.Instance.UpdateAvatarOutline(
                    username:                          username,
                    serviceColorInterpolatorColorMode: avatarUpdateData.Outline.Value
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.UpdateUserAvatarOutline(
                    username:                          username,
                    serviceColorInterpolatorColorMode: avatarUpdateData.Outline.Value       
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"👾 @{username} - Your avatar outline color has been updated to {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Outline.Value)}."
                    );
                }
                
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedAvatar
                );
            }
            else
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @SmoothDagger - Avatar update data contained null data for {username}."
                    );
                }
            }
        }
        else if (avatarUpdateData.Base is not null)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following avatar color: {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Base.Value)}"
                );
            }
        }
        else if (avatarUpdateData.Outline is not null)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following avatar color: {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Outline.Value)}"
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.Remove(
            key: username
        );
    }
    
    private static void OnValidatedUserHasColor_Badge(
        ServiceDatabaseTaskValidatedUserHasColor serviceDatabaseTaskValidatedUserHasColor
    )
    {
        var result   = serviceDatabaseTaskValidatedUserHasColor.Result;
        var username = result.Username;
        var isValid  = result.IsValid;
        
        var color = ServiceJoystickWebSocketPayloadChatHandler.s_pendingBadgeColorUpdateUsers[key: username];
        if (isValid is true)
        {
            if (color is ServiceColorInterpolatorColorMode.Transition)
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors.Remove(
                        key: username
                    );
                }
                
                Task.Run(
                    function: async () =>
                    {
                        await Task.Delay(
                            millisecondsDelay: 200
                        );
                        
                        Chat.UpdateChatMessageBadgeColorForInstances(
                            username:       username,
                            hasCustomColor: false,
                            customColor:    color
                        );
                    }
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.RemoveUserBadgeColor(
                    username: username
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🏷️ @{username} - Your badge color has been set to Default."
                    );
                }
            }
            else
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors[key: username] = color;
                }
                
                Task.Run(
                    function: async () =>
                    {
                        await Task.Delay(
                            millisecondsDelay: 200
                        );
                        
                        Chat.UpdateChatMessageBadgeColorForInstances(
                            username:       username,
                            hasCustomColor: true,
                            customColor:    color
                        );
                    }
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.UpdateUserBadgeColor(
                    username:                          username,
                    serviceColorInterpolatorColorMode: color
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🏷️ @{username} - Your badge color has been set to {EnumHelper.ToSpacedPascalCase(color)}."
                    );
                }
            }

            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedBadge
            );

            BackgroundAvatarsController.Instance.UpdateAvatarNameplateBadgeColor(
                username:                          username,
                serviceColorInterpolatorColorMode: color
            );
            
            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                NameplateIcon.UpdateIconColor(
                    color: color
                );
                EffectBackgroundGeometry.UpdateGeometryColorInverse(
                    color: color
                );
                EffectRainbowStripe.UpdateGlobalColorInverse(
                    color: color
                );
            }
        }
        else if (color is ServiceColorInterpolatorColorMode.Transition)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors.Remove(
                    key: username
                );
            }
                
            Task.Run(
                function: async () =>
                {
                    await Task.Delay(
                        millisecondsDelay: 200
                    );
                        
                    Chat.UpdateChatMessageBadgeColorForInstances(
                        username:       username,
                        hasCustomColor: false,
                        customColor:    color
                    );
                }
            );
                
            ServiceJoystickWebSocketPayloadChatHandler.RemoveUserBadgeColor(
                username: username
            );
            
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏷️ @{username} - Your badge color has been set to Default."
                );
            }
            
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedBadge
            );
            
            BackgroundAvatarsController.Instance.UpdateAvatarNameplateBadgeColor(
                username:                          username,
                serviceColorInterpolatorColorMode: color
            );
            
            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                NameplateIcon.UpdateIconColor(
                    color: color
                );
                EffectBackgroundGeometry.UpdateGeometryColorInverse(
                    color: color
                );
                EffectRainbowStripe.UpdateGlobalColorInverse(
                    color: color
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following color: {EnumHelper.ToSpacedPascalCase(color)}"
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingBadgeColorUpdateUsers.Remove(
            key: username
        );
    }
        
    private static void OnValidatedUserHasColor_Name(
        ServiceDatabaseTaskValidatedUserHasColor serviceDatabaseTaskValidatedUserHasColor
    )
    {
        var result   = serviceDatabaseTaskValidatedUserHasColor.Result;
        var username = result.Username;
        var isValid  = result.IsValid;
        
        var color = ServiceJoystickWebSocketPayloadChatHandler.s_pendingNameColorUpdateUsers[key: username];
        if (isValid is true)
        {
            if (color is ServiceColorInterpolatorColorMode.Transition)
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors.Remove(
                        key: username
                    );
                }

                Task.Run(
                    function: async () =>
                    {
                        await Task.Delay(
                            millisecondsDelay: 200
                        );
                        
                        Chat.UpdateChatMessageNameColorForInstances(
                            username:       username,
                            hasCustomColor: false,
                            customColor:    color
                        );
                    }
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.RemoveUserNameColor(
                    username: username
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserClearedNames
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🏷️ @{username} - Your name color has been set to Default."
                    );
                }
            }
            else
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors[key: username] = color;
                }
                
                Task.Run(
                    function: async () =>
                    {
                        await Task.Delay(
                            millisecondsDelay: 200
                        );
                        
                        Chat.UpdateChatMessageNameColorForInstances(
                            username:       username,
                            hasCustomColor: true,
                            customColor:    color
                        );
                    }
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.UpdateUserNameColor(
                    username:                          username,
                    serviceColorInterpolatorColorMode: color
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🏷️ @{username} - Your name color has been set to {EnumHelper.ToSpacedPascalCase(color)}."
                    );
                }
            }
            
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedName
            );
            
            BackgroundAvatarsController.Instance.UpdateAvatarNameplateNameColor(
                username:                          username,
                serviceColorInterpolatorColorMode: color
            );
            
            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                Nameplate.UpdateNameColor(
                    color: color
                );
                EffectBackgroundGeometry.UpdateGeometryColorNormal(
                    color: color
                );
                EffectRainbowStripe.UpdateGlobalColorNormal(
                    color: color
                );
            }
        }
        else if (color is ServiceColorInterpolatorColorMode.Transition)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors.Remove(
                    key: username
                );
            }

            Task.Run(
                function: async () =>
                {
                    await Task.Delay(
                        millisecondsDelay: 200
                    );
                        
                    Chat.UpdateChatMessageNameColorForInstances(
                        username:       username,
                        hasCustomColor: false,
                        customColor:    color
                    );
                }
            );
                
            ServiceJoystickWebSocketPayloadChatHandler.RemoveUserNameColor(
                username: username
            );
            
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏷️ @{username} - Your name color has been set to Default."
                );
            }
            
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedName
            );
            
            BackgroundAvatarsController.Instance.UpdateAvatarNameplateNameColor(
                username:                          username,
                serviceColorInterpolatorColorMode: color
            );
            
            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                NameplateIcon.UpdateIconColor(
                    color: color
                );
                EffectBackgroundGeometry.UpdateGeometryColorInverse(
                    color: color
                );
                EffectRainbowStripe.UpdateGlobalColorInverse(
                    color: color
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following color: {EnumHelper.ToSpacedPascalCase(color)}"
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingNameColorUpdateUsers.Remove(
            key: username
        );
    }
    
    private static void OnValidatedUserHasEffectAndColor(
        ServiceDatabaseTaskValidatedUserHasEffectAndColor serviceDatabaseTaskValidatedUserHasEffectAndColor
    )
    {
        var result   = serviceDatabaseTaskValidatedUserHasEffectAndColor.Result;
        var username = result.Username;
        var isValid  = result.IsValid;
        
        var avatarUpdateData = ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers[key: username];
        if (isValid is true)
        {
            if (
                avatarUpdateData.Color  is not null &&
                avatarUpdateData.Effect is not null &&
                avatarUpdateData.Slot   is not null
            )
            {
                BackgroundAvatarsController.Instance.UpdateAvatarEffect(
                    username:                           username,
                    effectBackgroundAvatarShaderSlot:   avatarUpdateData.Slot.Value,
                    effectBackgroundAvatarShaderEffect: avatarUpdateData.Effect.Value
                );
                BackgroundAvatarsController.Instance.UpdateAvatarEffectColor(
                    username:                          username,
                    effectBackgroundAvatarShaderSlot:  avatarUpdateData.Slot.Value,
                    serviceColorInterpolatorColorMode: avatarUpdateData.Color.Value
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.UpdateUserShaderEffectAndColor(
                    username:                           username,
                    effectBackgroundAvatarShaderSlot:   avatarUpdateData.Slot.Value,
                    effectBackgroundAvatarShaderEffect: avatarUpdateData.Effect.Value,
                    serviceColorInterpolatorColorMode:  avatarUpdateData.Color.Value
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedAvatar
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"👾 @{username} - Your avatar effect & color for shader{(int) avatarUpdateData.Slot} has been updated to {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Effect.Value)} & {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Color.Value)}."
                    );
                }
            }
            else
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @SmoothDagger - Avatar update data contained null data for {username}."
                    );
                }
            }
        }
        else if (
            avatarUpdateData.Color  is not null && 
            avatarUpdateData.Effect is not null
        )
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following avatar effect or color: {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Effect.Value)}, {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Color.Value)}"
                );
            }
        }

        ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.Remove(
            key: username
        );
    }
    
    private static void OnValidatedUserHasModel(
        ServiceDatabaseTaskValidatedUserHasModel serviceDatabaseTaskValidatedUserHasModel
    )
    {
        var result   = serviceDatabaseTaskValidatedUserHasModel.Result;
        var username = result.Username;
        var isValid  = result.IsValid;
        
        var avatarUpdateData = ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers[key: username];
        if (isValid is true)
        {
            if (avatarUpdateData.Model is not null)
            {
                BackgroundAvatarsController.Instance.UpdateAvatarModel(
                    username:                          username,
                    effectBackgroundAvatarShaderModel: avatarUpdateData.Model.Value
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.UpdateUserAvatarModel(
                    username:                          username,
                    effectBackgroundAvatarShaderModel: avatarUpdateData.Model.Value
                );
                
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"👾 @{username} - Your avatar model has been updated to {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Model.Value)}."
                    );
                }
                
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedAvatar
                );
            }
            else
            {
                lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @SmoothDagger - Avatar update data contained null data for {username}."
                    );
                }
            }
        }
        else if (avatarUpdateData.Model is not null)
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following avatar model: {EnumHelper.ToSpacedPascalCase(avatarUpdateData.Model.Value)}"
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.Remove(
            key: username
        );
    }
    
    private static void OnValidatedUserHasTitle(
        ServiceDatabaseTaskValidatedUserHasTitle serviceDatabaseTaskValidatedUserHasTitle
    )
    {
        var result   = serviceDatabaseTaskValidatedUserHasTitle.Result;
        var username = result.Username;
        var isValid  = result.IsValid;
        
        var title     = ServiceJoystickWebSocketPayloadChatHandler.s_pendingTitleUpdateUsers[key: username];
        var titleName = ServiceAchievement.GetTitleNameFromAchievementTitle(
            serviceAchievementTitle: title
        );
        if (isValid is true)
        {
            BackgroundAvatarsController.Instance.UpdateAvatarNameplateTitle(
                username: username,
                title:    titleName
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.UpdateUserAvatarNameplateTitle(
                username:                username,
                serviceAchievementTitle: title
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedTitle
            );
            
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏆 @{username} - Your title has been updated to {titleName}."
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You do not own the following title: {titleName}"
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingTitleUpdateUsers.Remove(
            key: username
        );
    }
    
    private static void OnRetrievedUserNameColors(
        ServiceDatabaseTaskRetrievedListUserNameColors serviceDatabaseTaskRetrievedListUserNameColors
    )
    {
        var result = serviceDatabaseTaskRetrievedListUserNameColors.Result;
        foreach (var userNameColor in result)
        {
            var username = userNameColor.UserNameColors_Username;
            var color    = (ServiceColorInterpolatorColorMode) userNameColor.UserNameColors_Color_Id;
            
            ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors[key: username] = color;
            
            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                Nameplate.SetInitialNameColor(
                    color: color
                );
                EffectBackgroundGeometry.SetInitialGeometryColorNormal(
                    color: color
                );
                EffectRainbowStripe.UpdateGlobalColorNormal(
                    color: color
                );
            }
        }
    }

    private static void OnRetrievedUserUnlockColors(
        ServiceDatabaseTaskRetrievedUserUnlocks serviceDatabaseTaskRetrievedUserUnlocks
    )
    {
        var result       = serviceDatabaseTaskRetrievedUserUnlocks.Result;
        var username     = result.Username;
        var unlockedIds  = result.UnlockIds;
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedUnlocks
        );

        Task.Run(
            function: async () =>
            {
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                var chunks             = unlockedIds.Chunk(
                    size: 10
                );
                foreach (var chunk in chunks)
                {
                    var names = chunk.Select(
                        selector: id => 
                            EnumHelper.ToSpacedPascalCase(
                                enumValue: (ServiceColorInterpolatorColorMode) id
                            )
                    );

                    serviceJoystickBot.SendWhisper(
                        username: username,
                        message: $"👾 You own the following colors: {string.Join(", ", names)}"
                    );

                    await Task.Delay(
                        millisecondsDelay: 200
                    );
                }
            }
        );
    }
    
    private static void OnRetrievedUserUnlockEffects(
        ServiceDatabaseTaskRetrievedUserUnlocks serviceDatabaseTaskRetrievedUserUnlocks
    )
    {
        var result       = serviceDatabaseTaskRetrievedUserUnlocks.Result;
        var username     = result.Username;
        var unlockedIds  = result.UnlockIds;
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedUnlocks
        );

        Task.Run(
            function: async () =>
            {
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                var chunks             = unlockedIds.Chunk(
                    size: 10
                );
                foreach (var chunk in chunks)
                {
                    var names = chunk.Select(
                        selector: id => 
                            EnumHelper.ToSpacedPascalCase(
                                enumValue: (EffectBackgroundAvatarShaderEffect) id
                            )
                    );

                    serviceJoystickBot.SendWhisper(
                        username: username,
                        message: $"👾 You own the following effects: {string.Join(", ", names)}"
                    );

                    await Task.Delay(
                        millisecondsDelay: 200
                    );
                }
            }
        );
    }
    
    private static void OnRetrievedUserUnlockModels(
        ServiceDatabaseTaskRetrievedUserUnlocks serviceDatabaseTaskRetrievedUserUnlocks
    )
    {
        var result       = serviceDatabaseTaskRetrievedUserUnlocks.Result;
        var username     = result.Username;
        var unlockedIds  = result.UnlockIds;
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedUnlocks
        );

        Task.Run(
            function: async () =>
            {
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                var chunks             = unlockedIds.Chunk(
                    size: 10
                );
                foreach (var chunk in chunks)
                {
                    var names = chunk.Select(
                        selector: id => 
                            EnumHelper.ToSpacedPascalCase(
                                enumValue: (EffectBackgroundAvatarShaderModel) id
                            )
                    );

                    serviceJoystickBot.SendWhisper(
                        username: username,
                        message: $"👾 You own the following models: {string.Join(", ", names)}"
                    );

                    await Task.Delay(
                        millisecondsDelay: 200
                    );
                }
            }
        );
    }
    
    private static void OnRetrievedUserUnlockTitles(
        ServiceDatabaseTaskRetrievedUserUnlocks serviceDatabaseTaskRetrievedUserUnlocks
    )
    {
        var result       = serviceDatabaseTaskRetrievedUserUnlocks.Result;
        var username     = result.Username;
        var unlockedIds  = result.UnlockIds;
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCheckedUnlocks
        );

        Task.Run(
            function: async () =>
            {
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                var chunks             = unlockedIds.Chunk(
                    size: 10
                );
                foreach (var chunk in chunks)
                {
                    var names = chunk.Select(
                        selector: id =>
                            ServiceAchievement.GetTitleNameFromAchievementTitle(
                                serviceAchievementTitle: (ServiceAchievementTitle) id
                            )
                    );

                    serviceJoystickBot.SendWhisper(
                        username: username,
                        message: $"🏆 You own the following titles: {string.Join(", ", names)}"
                    );

                    await Task.Delay(
                        millisecondsDelay: 200
                    );
                }
            }
        );
    }
    
    private static void OnValidatedUserUnlockColor(
        ServiceDatabaseTaskValidatedUserUnlockColor serviceDatabaseTaskValidatedUserUnlockColor
    )
    {
        var result   = serviceDatabaseTaskValidatedUserUnlockColor.Result;
        var isValid  = result.IsValid;
        var username = result.Username;
        var color    = (ServiceColorInterpolatorColorMode) result.Id;

        if (isValid is true)
        {
            var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
                new(
                    parameterName: nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username), 
                    value:         username
                ),
                new(
                    parameterName: nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id), 
                    value:         (int) color
                ),
            };
            ServiceDatabase.ExecuteTaskNonQuery(
                serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.BuyUserColor, 
                serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
            );
            
            if (
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers.TryGetValue(
                    key:   username, 
                    value: out var value
                )
            )
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers[key: username] = --value;
                if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers[key: username] <= 0)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers.Remove(
                        key: username
                    );
                }
            }

            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"👾 @{username} - You unlocked the {EnumHelper.ToSpacedPascalCase(color)} color! Check the !avatar commands below in the bio to use it!"
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You already own the {EnumHelper.ToSpacedPascalCase(color)} color. Please try again."
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
            item: username
        );
    }

    private static void OnValidatedUserUnlockEffect(
        ServiceDatabaseTaskValidatedUserUnlockEffect serviceDatabaseTaskValidatedUserUnlockEffect
    )
    {
        var result   = serviceDatabaseTaskValidatedUserUnlockEffect.Result;
        var isValid  = result.IsValid;
        var username = result.Username;
        var effect   = (EffectBackgroundAvatarShaderEffect) result.Id;

        if (isValid is true)
        {
            var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
                new(
                    parameterName: nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username), 
                    value:         username
                ),
                new(
                    parameterName: nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id), 
                    value:         (int) effect
                ),
            };
            ServiceDatabase.ExecuteTaskNonQuery(
                serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.BuyUserEffect, 
                serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
            );
            
            if (
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers.TryGetValue(
                    key:   username, 
                    value: out var value
                )
            )
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers[key: username] = --value;
                if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers[key: username] <= 0)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers.Remove(
                        key: username
                    );
                }
            }
            
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"👾 @{username} - You unlocked the {EnumHelper.ToSpacedPascalCase(effect)} effect! Check the !avatar commands below in the bio to use it!"
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You already own the {EnumHelper.ToSpacedPascalCase(effect)} effect. Please try again."
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
            item: username
        );
    }

    private static void OnValidatedUserUnlockModel(
        ServiceDatabaseTaskValidatedUserUnlockModel serviceDatabaseTaskValidatedUserUnlockModel
    )
    {
        var result   = serviceDatabaseTaskValidatedUserUnlockModel.Result;
        var isValid  = result.IsValid;
        var username = result.Username;
        var model    = (EffectBackgroundAvatarShaderModel) result.Id;

        if (isValid is true)
        {
            var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
                new(
                    parameterName: nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Username), 
                    value:         username
                ),
                new(
                    parameterName: nameof(ServiceDatabaseUserUnlockModel.UserUnlockModels_Model_Id), 
                    value:         (int) model
                ),
            };
            ServiceDatabase.ExecuteTaskNonQuery(
                serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.BuyUserModel, 
                serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
            );

            if (
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers.TryGetValue(
                    key:   username, 
                    value: out var value
                )
            )
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers[key: username] = --value;
                if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers[key: username] <= 0)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers.Remove(
                        key: username
                    );
                }
            }
            
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"👾 @{username} - You unlocked the {EnumHelper.ToSpacedPascalCase(model)} model! Check the !avatar commands below in the bio to use it!"
                );
            }
        }
        else
        {
            lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - You already own the {EnumHelper.ToSpacedPascalCase(model)} model. Please try again."
                );
            }
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
            item: username
        );
    }
}