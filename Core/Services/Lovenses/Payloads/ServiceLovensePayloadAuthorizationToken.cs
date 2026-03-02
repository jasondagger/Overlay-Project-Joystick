using System;
using System.Text.Json.Serialization;

namespace Overlay.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorizationToken()
{
    [JsonPropertyName(
        name: $"authToken"
    )]
    public string AuthorizationToken { get; set; } = _ = string.Empty;
}