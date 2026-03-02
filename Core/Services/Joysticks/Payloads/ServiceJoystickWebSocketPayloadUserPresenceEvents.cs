
using System;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadUserPresenceEvents
{
    internal static Action<ServiceJoystickWebSocketPayloadMessage> UserEntered { get; set; } = null;
    internal static Action<ServiceJoystickWebSocketPayloadMessage> UserLeft    { get; set; } = null;
}