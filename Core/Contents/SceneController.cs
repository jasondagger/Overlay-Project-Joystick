
using Godot;
using Godot.Collections;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Inputs;
using Overlay.Core.Services.OBS;

namespace Overlay.Core.Contents;

public sealed partial class SceneController() :
    Node()
{
    [Export] public Array<Control> Layouts = [];
    
    public override void _Ready()
    {
        this.RegisterForInputEvents();
    }

    private enum LayoutType :
        uint
    {
        Default = 0U,
        Large,
    }

    private void RegisterForInputEvents()
    {
        var serviceGodots     = _ = Services.Services.GetService<ServiceGodots>();
        var serviceGodotInput = _ = serviceGodots.GetServiceGodot<ServiceGodotInput>();

        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.ChangeLayoutToDefault] += this.HandleInputActionPressedChangeLayoutToDefault;
        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.ChangeLayoutToLarge]   += this.HandleInputActionPressedChangeLayoutToLarge;
        _ = serviceGodotInput.InputActionPressed[key: _ = ServiceGodotInputActionType.CloseApplication]      += this.HandleInputActionPressedCloseApplication;
    }
    
    private void HandleInputActionPressedChangeLayoutToDefault(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        _ = this.Layouts[_ = (int)LayoutType.Default].Visible = _ = true;
        
        // TODO: Can I connect to obs?
        //var serviceOBS = _ = Services.Services.GetService<ServiceOBS>();
        //serviceOBS.ChangeScene(
        //    sceneName: _ = $"Default"    
        //);
    }
    
    private void HandleInputActionPressedChangeLayoutToLarge(
        ServiceGodotInputStateType serviceGodotInputStateType
    )
    {
        this.HideLayouts();
        _ = this.Layouts[_ = (int)LayoutType.Large].Visible = _ = true;

        // TODO: Can I connect to obs?
        //var serviceOBS = _ = Services.Services.GetService<ServiceOBS>();
        //serviceOBS.ChangeScene(
        //    sceneName: _ = $"Large"    
        //);
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
    }
}