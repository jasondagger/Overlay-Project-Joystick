
using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Inputs;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.OBS;

namespace Overlay.Core.Contents;

public sealed partial class SceneController() :
    Node()
{
    [Export] public Array<Control> Layouts = [];
    [Export] public Control Hideable       = null;
    [Export] public Control Rainbows       = null;
    
    public override void _Ready()
    {
        this.RegisterForInputEvents();
    }

    private enum LayoutType :
        uint
    {
        Default = 0U,
        Code,
    }

    private CancellationTokenSource m_cancellationTokenSource = new();

    private void RegisterForInputEvents()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotInput = _ = serviceGodots.GetServiceGodot<ServiceGodotInput>();

        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.ChangeLayoutToDefault] += this.HandleInputActionPressedChangeLayoutToDefault;
        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.ChangeLayoutToCode]    += this.HandleInputActionPressedChangeLayoutToCode;
        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.ChangeLayoutToAfk]     += this.HandleInputActionPressedChangeLayoutToAfk;
        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.CloseApplication]      += this.HandleInputActionPressedCloseApplication;
    }
    
    private void HandleInputActionPressedChangeLayoutToAfk(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        _ = this.Layouts[_ = (int)LayoutType.Default].Visible = _ = true;
        Hideable.Visible = _ = false;
        Rainbows.Visible = _ = false;

        _ = Task.Run(
            function: 
            async () =>
            {
                _ = this.m_cancellationTokenSource = _ = new CancellationTokenSource();
                var cancellationToken = _ = m_cancellationTokenSource.Token;
                
                var serviceJoystickBot = _ = Services.Services.GetService<ServiceJoystickBot>();
                
                await Task.Delay(
                    delay: _ = TimeSpan.FromSeconds(
                        value: _ = 3d
                    ),
                    cancellationToken: _ = cancellationToken
                );
                
                while (_ = cancellationToken.IsCancellationRequested is false)
                {
                    serviceJoystickBot.SendChatMessage(
                        message: _ = $"SmoothDagger is currently AFK & will return shortly!"
                    );
                    
                    await Task.Delay(
                        delay: _ = TimeSpan.FromMinutes(
                            value: _ = 5d
                        ),
                        cancellationToken: _ = cancellationToken
                    );
                }
            }
        );

        var serviceOBS = _ = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: _ = $"AFK"    
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToCode(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        _ = this.Layouts[_ = (int)LayoutType.Code].Visible = _ = true;
        Rainbows.Visible = _ = true;
        var serviceOBS = _ = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: _ = $"Code"    
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToDefault(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        _ = this.Layouts[_ = (int)LayoutType.Default].Visible = _ = true;
        Hideable.Visible = _ = true;
        Rainbows.Visible = _ = true;

        var serviceOBS = _ = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: _ = $"Main"    
        );
    }
    
    private void HandleInputActionPressedCloseApplication(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        var sceneTree = _ = this.GetTree();
        sceneTree.Quit();
    }

    private void HideLayouts()
    {
        foreach (var layout in this.Layouts)
        {
            _ = layout.Visible = _ = false;
        }
        this.m_cancellationTokenSource.Cancel();
    }
}