
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks;

[Serializable()]
public sealed class ServiceJoystickSubscriptionResponse()
{
    [JsonPropertyName(
        name: $"items"
    )]
    public ServiceJoystickSubscriber[] Items      { get; set; } = [];
    
    [JsonPropertyName(
        name: $"pagination"
    )]
    public ServiceJoystickPagination   Pagination { get; set; } = new();
}