
using Godot;
using Overlay.Core.Contents;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.Mappings;
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Breaks;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Geminis;
using Overlay.Core.Services.Giveaways;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.Hydrations;
using Overlay.Core.Services.NSFWs;
using Overlay.Core.Services.OBS;
using Overlay.Core.Services.Spotifies;
using Overlay.Core.Services.Stretches;
using Overlay.Core.Services.TeamFortress2s;
using Overlay.Core.Services.WorkOuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomNumberGenerator = Godot.RandomNumberGenerator;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static partial class ServiceJoystickWebSocketPayloadChatHandler
{
    internal static void HandleBotCommands(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = payloadMessage.Text;
        if (
            message.StartsWith(
                value: '!'
            ) is false
        )
        {
            return;
        }
        
        var author       = payloadMessage.Author;
        var username     = author.Username;
        var isModerator  = author.IsModerator;
        var isSubscriber = author.IsSubscriber;
        var isStreamer   = author.IsStreamer;

        var commandSplit = message.Split(
            separator: ' ',
            count:     2
        );
        var command       = commandSplit[0].ToLower();
        var parameters    = commandSplit.Length > 1 ? commandSplit[1].ToLower() : string.Empty;
        var rawParameters = commandSplit.Length > 1 ? commandSplit[1] : string.Empty;

        lock (ServiceJoystickWebSocketPayloadChatHandler.s_errorMessageLock)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_isCommandAnErrorForSmoothDagger = false;
            
            switch (command)
            {
                case "!ask":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAsk(
                        username: username,
                        message:  parameters
                    );
                    break;
                
                case "!avatar":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatar(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!badge":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandBadge(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!bank":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandBank(
                        username:   username,
                        parameters: rawParameters
                    );
                    break;
                
                case "!break":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandBreak(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!claim":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandClaim(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!dropin":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandDropin(
                        username:   username,
                        isStreamer: isStreamer
                    );
                    break;
                
                case "!focus":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandFocus(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!giveaway":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandGiveaway(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!hydrate":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandHydrate(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!l":
                case "!layout":
                case "!lo":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandLayout(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!lights":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandLights(
                        username:     username,
                        parameters:   parameters,
                        isModerator:  isModerator,
                        isStreamer:   isStreamer,
                        isSubscriber: isSubscriber
                    );
                    break;
                
                case "!name":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandName(
                        username:   username,
                        parameters: parameters
                    );
                    break;

                case "!nsfw":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandNSFW(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!obs":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandOBS(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!paper":
                case "!rock":
                case "!scissors":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandRockPaperScissors(
                        username:   username,
                        command:    command,
                        parameters: parameters
                    );
                    break;
                
                case "!spin":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSpin(
                        username: username
                    );
                    break;
                
                case "!dice":
                case "!roll":
                case "!rollthedice":
                case "!rtd":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandRollTheDice(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!services":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandServices(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!sfx":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSFX(
                        username:     username,
                        parameters:   parameters,
                        isModerator:  isModerator,
                        isStreamer:   isStreamer,
                        isSubscriber: isSubscriber
                    );
                    break;
                
                case "!song":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSong(
                        username:     username,
                        parameters:   parameters,
                        isModerator:  isModerator,
                        isStreamer:   isStreamer,
                        isSubscriber: isSubscriber
                    );
                    break;
                
                case "!steam":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSteam(
                        username:   username,
                        parameters: rawParameters
                    );
                    break;
                
                case "!stretch":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandStretch(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!test":
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - There's nothing to test today."
                    );
                    break;
                
                case "!tf2":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandTF2(
                        username:     username,
                        parameters:   parameters,
                        isModerator:  isModerator,
                        isStreamer:   isStreamer,
                        isSubscriber: isSubscriber
                    );
                    break;

                case "!title":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandTitle(
                        username:     username,
                        parameters:   parameters
                    );
                    break;
                
                case "!titles":
                    ServiceJoystickWebSocketPayloadChatHandler.RequestUserTitles(
                        username: username
                    );
                    break;
                
                case "!unlock":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlock(
                        username:      username,
                        parameters:    parameters,
                        rawParameters: rawParameters
                    );
                    break;
                
                case "!walk":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandWalk(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                case "!workout":
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandWorkOut(
                        username:   username,
                        parameters: parameters
                    );
                    break;
                
                // joystick commands
                case "!cleartimers":
                case "!giphy":
                case "!highlight":
                case "!so":
                case "!social":
                case "!timer":
                case "!timers":
                case "!tip":
                case "!tokens":
                case "!uptime":
                case "!whisper":
                case "!wishlist":
                    break;
                
                default:
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - That is not a valid command. Please try again."
                    );
                    break;
            }

            if (username is ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
            {
                var serviceGodots = Services.GetService<ServiceGodots>();
                var serviceAudio  = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
                serviceAudio.PlaySoundAlert(
                    soundAlertType: ServiceJoystickWebSocketPayloadChatHandler.s_isCommandAnErrorForSmoothDagger is true ?
                        ServiceGodotAudio.SoundAlertType.CommandDeny :
                        ServiceGodotAudio.SoundAlertType.CommandAccept
                );
            }
        }
    }
    
    private static void HandleBotCommandAsk(
        string username,
        string message
    )
    {
        if (
            string.IsNullOrWhiteSpace(
                value: message
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !ask command - you must provide a prompt to use the !ask command."
            );
            return;
        }
        
        var serviceGemini = Services.GetService<ServiceGemini>();
        serviceGemini.Ask(
            username: username,
            message:  message
        );
    }

    private static void HandleBotCommandAvatar(
        string username,
        string parameters
    )
    {
        var parts   = parameters.Split(
            separator: ' ',
            options:   StringSplitOptions.RemoveEmptyEntries
        );
        var command = parts.Length > 0 ? parts[0] : string.Empty;
        switch (command)
        {
            case "reset":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatarReset(
                    username: username
                );
                break;
            
            case "set":
                if (parts.Length < 2)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !avatar set parameters - the following parameters are valid: !avatar set [base/model/outline/shader0/shader1/shader2/shader3]"
                    );
                    break;
                }

                var subCommand    = parts[1];
                var subParameters = string.Join(
                    separator: ' ',
                    values:    parts.Skip(
                        count: 2
                    )
                );
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatarSet(
                    username:   username,
                    command:    subCommand,
                    parameters: subParameters
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !avatar parameters - the following parameters are valid: !avatar [reset/set]"
                );
                break;
        }
    }

    private static void HandleBotCommandAvatarReset(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter>()
        {
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Username),
                value:         username
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Base_Color_Id),
                value:         (int) ServiceColorInterpolatorColorMode.Rainbow
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Outline_Color_Id),
                value:         (int) ServiceColorInterpolatorColorMode.White
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Model_Id),
                value:         (int) EffectBackgroundAvatarShaderModel.Human
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Color_Id),
                value:         (int) ServiceColorInterpolatorColorMode.Rainbow
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader0_Effect_Id),
                value:         (int) EffectBackgroundAvatarShaderEffect.Base
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Color_Id),
                value:         (int) ServiceColorInterpolatorColorMode.Rainbow
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader1_Effect_Id),
                value:         (int) EffectBackgroundAvatarShaderEffect.Base
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Color_Id),
                value:         (int) ServiceColorInterpolatorColorMode.Rainbow
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader2_Effect_Id),
                value:         (int) EffectBackgroundAvatarShaderEffect.Base
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Color_Id),
                value:         (int) ServiceColorInterpolatorColorMode.Rainbow
            ),
            new(
                parameterName: nameof(ServiceDatabaseUserAvatarSettings.UserAvatarSettings_Shader3_Effect_Id),
                value:         (int) EffectBackgroundAvatarShaderEffect.Base
            ),
        };
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.ResetUserAvatarSettings, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
        
        BackgroundAvatarsController.Instance.UpdateAvatarBase(
            username:                          username, 
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Rainbow
        );
        BackgroundAvatarsController.Instance.UpdateAvatarOutline(
            username:                          username, 
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.White
        );
        BackgroundAvatarsController.Instance.UpdateAvatarModel(
            username:                          username, 
            effectBackgroundAvatarShaderModel: EffectBackgroundAvatarShaderModel.Human
        );

        var shaderSlots = Enum.GetValues<EffectBackgroundAvatarShaderSlot>();
        foreach (var shaderSlot in shaderSlots)
        {
            BackgroundAvatarsController.Instance.UpdateAvatarEffect(
                username:                           username, 
                effectBackgroundAvatarShaderSlot:   shaderSlot,
                effectBackgroundAvatarShaderEffect: EffectBackgroundAvatarShaderEffect.Base
            );
            BackgroundAvatarsController.Instance.UpdateAvatarEffectColor(
                username:                          username, 
                effectBackgroundAvatarShaderSlot:  shaderSlot,
                serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Rainbow
            );
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserCustomizedAvatar
        );
    }

    private static void HandleBotCommandAvatarSet(
        string username,
        string command,
        string parameters
    )
    {
        switch (command)
        {
            case "base":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatarSetBase(
                    username:  username,
                    colorName: parameters
                );
                break;
            
            case "model":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatarSetModel(
                    username:  username,
                    modelName: parameters
                );
                break;
            
            case "outline":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatarSetOutline(
                    username:  username,
                    colorName: parameters
                );
                break;
            
            case "shader0":
            case "shader1":
            case "shader2":
            case "shader3":
                if (parameters == string.Empty)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !avatar set {command} parameters - the following parameters are valid: !avatar set {command} [effect name], [color name]"
                    );
                    break;
                }
                
                if (
                    int.TryParse(
                        s:      command[6..],
                        result: out var value
                    ) is true
                )
                {
                    var shaderSlot = (EffectBackgroundAvatarShaderSlot) value;
                    ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandAvatarSetShaderParameter(
                        username:         username,
                        shaderSlot:       shaderSlot,
                        shaderParameters: parameters
                    );
                }
                else
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !avatar set shader# - the following parameters are valid: !avatar set [shader0/shader1/shader2/shader3]"
                    );
                }
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !avatar set parameters - the following parameters are valid: !avatar set [base/model/outline/shader0/shader1/shader2/shader3]"
                );
                break;
        }
    }
    
    private static void HandleBotCommandAvatarSetBase(
        string username,
        string colorName
    )
    {
        var color = MappingColorNames.GetColorByColorName(
            colorName: colorName
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !avatar set base parameters - the following parameters are valid: !avatar set base [color name]"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasColorBase(
            username: username,
            color:    color.Value
        );
    }
    
    private static void HandleBotCommandAvatarSetModel(
        string username,
        string modelName
    )
    {
        var model = MappingModelNames.GetModelByModelName(
            modelName: modelName
        );
        if (model is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !avatar set model parameters - the following parameters are valid: !avatar set model [model name]"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasModel(
            username: username,
            model:    model.Value
        );
    }
    
    private static void HandleBotCommandAvatarSetOutline(
        string username,
        string colorName
    )
    {
        var color = MappingColorNames.GetColorByColorName(
            colorName: colorName
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !avatar set outline parameters - the following parameters are valid: !avatar set outline [color name]"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasColorOutline(
            username: username,
            color:    color.Value
        );
    }
    
    private static void HandleBotCommandAvatarSetShaderParameter(
        string                           username,
        EffectBackgroundAvatarShaderSlot shaderSlot,
        string                           shaderParameters
    )
    {
        var shaderValues = shaderParameters.Split(
            separator: ',',
            options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
        );
        if (shaderValues.Length is 2) 
        {
            var color  = MappingColorNames.GetColorByColorName(
                colorName: shaderValues[1]
            );
            if (color.HasValue is false)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !avatar set shader{(int) shaderSlot} parameters - color name does not exist."
                );
                return;
            }
            
            var effect = MappingEffectNames.GetEffectByEffectName(
                effectName: shaderValues[0]
            );
            if (effect.HasValue is false)
            {
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !avatar set shader{(int) shaderSlot} parameters - effect name does not exist."
                );
                return;
            }
            
            ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasEffectAndColor(
                username:   username,
                shaderSlot: shaderSlot,
                color:      color.Value,
                effect:     effect.Value
            );
        }
        else
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !avatar set shader{(int) shaderSlot} parameters - the following parameters are valid: !avatar set shader{(int) shaderSlot} [effect name], [color name]"
            );
        }
    }

    private static void HandleBotCommandBadge(
        string username,
        string parameters
    )
    {
        var color = parameters is "default" ? ServiceColorInterpolatorColorMode.Transition : MappingColorNames.GetColorByColorName(
            colorName: parameters
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !badge parameter - the color name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasColorBadge(
            username: username,
            color:    color.Value
        );
    }
    
    private static void HandleBotCommandBank(
        string username,
        string parameters
    )
    {
        var parts   = parameters.Split(
            separator: ' ',
            options:   StringSplitOptions.RemoveEmptyEntries
        );
        var command = parts.Length > 0 ? parts[0].ToLower() : string.Empty;
        switch (command)
        {
            case "check":
                ServiceJoystickWebSocketPayloadChatHandler.RequestBankUser(
                    username: username
                );
                break;
            
            case "deposit":
                if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !bank deposit user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} has access to this command."
                    );
                    return;
                }
                if (
                    parts.Length < 3 || 
                    int.TryParse(
                        s:      parts[2],
                        result: out var depositAmount
                    ) is false
                )
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !bank deposit parameters - the following parameters are valid: !bank deposit [username] [amount]"
                    );
                    return;
                }
                ServiceJoystickWebSocketPayloadChatHandler.ExecuteDeposit(
                    targetUser: parts[1], 
                    amount:     depositAmount
                );
                break;
            
            case "set":
                if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !bank set user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} has access to this command."
                    );
                    return;
                }
                
                if (
                    parts.Length < 3 || 
                    int.TryParse(
                        s:      parts[2], 
                        result: out var setAmount
                    ) is false
                )
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !bank set parameters - the following parameters are valid: !bank set [username] [amount]"
                    );
                    return;
                }
                ServiceJoystickWebSocketPayloadChatHandler.ExecuteSetBankBalance(
                    targetUser: parts[1], 
                    amount:     setAmount
                );
                break;

            case "withdraw":
                if (ServiceJoystickWebSocketPayloadChatHandler.s_isFocusing is true)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - The !bank withdraw command cannot be used while SmoothDagger is focusing."
                    );
                    return;
                }
                
                if (ServiceJoystickWebSocketPayloadChatHandler.s_isWalking is true)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - The !bank withdraw command cannot be used while SmoothDagger is walking."
                    );
                    return;
                }
                
                if (ServiceJoystickWebSocketPayloadChatHandler.s_gushControllerUsername != string.Empty)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - SmoothDagger is currently being controlled by {ServiceJoystickWebSocketPayloadChatHandler.s_gushControllerUsername}. Please try again in a moment."
                    );
                    return;
                }
                
                var withdrawalTotalCurrent = ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals.GetValueOrDefault(
                    key:          username, 
                    defaultValue: 0
                );

                if (
                    ServiceJoystickWebSocketPayloadChatHandler.s_bankUserLimits.TryGetValue(
                        key:           username,
                        value: out var userLimit
                    ) &&
                    withdrawalTotalCurrent == userLimit
                )
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - You already reached your maximum of {userLimit} Gush Control Link minutes per stream. You can increase this limit using the 🌟 Subscriber or 🥵 NSFW tip menus."
                    );
                    return;
                }
                
                if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingWithdrawals.Count > 0)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - A withdrawal is already in progress. Please try again in a moment."
                    );
                    return;
                }
                
                if (
                    parts.Length < 2 || 
                    int.TryParse(
                        s:      parts[1],
                        result: out var withdrawAmount
                    ) is false
                )
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !bank withdraw parameters - the following parameters are valid: !bank withdraw [amount]"
                    );
                    return;
                }

                if (withdrawAmount < 0)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !bank withdraw parameters - you cannot withdraw a negative amount of time."
                    );
                    return;
                }

                if (
                    ServiceJoystickWebSocketPayloadChatHandler.s_bankUserWithdrawTotals.ContainsKey(
                        key: username
                    )
                )
                {
                    var withdrawTotalNew = withdrawalTotalCurrent + withdrawAmount;
                    if (withdrawTotalNew > userLimit)
                    {
                        var remainingMinutes = userLimit - withdrawalTotalCurrent;
                        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                            message: $"🛑 @{username} - You can only claim {remainingMinutes} more Gush Control Link minute{(remainingMinutes != 1 ? "s" : string.Empty)} this stream. You can increase this limit using the 🌟 Subscriber or 🥵 NSFW tip menus."
                        );
                        return;
                    }
                }
                
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingWithdrawals[key: username]  = withdrawAmount;
                ServiceJoystickWebSocketPayloadChatHandler.RequestBankUser(
                    username: username
                );
                break;

            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !bank parameters - the following parameters are valid: !bank [check/withdraw]"
                );
                break;
        }
    }
    
    private static void HandleBotCommandBreak(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !break user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !break parameter - !break must be followed by start or stop."
            );
            return;
        }

        var serviceBreak = Services.GetService<ServiceBreak>();
        var command      = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceBreak.Start();
                break;
            
            case "stop":
                serviceBreak.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !break parameter - !break must be followed by start or stop."
                );
                return;
        }
    }

    private static void HandleBotCommandClaim(
        string username,
        string parameters
    )
    {
        if (ServiceJoystickWebSocketPayloadChatHandler.s_isFocusing is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - The !claim command cannot be used while SmoothDagger is focusing."
            );
            return;
        }
        
        if (ServiceJoystickWebSocketPayloadChatHandler.s_isWalking is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - The !claim command cannot be used while SmoothDagger is walking."
            );
            return;
        }
        
        if (ServiceJoystickWebSocketPayloadChatHandler.s_isAClaimCommandInProgress is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - A claim is already in progress. Please try again in a moment."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !claim parameter - the following parameters are valid: !claim [ass/cock/nipples]."
            );
            return;
        }
        
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingClaimRewardUsers.Remove(
                item: username
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !claim user - only new followers and subscribers can use this command once per stream."
            );
            return;
        }

        var command = parameters.ToLower();
        switch (command)
        {
            case "ass":
                var layoutMeDurationInSeconds = 12f;
                var startDelayInMilliseconds  = 2200;
                var timerDelayInMilliseconds  = 10000;
                var endMessage                = $"🔔 Flash ass for 10 seconds timer has ended.";
        
                var startMessage = $"🔔 {username} requested SmoothDagger flash their ass for 10 seconds! Timer started!";
        
                StreamEventsHelper.SetMeTimerWithNotifications(
                    layoutMeDurationInSeconds: layoutMeDurationInSeconds,
                    startDelayInMilliseconds:  startDelayInMilliseconds,
                    timerDelayInMilliseconds:  timerDelayInMilliseconds,
                    startMessage:              startMessage,
                    endMessage:                endMessage,
                    soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
                    soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
                );
                break;
            
            case "cock":
                layoutMeDurationInSeconds = 12f;
                startDelayInMilliseconds  = 2200;
                timerDelayInMilliseconds  = 10000;
                endMessage                = $"🔔 Flash cock for 10 seconds timer has ended.";
        
                startMessage = $"🔔 {username} requested SmoothDagger flash their cock for 10 seconds! Timer started!";
        
                StreamEventsHelper.SetMeTimerWithNotifications(
                    layoutMeDurationInSeconds: layoutMeDurationInSeconds,
                    startDelayInMilliseconds:  startDelayInMilliseconds,
                    timerDelayInMilliseconds:  timerDelayInMilliseconds,
                    startMessage:              startMessage,
                    endMessage:                endMessage,
                    soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
                    soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
                );
                break;
            
            case "nipples":
                layoutMeDurationInSeconds = 12f;
                startDelayInMilliseconds  = 2200;
                timerDelayInMilliseconds  = 10000;
                endMessage                = $"🔔 Pinch nips for 10 seconds timer has ended.";
        
                startMessage = $"🔔 {username} requested SmoothDagger pinch their nips for 10 seconds! Timer started!";
        
                StreamEventsHelper.SetMeTimerWithNotifications(
                    layoutMeDurationInSeconds: layoutMeDurationInSeconds,
                    startDelayInMilliseconds:  startDelayInMilliseconds,
                    timerDelayInMilliseconds:  timerDelayInMilliseconds,
                    startMessage:              startMessage,
                    endMessage:                endMessage,
                    soundAlertTypeStart:       ServiceGodotAudio.SoundAlertType.ActivityDing,
                    soundAlertTypeEnd:         ServiceGodotAudio.SoundAlertType.ActivityDing
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingClaimRewardUsers.Add(
                    item: username
                );
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !claim parameter - the following parameters are valid: !claim [ass/cock/nipples]."
                );
                return;
        }

        Task.Run(
            function: async () =>
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_isAClaimCommandInProgress = true;
                
                await Task.Delay(
                    millisecondsDelay: 12200
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.s_isAClaimCommandInProgress = false;
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🚨 The !claim command is now available."
                );
            }
        );
    }
    
    private static void HandleBotCommandDropin(
        string username,
        bool   isStreamer
    )
    {
        if (isStreamer is false)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !dropin user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} has access to this command."
            );
            return;
        }

        var serviceOBS = Services.GetService<ServiceOBS>();
        serviceOBS.StopStream();
        
        var sceneTree = (SceneTree) Engine.GetMainLoop();
        sceneTree.Quit();
    }
    
    private static void HandleBotCommandFocus(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !focus user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !focus parameter - !focus must be followed by start or stop."
            );
            return;
        }
        
        var serviceNSFW    = Services.GetService<ServiceNSFW>();
        var serviceWorkOut = Services.GetService<ServiceWorkOut>();
        
        var command        = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceNSFW.Stop();
                serviceWorkOut.Stop();
                
                ServiceJoystickWebSocketPayloadChatHandler.s_isFocusing = true;
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🧠 Focus mode started."
                );
                break;
            
            case "stop":
                ServiceJoystickWebSocketPayloadChatHandler.s_isFocusing = false;
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🧠 Focus mode stopped."
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !walk parameter - !walk must be followed by start or stop."
                );
                return;
        }
    }
    
    private static void HandleBotCommandGiveaway(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !giveaway user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !giveaway parameter - !giveaway must be followed by start or stop."
            );
            return;
        }

        var serviceGiveaway = Services.GetService<ServiceGiveaway>();
        var command         = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceGiveaway.Start();
                break;
            
            case "stop":
                serviceGiveaway.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !giveaway parameter - !giveaway must be followed by start or stop."
                );
                return;
        }
    }
    
    private static void HandleBotCommandHydrate(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !hydrate user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !hydrate parameter - !hydrate must be followed by start or stop."
            );
            return;
        }

        var serviceHydrate = Services.GetService<ServiceHydrate>();
        var command        = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceHydrate.Start();
                break;
            
            case "stop":
                serviceHydrate.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !hydrate parameter - !hydrate must be followed by start or stop."
                );
                return;
        }
    }
    
    private static void HandleBotCommandLayout(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !layout user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !layout parameter - the following parameters are valid: [main/code/me/avatars/afk]."
            );
            return;
        }

        var command = parameters.ToLower();
        switch (command)
        {
            case "afk":
                SceneController.Instance.SetLayoutToAfk();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🖥️ Layout set to AFK."
                );
                break;
            
            case "a":
            case "avatars":
                SceneController.Instance.SetLayoutToAvatars();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🖥️ Layout set to Avatars."
                );
                break;
            
            case "large":
            case "l":
            case "c":
            case "code":
                SceneController.Instance.SetLayoutToCode();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🖥️ Layout set to Code."
                );
                break;
            
            case "d":
            case "default":
            case "m":
            case "main":
                SceneController.Instance.SetLayoutToMain();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🖥️ Layout set to Main."
                );
                break;
            
            case "me":
                SceneController.Instance.SetLayoutToMe();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🖥️ Layout set to Me."
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !layout parameter - the following parameters are valid: [main/code/me/avatars/afk]."
                );
                break;
        }
    }

    private static void HandleBotCommandLights(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var isTipper = ServiceJoystickWebSocketPayloadChatHandler.s_pendingLightRequestTippers.ContainsKey(
            key: username
        );
        
        if (
            isModerator  is false &&
            isStreamer   is false &&
            isSubscriber is false && 
            isTipper     is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !lights user - only subscribers, users who claimed the Set Overlay Theme & Background Lights for 15 Minutes token reward, moderators & {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} have access to this command."
            );
            return;
        }
        
        var hasModeratorOrSubscriberUsedCommand = ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedLightCommand.Contains(
            item: username
        );
        
        if (
            isStreamer                          is false &&
            isTipper                            is false &&
            hasModeratorOrSubscriberUsedCommand is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !lights usage - subscribers & moderators can use this only once per stream."
            );
            return;
        }

        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !lights parameter - the following parameters are valid: [color], [color]."
            );
            return;
        }

        var command = parameters.ToLower();
        switch (command)
        {
            case "blue":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Blue, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Blue, null)
                );
                break;
            
            case "blue raspberry":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.BlueRaspberry),
                    lightsStanding: (null, ServiceGoveeSceneNames.BlueRaspberry)
                );
                break;
            
            case "creamsicle banana":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.CreamsicleBanana),
                    lightsStanding: (null, ServiceGoveeSceneNames.CreamsicleBanana)
                );
                break;
            
            case "creamsicle blueberry":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.CreamsicleBlueberry),
                    lightsStanding: (null, ServiceGoveeSceneNames.CreamsicleBlueberry)
                );
                break;
            
            case "creamsicle dragonfruit":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.CreamsicleDragonfruit),
                    lightsStanding: (null, ServiceGoveeSceneNames.CreamsicleDragonfruit)
                );
                break;
                
            case "creamsicle lime":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.CreamsicleLime),
                    lightsStanding: (null, ServiceGoveeSceneNames.CreamsicleLime)
                );
                break;
                
            case "creamsicle orange":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.CreamsicleOrange),
                    lightsStanding: (null, ServiceGoveeSceneNames.CreamsicleOrange)
                );
                break;
                
            case "creamsicle strawberry":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.CreamsicleStrawberry),
                    lightsStanding: (null, ServiceGoveeSceneNames.CreamsicleStrawberry)
                );
                break;
            
            case "cyan":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Cyan, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Cyan, null)
                );
                break;
            
            case "cyberpunk":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Cyberpunk),
                    lightsStanding: (null, ServiceGoveeSceneNames.Cyberpunk)
                );
                break;
            
            case "forest sunset":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.ForestSunset),
                    lightsStanding: (null, ServiceGoveeSceneNames.ForestSunset)
                );
                break;
            
            case "green":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Green, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Green, null)
                );
                break;
            
            case "heatwave":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Heatwave),
                    lightsStanding: (null, ServiceGoveeSceneNames.Heatwave)
                );
                break;
            
            case "icy":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Icy),
                    lightsStanding: (null, ServiceGoveeSceneNames.Icy)
                );
                break;
            
            case var _ when command.Contains(
                value: ','
            ):
                var lightPatterns = parameters.Split(
                    separator: ',',
                    options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
                );
                if (lightPatterns.Length == 2) 
                {
                    var lightPatternA = lightPatterns[0].Trim();
                    var lightPatternB = lightPatterns[1].Trim();
                    
                    var succeeded = ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandLightsMix(
                        username:      username, 
                        lightPatternA: lightPatternA, 
                        lightPatternB: lightPatternB
                    );
                    if (succeeded is false)
                    {
                        return;
                    }
                }
                else
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !lights parameter - the colors or scenes are invalid."
                    );
                }
                break;

            case "magenta":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Magenta, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Magenta, null)
                );
                break;
            
            case "orange":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Orange, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Orange, null)
                );
                break;
            
            case "orange purple":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.OrangePurple),
                    lightsStanding: (null, ServiceGoveeSceneNames.OrangePurple)
                );
                break;
            
            case "purple":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Purple, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Purple, null)
                );
                break;
            
            case "rainbow":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Rainbow),
                    lightsStanding: (null, ServiceGoveeSceneNames.Rainbow)
                );
                break;
            
            case "red":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Red, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Red, null)
                );
                break;
            
            case "red white blue":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.RedWhiteBlue),
                    lightsStanding: (null, ServiceGoveeSceneNames.RedWhiteBlue)
                );
                break;
            
            case "toxic":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Toxic),
                    lightsStanding: (null, ServiceGoveeSceneNames.Toxic)
                );
                break;
            
            case "vaporwave":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Vaporwave),
                    lightsStanding: (null, ServiceGoveeSceneNames.Vaporwave)
                );
                break;
            
            case "watermelon":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (null, ServiceGoveeSceneNames.Watermelon),
                    lightsStanding: (null, ServiceGoveeSceneNames.Watermelon)
                );
                break;

            case "white":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.White, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.White, null)
                );
                break;
            
            case "yellow":
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  (IServiceColorInterpolatorDefinition.ColorType.Yellow, null),
                    lightsStanding: (IServiceColorInterpolatorDefinition.ColorType.Yellow, null)
                );
                break;
                
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !lights parameter - the colors or scenes are invalid."
                );
                return;
        }

        if (isTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingLightRequestTippers[key: username]--;
            if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingLightRequestTippers[key: username] <= 0)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingLightRequestTippers.Remove(
                    key: username
                );
            }
        }
        else if (
            isModerator is true ||
            isSubscriber is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedLightCommand.Add(
                item: username
            );
        }
    }
    
    private static bool HandleBotCommandLightsMix(
        string username,
        string lightPatternA,
        string lightPatternB
    )
    {
        var lightPatternActionA = MappingLightPatterns.GetLightPatternAction(
            lightPattern: lightPatternA
        );
        var lightPatternActionB = MappingLightPatterns.GetLightPatternAction(
            lightPattern: lightPatternB
        );

        if (
            lightPatternActionA.IsValid is false || 
            lightPatternActionB.IsValid is false
        ) 
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !lights mix parameter - the colors or scenes are invalid."
            );
            return false;
        }
        
        GoveeLightControllers.SetGoveeLightControllers(
            lightsCeiling:  (lightPatternActionA.Color, lightPatternActionA.Scene),
            lightsStanding: (lightPatternActionB.Color, lightPatternActionB.Scene)
        );
        return true;
    }

    private static void HandleBotCommandName(
        string username,
        string parameters
    )
    {
        var color = parameters is "default" ? ServiceColorInterpolatorColorMode.Transition : MappingColorNames.GetColorByColorName(
            colorName: parameters
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !name parameter - the color name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasColorName(
            username: username,
            color:    color.Value
        );
    }
    
    private static void HandleBotCommandNSFW(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !nsfw user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !nsfw parameter - !nsfw must be followed by start or stop."
            );
            return;
        }

        var serviceNSFW = Services.GetService<ServiceNSFW>();
        var command     = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceNSFW.Start();
                break;
            
            case "stop":
                serviceNSFW.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !nsfw parameter - !nsfw must be followed by start or stop."
                );
                return;
        }
    }
    
    private static void HandleBotCommandOBS(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !obs user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        var command       = commandSplit[0];
        var subParameters = commandSplit.Length > 1 ? 
            string.Join(
                separator: ' ', 
                values:    commandSplit.Skip(
                    count: 1
                )
            ) :
            string.Empty;
        switch (command)
        {
            case "mic":
            case "microphone":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandOBSMicrophone(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "record":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandOBSRecord(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "stream":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandOBSStream(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs [mic/record/stream]"
                );
                return;
        }
    }
    
    private static void HandleBotCommandOBSMicrophone(
        string username,
        string parameters
    )
    {
        var commandSplit = parameters.Split(
            separator: ' '
        );
        if (commandSplit.Length > 1)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs mic [mute/unmute]"
            );
            return;
        }
        
        var serviceOBS = Services.GetService<ServiceOBS>();
        var command    = commandSplit[0];
        switch (command)
        {
            case "mute":
                serviceOBS.MuteMicrophone();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🎤 OBS microphone muted."
                );
                break;
            
            case "unmute":
                serviceOBS.UnmuteMicrophone();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🎤 OBS microphone unmuted."
                );
                break;

            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs mic [mute/unmute]"
                );
                return;
        }
    }

    private static void HandleBotCommandOBSRecord(
        string username,
        string parameters
    )
    {
        var commandSplit = parameters.Split(
            separator: ' '
        );
        if (commandSplit.Length > 1)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs record [start/stop]"
            );
            return;
        }
        
        var serviceOBS = Services.GetService<ServiceOBS>();
        var command    = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceOBS.StartRecord();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"📹 OBS recording started."
                );
                break;
            
            case "stop":
                serviceOBS.StopRecord();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"📹 OBS recording stopped."
                );
                break;

            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs record [start/stop]"
                );
                return;
        }
    }

    private static void HandleBotCommandOBSStream(
        string username,
        string parameters
    )
    {
        var commandSplit = parameters.Split(
            separator: ' '
        );
        if (commandSplit.Length > 1)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs stream [start/stop]"
            );
            return;
        }
        
        var serviceOBS = Services.GetService<ServiceOBS>();
        var command    = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceOBS.StartStream();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"📹 OBS stream started."
                );
                break;
            
            case "stop":
                serviceOBS.StopStream();
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"📹 OBS stream stopped."
                );
                break;

            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !obs parameter - the following commands are valid: !obs stream [start/stop]"
                );
                return;
        }
    }
    
    private static void HandleBotCommandRockPaperScissors(
        string username,
        string command, 
        string parameters
    )
    {
        if (parameters != string.Empty)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid {command} parameter - {command} has no parameters."
            );
            return;
        }
        
        string[] icons = [
            "🗿", 
            "📄", 
            "✂️"
        ];
        int user;
        switch (command)
        {
            case "!rock":
                user = 0;
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username: username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorRocks
                );
                break;
            
            case "!paper":
                user = 1;
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username: username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorPapers
                );
                break;
            
            case "!scissors":
                user = 2;
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username: username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorScissors
                );
                break;
            
            default:
                throw new NotImplementedException();
        }

        var random = new RandomNumberGenerator();
        var bot    = random.RandiRange(
            from: 0, 
            to:   2
        );

        string result;
        if (user == bot)
        {
            result = "Tie!";
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username: username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorTies
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorTies3InARow, 
                key:        username, 
                increment:  true
            );
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorLosses3InARow, 
                key:        username, 
                increment:  false
            );
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorWins3InARow, 
                key:        username, 
                increment:  false
            ) ;

            if (ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorTies3InARow[key: username] is 3)
            {
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username: username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorTie3InARow
                );
            }
        }
        else if (bot == (user + 1) % 3)
        {
            result = "You Lose!";
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username: username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorLosses
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorTies3InARow, 
                key:        username, 
                increment:  false
            );
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorLosses3InARow, 
                key:        username, 
                increment:  true
            );
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorWins3InARow, 
                key:        username, 
                increment:  false
            ) ;
            
            if (ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorLosses3InARow[key: username] is 3)
            {
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username: username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorLose3InARow
                );
            }
        }
        else
        {
            result = "You Win!";
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username: username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorWins
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorTies3InARow, 
                key:        username, 
                increment:  false
            );
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorLosses3InARow, 
                key:        username, 
                increment:  false
            );
            ServiceJoystickWebSocketPayloadChatHandler.UpdateRockPaperScissorStreak(
                dictionary: ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorWins3InARow, 
                key:        username, 
                increment:  true
            ) ;
            
            if (ServiceJoystickWebSocketPayloadChatHandler.s_userRockPaperScissorWins3InARow[key: username] is 3)
            {
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username: username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorWin3InARow
                );
            }
        }

        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username: username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRockPaperScissorMatchesPlayed
        );
        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
            message: $"{icons[bot]} - {result}"
        );
    }
    
    private static void HandleBotCommandRollTheDice(
        string username,
        string parameters
    )
    {
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !rtd parameter - !rtd must be in the following format: !rtd 69."
            );
            return;
        }
        
        var hasValue = long.TryParse(
            s:      commandSplit[0],
            result: out var value
        );
        var hasParameters = string.IsNullOrEmpty(
            value: parameters
        ) is false;
        
        if (
            hasParameters is true &&
            (
                hasValue is false ||
                value <= 0
            )
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !rtd parameter - !rtd parameter must be empty or a whole number greater than 0."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            value = ServiceJoystickWebSocketPayloadChatHandler.c_commandRollTheDiceDefaultParameter;
        }
        
        var random      = new Random();
        var randomValue = random.NextInt64() % value + 1;

        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
            message: $"🎲 {username} rolled a {randomValue} out of {value}!"
        );

        switch (randomValue)
        {
            case 1:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled1s
                );
                break;
            
            case 42:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled42s
                );
                break;
            
            case 67:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled67s
                );
                break;
            
            case 69:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled69s
                );
                break;
            
            case 100:
                if (value is ServiceJoystickWebSocketPayloadChatHandler.c_commandRollTheDiceDefaultParameter)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                        username:                     username,
                        serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled100s
                    );
                }
                break;
            
            case 240:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled240s
                );
                break;
            
            case 256:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled256s
                );
                break;
            
            case 420:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled420s
                );
                break;
            
            case 720:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled720s
                );
                break;
            
            case 1080:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled1080s
                );
                break;
            
            case 1337:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled1337s
                );
                break;
            
            case 3840:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled3840s
                );
                break;
            
            case 100000:
                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolled100000s
                );
                break;
            
            default:
                break;
        }

        if (randomValue == value)
        {
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRollsMaximum
            );
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserRolls
        );
    }
    
    private static void HandleBotCommandSFX(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var isTipper = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSFXRequestTippers.ContainsKey(
            key: username
        );
        
        if (
            isModerator is false &&
            isStreamer is false &&
            isSubscriber is false && 
            isTipper is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !sfx user - only subscribers, users who claimed the Play Sound Effect token reward, moderators & {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} have access to this command."
            );
            return;
        }
        
        var hasModeratorOrSubscriberUsedCommand = ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSFXCommand.Contains(
            item: username
        );
        
        if (
            isStreamer is false &&
            isTipper is false &&
            hasModeratorOrSubscriberUsedCommand is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !sfx usage - subscribers & moderators can use this only once per stream."
            );
            return;
        }

        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !sfx parameter - the following parameters are valid: [name]."
            );
            return;
        }

        var command = parameters.ToLower();
        switch (command)
        {
            case "applause":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Applause
                );
                break;
            
            case "airhorn":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Airhorn
                );
                break;
            
            case "ass":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Ass
                );
                break;
            
            case "balls":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.BallsOfSteel
                );
                break;
            
            case "blink":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Blink
                );
                break;
            
            case "boing":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Boing
                );
                break;
            
            case "bonk":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Bonk
                );
                break;
            
            case "bruh":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Bruh
                );
                break;
            
            case "buzzer":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Buzzer
                );
                break;
            
            case "censor":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Censor
                );
                break;
            
            case "critical hit":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.CriticalHit
                );
                break;
            
            case "discord":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Discord
                );
                break;
            
            case "dun dun":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.DunDun
                );
                break;
            
            case "fart":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Fart
                );
                break;
            
            case "godlike":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Godlike
                );
                break;

            case "golden pan":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.GoldenPan
                );
                break;
            
            case "grindr":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Grindr
                );
                break;
            
            case "hammer":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Hammer
                );
                break;
            
            case "heartbeats":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Heartbeats
                );
                break;
            
            case "hello there":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.HelloThere
                );
                break;
            
            case "holy shit":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.HolyShit
                );
                break;
            
            case "instant transmission":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.InstantTransmission
                );
                break;
            
            case "interesting":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Interesting
                );
                break;
            
            case "jeopardy":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Jeopardy
                );
                break;
            
            case "knocking":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Knocking
                );
                break;
            
            case "mario jump":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.MarioJump
                );
                break;
            
            case "mario power up":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.MarioPowerUp
                );
                break;

            case "nice":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Nice
                );
                break;
            
            case "nope":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Nope
                );
                break;
            
            case "nut":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Nut
                );
                break;
            
            case "oh my god":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.OhMyGod
                );
                break;

            case "pan":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Pan
                );
                break;
            
            case "pop":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Pop
                );
                break;
            
            case "quack":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Quack
                );
                break;
            
            case "rizz":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Rizz
                );
                break;
            
            case "rubber ducky":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.RubberDucky
                );
                break;
            
            case "sad trombone":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.SadTrombone
                );
                break;
            
            case "scored":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Scored
                );
                break;
            
            case "shocking":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Shocking
                );
                break;
            
            case "startle":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Startle
                );
                break;
            
            case "taco bell":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.TacoBell
                );
                break;
            
            case "uwu":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Uwu
                );
                break;
            
            case "whip":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Whip
                );
                break;
            
            case "wow":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Wow
                );
                break;
            
            case "yay":
                StreamEventsHelper.PlaySoundAlert(
                    soundAlertType: ServiceGodotAudio.SoundAlertType.Yay
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !sfx parameter - the name is invalid."
                );
                return;
        }

        if (isTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSFXRequestTippers[key: username]--;
            if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingSFXRequestTippers[key: username] <= 0)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingSFXRequestTippers.Remove(
                    key: username
                );
            }
        }
        else if (
            isModerator is true ||
            isSubscriber is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSFXCommand.Add(
                item: username
            );
        }
    }
    
    private static void HandleBotCommandServices(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !services user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !services parameter - !services must be followed by start or stop."
            );
            return;
        }

        var serviceBreak    = Services.GetService<ServiceBreak>();
        var serviceGiveaway = Services.GetService<ServiceGiveaway>();
        var serviceHydrate  = Services.GetService<ServiceHydrate>();
        var serviceNSFW     = Services.GetService<ServiceNSFW>();
        var serviceStretch  = Services.GetService<ServiceStretch>();
        var serviceTF2      = Services.GetService<ServiceTeamFortress2>();
        var serviceWorkOut  = Services.GetService<ServiceWorkOut>();
        
        var command        = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceBreak.Start();
                serviceGiveaway.Start();
                serviceHydrate.Start();
                serviceNSFW.Start();
                serviceStretch.Start();
                serviceTF2.Start();
                serviceWorkOut.Start();
                break;
            
            case "stop":
                serviceBreak.Stop();
                serviceGiveaway.Stop();
                serviceHydrate.Stop();
                serviceNSFW.Stop();
                serviceStretch.Stop();
                serviceTF2.Stop();
                serviceWorkOut.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !services parameter - !services must be followed by start or stop."
                );
                return;
        }
    }

    private static void HandleBotCommandSong(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var commandSplit = parameters.Split(
            separator: ' ',
            count:     2
        );
        var command       = commandSplit[0].ToLower();
        var subParameters = commandSplit.Length > 1 ? commandSplit[1].ToLower() : string.Empty;
        switch (command)
        {
            case "repeat":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSongRepeat(
                    username:     username,
                    parameters:   subParameters,
                    isModerator:  isModerator,
                    isStreamer:   isStreamer,
                    isSubscriber: isSubscriber
                );
                break;
            
            case "request":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSongRequest(
                    username:     username,
                    parameters:   subParameters,
                    isModerator:  isModerator,
                    isStreamer:   isStreamer,
                    isSubscriber: isSubscriber
                );
                break;
            
            case "skip":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandSongSkip(
                    username:     username,
                    parameters:   subParameters,
                    isModerator:  isModerator,
                    isStreamer:   isStreamer,
                    isSubscriber: isSubscriber
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !song parameter - the following parameters are valid: [repeat/request/skip]"
                );
                break;
        }
    }
    
    private static void HandleBotCommandSongRepeat(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Any(
                predicate: pendingSongRequester => pendingSongRequester.Name == username
            )
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Spotify is still processing your song request. Please wait before making another request."
            );
            return;
        }
        
        var isTipper = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.ContainsKey(
            key: username
        );
        
        if (
            isModerator is false &&
            isStreamer is false &&
            isSubscriber is false && 
            isTipper is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song repeat user - only subscribers, users who claimed the Spotify Song Request token reward, moderators & {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} have access to this command."
            );
            return;
        }
        
        var hasModeratorOrSubscriberUsedCommand = ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongRequestCommand.Contains(
            item: username
        );
        
        if (
            isStreamer is false &&
            isTipper is false &&
            hasModeratorOrSubscriberUsedCommand is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song repeat usage - subscribers & moderators can use this only once per stream."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song repeat parameter - no parameters are needed for this command."
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Enqueue(
            item: new SpotifySongActioner
            {
                Name                    = username,
                IsModeratorOrSubscriber = isSubscriber || isModerator,
                IsTipper                = isTipper
            }
        );

        if (isTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: username]--;
            if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: username] <= 0)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Remove(
                    key: username
                );
            }
        }
        else if (
            isModerator is true ||
            isSubscriber is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongRequestCommand.Add(
                item: username
            );
        }

        var serviceSpotify = Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestTrackRepeat();
    }

    private static void HandleBotCommandSongRequest(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Any(
                predicate: pendingSongRequester => pendingSongRequester.Name == username
            )
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Spotify is still processing your song request. Please wait before making another request."
            );
            return;
        }
        
        var isTipper = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.ContainsKey(
            key: username
        );
        
        if (
            isModerator is false &&
            isStreamer is false &&
            isSubscriber is false && 
            isTipper is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song request user - only subscribers, users who claimed the Spotify Song Request token reward, moderators & {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} have access to this command."
            );
            return;
        }
        
        var hasModeratorOrSubscriberUsedCommand = ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongRequestCommand.Contains(
            item: username
        );
        
        if (
            isStreamer is false &&
            isTipper is false &&
            hasModeratorOrSubscriberUsedCommand is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song request usage - subscribers & moderators can use this only once per stream."
            );
            return;
        }
        
        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song request parameter - the following parameters are valid: [song name - artist]."
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Enqueue(
            item: new SpotifySongActioner
            {
                Name                    = username,
                IsModeratorOrSubscriber = isSubscriber || isModerator,
                IsTipper                = isTipper
            }
        );

        if (isTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: username]--;
            if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: username] <= 0)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Remove(
                    key: username
                );
            }
        }
        else if (
            isModerator is true ||
            isSubscriber is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongRequestCommand.Add(
                item: username
            );
        }

        var serviceSpotify = Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestTrackQueueBySearchTerms(
            searchParameters: parameters
        );
    }
    
    private static void HandleBotCommandSongSkip(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        switch (isStreamer)
        {
            case false when
                isModerator is false &&
                isSubscriber is false:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !song skip user - only subscribers, moderators, & SmoothDagger have access to this command."
                );
                return;
            
            case false when
                ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongSkipCommand.Contains(
                    item: username
                ):
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !song skip usage - subscribers & moderators can use this only once per stream."
                );
                return;
        }

        if (
            string.IsNullOrEmpty(
                value: parameters
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !song skip parameter - there are no parameters available."
            );
            return;
        }

        var serviceSpotify = Services.GetService<ServiceSpotify>();
        serviceSpotify.RequestSkipToNextTrack();
        
        ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongSkipCommand.Add(
            item: username
        );
    }
    
    private static void HandleBotCommandSpin(
        string username
    )
    {
        var hasSpins = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSpinForRandomGushControlLinkTippers.TryGetValue(
            key:   username,
            value: out var remaining
        ) && remaining > 0;
        
        if (hasSpins is false)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !spin user - only users who claimed the Spin for a Random Gush Control Link token reward have access to this command."
            );
            return;
        }

        var random  = new Random();
        var roll    = random.Next(
            minValue: 1,
            maxValue: 101
        );
        var minutes = roll switch
        {
            100  => 10,
            > 89 => 5,
            > 59 => 3,
            _    => 1
        };
        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
            message: $"🎉 {username} spun the wheel & won {minutes} Gush Control Link minute{(minutes is 1 ? string.Empty : "s")}!"
        );
        ServiceJoystickWebSocketPayloadChatHandler.ExecuteDeposit(
            targetUser: username,
            amount:     minutes
        );
        
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSpinForRandomGushControlLinkTippers[key: username]--;
        if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingSpinForRandomGushControlLinkTippers[key: username] <= 0)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSpinForRandomGushControlLinkTippers.Remove(
                key: username
            );
        }
    }

    private static void HandleBotCommandSteam(
        string username,
        string parameters
    )
    {
        var commandSplit = parameters.Split(
            separator: ' '
        );
        var command      = commandSplit[0].ToLower();

        switch (command)
        {
            case "check":
                ServiceJoystickWebSocketPayloadChatHandler.RequestSteamUser(
                    username: username
                );
                break;
            
            case "link":
                if (commandSplit.Length == 1)
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !steam link parameter - !steam link must be followed by your Steam username."
                    );
                }
                
                var steamUsername = string.Join(
                    separator: ' ',
                    values:    commandSplit.Skip(
                        count: 1
                    )
                );

                ServiceJoystickWebSocketPayloadChatHandler.UpdateSteamUsername(
                    username:      username,
                    steamUsername: steamUsername
                );

                ServiceJoystickWebSocketPayloadChatHandler.SteamUserUpdated?.Invoke(
                    arg1: username,
                    arg2: steamUsername
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"☁️ @{username} - your Steam username has been updated to {steamUsername}."
                );

                ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                    username:                     username,
                    serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserLinkedSteams
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !steam parameter - the following commands are valid: !steam [check/link]"
                );
                break;
        }
    }
    
    private static void HandleBotCommandStretch(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !stretch user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !stretch parameter - !stretch must be followed by start or stop."
            );
            return;
        }

        var serviceStretch = Services.GetService<ServiceStretch>();
        var command        = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceStretch.Start();
                break;
            
            case "stop":
                serviceStretch.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !stretch parameter - !stretch must be followed by start or stop."
                );
                return;
        }
    }

    private static void HandleBotCommandTF2(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandTF2User(
                username:     username, 
                parameters:   parameters,
                isModerator:  isModerator,
                isStreamer:   isStreamer,
                isSubscriber: isSubscriber
            );
            return;
        }

        var firstSpaceIndex = parameters.IndexOf(
            value: ' '
        );
        var command    = firstSpaceIndex == -1 
            ? parameters 
            : parameters[..firstSpaceIndex];
        var subCommand = firstSpaceIndex == -1 
            ? string.Empty 
            : parameters[(firstSpaceIndex + 1)..].Trim();
        var serviceTF2 = Services.GetService<ServiceTeamFortress2>();
        switch (command)
        {
            case "explode":
                ServiceTeamFortress2BindHandler.Explode();
                break;

            case "jump":
                ServiceTeamFortress2BindHandler.Jump();
                break;
            
            case "kill":
                ServiceTeamFortress2BindHandler.Kill();
                break;
            
            case "start":
                serviceTF2.Start();
                break;
            
            case "stop":
                serviceTF2.Stop();
                break;

            case "taunt":
                if (
                    string.IsNullOrWhiteSpace(
                        value: subCommand
                    ) is true
                )
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !tf2 taunt command - the following parameters are valid [name]."
                    );
                    return;
                }

                int tauntIndex;
                switch (subCommand)
                {
                    case "rock paper scissors":
                        tauntIndex = 1;
                        break;

                    case "conga":
                        tauntIndex = 2;
                        break;

                    case "sit":
                        tauntIndex = 3;
                        break;

                    case "laugh":
                        tauntIndex = 4;
                        break;

                    case "celebrate":
                        tauntIndex = 5;
                        break;

                    case "special1":
                        tauntIndex = 6;
                        break;

                    case "bumper car":
                        tauntIndex = 7;
                        break;

                    case "special2":
                        tauntIndex = 8;
                        break;

                    default:
                        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                            message: $"🛑 @{username} - Invalid !tf2 taunt name - the following parameters are valid [name]."
                        );
                        return;
                }

                _ = Task.Run(
                    function: async () =>
                    {
                        ServiceTeamFortress2BindHandler.OpenEmoteMenu();

                        await Task.Delay(
                            millisecondsDelay: 16
                        );

                        ServiceTeamFortress2BindHandler.SelectTaunt(
                            index: tauntIndex
                        );
                    }
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !tf2 parameter - !tf2 must be followed by explode, kill, or taunt."
                );
                return;
        }
    }

    private static void HandleBotCommandTF2User(
        string username,
        string parameters,
        bool   isModerator,
        bool   isStreamer,
        bool   isSubscriber
    )
    {
        var isTipper = ServiceJoystickWebSocketPayloadChatHandler.s_pendingTF2TriggerAnActionTippers.ContainsKey(
            key: username
        );
        
        if (
            isModerator  is false &&
            isStreamer   is false &&
            isSubscriber is false && 
            isTipper     is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !tf2 user - only subscribers, users who claimed the TF2: Trigger an Action token reward, moderators & {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} have access to this command."
            );
            return;
        }
        
        var hasModeratorOrSubscriberUsedCommand = ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedTF2Command.Contains(
            item: username
        );
        
        if (
            isStreamer                          is false &&
            isTipper                            is false &&
            hasModeratorOrSubscriberUsedCommand is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !tf2 usage - subscribers & moderators can use this only once per stream."
            );
            return;
        }
        
        var firstSpaceIndex = parameters.IndexOf(
            value: ' '
        );

        var command    = firstSpaceIndex == -1 
            ? parameters 
            : parameters[..firstSpaceIndex];
        var subCommand = firstSpaceIndex == -1 
            ? string.Empty 
            : parameters[(firstSpaceIndex + 1)..].Trim();
        switch (command)
        {
            case "explode":
                ServiceTeamFortress2BindHandler.Explode();
                break;
            
            case "jump":
                ServiceTeamFortress2BindHandler.Jump();
                break;
            
            case "kill":
                ServiceTeamFortress2BindHandler.Kill();
                break;
            
            case "taunt":
                if (
                    string.IsNullOrWhiteSpace(
                        value: subCommand
                    ) is true
                )
                {
                    ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                        message: $"🛑 @{username} - Invalid !tf2 taunt command - the following parameters are valid [name]."
                    );
                    return;
                }

                int tauntIndex;
                switch (subCommand)
                {
                    case "rock paper scissors":
                        tauntIndex = 1;
                        break;
                    
                    case "conga":
                        tauntIndex = 2;
                        break;
                    
                    case "sit":
                        tauntIndex = 3;
                        break;
                    
                    case "laugh":
                        tauntIndex = 4;
                        break;
                    
                    case "celebrate":
                        tauntIndex = 5;
                        break;
                    
                    case "special1":
                        tauntIndex = 6;
                        break;
                    
                    case "bumper car":
                        tauntIndex = 7;
                        break;
                    
                    case "special2":
                        tauntIndex = 8;
                        break;

                    case "random":
                        tauntIndex = Random.Shared.Next(
                            maxValue: 8
                        ) + 1;
                        break;
                    
                    default:
                        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                            message: $"🛑 @{username} - Invalid !tf2 taunt name - the following parameters are valid [name]."
                        );
                        return;
                }

                _ = Task.Run(
                    function: async () =>
                    {
                        ServiceTeamFortress2BindHandler.OpenEmoteMenu();
                        
                        await Task.Delay(
                            millisecondsDelay: 50
                        );
                        
                        ServiceTeamFortress2BindHandler.SelectTaunt(
                            index: tauntIndex
                        );
                    }
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !tf2 parameter - !tf2 must be followed by explode, kill, or taunt."
                );
                return;
        }

        if (isTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingTF2TriggerAnActionTippers[key: username]--;
            if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingTF2TriggerAnActionTippers[key: username] <= 0)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingTF2TriggerAnActionTippers.Remove(
                    key: username
                );
            }
        }
        else if (
            isModerator is true ||
            isSubscriber is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedTF2Command.Add(
                item: username
            );
        }
    }
    
    private static void HandleBotCommandTitle(
        string username,
        string parameters
    )
    {
        if (parameters is "none")
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🏆 @{username} - Your title has been removed."
            );
            
            BackgroundAvatarsController.Instance.UpdateAvatarNameplateTitle(
                username: username,
                title:    string.Empty
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.RemoveUserTitle(
                username: username
            );
            
            ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
                username:                     username,
                serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserClearedTitles
            );
            return;
        }
        
        var title = MappingTitleNames.GetTitleByTitleName(
            titleName: parameters
        );
        if (title is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !title parameter - the title was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserHasTitle(
            username: username,
            title:    title.Value
        );
    }
    
    private static void HandleBotCommandUnlock(
        string username,
        string parameters,
        string rawParameters
    )
    {
        var commandSplit = parameters.Split(
            separator: ' '
        );
        var command = commandSplit[0];
        var subParameters = string.Join(
            separator: ' ', 
            values:    commandSplit.Skip(
                count: 1
            )
        );
        switch (command)
        {
            case "check":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockCheck(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "color":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockColor(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "effect":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockEffect(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "give":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockGive(
                    username:      username,
                    parameters:    subParameters,
                    rawParameters: rawParameters
                );
                break;
            
            case "model":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockModel(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "preview":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockPreview(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !unlock parameter - the following parameters are valid: !unlock [check/color/effect/model/preview]"
                );
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
                    item: username
                );
                return;
        }
    }
    
    private static void HandleBotCommandUnlockCheck(
        string username,
        string parameters
    )
    {
        switch (parameters)
        {
            case "colors":
                ServiceJoystickWebSocketPayloadChatHandler.RequestCheckUserUnlockColors(
                    username: username
                );
                break;
            
            case "effects":
                ServiceJoystickWebSocketPayloadChatHandler.RequestCheckUserUnlockEffects(
                    username: username
                );
                break;
            
            case "models":
                ServiceJoystickWebSocketPayloadChatHandler.RequestCheckUserUnlockModels(
                    username: username
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !unlock check parameter - the following parameters are valid: !unlock check [colors/effects/models]"
                );
                break;
        }
    }

    private static void HandleBotCommandUnlockColor(
        string username,
        string parameters
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers.ContainsKey(
                key: username
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock color command - Only users who claimed the Unlock an Avatar Color token reward can use this command."
            );
            return;
        }
        
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Contains(
                item: username
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending unlock in progress. Please try again in a moment."
            );
            return;
        }

        
        var color = MappingColorNames.GetColorByColorName(
            colorName: parameters
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock color parameter - the color name was invalid"
            );
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
                item: username
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserUnlockColor(
            username: username,
            color:    color.Value
        );
    }

    private static void HandleBotCommandUnlockEffect(
        string username,
        string parameters
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers.ContainsKey(
                key: username
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock effect command - Only users who claimed the Unlock an Avatar Effect token reward can use this command."
            );
            return;
        }
        
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Contains(
                item: username
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending unlock in progress. Please try again in a moment."
            );
            return;
        }
        
        var effect = MappingEffectNames.GetEffectByEffectName(
            effectName: parameters
        );
        if (effect is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock effect parameter - the effect name was invalid"
            );
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
                item: username
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserUnlockEffect(
            username: username,
            effect:   effect.Value
        );
    }

    private static void HandleBotCommandUnlockGive(
        string username,
        string parameters,
        string rawParameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock give user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} has access to this command."
            );
            return;
        }
        
        var usernameSplit = rawParameters.Split(
            separator: ' '
        );
        if (usernameSplit.Length < 3)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock give parameter - the following parameters are valid: !unlock give [color/effect/model] [username] [name]"
            );
            return;
        }

        var targetUser = usernameSplit[2];
        
        var commandSplit  = parameters.Split(
            separator: ' '
        );
        var command       = commandSplit[0];
        var subParameters = string.Join(
            separator: ' ', 
            values:    commandSplit.Skip(
                count: 2
            )
        );
        switch (command)
        {
            case "color":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockGiveColor(
                    username:   username,
                    targetUser: targetUser,
                    parameters: subParameters
                );
                break;
            
            case "effect":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockGiveEffect(
                    username:   username,
                    targetUser: targetUser,
                    parameters: subParameters
                );
                break;
            
            case "model":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockGiveModel(
                    username:   username,
                    targetUser: targetUser,
                    parameters: subParameters
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !unlock give parameter - the following parameters are valid: !unlock give [color/effect/model]"
                );
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
                    item: username
                );
                return;
        }
    }
    
    private static void HandleBotCommandUnlockGiveColor(
        string username,
        string targetUser,
        string parameters
    )
    {
        var color = MappingColorNames.GetColorByColorName(
            colorName: parameters
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock give color parameter - the color name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserUnlockColor(
            username: targetUser,
            color:    color.Value
        );
    }

    private static void HandleBotCommandUnlockGiveEffect(
        string username,
        string targetUser,
        string parameters
    )
    {
        var effect = MappingEffectNames.GetEffectByEffectName(
            effectName: parameters
        );
        if (effect is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock give effect parameter - the effect name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserUnlockEffect(
            username: targetUser,
            effect:   effect.Value
        );
    }
    
    private static void HandleBotCommandUnlockGiveModel(
        string username,
        string targetUser,
        string parameters
    )
    {
        var model = MappingModelNames.GetModelByModelName(
            modelName: parameters
        );
        if (model is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock give model parameter - the model name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserUnlockModel(
            username: targetUser,
            model:    model.Value
        );
    }

    private static void HandleBotCommandUnlockModel(
        string username,
        string parameters
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers.ContainsKey(
                key: username
            ) is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock model command - Only users who claimed the Unlock an Avatar Model token reward can use this command."
            );
            return;
        }
        
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Contains(
                item: username
            ) is true
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - You have a pending unlock in progress. Please try again in a moment."
            );
            return;
        }
        
        var model = MappingModelNames.GetModelByModelName(
            modelName: parameters
        );
        if (model is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock model parameter - the model name was invalid"
            );
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockers.Remove(
                item: username
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestValidateUserUnlockModel(
            username: username,
            model:    model.Value
        );
    }

    private static void HandleBotCommandUnlockPreview(
        string username,
        string parameters
    )
    {
        if (
            username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername && 
            ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature is false
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - The preview command is currently on cooldown. Please try again later."
            );
            return;
        }
        
        var commandSplit  = parameters.Split(
            separator: ' '
        );
        var command       = commandSplit[0];
        var subParameters = string.Join(
            separator: ' ', 
            values:    commandSplit.Skip(
                count: 1
            )
        );
        switch (command)
        {
            case "color":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockPreviewColor(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "effect":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockPreviewEffect(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            case "model":
                ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommandUnlockPreviewModel(
                    username:   username,
                    parameters: subParameters
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !unlock preview command parameter - the following are valid parameters: !unlock preview [color/effect/model]"
                );
                break;
        }
    }

    private static void HandleBotCommandUnlockPreviewColor(
        string username,
        string parameters
    )
    {
        var color = MappingColorNames.GetColorByColorName(
            colorName: parameters
        );
        if (color is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock preview color parameter - the color name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserPreviewedUnlocks
        );

        Task.Run(
            function: async () =>
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature = false;
                
                BackgroundAvatarsController.Instance.PreviewAvatarColor(
                    serviceColorInterpolatorColorMode: color.Value
                );
                BackgroundAvatarsController.Instance.ShowPreviewAvatar();

                await Task.Delay(
                    millisecondsDelay: ServiceJoystickWebSocketPayloadChatHandler.c_previewLengthInMilliseconds
                );
                
                BackgroundAvatarsController.Instance.HidePreviewAvatar();
                
                await Task.Delay(
                    millisecondsDelay: ServiceJoystickWebSocketPayloadChatHandler.c_previewTimeoutInMilliseconds
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature = true;
            }
        );
    }

    private static void HandleBotCommandUnlockPreviewEffect(
        string username,
        string parameters
    )
    {
        var effect = MappingEffectNames.GetEffectByEffectName(
            effectName: parameters
        );
        if (effect is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock preview effect parameter - the effect name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserPreviewedUnlocks
        );
        
        Task.Run(
            function: async () =>
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature = false;
                
                BackgroundAvatarsController.Instance.PreviewAvatarEffect(
                    effectBackgroundAvatarShaderEffect: effect.Value
                );
                BackgroundAvatarsController.Instance.ShowPreviewAvatar();

                await Task.Delay(
                    millisecondsDelay: ServiceJoystickWebSocketPayloadChatHandler.c_previewLengthInMilliseconds
                );
                
                BackgroundAvatarsController.Instance.HidePreviewAvatar();
                
                await Task.Delay(
                    millisecondsDelay: ServiceJoystickWebSocketPayloadChatHandler.c_previewTimeoutInMilliseconds
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature = true;
            }
        );
    }

    private static void HandleBotCommandUnlockPreviewModel(
        string username,
        string parameters
    )
    {
        var model = MappingModelNames.GetModelByModelName(
            modelName: parameters
        );
        if (model is null)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !unlock preview model parameter - the model name was invalid"
            );
            return;
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserPreviewedUnlocks
        );
        
        Task.Run(
            function: async () =>
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature = false;
                
                BackgroundAvatarsController.Instance.PreviewAvatarModel(
                    effectBackgroundAvatarShaderModel: model.Value
                );
                BackgroundAvatarsController.Instance.ShowPreviewAvatar();

                await Task.Delay(
                    millisecondsDelay: ServiceJoystickWebSocketPayloadChatHandler.c_previewLengthInMilliseconds
                );
                
                BackgroundAvatarsController.Instance.HidePreviewAvatar();
                
                await Task.Delay(
                    millisecondsDelay: ServiceJoystickWebSocketPayloadChatHandler.c_previewTimeoutInMilliseconds
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.s_canPreviewAnAvatarFeature = true;
            }
        );
    }
    
    private static void HandleBotCommandWalk(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !walk user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !walk parameter - !walk must be followed by start or stop."
            );
            return;
        }
        
        var serviceBreak   = Services.GetService<ServiceBreak>();
        var serviceNSFW    = Services.GetService<ServiceNSFW>();
        var serviceStretch = Services.GetService<ServiceStretch>();
        var serviceWorkOut = Services.GetService<ServiceWorkOut>();
        
        var command        = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceBreak.Stop();
                serviceNSFW.Stop();
                serviceStretch.Stop();
                serviceWorkOut.Stop();
                
                ServiceJoystickWebSocketPayloadChatHandler.s_isWalking = true;
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"👟 Walk mode started."
                );

                Task.Run(
                    function: async () =>
                    {
                        await Task.Delay(
                            millisecondsDelay: 200
                        );

                        ServiceStretch.DisplayStretchAndSwapLayoutToMe();
                    }
                );
                break;
            
            case "stop":
                ServiceJoystickWebSocketPayloadChatHandler.s_isWalking = false;
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"👟 Walk mode stopped."
                );
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !walk parameter - !walk must be followed by start or stop."
                );
                return;
        }
    }
    
    private static void HandleBotCommandWorkOut(
        string username,
        string parameters
    )
    {
        if (username is not ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername)
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !workout user - only {ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername} can use this command."
            );
            return;
        }
        
        var commandSplit = parameters.Split(
            separator: ' '
        );
        
        if (
            commandSplit.Length > 1
        )
        {
            ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                message: $"🛑 @{username} - Invalid !workout parameter - !workout must be followed by start or stop."
            );
            return;
        }

        var serviceWorkOut = Services.GetService<ServiceWorkOut>();
        var command        = commandSplit[0];
        switch (command)
        {
            case "start":
                serviceWorkOut.Start();
                break;
            
            case "stop":
                serviceWorkOut.Stop();
                break;
            
            default:
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🛑 @{username} - Invalid !workout parameter - !workout must be followed by start or stop."
                );
                return;
        }
    }

    private static void HandleStreamerShoutout(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var author           = payloadMessage.Author;
        var isContentCreator = author.IsContentCreator;
        if (isContentCreator is false)
        {
            return;
        }
        
        var username = author.Username;
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_streamersShoutedOut.Add(
                item: username
            ) is false
        )
        {
            return;
        }

        string[] messages =
        [
            $"📣 Oh shit, it's @{username}! Check out their streams: {ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{username}",
            $"📣 Holy fuck, a wild @{username} appeared! Go catch their streams: {ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{username}",
            $"📣 Shoutout to @{username}! Check out their streams: {ServiceJoystickWebSocketPayloadChatHandler.c_joystickUserStreamLinkPrefix}{username}",
        ];
        var random = new RandomNumberGenerator();
        var index  = random.RandiRange(
            from: 0,
            to:   messages.Length - 1
        );
        
        ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
            message: messages[index]
        );
    }
}