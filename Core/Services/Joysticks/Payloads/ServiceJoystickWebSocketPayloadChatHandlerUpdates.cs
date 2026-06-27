
using Godot;
using Overlay.Core.Contents;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.Effects.Rainbows;
using Overlay.Core.Contents.Nameplates;
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.Breaks;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Databases.Tasks.Validates;
using Overlay.Core.Services.Geminis;
using Overlay.Core.Services.Giveaways;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.TextToSpeeches;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.Hydrations;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Lovenses;
using Overlay.Core.Services.NSFWs;
using Overlay.Core.Services.OBS;
using Overlay.Core.Services.Spotifies;
using Overlay.Core.Services.Stretches;
using Overlay.Core.Services.TeamFortress2s;
using Overlay.Core.Services.WorkOuts;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomNumberGenerator = Godot.RandomNumberGenerator;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static partial class ServiceJoystickWebSocketPayloadChatHandler
{
    private static void UpdateSteamUsername(
        string username,
        string steamUsername
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseSteamUser.SteamUser_Joystick_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseSteamUser.SteamUser_Steam_Username), 
                value:         steamUsername
            ),
        };
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateSteamUser, 
            serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
        );
    }

    private static void UpdateUserAvatarBase(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id), 
                value:         (int) serviceColorInterpolatorColorMode
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateUserAvatarBase, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private static void UpdateUserAvatarModel(
        string                            username,
        EffectBackgroundAvatarShaderModel effectBackgroundAvatarShaderModel
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id), 
                value:        (int) effectBackgroundAvatarShaderModel
             )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateUserAvatarModel, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void UpdateUserAvatarNameplateTitle(
        string                  username,
        ServiceAchievementTitle serviceAchievementTitle
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserTitle.UserTitles_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserTitle.UserTitles_Title_Id), 
                value:        (int) serviceAchievementTitle
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateUserTitle, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private static void UpdateUserAvatarOutline(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id), 
                value:         (int) serviceColorInterpolatorColorMode
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateUserAvatarOutline, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void UpdateUserBadgeColor(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserBadgeColor.UserBadgeColors_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserBadgeColor.UserBadgeColors_Color_Id), 
                value:         (int) serviceColorInterpolatorColorMode
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateUserBadgeColor, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void UpdateUserNameColor(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserNameColor.UserNameColors_Username), 
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserNameColor.UserNameColors_Color_Id), 
                value:         (int) serviceColorInterpolatorColorMode
            )
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.UpdateUserNameColor, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private static void UpdateUserShaderEffectAndColor(
        string                             username,
        EffectBackgroundAvatarShaderSlot   effectBackgroundAvatarShaderSlot,
        EffectBackgroundAvatarShaderEffect effectBackgroundAvatarShaderEffect,
        ServiceColorInterpolatorColorMode  serviceColorInterpolatorColorMode
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter>()
        {
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username),
                value: username
            ),
        };
        
        ServiceDatabaseTaskNonQueryType serviceDatabaseTaskNonQueryType;
        switch (effectBackgroundAvatarShaderSlot)
        {
            case EffectBackgroundAvatarShaderSlot.ShaderSlot0:
                serviceDatabaseTaskNonQueryType = ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader0EffectAndColor;
                serviceDatabaseTaskNpgsqlParameters.AddRange(
                    collection: new List<ServiceDatabaseTaskNpgsqlParameter>()
                    {
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id),
                            value:         (int) serviceColorInterpolatorColorMode
                        ),
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id),
                            value:         (int) effectBackgroundAvatarShaderEffect
                        ),
                    }
                );
                break;
            
            case EffectBackgroundAvatarShaderSlot.ShaderSlot1:
                serviceDatabaseTaskNonQueryType = ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader1EffectAndColor;
                serviceDatabaseTaskNpgsqlParameters.AddRange(
                    collection: new List<ServiceDatabaseTaskNpgsqlParameter>()
                    {
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id),
                            value:         (int) serviceColorInterpolatorColorMode
                        ),
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id),
                            value:         (int) effectBackgroundAvatarShaderEffect
                        ),
                    }
                );
                break;
            
            case EffectBackgroundAvatarShaderSlot.ShaderSlot2:
                serviceDatabaseTaskNonQueryType = ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader2EffectAndColor;
                serviceDatabaseTaskNpgsqlParameters.AddRange(
                    collection: new List<ServiceDatabaseTaskNpgsqlParameter>()
                    {
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id),
                            value:         (int) serviceColorInterpolatorColorMode
                        ),
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id),
                            value:         (int) effectBackgroundAvatarShaderEffect
                        ),
                    }
                );
                break;
            
            case EffectBackgroundAvatarShaderSlot.ShaderSlot3:
                serviceDatabaseTaskNonQueryType = ServiceDatabaseTaskNonQueryType.UpdateUserAvatarShader3EffectAndColor;
                serviceDatabaseTaskNpgsqlParameters.AddRange(
                    collection: new List<ServiceDatabaseTaskNpgsqlParameter>()
                    {
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id),
                            value:         (int) serviceColorInterpolatorColorMode
                        ),
                        new(
                            parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id),
                            value:         (int) effectBackgroundAvatarShaderEffect
                        ),
                    }
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @SmoothDagger - Invalid Shader Slot found in {nameof(ServiceJoystickWebSocketPayloadChatHandler)}.{nameof(ServiceJoystickWebSocketPayloadChatHandler.UpdateUserShaderEffectAndColor)}()"
                );
                return;
        }
        
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
}