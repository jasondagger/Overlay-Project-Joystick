
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
                var mouseButton  = inputEventMouseButton.ButtonIndex;
                var pressedState = inputEventMouseButton.Pressed;

                this.HandleInputEventMouseButton(
                    mouseButton:  mouseButton,
                    pressedState: pressedState
                );
                break;
            }
            case InputEventMouseMotion inputEventMouseMotion:
            {
                var mousePosition  = inputEventMouseMotion.Relative;
                var velocityVector = this.m_lastMousePosition - mousePosition;
                this.MouseMoved?.Invoke(
                    obj: velocityVector
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
        return this.m_inputActionBinds[key: inputActionType];
    }

    internal void SetInputActionBind(
        ServiceGodotInputActionType inputActionType,
        ServiceGodotInputBind       inputBind
    )
    {
        var key         = inputBind.Key;
        var mouseButton = inputBind.MouseButton;

        if (
            this.m_inputActionKeys.TryGetValue(
                key:   key,
                value: out var oldInputActionType
            )
        )
        {
            this.UnbindInputAction(
                inputActionType: oldInputActionType
            );
            this.m_inputActionKeys.Remove(
                key: key
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
                inputActionType: oldInputActionType
            );
            m_inputActionMouseButtons.Remove(
                key: mouseButton
            );
        }

        this.m_inputActionBinds[key: inputActionType]  = inputBind;
        this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Released;
    }
    
    internal void SetStoredInputActionBinds(
        Dictionary<ServiceGodotInputActionType, ServiceGodotInputBind> storedInputActionBinds
    )
    {
        foreach (var inputActionBind in storedInputActionBinds)
        {
            var inputActionType = inputActionBind.Key;
            var inputBind       = inputActionBind.Value;

            var inputBindKey         = inputBind.Key;
            var inputBindMouseButton = inputBind.MouseButton;

            var isInputActionBound = false;
            if (inputBindKey is not Key.None)
            {
                isInputActionBound = this.m_inputActionKeys.TryAdd(
                    key:   inputBindKey,
                    value: inputActionType
                );
                if (isInputActionBound is true)
                {
                    inputBind.MouseButton = MouseButton.None;
                }
            }
            else if (inputBindMouseButton is not MouseButton.None)
            {
                isInputActionBound = this.m_inputActionMouseButtons.TryAdd(
                    key:   inputBindMouseButton,
                    value: inputActionType
                );
            }

            if (isInputActionBound)
            {
                this.m_inputActionBinds[key:  inputActionType] = inputBind;
                this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Released;
            }
            else
            {
                this.UnbindInputAction(
                    inputActionType: inputActionType
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

	private static readonly Dictionary<ServiceGodotInputActionType, ServiceGodotInputBind> s_defaultInputActionBinds = new()
    {
        {
            ServiceGodotInputActionType.ChangeLayoutToAfk,       
            new ServiceGodotInputBind(
                key:         Key.Kp2,
                mouseButton: MouseButton.None
            )
        },
        {
            ServiceGodotInputActionType.ChangeLayoutToCode,       
            new ServiceGodotInputBind(
                key:         Key.Kp1,
                mouseButton: MouseButton.None
            )
        },
        {
            ServiceGodotInputActionType.ChangeLayoutToDefault,       
            new ServiceGodotInputBind(
                key:         Key.Kp0,
                mouseButton: MouseButton.None
            )
        },
        {
            ServiceGodotInputActionType.CloseApplication,       
            new ServiceGodotInputBind(
                key:         Key.F2,
                mouseButton: MouseButton.None
            )
        },
    };
    private readonly Dictionary<ServiceGodotInputActionType, ServiceGodotInputBind>        m_inputActionBinds        = new()
    {
        { ServiceGodotInputActionType.ChangeLayoutToAfk,     new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
        { ServiceGodotInputActionType.ChangeLayoutToCode,    new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
        { ServiceGodotInputActionType.ChangeLayoutToDefault, new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
        { ServiceGodotInputActionType.CloseApplication,      new ServiceGodotInputBind(key: Key.None, mouseButton: MouseButton.None) },
    };
    private readonly Dictionary<ServiceGodotInputActionType, ServiceGodotInputStateType>   m_inputActionStates       = new()
    {
        { ServiceGodotInputActionType.ChangeLayoutToAfk,     ServiceGodotInputStateType.Unbound },
        { ServiceGodotInputActionType.ChangeLayoutToCode,    ServiceGodotInputStateType.Unbound },
        { ServiceGodotInputActionType.ChangeLayoutToDefault, ServiceGodotInputStateType.Unbound },
        { ServiceGodotInputActionType.CloseApplication,      ServiceGodotInputStateType.Unbound },
    };
    private readonly Dictionary<Key, ServiceGodotInputActionType>                          m_inputActionKeys         = new();
    private readonly Dictionary<MouseButton, ServiceGodotInputActionType>                  m_inputActionMouseButtons = new();

    private Vector2 m_lastMousePosition = Vector2.Zero;

    private void AddInputActionEvents()
    {
        var inputActionTypes = Enum.GetValues<ServiceGodotInputActionType>();
        foreach (var inputActionType in inputActionTypes)
        {
            this.InputActionPressed.Add(
                key:   inputActionType,
                value: null
            );
            this.InputActionPressing.Add(
                key:   inputActionType,
                value: null
            );
            this.InputActionReleased.Add(
                key:   inputActionType,
                value: null
            );
        }
    }
    
    private ServiceGodotInputStateType GetInputActionState(
        ServiceGodotInputActionType inputActionType
    )
    {
        return this.m_inputActionStates[key: inputActionType];
    }

    private void HandleInputEventMouseButton(
        MouseButton mouseButton,
        bool        pressedState
    )
    {
        if (
            this.m_inputActionMouseButtons.TryGetValue(
                key:   mouseButton,
                value: out var inputActionType
            ) is false
        )
        {
            return;
        }
        
        var inputStateType = m_inputActionStates[key: inputActionType];
        this.HandleInputState(
            inputActionType: inputActionType,
            inputStateType:  inputStateType,
            pressedState:    pressedState
        );
    }

    private void HandleInputState(
        ServiceGodotInputActionType inputActionType,
        ServiceGodotInputStateType  inputStateType,
        bool                        pressedState
    )
    {
        switch (inputStateType)
        {
            case ServiceGodotInputStateType.Pressed:
                this.HandleInputStatePressed(
                    inputActionType: inputActionType,
                    pressedState:    pressedState
                );
                break;

            case ServiceGodotInputStateType.Pressing:
                this.HandleInputStatePressing(
                    inputActionType: inputActionType,
                    pressedState:    pressedState
                );
                break;

            case ServiceGodotInputStateType.Released:
                this.HandleInputStateReleased(
                    inputActionType: inputActionType,
                    pressedState:    pressedState
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
            this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Pressing;
            this.InputActionPressing[key: inputActionType]?.Invoke(
                obj: ServiceGodotInputStateType.Pressing
            );
        }
        else
        {
            this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Released;
            this.InputActionReleased[key: inputActionType]?.Invoke(
                obj: ServiceGodotInputStateType.Released
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
            this.InputActionPressing[key: inputActionType]?.Invoke(
                obj: ServiceGodotInputStateType.Pressing
            );
        }
        else
        {
            this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Released;
            this.InputActionReleased[key: inputActionType]?.Invoke(
                obj: ServiceGodotInputStateType.Released
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
        
        this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Pressed;
        this.InputActionPressed[key: inputActionType]?.Invoke(
            obj: ServiceGodotInputStateType.Pressed
        );
    }

    private void ProcessInputActionKeys()
    {
        foreach (var inputActionKey in this.m_inputActionKeys)
        {
            var key          = inputActionKey.Key;
            var pressedState = Input.IsPhysicalKeyPressed(
                keycode: key
            );

            var inputActionType = inputActionKey.Value;
            var inputStateType  = this.m_inputActionStates[key: inputActionType];

            this.HandleInputState(
                inputActionType: inputActionType,
                inputStateType:  inputStateType,
                pressedState:    pressedState
            );
        }
    }

    private void SetDefaultInputActionBinds()
    {
        foreach (var inputActionBind in ServiceGodotInput.s_defaultInputActionBinds)
        {
            var inputAction = inputActionBind.Key;
            var inputBind   = inputActionBind.Value;

            var inputBindKey         = inputBind.Key;
            var inputBindMouseButton = inputBind.MouseButton;

            if (inputBindKey is not Key.None)
            {
                this.m_inputActionKeys.Add(
                    key:   inputBindKey,
                    value: inputAction
                );
            }
            else if (inputBindMouseButton is not MouseButton.None)
            {
                this.m_inputActionMouseButtons.Add(
                    key:   inputBindMouseButton,
                    value: inputAction
                );
            }

            this.m_inputActionBinds[key: inputAction]  = inputBind;
            this.m_inputActionStates[key: inputAction] = ServiceGodotInputStateType.Released;
        }
    }

    private void UnbindInputAction(
        ServiceGodotInputActionType inputActionType
    )
    {
        this.m_inputActionBinds[key: inputActionType] = new ServiceGodotInputBind(
            key:         Key.None,
            mouseButton: MouseButton.None
        );
        this.m_inputActionStates[key: inputActionType] = ServiceGodotInputStateType.Unbound;
    }
}