
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
using Overlay.Core.Services.Govee;
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
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCheckedBanks                  += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserCheckedBanks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCheckedUnlocks                += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserCheckedUnlocks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserClearedNames                  += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserClearedNames;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserClearedTitles                 += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserClearedTitles;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedAvatar              += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserCustomizedAvatar;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedBadge               += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserCustomizedBadge;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedName                += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserCustomizedName;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserCustomizedTitle               += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserCustomizedTitle;    
        ServiceDatabaseTaskEvents.RetrievedAchievementUserLinkedSteams                  += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserLinkedSteams;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserMessagesSent                  += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserMessagesSent;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserPreviewedUnlocks              += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserPreviewedUnlocks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorLose3InARow   += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorLose3InARow;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorLosses        += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorLosses;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorMatchesPlayed += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorMatchesPlayed;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorPapers        += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorPapers;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorRocks         += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorRocks;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorScissors      += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorScissors;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorTie3InARow    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorTie3InARow;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorTies          += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorTies;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorWin3InARow    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorWin3InARow;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRockPaperScissorWins          += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRockPaperScissorWins;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1s                      += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled1s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled42s                     += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled42s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled67s                     += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled67s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled69s                     += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled69s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled100s                    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled100s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolls                         += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolls;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRollsMaximum                  += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRollsMaximum;
        
        // Non-Meta Achievements
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled240s                    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled240s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled256s                    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled256s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled420s                    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled420s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled720s                    += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled720s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1080s                   += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled1080s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled1337s                   += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled1337s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled3840s                   += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled3840s;
        ServiceDatabaseTaskEvents.RetrievedAchievementUserRolled100000s                 += ServiceJoystickWebSocketPayloadChatHandlerAchievements.OnRetrievedAchievementUserRolled100000s;

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
    
    private struct LightCommandResult 
    {
        public IServiceColorInterpolatorDefinition.ColorType? Color;
        public string                                         Scene;
        public bool IsValid => 
            this.Color.HasValue || 
            !string.IsNullOrEmpty(
                value: this.Scene
            );
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
    private const string                                                          c_joystickUserStreamLinkPrefix                      = "https://api.joystick.tv/u/";
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
                    message: $"🏦 Deposited {amount} Gush Control Link minute{(amount > 1 ? "s" : string.Empty)} for {targetUser}. Type !bank withdraw {amount} if you would like to use it now!"
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
                    message: $"🏦 {targetUser}'s bank balance has been set to {amount} minute{(amount != 1 ? "s" : string.Empty)}."
                );
            }
        );
    }

    private static ServiceColorInterpolatorColorMode? GetColorByColorName(
        string colorName
    )
    {
        return colorName switch
        {
            "blue"                   => ServiceColorInterpolatorColorMode.Blue,
            "blue raspberry"         => ServiceColorInterpolatorColorMode.BlueRaspberry,
            "creamsicle banana"      => ServiceColorInterpolatorColorMode.CreamsicleBanana,
            "creamsicle blueberry"   => ServiceColorInterpolatorColorMode.CreamsicleBlueberry,
            "creamsicle dragonfruit" => ServiceColorInterpolatorColorMode.CreamsicleDragonfruit,
            "creamsicle lime"        => ServiceColorInterpolatorColorMode.CreamsicleLime,
            "creamsicle orange"      => ServiceColorInterpolatorColorMode.CreamsicleOrange,
            "creamsicle strawberry"  => ServiceColorInterpolatorColorMode.CreamsicleStrawberry,
            "cyan"                   => ServiceColorInterpolatorColorMode.Cyan,
            "cyberpunk"              => ServiceColorInterpolatorColorMode.Cyberpunk,
            "forest sunset"          => ServiceColorInterpolatorColorMode.ForestSunset,
            "green"                  => ServiceColorInterpolatorColorMode.Green,
            "heatwave"               => ServiceColorInterpolatorColorMode.Heatwave,
            "icy"                    => ServiceColorInterpolatorColorMode.Icy,
            "magenta"                => ServiceColorInterpolatorColorMode.Magenta,
            "orange"                 => ServiceColorInterpolatorColorMode.Orange,
            "orange purple"          => ServiceColorInterpolatorColorMode.OrangePurple,
            "purple"                 => ServiceColorInterpolatorColorMode.Purple,
            "rainbow"                => ServiceColorInterpolatorColorMode.Rainbow,
            "red"                    => ServiceColorInterpolatorColorMode.Red,
            "red white blue"         => ServiceColorInterpolatorColorMode.RedWhiteBlue,
            "toxic"                  => ServiceColorInterpolatorColorMode.Toxic,
            "vaporwave"              => ServiceColorInterpolatorColorMode.Vaporwave,
            "watermelon"             => ServiceColorInterpolatorColorMode.Watermelon,
            "white"                  => ServiceColorInterpolatorColorMode.White,
            "yellow"                 => ServiceColorInterpolatorColorMode.Yellow,
            _                        => null
        };
    }
    
    private static EffectBackgroundAvatarShaderEffect? GetEffectByEffectName(
        string effectName
    )
    {
        return effectName switch
        {
            "base"               => EffectBackgroundAvatarShaderEffect.Base,
            "atomic particles"   => EffectBackgroundAvatarShaderEffect.AtomicParticles,
            "binary rain"        => EffectBackgroundAvatarShaderEffect.BinaryRain,
            "bio sparks"         => EffectBackgroundAvatarShaderEffect.BioSparks,
            "cellular armor"     => EffectBackgroundAvatarShaderEffect.CellularArmor,
            "circuit flow"       => EffectBackgroundAvatarShaderEffect.CircuitFlow,
            "clockwise sweep"    => EffectBackgroundAvatarShaderEffect.ClockwiseSweep,
            "data stream"        => EffectBackgroundAvatarShaderEffect.DataStream,
            "diagonal rain"      => EffectBackgroundAvatarShaderEffect.DiagonalRain,
            "diamond pulse"      => EffectBackgroundAvatarShaderEffect.DiamondPulse,
            "dot matrix"         => EffectBackgroundAvatarShaderEffect.DotMatrix,
            "electric cracks"    => EffectBackgroundAvatarShaderEffect.ElectricCracks,
            "energy scan"        => EffectBackgroundAvatarShaderEffect.EnergyScan,
            "falling shards"     => EffectBackgroundAvatarShaderEffect.FallingShards,
            "fractal zoom"       => EffectBackgroundAvatarShaderEffect.FractalZoom,
            "glitch slices"      => EffectBackgroundAvatarShaderEffect.GlitchSlices,
            "hex shield"         => EffectBackgroundAvatarShaderEffect.HexShield,
            "honeycomb pulse"    => EffectBackgroundAvatarShaderEffect.HoneycombPulse,
            "interference bars"  => EffectBackgroundAvatarShaderEffect.InterferenceBars,
            "laser sweep"        => EffectBackgroundAvatarShaderEffect.LaserSweep,
            "lava bubbles"       => EffectBackgroundAvatarShaderEffect.LavaBubbles,
            "matrix stripes"     => EffectBackgroundAvatarShaderEffect.MatrixStripes,
            "moire lines"        => EffectBackgroundAvatarShaderEffect.MoireLines,
            "moving arcs"        => EffectBackgroundAvatarShaderEffect.MovingArcs,
            "neural flash"       => EffectBackgroundAvatarShaderEffect.NeuralFlash,
            "outward ripples"    => EffectBackgroundAvatarShaderEffect.OutwardRipples,
            "overdrive bars"     => EffectBackgroundAvatarShaderEffect.OverdriveBars,
            "plasma drift"       => EffectBackgroundAvatarShaderEffect.PlasmaDrift,
            "power grid"         => EffectBackgroundAvatarShaderEffect.PowerGrid,
            "pulse wave"         => EffectBackgroundAvatarShaderEffect.PulseWave,
            "radar ring"         => EffectBackgroundAvatarShaderEffect.RadarRing,
            "rgb shift"          => EffectBackgroundAvatarShaderEffect.RgbShift,
            "rolling magma"      => EffectBackgroundAvatarShaderEffect.RollingMagma,
            "rotating cubes"     => EffectBackgroundAvatarShaderEffect.RotatingCubes,
            "scrolling vines"    => EffectBackgroundAvatarShaderEffect.ScrollingVines,
            "silk threads"       => EffectBackgroundAvatarShaderEffect.SilkThreads,
            "solar orbits"       => EffectBackgroundAvatarShaderEffect.SolarOrbits,
            "sonar pings"        => EffectBackgroundAvatarShaderEffect.SonarPings,
            "spinning vortex"    => EffectBackgroundAvatarShaderEffect.SpinningVortex,
            "square tiling"      => EffectBackgroundAvatarShaderEffect.SquareTiling,
            "static glitch"      => EffectBackgroundAvatarShaderEffect.StaticGlitch,
            "swarming nanites"   => EffectBackgroundAvatarShaderEffect.SwarmingNanites,
            "topo lines"         => EffectBackgroundAvatarShaderEffect.TopoLines,
            "tracking lines"     => EffectBackgroundAvatarShaderEffect.TrackingLines,
            "tunnel starfield"   => EffectBackgroundAvatarShaderEffect.TunnelStarfield,
            "vapor grid"         => EffectBackgroundAvatarShaderEffect.VaporGrid,
            "vertical bitstream" => EffectBackgroundAvatarShaderEffect.VerticalBitstream,
            "vertical drift"     => EffectBackgroundAvatarShaderEffect.VerticalDrift,
            "wind streaks"       => EffectBackgroundAvatarShaderEffect.WindStreaks,
            "zebra sweep"        => EffectBackgroundAvatarShaderEffect.ZebraSweep,
            _                    => null
        };
    }

    private static EffectBackgroundAvatarShaderModel? GetModelByModelName(
        string modelName
    )
    {
        return modelName switch
        {
            "human"               => EffectBackgroundAvatarShaderModel.Human,
            "airplane"            => EffectBackgroundAvatarShaderModel.Airplane,
            "asteroid"            => EffectBackgroundAvatarShaderModel.Asteroid,
            "banana"              => EffectBackgroundAvatarShaderModel.Banana,
            "bone"                => EffectBackgroundAvatarShaderModel.Bone,
            "boobs"               => EffectBackgroundAvatarShaderModel.Boobs,
            "brain"               => EffectBackgroundAvatarShaderModel.Brain,
            "branch"              => EffectBackgroundAvatarShaderModel.Branch,
            "bread"               => EffectBackgroundAvatarShaderModel.Bread,
            "cloud"               => EffectBackgroundAvatarShaderModel.Cloud,
            "companion cube"      => EffectBackgroundAvatarShaderModel.CompanionCube,
            "deep sea jellyfish"  => EffectBackgroundAvatarShaderModel.DeepSeaJellyfish,
            "die"                 => EffectBackgroundAvatarShaderModel.Die,
            "dinosaur"            => EffectBackgroundAvatarShaderModel.Dinosaur,
            "donut"               => EffectBackgroundAvatarShaderModel.Donut,
            "double helix"        => EffectBackgroundAvatarShaderModel.DoubleHelix,
            "dugtrio"             => EffectBackgroundAvatarShaderModel.Dugtrio,
            "egg"                 => EffectBackgroundAvatarShaderModel.Egg,
            "flask"               => EffectBackgroundAvatarShaderModel.Flask,
            "frying pan"          => EffectBackgroundAvatarShaderModel.FryingPan,
            "gears"               => EffectBackgroundAvatarShaderModel.Gears,
            "ghost"               => EffectBackgroundAvatarShaderModel.Ghost,
            "glados"              => EffectBackgroundAvatarShaderModel.GLADoS,
            "gun"                 => EffectBackgroundAvatarShaderModel.Gun,
            "hand"                => EffectBackgroundAvatarShaderModel.Hand,
            "hatsune miku"        => EffectBackgroundAvatarShaderModel.HatsuneMiku,
            "heart"               => EffectBackgroundAvatarShaderModel.Heart,
            "jellyfish"           => EffectBackgroundAvatarShaderModel.Jellyfish,
            "katana"              => EffectBackgroundAvatarShaderModel.Katana,
            "mushroom"            => EffectBackgroundAvatarShaderModel.Mushroom,
            "octopus"             => EffectBackgroundAvatarShaderModel.Octopus,
            "penis"               => EffectBackgroundAvatarShaderModel.Penis,
            "pikmin"              => EffectBackgroundAvatarShaderModel.Pikmin,
            "pokeball"            => EffectBackgroundAvatarShaderModel.Pokeball,
            "potato"              => EffectBackgroundAvatarShaderModel.Potato,
            "robot"               => EffectBackgroundAvatarShaderModel.Robot,
            "rocket"              => EffectBackgroundAvatarShaderModel.Rocket,
            "rook"                => EffectBackgroundAvatarShaderModel.Rook,
            "sentry"              => EffectBackgroundAvatarShaderModel.Sentry,
            "snowman"             => EffectBackgroundAvatarShaderModel.Snowman,
            "solar system"        => EffectBackgroundAvatarShaderModel.SolarSystem,
            "spider"              => EffectBackgroundAvatarShaderModel.Spider,
            "squid"               => EffectBackgroundAvatarShaderModel.Squid,
            "star"                => EffectBackgroundAvatarShaderModel.Star,
            "sticky bomb"         => EffectBackgroundAvatarShaderModel.StickyBomb,
            "tank"                => EffectBackgroundAvatarShaderModel.Tank,
            "tie fighter"         => EffectBackgroundAvatarShaderModel.TieFighter,
            "tree"                => EffectBackgroundAvatarShaderModel.Tree,
            "triangle"            => EffectBackgroundAvatarShaderModel.Triangle,
            "ufo"                 => EffectBackgroundAvatarShaderModel.UFO,
            "xwing"               => EffectBackgroundAvatarShaderModel.XWing,
            _                     => null
        };
    }
    
    private static ServiceAchievementTitle? GetTitleByTitleName(
        string titleName
    )
    {
        return titleName switch
        {
            // Meta Achievements
            "interesting"                      => ServiceAchievementTitle.CheckedBank_1,
            "unlocked"                         => ServiceAchievementTitle.CheckedUnlocks_1,
            "unnamed"                          => ServiceAchievementTitle.ClearedName_1,
            "no title"                         => ServiceAchievementTitle.ClearedTitle_1,
            "bonus"                            => ServiceAchievementTitle.CompletedBhopBonus_1,
            "bunny hopper"                     => ServiceAchievementTitle.CompletedBhopMap_1,
            "custom built"                     => ServiceAchievementTitle.CustomizedAvatar_1,
            "papers, please"                   => ServiceAchievementTitle.CustomizedBadge_1,
            "i have a name"                    => ServiceAchievementTitle.CustomizedName_1,
            "title & registration"             => ServiceAchievementTitle.CustomizedTitle_1,
            "steam linked"                     => ServiceAchievementTitle.LinkedSteam_1,
            "i talked"                         => ServiceAchievementTitle.MessagesSent_1,
            "i chatted"                        => ServiceAchievementTitle.MessagesSent_10,
            "i expressed"                      => ServiceAchievementTitle.MessagesSent_100,
            "i voiced"                         => ServiceAchievementTitle.MessagesSent_1000,
            "i proclaimed"                     => ServiceAchievementTitle.MessagesSent_10000,
            "i talked... a lot"                => ServiceAchievementTitle.MessagesSent_100000,
            "the spectator"                    => ServiceAchievementTitle.MinutesWatched_1,
            "the observer"                     => ServiceAchievementTitle.MinutesWatched_10,
            "the watcher"                      => ServiceAchievementTitle.MinutesWatched_100,
            "the regular"                      => ServiceAchievementTitle.MinutesWatched_1000,
            "the fixture"                      => ServiceAchievementTitle.MinutesWatched_10000,
            "the monument"                     => ServiceAchievementTitle.MinutesWatched_100000,
            "the demoman"                      => ServiceAchievementTitle.PlayedDemoman_1,
            "the engineer"                     => ServiceAchievementTitle.PlayedEngineer_1,
            "the heavy"                        => ServiceAchievementTitle.PlayedHeavy_1,
            "the medic"                        => ServiceAchievementTitle.PlayedMedic_1,
            "the pyro"                         => ServiceAchievementTitle.PlayedPyro_1,
            "the scout"                        => ServiceAchievementTitle.PlayedScout_1,
            "the sniper"                       => ServiceAchievementTitle.PlayedSniper_1,
            "the soldier"                      => ServiceAchievementTitle.PlayedSoldier_1,
            "the spy"                          => ServiceAchievementTitle.PlayedSpy_1,
            "eye candy"                        => ServiceAchievementTitle.PreviewedUnlocks_1,
            "just a little curious"            => ServiceAchievementTitle.QuestionsAsked_1,
            "asking the tough questions"       => ServiceAchievementTitle.QuestionsAsked_10,
            "questionable"                     => ServiceAchievementTitle.QuestionsAsked_100,
            "i'm done asking"                  => ServiceAchievementTitle.QuestionsAsked_1000,
            "unlucky"                          => ServiceAchievementTitle.RockPaperScissorLose3InARow_1,
            "i lost"                           => ServiceAchievementTitle.RockPaperScissorLosses_1,
            "skill issue"                      => ServiceAchievementTitle.RockPaperScissorLosses_10,
            ";_;"                              => ServiceAchievementTitle.RockPaperScissorLosses_100,
            "loser - beck"                     => ServiceAchievementTitle.RockPaperScissorLosses_1000,
            "challenger"                       => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1,
            "contender"                        => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10,
            "experienced"                      => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_100,
            "veteran"                          => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_1000,
            "traumatized"                      => ServiceAchievementTitle.RockPaperScissorMatchesPlayed_10000,
            "paper"                            => ServiceAchievementTitle.RockPaperScissorPapers_1,
            "flat hand"                        => ServiceAchievementTitle.RockPaperScissorPapers_10,
            "loose leaf"                       => ServiceAchievementTitle.RockPaperScissorPapers_100,
            "hole punched"                     => ServiceAchievementTitle.RockPaperScissorPapers_1000,
            "rock"                             => ServiceAchievementTitle.RockPaperScissorRocks_1,
            "fisticuffs"                       => ServiceAchievementTitle.RockPaperScissorRocks_10,
            "rock hard"                        => ServiceAchievementTitle.RockPaperScissorRocks_100,
            "stoned"                           => ServiceAchievementTitle.RockPaperScissorRocks_1000,
            "scissors"                         => ServiceAchievementTitle.RockPaperScissorScissors_1,
            "a cut above"                      => ServiceAchievementTitle.RockPaperScissorScissors_10,
            "sliced"                           => ServiceAchievementTitle.RockPaperScissorScissors_100,
            "straight edge"                    => ServiceAchievementTitle.RockPaperScissorScissors_1000,
            "average"                          => ServiceAchievementTitle.RockPaperScissorTie3InARow_1,
            "i tied"                           => ServiceAchievementTitle.RockPaperScissorTies_1,
            "mirrored"                         => ServiceAchievementTitle.RockPaperScissorTies_10,
            "all tied up"                      => ServiceAchievementTitle.RockPaperScissorTies_100,
            "deadlock"                         => ServiceAchievementTitle.RockPaperScissorTies_1000,
            "lucky"                            => ServiceAchievementTitle.RockPaperScissorWin3InARow_1,
            "i won"                            => ServiceAchievementTitle.RockPaperScissorWins_1,
            "hot hands"                        => ServiceAchievementTitle.RockPaperScissorWins_10,
            "^_^"                              => ServiceAchievementTitle.RockPaperScissorWins_100,
            "professional"                     => ServiceAchievementTitle.RockPaperScissorWins_1000,
            "1"                                => ServiceAchievementTitle.Rolled1_1,
            "answered"                         => ServiceAchievementTitle.Rolled42_1,
            "6-7"                              => ServiceAchievementTitle.Rolled67_1,
            "nice"                             => ServiceAchievementTitle.Rolled69_1,
            "a+"                               => ServiceAchievementTitle.Rolled100_1,
            "roll'd"                           => ServiceAchievementTitle.Rolls_1,
            "roller"                           => ServiceAchievementTitle.Rolls_10,
            "high roller"                      => ServiceAchievementTitle.Rolls_100,
            "diced"                            => ServiceAchievementTitle.Rolls_1000,
            "gambler"                          => ServiceAchievementTitle.Rolls_10000,
            "max"                              => ServiceAchievementTitle.RollsMaximum_1,
            "maximum"                          => ServiceAchievementTitle.RollsMaximum_1000,
            "min"                              => ServiceAchievementTitle.RollsMinimum_1,
            "minimum"                          => ServiceAchievementTitle.RollsMinimum_1000,
            "owned"                            => ServiceAchievementTitle.SmoothDaggerDominatedBys_1,
            "i'm done"                         => ServiceAchievementTitle.SmoothDaggerDominatedBys_10,
            "i died to smoothdagger"           => ServiceAchievementTitle.SmoothDaggerDeaths_1,
            "uninstalling"                     => ServiceAchievementTitle.SmoothDaggerDeaths_10,
            "i quit"                           => ServiceAchievementTitle.SmoothDaggerDeaths_100,
            "tf2 sucks"                        => ServiceAchievementTitle.SmoothDaggerDeaths_1000,
            "dominated"                        => ServiceAchievementTitle.SmoothDaggerDominations_1,
            "lol"                              => ServiceAchievementTitle.SmoothDaggerDominations_10,
            "first of many"                    => ServiceAchievementTitle.SmoothDaggerKills_1,
            "target practice"                  => ServiceAchievementTitle.SmoothDaggerKills_10,
            "kingslayer"                       => ServiceAchievementTitle.SmoothDaggerKills_100,
            "i killed smoothdagger"            => ServiceAchievementTitle.SmoothDaggerKills_1000,
            "a dish served cold"               => ServiceAchievementTitle.SmoothDaggerRevenges_1,
            "revenge"                          => ServiceAchievementTitle.SmoothDaggerRevenges_10,
            "new hire"                         => ServiceAchievementTitle.TimesViewed_1,
            "enjoying the view"                => ServiceAchievementTitle.TimesViewed_10,
            "lurkin' around"                   => ServiceAchievementTitle.TimesViewed_100,
            "gooner"                           => ServiceAchievementTitle.TimesViewed_1000,
            "cannot unsee"                     => ServiceAchievementTitle.TimesViewed_10000,
        
            // Non-Meta Achievements
            "240p"                             => ServiceAchievementTitle.Rolled240_1,
            "0xffffffff"                       => ServiceAchievementTitle.Rolled256_4,
            "#yoloswag420"                     => ServiceAchievementTitle.Rolled420_1,
            "720p"                             => ServiceAchievementTitle.Rolled720_1,
            "1080p"                            => ServiceAchievementTitle.Rolled1080_1,
            "i speak l33t"                     => ServiceAchievementTitle.Rolled1337_1,
            "ultra-wide 4k"                    => ServiceAchievementTitle.Rolled3840_1,
            "0.0001%"                          => ServiceAchievementTitle.Rolled100000_1,
            "i did it!"                        => ServiceAchievementTitle.TitlesUnlocked_1,
            "title grinder"                    => ServiceAchievementTitle.TitlesUnlocked_10,
            "the collector"                    => ServiceAchievementTitle.TitlesUnlocked_25,
            "legacy bearer"                    => ServiceAchievementTitle.TitlesUnlocked_50,
            "basically a pokemon trainer"      => ServiceAchievementTitle.TitlesUnlocked_75,
            "got'em all"                       => ServiceAchievementTitle.TitlesUnlocked_101,
            "tutorial completed"               => ServiceAchievementTitle.TracksCompleted_1,
            "on track"                         => ServiceAchievementTitle.TracksCompleted_5,
            "paving the way"                   => ServiceAchievementTitle.TracksCompleted_10,
            "trailblazer"                      => ServiceAchievementTitle.TracksCompleted_20,
            "completionist"                    => ServiceAchievementTitle.TracksCompleted_30,
            "god walking amongst mere mortals" => ServiceAchievementTitle.TracksCompleted_40,
            "i just work here"                 => ServiceAchievementTitle.TracksCompleted_48,
        
            // Only Given Achievements
            "dev"                              => ServiceAchievementTitle.Dev,
            _                                  => null,
        };
    }
    
    private static LightCommandResult GetLightPatternAction(
        string lightPattern
    )
    {
        return lightPattern switch 
        { 
            "blue"                   => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Blue    },
            "blue raspberry"         => new LightCommandResult { Scene = ServiceGoveeSceneNames.BlueRaspberry                  },
            "creamsicle banana"      => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleBanana               },
            "creamsicle blueberry"   => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleBlueberry            },
            "creamsicle dragonfruit" => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleDragonfruit          },
            "creamsicle lime"        => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleLime                 },
            "creamsicle orange"      => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleOrange               },
            "creamsicle strawberry"  => new LightCommandResult { Scene = ServiceGoveeSceneNames.CreamsicleStrawberry           },
            "cyan"                   => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Cyan    },
            "cyberpunk"              => new LightCommandResult { Scene = ServiceGoveeSceneNames.Cyberpunk                      },
            "forest sunset"          => new LightCommandResult { Scene = ServiceGoveeSceneNames.ForestSunset                   },
            "green"                  => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Green   },
            "heatwave"               => new LightCommandResult { Scene = ServiceGoveeSceneNames.Heatwave                       },
            "icy"                    => new LightCommandResult { Scene = ServiceGoveeSceneNames.Icy                            },
            "magenta"                => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Magenta },
            "orange"                 => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Orange  },
            "orange purple"          => new LightCommandResult { Scene = ServiceGoveeSceneNames.OrangePurple                   },
            "purple"                 => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Purple  },
            "rainbow"                => new LightCommandResult { Scene = ServiceGoveeSceneNames.Rainbow                        },
            "red"                    => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Red     },
            "red white blue"         => new LightCommandResult { Scene = ServiceGoveeSceneNames.RedWhiteBlue                   },
            "toxic"                  => new LightCommandResult { Scene = ServiceGoveeSceneNames.Toxic                          },
            "vaporwave"              => new LightCommandResult { Scene = ServiceGoveeSceneNames.Vaporwave                      },
            "watermelon"             => new LightCommandResult { Scene = ServiceGoveeSceneNames.Watermelon                     },
            "white"                  => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.White   },
            "yellow"                 => new LightCommandResult { Color = IServiceColorInterpolatorDefinition.ColorType.Yellow  },
            _                        => default
        };
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