using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.OBS;

[Serializable()]
public sealed class ServiceOBSResponseOpCode0Object()
{
    [JsonPropertyName(
        name: $"obsStudioVersion"
    )]
    public string                    ObsStudioVersion    { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"obsWebSocketVersion"
    )]
    public string                    ObsWebSocketVersion { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"rpcVersion"
    )]
    public int                       RpcVersion          { get; set; } = 0;
}