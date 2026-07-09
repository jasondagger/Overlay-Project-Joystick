
using Godot;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Services.Achievements;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Joysticks.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Contents;

public sealed partial class BackgroundAvatarsController() :
    Node()
{
    public static BackgroundAvatarsController Instance { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();
        
        this.SetInstance();
    }

    public override void _Ready()
    {
        base._Ready();
        
        BackgroundAvatarsController.SubscribeToEvents();
        this.RetrieveResources();
        this.SetPreviewAvatar();
        //this.AddDummies();
    }
    
    public override void _Process(
        double delta
    )
    {
        base._Process(
            delta: delta    
        );
        
        lock (this.m_lock)
        {
            if (this.m_backgroundAvatars.Count is 0)
            {
                return;
            }
        
            this.UpdateWander(
                delta: (float) delta
            );
            this.SortAvatars();
        }
    }

    internal async void AddAvatar(
        string                                                                           username,
        ServiceColorInterpolatorColorMode                                                colorModeBase,
        ServiceColorInterpolatorColorMode                                                colorModeOutline,
        Dictionary<EffectBackgroundAvatarShaderSlot, ServiceColorInterpolatorColorMode>  shaderEffectColorModes,
        Dictionary<EffectBackgroundAvatarShaderSlot, EffectBackgroundAvatarShaderEffect> shaderEffects,
        EffectBackgroundAvatarShaderModel                                                shaderModel
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username, 
                    value: out var existingAvatar
                ) is true
            )
            {
                if (
                    this.m_removalSchedules.Remove(
                        key: existingAvatar
                    ) is true
                )
                {
                    this.m_targetPositions[key: existingAvatar] = BackgroundAvatarsController.GetPositionGlobal();
                }
                
                return;
            }
            
            var totalOccupiedSlots = this.m_backgroundAvatars.Count + this.m_pendingQueueSubscribers.Count + this.m_pendingQueueViewers.Count;
            if (totalOccupiedSlots >= BackgroundAvatarsController.c_maxAvatars)
            {
                if (
                    this.m_pendingQueueViewers.Any(
                        predicate: p => p.Username == username
                    ) is true ||
                    this.m_pendingQueueSubscribers.Any(
                        predicate: p => p.Username == username
                    ) is true
                )
                {
                    return;
                }

                var isSubscriber = this.m_serviceJoystick.IsSubscriber(
                    username: username
                );
                if (isSubscriber is true)
                {
                    this.m_pendingQueueSubscribers.Add(
                        item: new PendingAvatar(
                            Username:               username,
                            ColorModeBase:          colorModeBase,
                            ColorModeOutline:       colorModeOutline,
                            ShaderEffectColorModes: shaderEffectColorModes,
                            ShaderEffects:          shaderEffects,
                            ShaderModel:            shaderModel
                        )
                    );
                }
                else
                {
                    this.m_pendingQueueViewers.Add(
                        item: new PendingAvatar(
                            Username:               username,
                            ColorModeBase:          colorModeBase,
                            ColorModeOutline:       colorModeOutline,
                            ShaderEffectColorModes: shaderEffectColorModes,
                            ShaderEffects:          shaderEffects,
                            ShaderModel:            shaderModel
                        )
                    );
                }
                return;
            }
        }
        
        const float c_spawnY = BackgroundAvatarsController.c_boundaryConstraintYMax + BackgroundAvatarsController.c_offScreenOffset;
        var rangeX = BackgroundAvatarsController.GetXRangeAtY(
            y: BackgroundAvatarsController.c_boundaryConstraintYMax
        );
        var spawnX = (float) GD.RandRange(
            from: rangeX.minX,
            to:   rangeX.maxX
        );
        
        var effectBackgroundAvatar      = this.BackgroundAvatarScene.Instantiate<EffectBackgroundAvatar>();
        effectBackgroundAvatar.Position = new Vector2(
            x: spawnX,
            y: c_spawnY
        );
        
        this.BackgroundAvatars.CallDeferred(
            method: Node.MethodName.AddChild, 
            args:   effectBackgroundAvatar
        );
        
        await effectBackgroundAvatar.ToSignal(
            source: effectBackgroundAvatar, 
            signal: Node.SignalName.Ready
        );
        
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryAdd(
                    key:   username,
                    value: effectBackgroundAvatar
                ) is false
            )
            {
                effectBackgroundAvatar.QueueFree();
                return;
            }
            
            this.m_sortList.Add(
                item: effectBackgroundAvatar
            );
            this.m_targetPositions[key: effectBackgroundAvatar] = BackgroundAvatarsController.GetPositionGlobal();
        }
        
        BackgroundAvatarsController.RequestUserTitle(
            username: username
        );
        
        effectBackgroundAvatar.SetNameplateUsername(
            username: username
        );
        this.UpdateAvatarBase(
            username:                          username,
            serviceColorInterpolatorColorMode: colorModeBase
        );
        this.UpdateAvatarOutline(
            username:                          username,
            serviceColorInterpolatorColorMode: colorModeOutline
        );
        this.UpdateAvatarModel(
            username:                          username,
            effectBackgroundAvatarShaderModel: shaderModel
        );

        foreach (var shaderSlot in BackgroundAvatarsController.s_shaderSlots)
        {
            var shaderEffectColorMode = shaderEffectColorModes[shaderSlot];
            var shaderEffect          = shaderEffects[shaderSlot];
                
            this.UpdateAvatarEffectColor(
                username:                          username,
                effectBackgroundAvatarShaderSlot:  shaderSlot,
                serviceColorInterpolatorColorMode: shaderEffectColorMode
            );
                
            this.UpdateAvatarEffect(
                username:                           username,
                effectBackgroundAvatarShaderSlot:   shaderSlot,
                effectBackgroundAvatarShaderEffect: shaderEffect
            );
        }

        var nameplateBadgeColor = ServiceJoystickWebSocketPayloadChatHandler.GetUserCustomBadgeColor(
            username: username
        );
        if (nameplateBadgeColor is not null)
        {
            this.UpdateAvatarNameplateBadgeColor(
                username:                          username,
                serviceColorInterpolatorColorMode: nameplateBadgeColor.Value
            );
        }
        
        var nameplateNameColor = ServiceJoystickWebSocketPayloadChatHandler.GetUserCustomNameColor(
            username: username
        );
        if (nameplateNameColor is not null)
        {
            this.UpdateAvatarNameplateNameColor(
                username:                          username,
                serviceColorInterpolatorColorMode: nameplateNameColor.Value
            );
        }
        
        effectBackgroundAvatar.ShowNameplateForFirstTime();
        
        BackgroundAvatarsController.SendDelayedBotMessage(
            message: $"👾 {username} joined the battleground."
        );
    }

    internal void HidePreviewAvatar()
    {
        this.CallDeferred(
            method: MethodName.HidePreviewAvatarDeferred
        );
    }
    
    internal void PreviewAvatarColor(
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        this.m_backgroundAvatarPreview.SetShaderModel(
            effectBackgroundAvatarShaderModel: EffectBackgroundAvatarShaderModel.Human
        );
        this.m_backgroundAvatarPreview.SetShaderColorBase(
            serviceColorInterpolatorColorMode: serviceColorInterpolatorColorMode
        );
        this.m_backgroundAvatarPreview.SetShaderEffectSlotColor(
            effectBackgroundAvatarShaderSlot:  EffectBackgroundAvatarShaderSlot.ShaderSlot0,
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.White
        );
        this.m_backgroundAvatarPreview.SetShaderEffectSlotEffect(
            effectBackgroundAvatarShaderSlot:   EffectBackgroundAvatarShaderSlot.ShaderSlot0,
            effectBackgroundAvatarShaderEffect: EffectBackgroundAvatarShaderEffect.Base
        );
    }
    
    internal void PreviewAvatarEffect(
        EffectBackgroundAvatarShaderEffect effectBackgroundAvatarShaderEffect
    )
    {
        this.m_backgroundAvatarPreview.SetShaderModel(
            effectBackgroundAvatarShaderModel: EffectBackgroundAvatarShaderModel.Human
        );
        this.m_backgroundAvatarPreview.SetShaderColorBase(
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.White
        );
        this.m_backgroundAvatarPreview.SetShaderEffectSlotColor(
            effectBackgroundAvatarShaderSlot:  EffectBackgroundAvatarShaderSlot.ShaderSlot0,
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Rainbow
        );
        this.m_backgroundAvatarPreview.SetShaderEffectSlotEffect(
            effectBackgroundAvatarShaderSlot:   EffectBackgroundAvatarShaderSlot.ShaderSlot0,
            effectBackgroundAvatarShaderEffect: effectBackgroundAvatarShaderEffect
        );
    }

    internal void PreviewAvatarModel(
        EffectBackgroundAvatarShaderModel effectBackgroundAvatarShaderModel
    )
    {
        this.m_backgroundAvatarPreview.SetShaderColorBase(
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.White
        );
        this.m_backgroundAvatarPreview.SetShaderEffectSlotColor(
            effectBackgroundAvatarShaderSlot:  EffectBackgroundAvatarShaderSlot.ShaderSlot0,
            serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.White
        );
        this.m_backgroundAvatarPreview.SetShaderEffectSlotEffect(
            effectBackgroundAvatarShaderSlot:   EffectBackgroundAvatarShaderSlot.ShaderSlot0,
            effectBackgroundAvatarShaderEffect: EffectBackgroundAvatarShaderEffect.Base
        );
        this.m_backgroundAvatarPreview.SetShaderModel(
            effectBackgroundAvatarShaderModel: effectBackgroundAvatarShaderModel
        );
    }
    
    internal void RemoveAvatar(
        string username
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_pendingQueueSubscribers.RemoveAll(
                    match: p => p.Username == username
                ) > 0
            )
            {
                return;
            }
            
            if (
                this.m_pendingQueueViewers.RemoveAll(
                    match: p => p.Username == username
                ) > 0
            )
            {
                return;
            }
            
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username, 
                    value: out var avatar
                ) is true
            )
            {
                this.m_removalSchedules[key: avatar] = DateTime.Now.AddSeconds(
                    value: BackgroundAvatarsController.c_removalDelayInSeconds
                );
            }
        }
    }
    
    internal void ShowAvatarNameplate(
        string username
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username, 
                    value: out var avatar
                ) is true
            )
            {
                avatar.ShowNameplate();
            }
        }
    }
    
    internal void ShowPreviewAvatar()
    {
        this.CallDeferred(
            method: MethodName.ShowPreviewAvatarDeferred
        );
    }
    
    internal void UpdateAvatarBase(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetShaderColorBase(
                    serviceColorInterpolatorColorMode: serviceColorInterpolatorColorMode
                );
            }
        }
    }
    
    internal void UpdateAvatarEffect(
        string                             username,
        EffectBackgroundAvatarShaderSlot   effectBackgroundAvatarShaderSlot,
        EffectBackgroundAvatarShaderEffect effectBackgroundAvatarShaderEffect
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetShaderEffectSlotEffect(
                    effectBackgroundAvatarShaderSlot:   effectBackgroundAvatarShaderSlot,
                    effectBackgroundAvatarShaderEffect: effectBackgroundAvatarShaderEffect
                );
            }
        }
    }
    
    internal void UpdateAvatarEffectColor(
        string                            username,
        EffectBackgroundAvatarShaderSlot  effectBackgroundAvatarShaderSlot,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetShaderEffectSlotColor(
                    effectBackgroundAvatarShaderSlot:  effectBackgroundAvatarShaderSlot,
                    serviceColorInterpolatorColorMode: serviceColorInterpolatorColorMode
                );
            }
        }
    }
    
    internal void UpdateAvatarModel(
        string                            username,
        EffectBackgroundAvatarShaderModel effectBackgroundAvatarShaderModel
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetShaderModel(
                    effectBackgroundAvatarShaderModel: effectBackgroundAvatarShaderModel
                );
            }
        }
    }

    internal void UpdateAvatarNameplateBadgeColor(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetNameplateBadgeColor(
                    color: serviceColorInterpolatorColorMode
                );
            }
        }
    }
    
    internal void UpdateAvatarNameplateNameColor(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetNameplateUsernameColor(
                    color: serviceColorInterpolatorColorMode
                );
            }
        }
    }
    
    internal void UpdateAvatarNameplateTitle(
        string username,
        string title
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetNameplateTitle(
                    title: title
                );
            }
        }
    }
    
    internal void UpdateAvatarOutline(
        string                            username,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            if (
                this.m_backgroundAvatars.TryGetValue(
                    key:   username,
                    value: out var avatar
                ) is true
            )
            {
                avatar.SetShaderColorOutline(
                    serviceColorInterpolatorColorMode: serviceColorInterpolatorColorMode
                );
            }
        }
    }
    
    private record PendingAvatar(
        string                                                                           Username,
        ServiceColorInterpolatorColorMode                                                ColorModeBase,
        ServiceColorInterpolatorColorMode                                                ColorModeOutline,
        Dictionary<EffectBackgroundAvatarShaderSlot, ServiceColorInterpolatorColorMode>  ShaderEffectColorModes,
        Dictionary<EffectBackgroundAvatarShaderSlot, EffectBackgroundAvatarShaderEffect> ShaderEffects,
        EffectBackgroundAvatarShaderModel                                                ShaderModel
    );
    
    private const float                                           c_arrivalThreshold           = 2f;
    private const int                                             c_boundaryConstraintYMaxXMax = 2878;
    private const int                                             c_boundaryConstraintYMaxXMin = -256;
    private const int                                             c_boundaryConstraintYMinXMax = 2008;
    private const int                                             c_boundaryConstraintYMinXMin = 1240;
    private const int                                             c_boundaryConstraintYMax     = 1425;
    private const int                                             c_boundaryConstraintYMin     = 1250;
    private const int                                             c_boundaryPreviewLeft        = 800;
    private const int                                             c_boundaryPreviewRight       = 1800;
    private const float                                           c_longDistanceChance         = 0.2f;
    private const int                                             c_maxAvatars                 = 51;
    private const float                                           c_maxWanderDistance          = 400f;
    private const float                                           c_offScreenOffset            = 500f;
    private const float                                           c_removalDelayInSeconds      = 12f;
    private const float                                           c_waitTimeMax                = 30f;
    private const float                                           c_waitTimeMin                = 10f;
    private const float                                           c_walkSpeed                  = 60f;
    
    private static readonly EffectBackgroundAvatarShaderSlot[]    s_shaderSlots                = Enum.GetValues<EffectBackgroundAvatarShaderSlot>();
    
    [Export] private Control                                      BackgroundAvatarPreview      = null;
    [Export] private Control                                      BackgroundAvatars            = null;
    [Export] private PackedScene                                  BackgroundAvatarScene        = null;

    private EffectBackgroundAvatar                                m_backgroundAvatarPreview    = null;
    private readonly Dictionary<string, EffectBackgroundAvatar>   m_backgroundAvatars          = [];
    private readonly object                                       m_lock                       = new();
    private readonly List<PendingAvatar>                          m_pendingQueueSubscribers    = [];
    private readonly List<PendingAvatar>                          m_pendingQueueViewers        = [];
    private readonly Dictionary<EffectBackgroundAvatar, DateTime> m_removalSchedules           = [];
    private readonly Dictionary<EffectBackgroundAvatar, string>   m_removingAvatars            = [];
    private ServiceJoystick                                       m_serviceJoystick            = null;
    private readonly List<EffectBackgroundAvatar>                 m_sortList                   = [];
    private readonly Dictionary<EffectBackgroundAvatar, Vector2>  m_targetPositions            = [];
    private readonly Dictionary<EffectBackgroundAvatar, float>    m_waitTimers                 = [];
    
    private static (float minX, float maxX) GetXRangeAtY(
        float y
    )
    {
        const int constraintYRatio = BackgroundAvatarsController.c_boundaryConstraintYMax - BackgroundAvatarsController.c_boundaryConstraintYMin;
        
        var currentYRatio = y - BackgroundAvatarsController.c_boundaryConstraintYMin;
        var ratio         = Mathf.Clamp(
            value: currentYRatio / constraintYRatio,
            min:   0f, 
            max:   1f
        );
    
        var minX = Mathf.Lerp(
            from:   BackgroundAvatarsController.c_boundaryConstraintYMinXMin, 
            to:     BackgroundAvatarsController.c_boundaryConstraintYMaxXMin, 
            weight: ratio
        );
        var maxX = Mathf.Lerp(
            from:   BackgroundAvatarsController.c_boundaryConstraintYMinXMax, 
            to:     BackgroundAvatarsController.c_boundaryConstraintYMaxXMax, 
            weight: ratio
        );
    
        return (minX, maxX);
    }
    
    private static Vector2 GetPositionGlobal()
    {
        var positionY = (float) GD.RandRange(
            from: BackgroundAvatarsController.c_boundaryConstraintYMin,
            to:   BackgroundAvatarsController.c_boundaryConstraintYMax
        );
        var rangeX    = BackgroundAvatarsController.GetXRangeAtY(
            y: positionY
        );
        
        return new Vector2(
            x: (float) GD.RandRange(
                from: rangeX.minX,
                to:   rangeX.maxX
            ),
            y: positionY
        );
    }
    
    private static Vector2 GetPositionLocal(
        Vector2 currentPosition
    )
    {
        var angle    = (float) GD.RandRange(
            from: 0, 
            to:   Mathf.Tau
        );
        var distance = (float) GD.RandRange(
            from: BackgroundAvatarsController.c_maxWanderDistance * 0.3f, 
            to:   BackgroundAvatarsController.c_maxWanderDistance
        );
    
        var offset = new Vector2(
            x: Mathf.Cos(
                s: angle
            ), 
            y: Mathf.Sin(
                s: angle
            )
        ) * distance;
        var newPos = currentPosition + offset;
        
        var positionY = Mathf.Clamp(
            value: newPos.Y,
            min:   BackgroundAvatarsController.c_boundaryConstraintYMin, 
            max:   BackgroundAvatarsController.c_boundaryConstraintYMax
        );
        var rangeX    = BackgroundAvatarsController.GetXRangeAtY(
            y: positionY
        );
        var positionX = Mathf.Clamp(
            value: newPos.X, 
            min:   rangeX.minX,
            max:   rangeX.maxX
        );

        return new Vector2(
            x: positionX,
            y: positionY
        );
    }
    
    private static Vector2 GetPositionSideline(
        Vector2 position
    )
    {
        if (
            position.X  is > BackgroundAvatarsController.c_boundaryPreviewLeft
                       and < BackgroundAvatarsController.c_boundaryPreviewRight
        )
        {
            return position.X < (BackgroundAvatarsController.c_boundaryPreviewLeft + BackgroundAvatarsController.c_boundaryPreviewRight) / 2f 
                ? new Vector2(
                    x: BackgroundAvatarsController.c_boundaryPreviewLeft,
                    y: position.Y
                ) 
                : new Vector2(
                    x: BackgroundAvatarsController.c_boundaryPreviewRight,
                    y: position.Y
                );
        }
        
        return position;
    }
    
    private static void OnUserTitleRetrieved(
        ServiceDatabaseTaskRetrievedUserTitle serviceDatabaseTaskRetrievedUserTitle
    )
    {
        var result = serviceDatabaseTaskRetrievedUserTitle.NullableResult;
        if (result is not null)
        {
            var username                = result.UserTitles_Username;
            var serviceAchievementTitle = (ServiceAchievementTitle) result.UserTitles_Title_Id;
            var titleName               = ServiceAchievement.GetTitleNameFromAchievementTitle(
                serviceAchievementTitle: serviceAchievementTitle
            );
            BackgroundAvatarsController.Instance.UpdateAvatarNameplateTitle(
                username: username,
                title:    titleName
            );
        }
    }
    
    private static void RequestUserTitle(
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
                await ServiceDatabaseTaskQueries.ExecuteAsyncQuery(
                    serviceDatabaseTaskQueryType:        ServiceDatabaseTaskQueryType.RetrieveUserTitle, 
                    serviceDatabaseTaskNpgsqlParameters: serviceDatabaseTaskNpgsqlParameters
                );
            }
        );
    }
    
    private static void SendDelayedBotMessage(
        string message
    )
    {
        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: 200
                );
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                serviceJoystickBot.SendChatMessageSilently(
                    message: message
                );
            }
        );
    }
    
    private static void SubscribeToEvents()
    {
        ServiceDatabaseTaskEvents.RetrievedUserTitle += BackgroundAvatarsController.OnUserTitleRetrieved;
    }

    private void AddDummies()
    {
        const int c_adjustedAvatarsWithMe = BackgroundAvatarsController.c_maxAvatars - 1;
        for (var i = 0; i < c_adjustedAvatarsWithMe; i++)
        {
            var randomBase    = (ServiceColorInterpolatorColorMode) GD.RandRange(
                from: 0, 
                to:   35
            );
            var randomOutline = (ServiceColorInterpolatorColorMode) GD.RandRange(
                from: 0, 
                to:   35
            );
            var randomModel   = (EffectBackgroundAvatarShaderModel) GD.RandRange(
                from: 0, 
                to:   50
            );
            var randomEffectColorModes = new Dictionary<EffectBackgroundAvatarShaderSlot, ServiceColorInterpolatorColorMode>();
            var randomEffects          = new Dictionary<EffectBackgroundAvatarShaderSlot, EffectBackgroundAvatarShaderEffect>();
            foreach (var slot in BackgroundAvatarsController.s_shaderSlots)
            {
                randomEffectColorModes[key: slot] = (ServiceColorInterpolatorColorMode)GD.RandRange(
                    from: 0, 
                    to:   35
                );
                randomEffects[key: slot]          = (EffectBackgroundAvatarShaderEffect)GD.RandRange(
                    from: 0, 
                    to:   49
                );
            }
    
            this.AddAvatar(
                username:               $"user_{i}",
                colorModeBase:          randomBase,
                colorModeOutline:       randomOutline,
                shaderEffectColorModes: randomEffectColorModes,
                shaderEffects:          randomEffects,
                shaderModel:            randomModel
            );
        }
    }
    
    private Vector2 GetDirection(
        Vector2 current, 
        Vector2 target
    )
    {
        if (this.BackgroundAvatarPreview.Visible is false)
        {
            return (target - current).Normalized();
        }
    
        var crossingCenter     = (current.X < BackgroundAvatarsController.c_boundaryPreviewLeft  && target.X > BackgroundAvatarsController.c_boundaryPreviewLeft) || 
                                 (current.X > BackgroundAvatarsController.c_boundaryPreviewRight && target.X < BackgroundAvatarsController.c_boundaryPreviewRight);
        var isInsideForbiddenX = current.X is > BackgroundAvatarsController.c_boundaryPreviewLeft and < BackgroundAvatarsController.c_boundaryPreviewRight;
        if (crossingCenter is false && isInsideForbiddenX is false)
        {
            return (target - current).Normalized();
        }
    
        var isAtVerticalEdge = current.Y <= BackgroundAvatarsController.c_boundaryConstraintYMin || 
                               current.Y >= BackgroundAvatarsController.c_boundaryConstraintYMax;

        if (isAtVerticalEdge is true)
        {
            return (target - current).Normalized();
        }
    
        var detourY = (current.Y < (BackgroundAvatarsController.c_boundaryConstraintYMin + BackgroundAvatarsController.c_boundaryConstraintYMax) / 2f) 
            ? BackgroundAvatarsController.c_boundaryConstraintYMin 
            : BackgroundAvatarsController.c_boundaryConstraintYMax;
            
        return (
            new Vector2(
                x: current.X, 
                y: detourY
            ) - current
        ).Normalized();
    }

    private void HidePreviewAvatarDeferred()
    {
        this.BackgroundAvatarPreview.Visible = false;
        
        lock (this.m_lock)
        {
            foreach (var avatar in this.m_sortList)
            {
                this.m_targetPositions[key: avatar] = BackgroundAvatarsController.GetPositionGlobal();
                this.m_waitTimers.Remove(
                    key: avatar
                );
            }
        }
    }

    private void RetrieveResources()
    {
        this.m_serviceJoystick = Services.Services.GetService<ServiceJoystick>();
    }

    private void SetInstance()
    {
        BackgroundAvatarsController.Instance = this;
    }

    private void SetPreviewAvatar()
    {
        this.m_backgroundAvatarPreview = this.BackgroundAvatarPreview as EffectBackgroundAvatar;
    }
    
    private void ShowPreviewAvatarDeferred()
    {
        this.BackgroundAvatarPreview.Visible = true;
        
        lock (this.m_lock)
        {
            foreach (var avatar in this.m_sortList)
            {
                if (
                    this.m_targetPositions.TryGetValue(
                        key:   avatar, 
                        value: out var currentTarget
                    ) is true
                )
                {
                    this.m_targetPositions[avatar] = BackgroundAvatarsController.GetPositionSideline(
                        position: currentTarget
                    );
                }
            }
        }
    }
    
    private void SortAvatars()
    {
        this.m_sortList.Sort(
            comparison: (a, b) => 
            {
                var yCompare = a.Position.Y.CompareTo(
                    value: b.Position.Y
                );
                return yCompare != 0 ? yCompare : a.Position.X.CompareTo(
                    value: b.Position.X
                );
            }
        );

        for (var i = 0; i < this.m_sortList.Count; i++)
        {
            var index = this.m_sortList[index: i].GetIndex();
            if (index != i)
            {
                this.BackgroundAvatars.MoveChild(
                    childNode: this.m_sortList[index: i], 
                    toIndex:   i
                );
            }
        }
    }
    
    private void UpdateWander(
        float delta
    )
    {
        var intensity           = SpectrumMusicAnalyzer.Intensity;
        var intensityNormalized = 1f + intensity * 2f;
        var timerDrain          = delta * intensityNormalized;
        var speed               = BackgroundAvatarsController.c_walkSpeed * timerDrain;

        for (var i = this.m_sortList.Count - 1; i >= 0; i--)
        {
            var avatar = this.m_sortList[index: i];
            
            if (
                this.m_removalSchedules.TryGetValue(
                    key:   avatar, 
                    value: out var departureTime
                ) is true
            )
            {
                if (DateTime.Now >= departureTime)
                {
                    this.m_removalSchedules.Remove(
                        key: avatar
                    );
                    
                    var username = string.Empty;
                    foreach (
                        var entry in this.m_backgroundAvatars.Where(
                            predicate: entry => entry.Value == avatar
                        )
                    )
                    {
                        username = entry.Key;
                        this.m_backgroundAvatars.Remove(
                            entry.Key
                        );
                        break;
                    }

                    this.m_removingAvatars.Add(
                        key:   avatar, 
                        value: username
                    );
                    this.m_waitTimers.Remove(
                        key: avatar
                    );

                    const float c_targetY = BackgroundAvatarsController.c_boundaryConstraintYMax + BackgroundAvatarsController.c_offScreenOffset;
                    
                    var rangeX  = BackgroundAvatarsController.GetXRangeAtY(
                        y: BackgroundAvatarsController.c_boundaryConstraintYMax
                    );
                    var targetX = (float) GD.RandRange(
                        from: rangeX.minX, 
                        to:   rangeX.maxX
                    );
                    
                    this.m_targetPositions[key: avatar] = new Vector2(
                        x: targetX,
                        y: c_targetY
                    );
                }
            }
            
            if (
                this.m_waitTimers.TryGetValue(
                    key:   avatar, 
                    value: out var timeLeft
                ) is true
            )
            {
                if (timeLeft > 0f)
                {
                    this.m_waitTimers[key: avatar] = timeLeft - timerDrain;
                    continue;
                }
                
                this.m_waitTimers.Remove(
                    key: avatar
                );
                
                var roll = GD.Randf();
                if (roll < BackgroundAvatarsController.c_longDistanceChance)
                {
                    this.m_targetPositions[key: avatar] = BackgroundAvatarsController.GetPositionGlobal();
                }
                else
                {
                    this.m_targetPositions[key: avatar] = BackgroundAvatarsController.GetPositionLocal(
                        currentPosition: avatar.Position
                    );
                }

                if (this.m_backgroundAvatarPreview.Visible is true)
                {
                    this.m_targetPositions[key: avatar] = BackgroundAvatarsController.GetPositionSideline(
                        position: this.m_targetPositions[key: avatar]
                    );
                }
            }
            
            if (
                this.m_targetPositions.TryGetValue(
                    key:   avatar,
                    value: out var target
                ) is false
            )
            {
                continue;
            }

            var distance = avatar.Position.DistanceTo(
                to: target
            );
            if (distance < BackgroundAvatarsController.c_arrivalThreshold)
            {
                if (
                    this.m_removingAvatars.ContainsKey(
                        key: avatar
                    ) is true
                )
                {
                    var username = this.m_removingAvatars[key: avatar];
                    
                    this.m_removingAvatars.Remove(
                        key: avatar
                    );
                    this.m_targetPositions.Remove(
                        key: avatar
                    );
                    this.m_sortList.RemoveAt(
                        index: i
                    );
                    avatar.QueueFree();
                    
                    BackgroundAvatarsController.SendDelayedBotMessage(
                        message: $"👾 {username} left the battleground."
                    );

                    if (this.m_pendingQueueSubscribers.Count > 0)
                    {
                        var queuedSubscriber = this.m_pendingQueueSubscribers[0];
                        this.m_pendingQueueSubscribers.RemoveAt(
                            index: 0
                        );
                        
                        this.AddAvatar(
                            username:               queuedSubscriber.Username,
                            colorModeBase:          queuedSubscriber.ColorModeBase,
                            colorModeOutline:       queuedSubscriber.ColorModeOutline,
                            shaderEffectColorModes: queuedSubscriber.ShaderEffectColorModes,
                            shaderEffects:          queuedSubscriber.ShaderEffects,
                            shaderModel:            queuedSubscriber.ShaderModel
                        );
                    }
                    else if (this.m_pendingQueueViewers.Count > 0)
                    {
                        var queuedViewer = this.m_pendingQueueViewers[0];
                        this.m_pendingQueueViewers.RemoveAt(
                            index: 0
                        );
                        
                        this.AddAvatar(
                            username:               queuedViewer.Username,
                            colorModeBase:          queuedViewer.ColorModeBase,
                            colorModeOutline:       queuedViewer.ColorModeOutline,
                            shaderEffectColorModes: queuedViewer.ShaderEffectColorModes,
                            shaderEffects:          queuedViewer.ShaderEffects,
                            shaderModel:            queuedViewer.ShaderModel
                        );
                    }
                    continue;
                }
                
                this.m_targetPositions.Remove(
                    key: avatar
                );
                this.m_waitTimers[key: avatar] = (float) GD.RandRange(
                    from: BackgroundAvatarsController.c_waitTimeMin, 
                    to:   BackgroundAvatarsController.c_waitTimeMax
                );
            }
            else
            {
                var direction   =  this.GetDirection(
                    current: avatar.Position,
                    target:  target
                );
                avatar.Position += direction * speed;
                
                if (intensity > 0.5f)
                {
                    avatar.Position += new Vector2(
                        (float) GD.RandRange(
                            from: -1f, 
                            to:   1f
                        ),
                        (float) GD.RandRange(
                            from: -1f, 
                            to:   1f
                        )
                    ) * intensity;
                }
            }
        }
    }
}