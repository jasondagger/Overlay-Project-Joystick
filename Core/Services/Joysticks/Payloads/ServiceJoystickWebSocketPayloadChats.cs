using System;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Services.Joysticks.Payloads;

internal static class ServiceJoystickWebSocketPayloadChats
{
    internal static Action<ServiceJoystickWebSocketPayloadMessage> TipReceived      { get; set; } = null;
}