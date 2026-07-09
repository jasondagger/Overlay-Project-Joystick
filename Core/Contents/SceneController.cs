
using Godot;
using Godot.Collections;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Inputs;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.OBS;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Contents;

public sealed partial class SceneController() :
    Node()
{
    [Export] public Array<Control> Layouts = [];
    [Export] public Control Hideable       = null;
    [Export] public Control Interaction    = null;

    public static SceneController Instance { get; private set; } = null;

    public override void _Process(
        double delta
    )
    {
        this.ProcessLayoutMeForDuration(
            delta: (float) delta
        );
    }
    
    public override void _Ready()
    {
        this.RegisterForInputEvents();
        this.SetInstance();
    }

    internal enum AttentionMode :
        uint
    {
        Normal = 0U,
        Me,
    }
    
    internal void AddToLayoutMeRemainingNormalDurationInSeconds(
        float seconds
    )
    {
        lock (this.m_lockLayoutMeNormalTime)
        {
            this.m_layoutMeNormalTimeRemainingInSeconds += seconds;
        }
    }

    internal AttentionMode GetAttentionMode()
    {
        lock (this.m_lockLayoutMeTime)
        {
            return this.m_attentionMode;
        }
    }
    
    internal int GetLayoutMeRemainingDurationInMilliseconds()
    {
        lock (this.m_lockLayoutMeTime)
        {
            return (int)(this.m_layoutMeTimeRemainingInSeconds * 1000);
        }
    }
    
    internal int GetLayoutMeRemainingNormalDurationInMilliseconds()
    {
        lock (this.m_lockLayoutMeNormalTime)
        {
            return (int)(this.m_layoutMeNormalTimeRemainingInSeconds * 1000);
        }
    }
    
    internal bool IsInLayoutMeDurationMode()
    {
        lock (this.m_lockLayoutMeTime)
        {
            return this.m_layoutMeTimeRemainingInSeconds > 0f;
        }
    }

    internal void SetLayoutToAfk()
    {
        this.CallDeferred(
            method: $"{nameof(this.HandleInputActionPressedChangeLayoutToAfk)}", 
            args:   (uint)ServiceGodotInputStateType.Pressed
        );
    }

    internal void SetLayoutToAvatars()
    {
        this.CallDeferred(
            method: $"{nameof(this.HandleInputActionPressedChangeLayoutToAvatars)}", 
            args:   (uint)ServiceGodotInputStateType.Pressed
        );
    }

    internal void SetLayoutToCode()
    {
        this.CallDeferred(
            method: $"{nameof(this.HandleInputActionPressedChangeLayoutToCode)}",
            args:   (uint)ServiceGodotInputStateType.Pressed
        );
    }

    internal void SetLayoutToMain()
    {
        this.CallDeferred(
            method: $"{nameof(this.HandleInputActionPressedChangeLayoutToMain)}",
            args:   (uint)ServiceGodotInputStateType.Pressed
        );
    }

    internal void SetLayoutToMe()
    {
        this.CallDeferred(
            method: $"{nameof(this.HandleInputActionPressedChangeLayoutToMe)}",
            args:   (uint)ServiceGodotInputStateType.Pressed
        );
    }

    internal void SetAttentionModeAndLayoutToMeForDuration(
        AttentionMode attentionMode,
        float         durationInSeconds
    )
    {
        lock (this.m_lockLayoutMeTime)
        {
            if (attentionMode == AttentionMode.Normal)
            {
                this.m_layoutMeNormalTimeRemainingInSeconds = durationInSeconds;
            }
            
            this.m_attentionMode                  = attentionMode;
            this.m_layoutMeTimeRemainingInSeconds = durationInSeconds;
        }
        
        this.CallDeferred(
            method: $"{nameof(this.HandleLayoutToMeForDuration)}"
        );
    }

    private enum LayoutType :
        uint
    {
        Main = 0U,
        Code,
        Me,
        Avatars,
        Afk,
    }
    
    private const int               c_positionYInteractionMain             = 1552;
    private const int               c_positionYInteractionAfk              = 128;

    private AttentionMode           m_attentionMode                        = AttentionMode.Normal;
    private CancellationTokenSource m_cancellationTokenSource              = new();
    private LayoutType              m_currentLayoutType                    = LayoutType.Avatars;
    private float                   m_layoutMeTimeRemainingInSeconds       = 0f;
    private float                   m_layoutMeNormalTimeRemainingInSeconds = 0f;
    private readonly object         m_lockLayoutMeTime                     = new();
    private readonly object         m_lockLayoutMeNormalTime               = new();
    
    private void HandleInputActionPressedChangeLayoutToAfk(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        
        this.Layouts[(int)LayoutType.Main].Visible = true;
        this.m_currentLayoutType                   = LayoutType.Afk;
        this.Hideable.Visible                      = false;

        var position              = this.Interaction.Position;
        position.Y                = SceneController.c_positionYInteractionAfk;
        this.Interaction.Position = position;

        Task.Run(
            function: 
            async () =>
            {
                this.m_cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = m_cancellationTokenSource.Token;
                
                var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
                
                await Task.Delay(
                    delay: TimeSpan.FromSeconds(
                        value: 3d
                    ),
                    cancellationToken: cancellationToken
                );
                
                while (cancellationToken.IsCancellationRequested is false)
                {
                    serviceJoystickBot.SendChatMessageSilently(
                        message: $"💤 SmoothDagger is currently AFK & will return shortly!"
                    );
                    
                    await Task.Delay(
                        delay: TimeSpan.FromMinutes(
                            value: 5d
                        ),
                        cancellationToken: cancellationToken
                    );
                }
            }
        );

        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: nameof(LayoutType.Afk)    
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToAvatars(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        
        this.Layouts[(int)LayoutType.Avatars].Visible = true;
        this.m_currentLayoutType                      = LayoutType.Avatars;

        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: nameof(LayoutType.Avatars)    
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToCode(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        this.Layouts[(int)LayoutType.Code].Visible = true;
        this.m_currentLayoutType                   = LayoutType.Code;
        
        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: nameof(LayoutType.Code)
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToMain(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        
        this.Layouts[(int)LayoutType.Main].Visible = true;
        this.m_currentLayoutType                   = LayoutType.Main;
        this.Hideable.Visible                      = true;
        
        var position              = this.Interaction.Position;
        position.Y                = SceneController.c_positionYInteractionMain;
        this.Interaction.Position = position;

        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: nameof(LayoutType.Main)
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToMe(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        
        this.Layouts[(int)LayoutType.Me].Visible = true;
        this.m_currentLayoutType                 = LayoutType.Me;

        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: nameof(LayoutType.Me)
        );
    }
    
    private void HandleInputActionPressedCloseApplication(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        var sceneTree = this.GetTree();
        sceneTree.Quit();
    }

    private void HandleLayoutToMeForDuration()
    {
        if (this.m_currentLayoutType == LayoutType.Me)
        {
            return;
        }
        
        this.HideLayouts();
        
        this.Layouts[(int)LayoutType.Me].Visible = true;

        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: nameof(LayoutType.Me)
        );
    }

    private void HideLayouts()
    {
        foreach (var layout in this.Layouts)
        {
            layout.Visible = false;
        }
        this.m_cancellationTokenSource.Cancel();
    }

    private void ProcessLayoutMeForDuration(
        float delta
    )
    {
        lock (this.m_lockLayoutMeTime)
        {
            lock (this.m_lockLayoutMeNormalTime)
            {
                if (this.m_layoutMeNormalTimeRemainingInSeconds > 0f)
                {
                    this.m_layoutMeNormalTimeRemainingInSeconds -= delta;
                    if (this.m_layoutMeNormalTimeRemainingInSeconds <= 0f)
                    {
                        this.m_layoutMeNormalTimeRemainingInSeconds = 0f;
                    }
                }
                if (this.m_layoutMeTimeRemainingInSeconds > 0f)
                {
                    this.m_layoutMeTimeRemainingInSeconds -= delta;
                    if (this.m_layoutMeTimeRemainingInSeconds <= 0f)
                    {
                        this.m_layoutMeTimeRemainingInSeconds = 0f;
                        this.m_attentionMode                  = AttentionMode.Normal;

                        switch (this.m_currentLayoutType)
                        {
                            case LayoutType.Afk:
                                this.HandleInputActionPressedChangeLayoutToAfk(
                                    serviceGodotInputStateType: ServiceGodotInputStateType.Pressed
                                );
                                break;
                            
                            case LayoutType.Avatars:
                                this.HandleInputActionPressedChangeLayoutToAvatars(
                                    serviceGodotInputStateType: ServiceGodotInputStateType.Pressed
                                );
                                break;
                        
                            case LayoutType.Code:
                                this.HandleInputActionPressedChangeLayoutToCode(
                                    serviceGodotInputStateType: ServiceGodotInputStateType.Pressed
                                );
                                break;
                        
                            case LayoutType.Main:
                                this.HandleInputActionPressedChangeLayoutToMain(
                                    serviceGodotInputStateType: ServiceGodotInputStateType.Pressed
                                );
                                break;
                        
                            case LayoutType.Me:
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
    
    private void RegisterForInputEvents()
    {
        var serviceGodots     = Services.Services.GetService<ServiceGodots>();
        var serviceGodotInput = serviceGodots.GetServiceGodot<ServiceGodotInput>();

        serviceGodotInput.InputActionPressed[key: ServiceGodotInputActionType.ChangeLayoutToDefault] += this.HandleInputActionPressedChangeLayoutToMain;
        serviceGodotInput.InputActionPressed[key: ServiceGodotInputActionType.ChangeLayoutToCode]    += this.HandleInputActionPressedChangeLayoutToCode;
        serviceGodotInput.InputActionPressed[key: ServiceGodotInputActionType.ChangeLayoutToAfk]     += this.HandleInputActionPressedChangeLayoutToAfk;
        serviceGodotInput.InputActionPressed[key: ServiceGodotInputActionType.CloseApplication]      += this.HandleInputActionPressedCloseApplication;
    }

    private void SetInstance()
    {
        SceneController.Instance = this;
    }
}