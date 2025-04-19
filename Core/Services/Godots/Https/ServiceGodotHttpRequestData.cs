
namespace Overlay.Core.Services.Godots.Https;

using static Godot.HttpClient;
using HttpRequestCompletedHandler = Godot.HttpRequest.RequestCompletedEventHandler;

internal struct ServiceGodotHttpRequestData
{
    internal ServiceGodotHttpRequestData(
        string                      url,
        string[]                    headers,
        Method                      method,
        string                      json,
        HttpRequestCompletedHandler requestCompletedHandler
    )
    {
        this.Url                     = _ = url;
        this.Headers                 = _ = headers;
        this.Method                  = _ = method;
        this.Json                    = _ = json;
        this.RequestCompletedHandler = _ = requestCompletedHandler;
    }

    internal string                      Url                     { get; set; } = _ = string.Empty;
    internal string[]                    Headers                 { get; set; } = null;
    internal Method                      Method                  { get; set; } = _ = Method.Get;
    internal string                      Json                    { get; set; } = _ = string.Empty;
    internal HttpRequestCompletedHandler RequestCompletedHandler { get; set; } = null;
}