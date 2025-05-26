
using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.OBS;

[Serializable()]
public sealed class ServiceOBSResponseOpCode0()
{
    [JsonPropertyName(
        name: $"op"
    )]
    public int                            Op { get; set; } = 0;
    
    [JsonPropertyName(
        name: $"d"
    )]
    public ServiceOBSResponseOpCode0Object D  { get; set; } = null;
}