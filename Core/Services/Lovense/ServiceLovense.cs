
using System;
using System.Text;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using System.Threading.Tasks;
using Godot;
using Overlay.Core.Services.Lovense.Payloads;
using Overlay.Core.Tools;

namespace Overlay.Core.Services.Lovense;

internal sealed class ServiceLovense() :
    IService
{
    Task IService.Setup()
    {
        this.SubscribeToServiceDatabaseEvents();
        this.RetrieveResources();
        return _ = Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        return _ = Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        return _ = Task.CompletedTask;
    }
    
    private const string     c_lovenseAddress   = "https://api.lovense-api.com/";
    
    private string           m_apiIv            = string.Empty;
    private string           m_apiKey           = string.Empty;
    private string           m_apiToken         = string.Empty;
    private string           m_apiUserId        = string.Empty;
    private ServiceGodotHttp m_serviceGodotHttp = null;
    
    private void HandleServiceDatabaseRetrievedLovenseData(
        ServiceDatabaseTaskRetrievedLovenseData lovenseData    
    )
    {
        var result           = _ = lovenseData.Result;
        
        _ = this.m_apiIv     = _ = result.LovenseData_Api_Iv;
        _ = this.m_apiKey    = _ = result.LovenseData_Api_Key;
        _ = this.m_apiToken  = _ = result.LovenseData_Api_Token;
        _ = this.m_apiUserId = _ = Guid.NewGuid().ToString();
        
        this.RetrieveAuthenticationToken();
    }

    private void RetrieveAuthenticationToken()
    {
        var payloadAuthorization = _ = new ServiceLovensePayloadAuthorization();
        
        payloadAuthorization.Token  = _ = this.m_apiToken;
        payloadAuthorization.UserId = _ = this.m_apiUserId;
        
        var json = _ = JsonHelper.Serialize(
            @object: _ = payloadAuthorization
        );
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     _ = $"{_ = ServiceLovense.c_lovenseAddress}api/basicApi/getToken",
            headers:                 [
                $"Content-Type: application/json",
            ],
            method:                  _ = HttpClient.Method.Post,
            json:                    _ = json,
            requestCompletedHandler: (
                long     result,
                long     responseCode,
                string[] headers,
                byte[]   body
            ) =>
            {
                if (responseCode >= 300)
                {
                    ConsoleLogger.LogMessageError(
                        messageError: _ =
                            $"{_ = nameof(ServiceLovense)}." +
                            $"{_ = nameof(ServiceLovense.RetrieveAuthenticationToken)}() " +
                            $"EXCEPTION: {_ = responseCode} error."
                    );
                }
                
                json = _ = Encoding.UTF8.GetString(
                    bytes: _ = body
                );
                var token = _ = JsonHelper.Deserialize<ServiceLovensePayloadAuthorizationResult>(
                    json: _ = json
                );

                int i = 0;
                i++;
            }
        );
    }
    
    private void RetrieveResources()
    {
        var serviceGodots           = _ = Services.GetService<ServiceGodots>();
        _ = this.m_serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
    }
    
    private void SubscribeToServiceDatabaseEvents()
    {
        _ = ServiceDatabaseTaskEvents.RetrievedLovenseData += this.HandleServiceDatabaseRetrievedLovenseData;
    }
}