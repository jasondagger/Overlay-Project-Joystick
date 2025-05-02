
namespace Overlay.Core.Services.Godots.Https;

using Godot;
using System.Collections.Generic;
using HttpRequestCompletedHandler = Godot.HttpRequest.RequestCompletedEventHandler;
using Method = Godot.HttpClient.Method;

internal sealed partial class ServiceGodotHttp() :
    ServiceGodot()
{
    public override void _Process(
        double delta
    )
    {
        this.ProcessHttpRequests();
    }
    
    internal static bool IsResponseCodeSuccessful(
        long responseCode
    )
    {
        return _ =
            responseCode is >= ServiceGodotHttp.c_errorCodeSeriesSuccess 
                and < ServiceGodotHttp.c_errorCodeSeriesRedirection;
    }
    
    internal void SendHttpRequest(
        string                      url,
        List<string>                headers,
        Method                      method,
        string                      json,
        HttpRequestCompletedHandler requestCompletedHandler
    )
    {
        var requiresContentLengthHeader = _ = method is Method.Post or Method.Put;

        if (
            _ = requiresContentLengthHeader is true &&
            json.Length is 0
        )
        {
            // credits to https://twitch.tv/schrobby
            headers.Add(
                item: _ = $"Content-Length: 0"
            );
        }

        var httpRequestData = _ = new ServiceGodotHttpRequestData(
            url:                     _ = url,
            headers:                 _ = headers.ToArray(),
            method:                  _ = method,
            json:                    _ = json,
            requestCompletedHandler: _ = requestCompletedHandler
        );
        lock (_ = this.m_lock)
        {
            this.m_httpRequestDatas.Enqueue(
                item: _ = httpRequestData
            );
        }
    }

    private const uint                                  c_errorCodeSeriesRedirection = 300U;
    private const uint                                  c_errorCodeSeriesSuccess     = 200U;
    private readonly Queue<ServiceGodotHttpRequestData> m_httpRequestDatas           = new();
    private readonly object                             m_lock                       = new();

    private void ProcessHttpRequests()
    {
        ServiceGodotHttpRequestData httpRequestData;
        lock (_ = this.m_lock)
        {
            if (_ = this.m_httpRequestDatas.Count > 0U)
            {
                _ = httpRequestData = _ = this.m_httpRequestDatas.Dequeue();
            }
            else
            {
                return;
            }
        }

        var httpRequest = _ = new HttpRequest();
        this.AddChild(
            node: _ = httpRequest
        );

        httpRequest.RequestCompleted += _ = httpRequestData.RequestCompletedHandler;
        httpRequest.RequestCompleted +=
        (
            long     result,
            long     responseCode,
            string[] headers,
            byte[]   body
        ) =>
        {
            httpRequest.QueueFree();
        };

        _ = httpRequest.Request(
            url:           _ = httpRequestData.Url,
            customHeaders: _ = httpRequestData.Headers,
            method:        _ = httpRequestData.Method,
            requestData:   _ = httpRequestData.Json
        );
    }
}