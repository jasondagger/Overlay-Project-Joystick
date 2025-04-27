
using Godot;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Govee.Payloads;
using Overlay.Core.Tools;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Overlay.Core.Services.Govee;

internal sealed class ServiceGovee() :
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
    
    internal void SetLightColor(
        Color color
    )
    {
        var rgbValue = _ = ServiceGovee.ConvertColorToInt(
            color: _ = color
        );
        var serviceGoveePayload = _ = new ServiceGoveePayload();
        _ = serviceGoveePayload.Payload.Capability.Value = _ = rgbValue;
        this.SendPayloads(
            payload: _ = serviceGoveePayload
        );
    }
    
    internal void TurnOffLights()
    {
        var serviceGoveePayload = _ = new ServiceGoveePayload();
        _ = serviceGoveePayload.Payload.Capability.Type     = $"devices.capabilities.on_off";
        _ = serviceGoveePayload.Payload.Capability.Instance = $"powerSwitch";
        _ = serviceGoveePayload.Payload.Capability.Value    = _ = 0;
        
        this.SendPayloads(
            payload: _ = serviceGoveePayload
        );
    }
    
    internal void TurnOnLights()
    {
        var serviceGoveePayload = _ = new ServiceGoveePayload();
        _ = serviceGoveePayload.Payload.Capability.Type     = $"devices.capabilities.on_off";
        _ = serviceGoveePayload.Payload.Capability.Instance = $"powerSwitch";
        _ = serviceGoveePayload.Payload.Capability.Value    = _ = 1;
        
        this.SendPayloads(
            payload: _ = serviceGoveePayload
        );
    }
    
    private const string          c_goveeAddress     = "https://openapi.api.govee.com/";
    
    private string                m_apiKey           = string.Empty;
    private readonly List<string> m_hardwareIds      = [];
    private ServiceGodotHttp      m_serviceGodotHttp = null;
    
    private static int ConvertColorToInt(
        Color color
    )
    {
        return ((color.R8 & 0xFF) << 16) | ((color.G8 & 0xFF) << 8) | ((color.B8 & 0xFF) << 0);
    }
    
    private void HandleServiceDatabaseRetrievedListGoveeLights(
        ServiceDatabaseTaskRetrievedListGoveeLights lights    
    )
    {
        var result = _ = lights.Result;
        
        foreach (var light in _ = result)
        {
            this.m_hardwareIds.Add(
                item: _ = light.GoveeLight_Hardware_Id
            );
        }
    }
    
    private void HandleServiceDatabaseRetrievedGoveeData(
        ServiceDatabaseTaskRetrievedGoveeData goveeData    
    )
    {
        var result        = _ = goveeData.Result;
        _ = this.m_apiKey = _ = result.GoveeData_Api_Key;
    }
    
    private void RetrieveResources()
    {
        var serviceGodots           = _ = Services.GetService<ServiceGodots>();
        _ = this.m_serviceGodotHttp = _ = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
    }
    
    private void SendPayloads(
        ServiceGoveePayload payload
    )
    {
        foreach (var hardwareId in _ = this.m_hardwareIds)
        {
            _ = payload.Payload.Device = _ = hardwareId;
            
            var json = _ = JsonHelper.Serialize(
                @object: _ = payload
            );
            
            this.m_serviceGodotHttp.SendHttpRequest(
                url:                     _ = $"{_ = ServiceGovee.c_goveeAddress}router/api/v1/device/control",
                headers:                 [
                    $"Govee-API-Key: {_ = this.m_apiKey}",
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
                                $"{_ = nameof(ServiceGovee)}." +
                                $"{_ = nameof(ServiceGovee.SendPayloads)}() " +
                                $"EXCEPTION: {_ = responseCode} error."
                        );
                    }
                }
            );
        }
    }
    
    private void SubscribeToServiceDatabaseEvents()
    {
        _ = ServiceDatabaseTaskEvents.RetrievedGoveeData       += this.HandleServiceDatabaseRetrievedGoveeData;
        _ = ServiceDatabaseTaskEvents.RetrievedListGoveeLights += this.HandleServiceDatabaseRetrievedListGoveeLights;
    }
}