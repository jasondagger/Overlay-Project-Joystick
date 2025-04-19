using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks.Payloads.Metadatas;

[Serializable()]
public sealed class ServiceJoystickWebSocketPayloadMessageMetadataSubscriberCountUpdated()
{
    [JsonPropertyName(
        name: $"number_of_subscribers"
    )]
    public int NumberOfSubscribers { get; set; } = _ = 0;
}