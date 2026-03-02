
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
        return responseCode is >= ServiceGodotHttp.c_errorCodeSeriesSuccess 
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
        var requiresContentLengthHeader = method is Method.Post or Method.Put;

        if (
            requiresContentLengthHeader is true &&
            json.Length is 0
        )
        {
            headers.Add(
                item: $"Content-Length: 0"
            );
        }

        var httpRequestData = new ServiceGodotHttpRequestData(
            url:                     url,
            headers:                 headers.ToArray(),
            method:                  method,
            json:                    json,
            requestCompletedHandler: requestCompletedHandler
        );
        lock (this.m_lock)
        {
            this.m_httpRequestDatas.Enqueue(
                item: httpRequestData
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
        lock (this.m_lock)
        {
            if (this.m_httpRequestDatas.Count > 0U)
            {
                httpRequestData = this.m_httpRequestDatas.Dequeue();
            }
            else
            {
                return;
            }
        }

        var httpRequest = new HttpRequest();
        this.AddChild(
            node: httpRequest
        );

        httpRequest.RequestCompleted += httpRequestData.RequestCompletedHandler;
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

        httpRequest.Request(
            url:           httpRequestData.Url,
            customHeaders: httpRequestData.Headers,
            method:        httpRequestData.Method,
            requestData:   httpRequestData.Json
        );
    }
}