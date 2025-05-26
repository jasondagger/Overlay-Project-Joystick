
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

    public static SceneController Instance = null;
    
    public override void _Ready()
    {
        this.RegisterForInputEvents();
        this.SetInstance();
    }

    internal void SetLayoutToAfk()
    {
        this.CallDeferred(
            method: $"{nameof(this.HandleInputActionPressedChangeLayoutToAfk)}", 
            (uint)ServiceGodotInputStateType.Pressed
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
            args:    (uint)ServiceGodotInputStateType.Pressed
        );
    }

    private enum LayoutType :
        uint
    {
        Main = 0U,
        Code,
    }

    private CancellationTokenSource m_cancellationTokenSource = new();
    
    private void HandleInputActionPressedChangeLayoutToAfk(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        this.Layouts[(int)LayoutType.Main].Visible = true;
        Hideable.Visible = false;
        Rainbows.Visible = false;

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
                    serviceJoystickBot.SendChatMessage(
                        message: $"SmoothDagger is currently AFK & will return shortly!"
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
            sceneName: $"AFK"    
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToCode(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        this.Layouts[(int)LayoutType.Code].Visible = true;
        Rainbows.Visible = true;
        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: $"Code"    
        );
    }
    
    private void HandleInputActionPressedChangeLayoutToMain(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        this.Layouts[(int)LayoutType.Main].Visible = true;
        Hideable.Visible = true;
        Rainbows.Visible = true;

        var serviceOBS = Services.Services.GetService<ServiceOBS>();
        serviceOBS.ChangeScene(
            sceneName: $"Main"    
        );
    }
    
    private void HandleInputActionPressedCloseApplication(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        var sceneTree = this.GetTree();
        sceneTree.Quit();
    }

    private void HideLayouts()
    {
        foreach (var layout in this.Layouts)
        {
            layout.Visible = false;
        }
        this.m_cancellationTokenSource.Cancel();
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