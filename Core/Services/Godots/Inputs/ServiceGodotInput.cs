
namespace Overlay.Core.Services.Godots.Inputs;

using Godot;
using System;
using System.Collections.Generic;

internal sealed partial class ServiceGodotInput() :
    ServiceGodot()
{
    public override void _Input(
        InputEvent inputEvent
    )
    {
        switch (inputEvent)
        {
            case InputEventMouseButton inputEventMouseButton:
            {
                var mouseButton  = _ = inputEventMouseButton.ButtonIndex;
                var pressedState = _ = inputEventMouseButton.Pressed;

                this.HandleInputEventMouseButton(
                    mouseButton:  _ = mouseButton,
                    pressedState: _ = pressedState
                );
                break;
            }
            case InputEventMouseMotion inputEventMouseMotion:
            {
                var mousePosition  = _ = inputEventMouseMotion.Relative;
                var velocityVector = _ = this.m_lastMousePosition - mousePosition;
                this.MouseMoved?.Invoke(
                    obj: _ = velocityVector
                );
                break;
            }
        }
    }

    public override void _Process(
        double delta
    )
    {
        this.ProcessInputActionKeys();
    }
    
    internal readonly Dictionary<ServiceGodotInputActionType, Action<ServiceGodotInputStateType>> InputActionPressed  = new();
    internal readonly Dictionary<ServiceGodotInputActionType, Action<ServiceGodotInputStateType>> InputActionPressing = new();
    internal readonly Dictionary<ServiceGodotInputActionType, Action<ServiceGodotInputStateType>> InputActionReleased = new();

    internal readonly Action<Vector2> MouseMoved = null;

    internal ServiceGodotInputBind GetInputActionBind(
        ServiceGodotInputActionType inputActionType
    )
    {
        return _ = this.m_inputActionBinds[key: _ = inputActionType];
    }

    internal void SetInputActionBind(
        ServiceGodotInputActionType inputActionType,
        ServiceGodotInputBind       inputBind
    )
    {
        var key         = _ = inputBind.Key;
        var mouseButton = _ = inputBind.MouseButton;

        if (
            this.m_inputActionKeys.TryGetValue(
                key:   _ = key,
                value: out var oldInputActionType
            )
        )
        {
            this.UnbindInputAction(
                inputActionType: _ = oldInputActionType
            );
            _ = this.m_inputActionKeys.Remove(
                key: _ = key
            );
        }
        else if (
            this.m_inputActionMouseButtons.TryGetValue(
                key:   _ =mouseButton,
                value: out oldInputActionType
            )
        )
        {
            this.UnbindInputAction(
                inputActionType: _ = oldInputActionType
            );
            _ = m_inputActionMouseButtons.Remove(
                key: _ = mouseButton
            );
        }

        this.m_inputActionBinds[key: _ = inputActionType]  = _ = inputBind;
        this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Released;
    }
    
    internal void SetStoredInputActionBinds(
        Dictionary<ServiceGodotInputActionType, ServiceGodotInputBind> storedInputActionBinds
    )
    {
        foreach (var inputActionBind in _ = storedInputActionBinds)
        {
            var inputActionType = _ = inputActionBind.Key;
            var inputBind       = _ = inputActionBind.Value;

            var inputBindKey         = _ = inputBind.Key;
            var inputBindMouseButton = _ = inputBind.MouseButton;

            var isInputActionBound = _ = false;
            if (inputBindKey is not Key.None)
            {
                _ = isInputActionBound = _ = this.m_inputActionKeys.TryAdd(
                    key:   _ = inputBindKey,
                    value: _ = inputActionType
                );
                if (isInputActionBound is true)
                {
                    _ = inputBind.MouseButton = _ = MouseButton.None;
                }
            }
            else if (inputBindMouseButton is not MouseButton.None)
            {
                _ = isInputActionBound = _ = this.m_inputActionMouseButtons.TryAdd(
                    key:   _ = inputBindMouseButton,
                    value: _ = inputActionType
                );
            }

            if (isInputActionBound)
            {
                _ = this.m_inputActionBinds[key:  _ = inputActionType] = _ = inputBind;
                _ = this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Released;
            }
            else
            {
                this.UnbindInputAction(
                    inputActionType: _ = inputActionType
                );
            }
        }
    }

	internal override void Start()
	{
        base.Start();

		this.AddInputActionEvents();
		this.SetDefaultInputActionBinds();
	}

	private static readonly Dictionary<ServiceGodotInputActionType, ServiceGodotInputBind> c_defaultInputActionBinds = new()
    {
        {
            ServiceGodotInputActionType.ChangeLayoutToDefault,       
            new ServiceGodotInputBind(
                key:         _ = Key.Kp0,
                mouseButton: _ = MouseButton.None
            )
        },
        {
            ServiceGodotInputActionType.ChangeLayoutToLarge,       
            new ServiceGodotInputBind(
                key:         _ = Key.Kp1,
                mouseButton: _ = MouseButton.None
            )
        },
        {
            ServiceGodotInputActionType.CloseApplication,       
            new ServiceGodotInputBind(
                key:         _ = Key.F2,
                mouseButton: _ = MouseButton.None
            )
        },
    };
    private readonly Dictionary<ServiceGodotInputActionType, ServiceGodotInputBind>        m_inputActionBinds        = new()
    {
        { ServiceGodotInputActionType.ChangeLayoutToDefault, new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
        { ServiceGodotInputActionType.ChangeLayoutToLarge,   new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
        { ServiceGodotInputActionType.CloseApplication,              new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
    };
    private readonly Dictionary<ServiceGodotInputActionType, ServiceGodotInputStateType>   m_inputActionStates       = new()
    {
        { ServiceGodotInputActionType.ChangeLayoutToDefault, ServiceGodotInputStateType.Unbound },
        { ServiceGodotInputActionType.ChangeLayoutToLarge,   ServiceGodotInputStateType.Unbound },
        { ServiceGodotInputActionType.CloseApplication,              ServiceGodotInputStateType.Unbound },
    };
    private readonly Dictionary<Key, ServiceGodotInputActionType>                          m_inputActionKeys         = new();
    private readonly Dictionary<MouseButton, ServiceGodotInputActionType>                  m_inputActionMouseButtons = new();

    private Vector2 m_lastMousePosition = _ = Vector2.Zero;

    private void AddInputActionEvents()
    {
        var inputActionTypes = _ = Enum.GetValues<ServiceGodotInputActionType>();
        foreach (var inputActionType in _ = inputActionTypes)
        {
            this.InputActionPressed.Add(
                key:   _ = inputActionType,
                value: null
            );
            this.InputActionPressing.Add(
                key:   _ = inputActionType,
                value: null
            );
            this.InputActionReleased.Add(
                key:   _ = inputActionType,
                value: null
            );
        }
    }
    
    private ServiceGodotInputStateType GetInputActionState(
        ServiceGodotInputActionType inputActionType
    )
    {
        return _ = this.m_inputActionStates[key: _ = inputActionType];
    }

    private void HandleInputEventMouseButton(
        MouseButton mouseButton,
        bool        pressedState
    )
    {
        if (
            this.m_inputActionMouseButtons.TryGetValue(
                key:   _ = mouseButton,
                value: out var inputActionType
            ) is false
        )
        {
            return;
        }
        
        var inputStateType = _ = m_inputActionStates[key: _ = inputActionType];
        this.HandleInputState(
            inputActionType: _ = inputActionType,
            inputStateType:  _ = inputStateType,
            pressedState:    _ = pressedState
        );
    }

    private void HandleInputState(
        ServiceGodotInputActionType inputActionType,
        ServiceGodotInputStateType  inputStateType,
        bool                        pressedState
    )
    {
        switch (_ = inputStateType)
        {
            case ServiceGodotInputStateType.Pressed:
                this.HandleInputStatePressed(
                    inputActionType: _ = inputActionType,
                    pressedState:    _ = pressedState
                );
                break;

            case ServiceGodotInputStateType.Pressing:
                this.HandleInputStatePressing(
                    inputActionType: _ = inputActionType,
                    pressedState:    _ = pressedState
                );
                break;

            case ServiceGodotInputStateType.Released:
                this.HandleInputStateReleased(
                    inputActionType: _ = inputActionType,
                    pressedState:    _ = pressedState
                );
                break;

            case ServiceGodotInputStateType.Unbound:
            default:
                break;
        }
    }

    private void HandleInputStatePressed(
        ServiceGodotInputActionType inputActionType,
        bool                        pressedState
    )
    {
        if (pressedState)
        {
            this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Pressing;
            this.InputActionPressing[key: _ = inputActionType]?.Invoke(
                obj: ServiceGodotInputStateType.Pressing
            );
        }
        else
        {
            this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Released;
            this.InputActionReleased[key: _ = inputActionType]?.Invoke(
                obj: _ = ServiceGodotInputStateType.Released
            );
        }
    }

    private void HandleInputStatePressing(
        ServiceGodotInputActionType inputActionType,
        bool                        pressedState
    )
    {
        if (pressedState)
        {
            this.InputActionPressing[key: _ = inputActionType]?.Invoke(
                obj: _ = ServiceGodotInputStateType.Pressing
            );
        }
        else
        {
            this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Released;
            this.InputActionReleased[key: _ = inputActionType]?.Invoke(
                obj: _ = ServiceGodotInputStateType.Released
            );
        }
    }

    private void HandleInputStateReleased(
        ServiceGodotInputActionType inputActionType,
        bool                        pressedState
    )
    {
        if (pressedState is false)
        {
            return;
        }
        
        this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Pressed;
        this.InputActionPressed[key: _ = inputActionType]?.Invoke(
            obj: _ = ServiceGodotInputStateType.Pressed
        );
    }

    private void ProcessInputActionKeys()
    {
        foreach (var inputActionKey in _ = this.m_inputActionKeys)
        {
            var key          = _ = inputActionKey.Key;
            var pressedState = _ = Input.IsPhysicalKeyPressed(
                keycode: _ = key
            );

            var inputActionType = _ = inputActionKey.Value;
            var inputStateType  = _ = this.m_inputActionStates[key: _ = inputActionType];

            this.HandleInputState(
                inputActionType: _ = inputActionType,
                inputStateType:  _ = inputStateType,
                pressedState:    _ = pressedState
            );
        }
    }

    private void SetDefaultInputActionBinds()
    {
        foreach (var inputActionBind in _ = ServiceGodotInput.c_defaultInputActionBinds)
        {
            var inputAction = _ = inputActionBind.Key;
            var inputBind   = _ = inputActionBind.Value;

            var inputBindKey         = _ = inputBind.Key;
            var inputBindMouseButton = _ = inputBind.MouseButton;

            if (inputBindKey is not Key.None)
            {
                this.m_inputActionKeys.Add(
                    key:   _ = inputBindKey,
                    value: _ = inputAction
                );
            }
            else if (inputBindMouseButton is not MouseButton.None)
            {
                this.m_inputActionMouseButtons.Add(
                    key:   _ = inputBindMouseButton,
                    value: _ = inputAction
                );
            }

            _ = this.m_inputActionBinds[key: _ = inputAction]  = _ = inputBind;
            _ = this.m_inputActionStates[key: _ = inputAction] = _ = ServiceGodotInputStateType.Released;
        }
    }

    private void UnbindInputAction(
        ServiceGodotInputActionType inputActionType
    )
    {
        _ = this.m_inputActionBinds[key: _ = inputActionType] = new ServiceGodotInputBind(
            key:         _ = Key.None,
            mouseButton: _ = MouseButton.None
        );
        _ = this.m_inputActionStates[key: _ = inputActionType] = _ = ServiceGodotInputStateType.Unbound;
    }
}