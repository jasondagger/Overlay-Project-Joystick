
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using System.Collections.Generic;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static partial class ServiceJoystickWebSocketPayloadChatHandler
{
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
    
    private static void RequestBankUser(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         username
            )
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveBankUser, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestCheckUserUnlockColors(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: "usr", 
                value:         username
            ),
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserUnlockColors, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestCheckUserUnlockEffects(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: "usr", 
                value:         username
            ),
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserUnlockEffects, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static void RequestCheckUserUnlockModels(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: "usr", 
                value:         username
            ),
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserUnlockModels, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestUserTitles(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: "usr", 
                value:         username
            ),
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserUnlockTitles, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static void RequestSteamUser(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: $"{nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username)}", 
                value:         username
            ),
        };
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveSteamUser, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserHasColorBadge(
        string                            username,
        ServiceColorInterpolatorColorMode color
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingBadgeColorUpdateUsers.TryAdd(
                key:   username,
                value: color
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending badge color update. Please try again in a moment."
            );
            return;
        }
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasColor_Badge, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserHasColorBase(
        string                            username,
        ServiceColorInterpolatorColorMode color
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.TryAdd(
                key:   username,
                value: new AvatarUpdateData
                {
                    Base = color
                }
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending avatar update. Please try again in a moment."
            );
            return;
        }
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasColor_Avatar, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserHasColorName(
        string                            username,
        ServiceColorInterpolatorColorMode color
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingNameColorUpdateUsers.TryAdd(
                key:   username,
                value: color
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending name color update. Please try again in a moment."
            );
            return;
        }
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasColor_Name, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserHasColorOutline(
        string                            username,
        ServiceColorInterpolatorColorMode color
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.TryAdd(
                key:   username,
                value: new AvatarUpdateData
                {
                    Outline = color
                }
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending avatar update. Please try again in a moment."
            );
            return;
        }
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasColor_Avatar, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static void RequestValidateUserHasEffectAndColor(
        string                             username,
        EffectBackgroundAvatarShaderSlot   shaderSlot,
        ServiceColorInterpolatorColorMode  color,
        EffectBackgroundAvatarShaderEffect effect
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.TryAdd(
                key:   username,
                value: new AvatarUpdateData
                {
                    Color  = color,
                    Effect = effect,
                    Slot   = shaderSlot,
                }
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending avatar update. Please try again in a moment."
            );
            return;
        }
        
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockColor.UserUnlockColors_Color_Id), 
                value:         (int) color
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockEffect.UserUnlockEffects_Effect_Id), 
                value:         (int) effect
            ),
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasEffectAndColor, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static void RequestValidateUserHasModel(
        string                            username,
        EffectBackgroundAvatarShaderModel model
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingAvatarUpdateUsers.TryAdd(
                key:   username,
                value: new AvatarUpdateData
                {
                    Model = model
                }
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending avatar update. Please try again in a moment."
            );
            return;
        }
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasModel, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserHasTitle(
        string                  username,
        ServiceAchievementTitle title
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingTitleUpdateUsers.TryAdd(
                key:   username,
                value: title
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending title update. Please try again in a moment."
            );
            return;
        }
        
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserUnlockTitle.UserUnlockTitles_Title_Id), 
                value:         (int) title
            ),
        };
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserHasTitle, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserUnlockColor(
        string                            username,
        ServiceColorInterpolatorColorMode color
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Add(
            item: username
        );
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserUnlockColor, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static void RequestValidateUserUnlockEffect(
        string                             username,
        EffectBackgroundAvatarShaderEffect effect
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Add(
            item: username
        );
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserUnlockEffect, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
    
    private static void RequestValidateUserUnlockModel(
        string                            username,
        EffectBackgroundAvatarShaderModel model
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Add(
            item: username
        );
        
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
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.ValidateUserUnlockModel, 
            serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }
}