
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Joysticks;

[Serializable()]
public sealed class ServiceJoystickPagination()
{
    [JsonPropertyName(
        name: $"total_items"
    )]
    public int               TotalItems { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"total_pages"
    )]
    public int               TotalPages { get; set; } = 0;
}