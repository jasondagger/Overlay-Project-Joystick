
using Overlay.Core.Contents;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.StreamEvents;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.TextToSpeeches;
using Overlay.Core.Services.JoystickBots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static partial class ServiceJoystickWebSocketPayloadChatHandler
{
    static ServiceJoystickWebSocketPayloadChatHandler()
    {

    }

    internal static Action<string, string> SteamUserUpdated = null;
    
    internal static void AddUserForClaimRewardRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingClaimRewardUsers.Add(
            item: username
        );
    }
    
    internal static void AddUserForGushControlLinkRollRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSpinForRandomGushControlLinkTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSpinForRandomGushControlLinkTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }
    
    internal static void AddUserForLightRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingLightRequestTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingLightRequestTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }
    
    internal static void AddUserForSFXRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSFXRequestTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSFXRequestTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }

    internal static void AddUserForSongRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }
    
    internal static void AddUserForTF2TriggerAnActionRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingTF2TriggerAnActionTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingTF2TriggerAnActionTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }
    
    internal static void AddUserForUnlockColorRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockColorRequestTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }
    
    internal static void AddUserForUnlockEffectRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockEffectRequestTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }
    
    internal static void AddUserForUnlockModelRequests(
        string username
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers[key: username] = 
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingUnlockModelRequestTippers.GetValueOrDefault(
                key:          username, 
                defaultValue: 0
            ) + 1;
    }

    internal static ServiceColorInterpolatorColorMode? GetUserCustomBadgeColor(
        string username
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors.TryGetValue(
                key:   username,
                value: out var color
            ) is true
        )
        {
            return color;
        }
        return null;
    }
    
    internal static ServiceColorInterpolatorColorMode? GetUserCustomNameColor(
        string username
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors.TryGetValue(
                key:   username,
                value: out var color
            ) is true
        )
        {
            return color;
        }
        return null;
    }
    
    internal static void HandleWebSocketPayloadChat(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var isCommand = ServiceJoystickWebSocketPayloadChatHandler.IsMessageACommand(
            payloadMessage: payloadMessage
        );
        
        ServiceJoystickWebSocketPayloadChatHandler.AddChatMessage(
            payloadMessage: payloadMessage
        );

        if (isCommand is false)
        {
            ServiceJoystickWebSocketPayloadChatHandler.PlayChatNotificationSoundEffect(
                payloadMessage: payloadMessage
            );
        }
        
        ServiceJoystickWebSocketPayloadChatHandler.HandleStreamerShoutout(
            payloadMessage: payloadMessage
        );
        ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommands(
            payloadMessage: payloadMessage
        );
        
        if (isCommand is false)
        {
            ServiceGodotTextToSpeech.Speak(
                message: $"{payloadMessage.Author.Username} says... {payloadMessage.Text}"
            );
        }
    }

    internal static void ProcessSongRequest(
        bool succeeded
    )
    {
        var actioner = ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequesters.Dequeue();
        if (succeeded is true)
        {
            return;
        }
        
        if (actioner.IsModeratorOrSubscriber is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_moderatorsAndSubscribersWhoUsedSongRequestCommand.Remove(
                item: actioner.Name
            );
        }
        else if (actioner.IsTipper is true)
        {
            ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: actioner.Name]--;
            if (ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers[key: actioner.Name] <= 0)
            {
                ServiceJoystickWebSocketPayloadChatHandler.s_pendingSongRequestTippers.Remove(
                    key: actioner.Name
                );
            }
        }
    }

    internal static void RegisterForEvents()
    {
        // Meta Achievements
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCheckedBanks                  += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserCheckedBanks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCheckedUnlocks                += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserCheckedUnlocks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserClearedNames                  += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserClearedNames;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserClearedTitles                 += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserClearedTitles;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedAvatar              += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserCustomizedAvatar;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedBadge               += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserCustomizedBadge;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedName                += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserCustomizedName;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedTitle               += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserCustomizedTitle;    
        ServiceDatabaseTaskEvents.RetrievedAchievementUserLinkedSteams                  += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserLinkedSteams;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserMessagesSent                  += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserMessagesSent;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserPreviewedUnlocks              += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserPreviewedUnlocks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorLose3InARow   += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorLose3InARow;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorLosses        += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorLosses;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorMatchesPlayed += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorMatchesPlayed;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorPapers        += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorPapers;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorRocks         += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorRocks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorScissors      += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorScissors;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorTie3InARow    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorTie3InARow;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorTies          += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorTies;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorWin3InARow    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorWin3InARow;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorWins          += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorWins;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1s                      += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled1s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled42s                     += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled42s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled67s                     += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled67s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled69s                     += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled69s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled100s                    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled100s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolls                         += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolls;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRollsMaximum                  += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRollsMaximum;
        
        // Non-Meta Achievements
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled240s                    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled240s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled256s                    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled256s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled420s                    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled420s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled720s                    += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled720s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1080s                   += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled1080s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1337s                   += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled1337s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled3840s                   += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled3840s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled100000s                 += ServiceDatabaseHandlerAchievements.OnRetrievedAchievementUserRolled100000s;

        ServiceDatabaseTaskEvents.RetrievedBankUser                                     += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedBankUser;
        ServiceDatabaseTaskEvents.RetrievedBankUserTimeLimit                            += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedBankUserTimeLimit;
        ServiceDatabaseTaskEvents.RetrievedSteamUser                                    += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedSteamUser;
        ServiceDatabaseTaskEvents.RetrievedListUserBadgeColors                          += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedUserBadgeColors;
        ServiceDatabaseTaskEvents.RetrievedListUserNameColors                           += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedUserNameColors;
        ServiceDatabaseTaskEvents.RetrievedUserUnlockColors                             += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedUserUnlockColors;
        ServiceDatabaseTaskEvents.RetrievedUserUnlockEffects                            += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedUserUnlockEffects;
        ServiceDatabaseTaskEvents.RetrievedUserUnlockModels                             += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedUserUnlockModels;
        ServiceDatabaseTaskEvents.RetrievedUserUnlockTitles                             += ServiceJoystickWebSocketPayloadChatHandler.OnRetrievedUserUnlockTitles;

        ServiceDatabaseTaskEvents.ValidatedUserHasColor_Avatar                          += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserHasColor_Avatar;
        ServiceDatabaseTaskEvents.ValidatedUserHasColor_Badge                           += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserHasColor_Badge;
        ServiceDatabaseTaskEvents.ValidatedUserHasColor_Name                            += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserHasColor_Name;
        ServiceDatabaseTaskEvents.ValidatedUserHasEffectAndColor                        += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserHasEffectAndColor;
        ServiceDatabaseTaskEvents.ValidatedUserHasModel                                 += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserHasModel;
        ServiceDatabaseTaskEvents.ValidatedUserHasTitle                                 += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserHasTitle;
        ServiceDatabaseTaskEvents.ValidatedUserUnlockColor                              += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserUnlockColor;
        ServiceDatabaseTaskEvents.ValidatedUserUnlockEffect                             += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserUnlockEffect;
        ServiceDatabaseTaskEvents.ValidatedUserUnlockModel                              += ServiceJoystickWebSocketPayloadChatHandler.OnValidatedUserUnlockModel;
    }
    
    private struct AvatarUpdateData
        {
            public EffectBackgroundAvatarShaderEffect? Effect  = null;
            public EffectBackgroundAvatarShaderModel?  Model   = null;
            public EffectBackgroundAvatarShaderSlot?   Slot    = null;
            public ServiceColorInterpolatorColorMode?  Color   = null;
            public ServiceColorInterpolatorColorMode?  Base    = null;
            public ServiceColorInterpolatorColorMode?  Outline = null;
    
            public AvatarUpdateData()
            {
                
            }
        }
    
    private class SpotifySongActioner
    {
        internal string Name                    = string.Empty;
        internal bool   IsModeratorOrSubscriber = false;
        internal bool   IsTipper                = false;
    }
    
    private const string                                                          c_commandPrefix                                     = $"!";
    private const int                                                             c_commandRollTheDiceDefaultParameter                = 100;
    private const string                                                          c_commandSFX                                        = $"!sfx";
    private const string                                                          c_commandTip                                        = $"!tip";
    private const int                                                             c_delayGushControlLinkSetupInMilliseconds           = 30000;
    private const string                                                          c_joystickUserStreamLinkPrefix                      = "https://www.joystick.tv/u/";
    private const int                                                             c_previewLengthInMilliseconds                       = 10000;
    private const int                                                             c_previewTimeoutInMilliseconds                      = 300000;
    private const string                                                          c_streamerUsername                                  = $"SmoothDagger";
    
    private static readonly Dictionary<string, int>                               s_bankUserLimits                                    = [];
    private static readonly Dictionary<string, int>                               s_bankUserWithdrawTotals                            = [];
    private static readonly object                                                s_errorMessageLock                                  = new();
    private static readonly HashSet<string>                                       s_moderatorsAndSubscribersWhoUsedLightCommand       = [];
    private static readonly HashSet<string>                                       s_moderatorsAndSubscribersWhoUsedSFXCommand         = [];
    private static readonly HashSet<string>                                       s_moderatorsAndSubscribersWhoUsedSongRequestCommand = [];
    private static readonly HashSet<string>                                       s_moderatorsAndSubscribersWhoUsedSongSkipCommand    = [];
    private static readonly HashSet<string>                                       s_moderatorsAndSubscribersWhoUsedTF2Command         = [];
    private static readonly Dictionary<string, AvatarUpdateData>                  s_pendingAvatarUpdateUsers                          = [];
    private static readonly Dictionary<string, ServiceColorInterpolatorColorMode> s_pendingBadgeColorUpdateUsers                      = [];
    private static readonly HashSet<string>                                       s_pendingClaimRewardUsers                           = [];
    private static readonly Dictionary<string, int>                               s_pendingLightRequestTippers                        = [];
    private static readonly Dictionary<string, ServiceColorInterpolatorColorMode> s_pendingNameColorUpdateUsers                       = [];
    private static readonly Dictionary<string, int>                               s_pendingSFXRequestTippers                          = [];
    private static readonly Queue<SpotifySongActioner>                            s_pendingSongRequesters                             = [];
    private static readonly Dictionary<string, int>                               s_pendingSongRequestTippers                         = [];
    private static readonly Dictionary<string, int>                               s_pendingSpinForRandomGushControlLinkTippers        = [];
    private static readonly Dictionary<string, int>                               s_pendingTF2TriggerAnActionTippers                  = [];
    private static readonly Dictionary<string, ServiceAchievementTitle>           s_pendingTitleUpdateUsers                           = [];
    private static readonly Dictionary<string, int>                               s_pendingUnlockColorRequestTippers                  = [];
    private static readonly Dictionary<string, int>                               s_pendingUnlockEffectRequestTippers                 = [];
    private static readonly HashSet<string>                                       s_pendingUnlockers                                  = [];
    private static readonly Dictionary<string, int>                               s_pendingUnlockModelRequestTippers                  = [];
    private static readonly Dictionary<string, int>                               s_pendingWithdrawals                                = [];
    private static readonly HashSet<string>                                       s_streamersShoutedOut                               = [
        ServiceJoystickWebSocketPayloadChatHandler.c_streamerUsername
    ];
    private static readonly Dictionary<string, ServiceColorInterpolatorColorMode> s_userCustomBadgeColors                             = [];
    private static readonly Dictionary<string, ServiceColorInterpolatorColorMode> s_userCustomNameColors                              = [];
    private static readonly object                                                s_userCustomColorLock                               = new();
    private static readonly Dictionary<string, int>                               s_userRockPaperScissorLosses3InARow                 = new();
    private static readonly Dictionary<string, int>                               s_userRockPaperScissorTies3InARow                   = new();
    private static readonly Dictionary<string, int>                               s_userRockPaperScissorWins3InARow                   = new();
    
    private static bool                                                           s_canPreviewAnAvatarFeature                         = true;
    private static string                                                         s_gushControllerUsername                            = string.Empty;
    private static bool                                                           s_isAClaimCommandInProgress                         = false;
    private static bool                                                           s_isCommandAnErrorForSmoothDagger                   = false;
    private static bool                                                           s_isFocusing                                        = false;
    private static bool                                                           s_isWalking                                         = false;
    
    private static void AddChatMessage(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var author            = payloadMessage.Author;
        var message           = payloadMessage.Text;
        
        var username          = author.Username;
        var isModerator       = author.IsModerator;
        var isStreamer        = author.IsStreamer;
        var isSubscriber      = author.IsSubscriber;
        
        var emotes            = payloadMessage.EmotesUsed;
        var numberOfEmotes    = emotes.Length;
        var chatMessageEmotes = new ChatMessageEmote[numberOfEmotes];
        for (var i = 0U; i < numberOfEmotes; i++)
        {
            var emote            = emotes[i];
            chatMessageEmotes[i] = new ChatMessageEmote(
                code: emote.Code,
                url:  emote.SignedUrl
            );
        }
        
        bool hasCustomBadgeColor;
        ServiceColorInterpolatorColorMode customBadgeColor;
        
        bool hasCustomNameColor;
        ServiceColorInterpolatorColorMode customNameColor;
        
        lock (ServiceJoystickWebSocketPayloadChatHandler.s_userCustomColorLock)
        {
            hasCustomBadgeColor = ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors.ContainsKey(
                key: username
            );
            customBadgeColor = hasCustomBadgeColor ?
                ServiceJoystickWebSocketPayloadChatHandler.s_userCustomBadgeColors[key: username] :
                ServiceColorInterpolatorColorMode.White;
            
            hasCustomNameColor = ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors.ContainsKey(
                key: username
            );
            customNameColor = hasCustomNameColor ?
                ServiceJoystickWebSocketPayloadChatHandler.s_userCustomNameColors[key: username] :
                ServiceColorInterpolatorColorMode.White;
        }
        
        Chat.AddChatMessageToInstances(
            username:            username,
            hasCustomBadgeColor: hasCustomBadgeColor,
            customBadgeColor:    customBadgeColor,
            hasCustomNameColor:  hasCustomNameColor,
            customNameColor:     customNameColor,
            message:             message,
            chatMessageEmotes:   chatMessageEmotes,
            isModerator:         isModerator,
            isStreamer:          isStreamer,
            isSubscriber:        isSubscriber,
            isBot:               false
        );
        
        BackgroundAvatarsController.Instance.ShowAvatarNameplate(
            username: username
        );
        
        ServiceJoystickWebSocketPayloadChatHandler.RequestAchievementUser(
            username:                     username,
            serviceDatabaseTaskQueryType: ServiceDatabaseTaskQueryType.RetrieveAchievementUserMessagesSent
        );
    }

    private static int ConvertMinutesToMilliseconds(
        int minutes
    )
    {
        return minutes * 60000;
    }
    
    private static void ExecuteDeposit(
        string targetUser, 
        int    amount
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
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
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.DepositTimeForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
                
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏦 Deposited {amount} Gush Control Link minute{(amount > 1 ? "s" : string.Empty)} for @{targetUser}. Type !bank withdraw {amount} if you would like to use it now!"
                );
            }
        );
    }
    
    private static void ExecuteSetBankBalance(
        string targetUser, 
        int amount
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseBankUser.BankUser_Joystick_Username), 
                value:         targetUser
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
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.SetTimeForBankUser, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            
                ServiceJoystickWebSocketPayloadChatHandler.SendDelayedBotMessage(
                    message: $"🏦 @{targetUser}'s bank balance has been set to {amount} minute{(amount != 1 ? "s" : string.Empty)}."
                );
            }
        );
    }
    
    private static bool IsMessageACommand(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = payloadMessage.Text;
        return message.StartsWith(
            value:          ServiceJoystickWebSocketPayloadChatHandler.c_commandPrefix,
            comparisonType: StringComparison.OrdinalIgnoreCase
        ) is true;
    }

    private static bool IsMessageASfxCommand(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        var message = payloadMessage.Text;
        return message.StartsWith(
            value:          ServiceJoystickWebSocketPayloadChatHandler.c_commandSFX,
            comparisonType: StringComparison.OrdinalIgnoreCase
        ) is true;
    }
    
    private static void PlayChatNotificationSoundEffect(
        ServiceJoystickWebSocketPayloadMessage payloadMessage
    )
    {
        if (
            ServiceJoystickWebSocketPayloadChatHandler.IsMessageASfxCommand(
                payloadMessage: payloadMessage
            ) is true
        )
        {
            return;
        }
        
        var message = payloadMessage.Text;
        if (
            message.StartsWith(
                value:          ServiceJoystickWebSocketPayloadChatHandler.c_commandTip,
                comparisonType: StringComparison.OrdinalIgnoreCase
            ) is true
        )
        {
            var remainingMessage = message.Replace(
                oldValue: $"{ServiceJoystickWebSocketPayloadChatHandler.c_commandTip}",
                newValue: string.Empty
            );
            
            var isValidTip = true;
            foreach (var character in remainingMessage)
            {
                var isCharacterASpace = character is ' ';
                if (isCharacterASpace)
                {
                    continue;
                }
                var isCharacterANumber = char.IsNumber(
                    c: character
                );
                if (isCharacterANumber)
                {
                    break;
                }
                
                isValidTip = false;
                break;
            }
            
            if (isValidTip is true)
            {
                return;
            }
        }
        
        StreamEventsHelper.PlaySoundAlert(
            soundAlertType: ServiceGodotAudio.SoundAlertType.ChatNotification
        );
    }
    
    private static void RemoveUserBadgeColor(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserBadgeColor.UserBadgeColors_Username), 
                value:         username
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.RemoveUserBadgeColor, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private static void RemoveUserNameColor(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserNameColor.UserNameColors_Username), 
                value:         username
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.RemoveUserNameColor, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }

    private static void RemoveUserTitle(
        string username
    )
    {
        var serviceDatabaseTaskNpgsqlParameters = new List<ServiceDatabaseTaskNpgsqlParameter> {
            new(
                parameterName: nameof(ServiceDatabaseUserTitle.UserTitles_Username), 
                value:         username
            ),
        };
        
        Task.Run(
            function: async () =>
            {
                await ServiceDatabaseTaskNonQueries.ExecuteAsyncNonQuery(
                    serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.RemoveUserTitle, 
                    serviceDatabaseTaskSqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void SendDelayedBotMessage(
        string message
    )
    {
        ServiceJoystickWebSocketPayloadChatHandler.s_isCommandAnErrorForSmoothDagger = message.StartsWith(
            value: $"🛑 @SmoothDagger"
        );
        
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
    
    private static void SendDelayedBotWhisper(
        string username,
        string whisper
    )
    {
        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 200
                );
                
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendWhisper(
                    username: username,
                    message:  whisper
                );
            }
        );
    }
    
    private static void UpdateRockPaperScissorStreak(
        Dictionary<string, int> dictionary, 
        string                  key, 
        bool                    increment
    ) 
    {
        dictionary[key: key] = increment ? dictionary.TryGetValue(
            key:   key, 
            value: out var val
        ) ? val + 1 : 1 : 0;
    }
}