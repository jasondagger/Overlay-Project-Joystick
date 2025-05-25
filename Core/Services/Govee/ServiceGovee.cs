
using Godot;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Govee.Payloads;
using Overlay.Core.Tools;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Overlay.Core.Services.Govee;

internal sealed class ServiceGovee() :
    IService
{
    Task IService.Setup()
    {
        this.SubscribeToServiceDatabaseEvents();
        this.RetrieveResources();
        return Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        return Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        return Task.CompletedTask;
    }

    internal enum LightScene :
        uint
    {
        Default = 15863356
    }
    
    internal void SetLightColor(
        Color color
    )
    {
        var rgbValue = ServiceGovee.ConvertColorToInt(
            color: color
        );
        var serviceGoveePayload = new ServiceGoveePayload();
        serviceGoveePayload.Payload.Capability.Value = rgbValue;
        this.SendPayloads(
            payload: serviceGoveePayload
        );
    }
    
    internal void TurnOffLights()
    {
        var serviceGoveePayload = new ServiceGoveePayload();
        serviceGoveePayload.Payload.Capability.Type     = $"devices.capabilities.on_off";
        serviceGoveePayload.Payload.Capability.Instance = $"powerSwitch";
        serviceGoveePayload.Payload.Capability.Value    = 0;
        
        this.SendPayloads(
            payload: serviceGoveePayload
        );
    }

    internal void SetLightScene(
        string sceneName
    )
    {
        if (
            this.m_scenes.ContainsKey(
                key: sceneName
            ) is false
        )
        {
            return;
        }
        
        var serviceGoveePayload = new ServiceGoveePayload();
        serviceGoveePayload.Payload.Capability.Type     = $"devices.capabilities.dynamic_scene";
        serviceGoveePayload.Payload.Capability.Instance = $"diyScene";
        serviceGoveePayload.Payload.Capability.Value    = this.m_scenes[key: sceneName];
        
        this.SendPayloads(
            payload: serviceGoveePayload
        );
    }
    
    private const string                     c_goveeAddress     = "https://openapi.api.govee.com/";

    private readonly Dictionary<string, int> m_scenes           = [];
    private string                           m_apiKey           = string.Empty;
    private readonly List<string>            m_hardwareIds      = [];
    private ServiceGodotHttp                 m_serviceGodotHttp = null;
    
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
        var result = lights.Result;
        
        foreach (var light in result)
        {
            this.m_hardwareIds.Add(
                item: light.GoveeLight_Hardware_Id
            );
        }
        
        this.RequestDIYScenes();
    }
    
    private void HandleServiceDatabaseRetrievedGoveeData(
        ServiceDatabaseTaskRetrievedGoveeData goveeData    
    )
    {
        var result    = goveeData.Result;
        this.m_apiKey = result.GoveeData_Api_Key;
    }
    
    private void RetrieveResources()
    {
        var serviceGodots           = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
    }
    
    private void RequestDIYScenes()
    {
        var payload = new ServiceGoveePayload();
            
        payload.Payload.Device = this.m_hardwareIds[0];
        
        var json = JsonHelper.Serialize(
            @object: payload
        );
        
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceGovee.c_goveeAddress}router/api/v1/device/diy-scenes",
            headers:                 [
                $"Govee-API-Key: {this.m_apiKey}",
                $"Content-Type: application/json",
            ],
            method:                  HttpClient.Method.Post,
            json:                    json,
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
                            $"{nameof(ServiceGovee)}." +
                            $"{nameof(ServiceGovee.SendPayloads)}() " +
                            $"EXCEPTION: {responseCode} error."
                    );
                }
                
                var bodyAsString = Encoding.UTF8.GetString(
                    bytes: body
                );
                var serviceGoveeDIY = JsonHelper.Deserialize<ServiceGoveeDIY>(
                    json: bodyAsString
                );

                foreach (var capability in serviceGoveeDIY.Payload.Capabilities)
                {
                    var options = capability.Parameters.Options;
                    foreach (var option in options)
                    {
                        var value = (JsonElement)option.Value;

                        this.m_scenes.TryAdd(
                            key:   option.Name,
                            value: value.GetInt32()
                        );
                    }
                }
            }
        );
    }

    private void RequestDeviceInformation()
    {
        this.m_serviceGodotHttp.SendHttpRequest(
            url:                     $"{ServiceGovee.c_goveeAddress}router/api/v1/user/devices",
            headers:                 [
                $"Govee-API-Key: {this.m_apiKey}",
                $"Content-Type: application/json",
            ],
            method:                  HttpClient.Method.Get,
            json:                    string.Empty,
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
                            $"{nameof(ServiceGovee)}." +
                            $"{nameof(ServiceGovee.SendPayloads)}() " +
                            $"EXCEPTION: {responseCode} error."
                    );
                }
                
                var bodyAsString = Encoding.UTF8.GetString(
                    bytes: body
                );
            }
        );
    }
    
    private void SendPayloads(
        ServiceGoveePayload payload
    )
    {
        foreach (var hardwareId in this.m_hardwareIds)
        {
            payload.Payload.Device = hardwareId;
            
            var json = JsonHelper.Serialize(
                @object: payload
            );
            
            this.m_serviceGodotHttp.SendHttpRequest(
                url:                     $"{ServiceGovee.c_goveeAddress}router/api/v1/device/control",
                headers:                 [
                    $"Govee-API-Key: {this.m_apiKey}",
                    $"Content-Type: application/json",
                ],
                method:                  HttpClient.Method.Post,
                json:                    json,
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
                                $"{nameof(ServiceGovee)}." +
                                $"{nameof(ServiceGovee.SendPayloads)}() " +
                                $"EXCEPTION: {responseCode} error."
                        );
                    }

                    var bodyAsString = Encoding.UTF8.GetString(
                        bytes: body
                    );
                }
            );
        }
    }
    
    private void SubscribeToServiceDatabaseEvents()
    {
        ServiceDatabaseTaskEvents.RetrievedGoveeData       += this.HandleServiceDatabaseRetrievedGoveeData;
        ServiceDatabaseTaskEvents.RetrievedListGoveeLights += this.HandleServiceDatabaseRetrievedListGoveeLights;
    }
}