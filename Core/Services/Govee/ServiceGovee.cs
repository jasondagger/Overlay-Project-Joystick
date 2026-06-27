
using System;
using Godot;
using Overlay.Core.Contents;
using Overlay.Core.Services.ColorInterpolators;
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

public sealed class ServiceGovee() :
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
    
    internal void SetLightColorForCeilingLights(
        Color color
    )
    {
        var rgbValue = ServiceGovee.ConvertColorToInt(
            color: color
        );
        
        var serviceGoveePayload = new ServiceGoveePayload();
        serviceGoveePayload.Payload.Capability.Value = rgbValue;
        
        this.SendPayloads(
            payload:       serviceGoveePayload,
            ceilingLights: true
        );
    }
    
    internal void SetLightColorForStandingLights(
        Color color
    )
    {
        var rgbValue = ServiceGovee.ConvertColorToInt(
            color: color
        );
        
        var serviceGoveePayload = new ServiceGoveePayload();
        serviceGoveePayload.Payload.Capability.Value = rgbValue;
        
        this.SendPayloads(
            payload:       serviceGoveePayload,
            ceilingLights: false
        );
    }
    
    internal void SetLightSceneForCeilingLights(
        string sceneName
    )
    {
        if (
            this.m_scenesCeilingLights.TryGetValue(
                key:   sceneName, 
                value: out var light
            ) is true
        )
        {
            var serviceGoveePayloadLightsCeiling = new ServiceGoveePayload();
            serviceGoveePayloadLightsCeiling.Payload.Capability.Type     = $"devices.capabilities.dynamic_scene";
            serviceGoveePayloadLightsCeiling.Payload.Capability.Instance = $"diyScene";
            serviceGoveePayloadLightsCeiling.Payload.Capability.Value    = light;
        
            this.SendPayloads(
                payload:       serviceGoveePayloadLightsCeiling,
                ceilingLights: true
            );
        }
    }

    internal void SetLightSceneForStandingLights(
        string sceneName
    )
    {
        if (
            this.m_scenesStandingLights.TryGetValue(
                key:   sceneName, 
                value: out var light
            ) is true
        )
        {
            var serviceGoveePayloadLightsStanding = new ServiceGoveePayload();
            serviceGoveePayloadLightsStanding.Payload.Capability.Type     = $"devices.capabilities.dynamic_scene";
            serviceGoveePayloadLightsStanding.Payload.Capability.Instance = $"diyScene";
            serviceGoveePayloadLightsStanding.Payload.Capability.Value    = light;
        
            this.SendPayloads(
                payload:       serviceGoveePayloadLightsStanding,
                ceilingLights: false
            );
        }
    }
    
    internal void TurnOffLights()
    {
        var serviceGoveePayload = new ServiceGoveePayload();
        serviceGoveePayload.Payload.Capability.Type     = $"devices.capabilities.on_off";
        serviceGoveePayload.Payload.Capability.Instance = $"powerSwitch";
        serviceGoveePayload.Payload.Capability.Value    = 0;
        
        this.SendPayloads(
            payload:       serviceGoveePayload,
            ceilingLights: false
        );
        this.SendPayloads(
            payload:       serviceGoveePayload,
            ceilingLights: true
        );
    }
    
    private const string                     c_goveeAddress          = "https://openapi.api.govee.com/";
    private const string                     c_goveeLightSkuCeiling  = "H6008";
    private const string                     c_goveeLightSkuStanding = "H607C";
    private readonly Dictionary<string, int> m_scenesStandingLights  = [];
    private readonly Dictionary<string, int> m_scenesCeilingLights   = [];
    private string                           m_apiKey                = string.Empty;
    private readonly List<string>            m_hardwareIds           = [];
    private ServiceGodotHttp                 m_serviceGodotHttp      = null;
    private bool                             m_fetchedCeilingLights  = false;
    private bool                             m_fetchedStandingLights = false;

    private enum GoveeLights
    {
        StandingLight1,
        StandingLight2,
        CeilingLight1,
        CeilingLight2,
        CeilingLight3,
        StandingLight3,
    }
    
    private static int ConvertColorToInt(
        Color color
    )
    {
        return ((color.R8 & 0xFF) << 16) | ((color.G8 & 0xFF) << 8) | ((color.B8 & 0xFF) << 0);
    }
    
    private static (IServiceColorInterpolatorDefinition.ColorType? color, string scene) GetRandomLightState()
    {
        if (
            Random.Shared.Next(
                maxValue: 2
            ) is 0
        )
        {
            return (ServiceColorInterpolatorColorRandomizer.GetRandomColorType(), null);
        }
        
        return (null, ServiceGoveeSceneRandomizer.GetRandomSceneName());
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
        
        this.RequestDIYScenesForStandingLights();
        this.RequestDIYScenesForCeilingLights();
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
        var serviceGodots       = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
    }
    
    private void RequestDIYScenesForStandingLights()
    {
        var payload = new ServiceGoveePayload
        {
            Payload =
            {
                Device     = this.m_hardwareIds[(int) GoveeLights.StandingLight1],
                Sku        = ServiceGovee.c_goveeLightSkuStanding,
                Pagination = new ServiceGoveePayloadDataPagination
                {
                    PageSize = 50
                }
            }
        };

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
                        messageError: 
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

                        this.m_scenesStandingLights.TryAdd(
                            key:   option.Name,
                            value: value.GetInt32()
                        );
                    }
                }
                
                this.m_fetchedStandingLights = true;
                
#if !DEBUG
                    this.SetInitialLightColor();
#endif
            }
        );
    }
    
    private void RequestDIYScenesForCeilingLights()
        {
            var payload = new ServiceGoveePayload
            {
                Payload =
                {
                    Device = this.m_hardwareIds[(int) GoveeLights.CeilingLight1],
                    Sku    = ServiceGovee.c_goveeLightSkuCeiling
                }
            };
    
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
                            messageError: 
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
    
                            this.m_scenesCeilingLights.TryAdd(
                                key:   option.Name,
                                value: value.GetInt32()
                            );
                        }
                    }

                    this.m_fetchedCeilingLights = true;
                    
#if !DEBUG
                    this.SetInitialLightColor();
#endif
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
                        messageError: 
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
        ServiceGoveePayload payload,
        bool ceilingLights
    )
    {
        foreach (var hardwareId in this.m_hardwareIds)
        {
            payload.Payload.Device = hardwareId;
            payload.Payload.Sku    = ceilingLights ? ServiceGovee.c_goveeLightSkuCeiling : ServiceGovee.c_goveeLightSkuStanding;
            
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
                            messageError: 
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

    private void SetInitialLightColor()
    {
        if (
            this.m_fetchedCeilingLights is true && 
            this.m_fetchedStandingLights is true
        )
        {
            var useSameValue = Random.Shared.Next(
                maxValue: 5
            ) is not 0;
            
            if (useSameValue)
            {
                if (
                    Random.Shared.Next(
                        maxValue: 5
                    ) is not 0
                )
                {
                    var color = ServiceColorInterpolatorColorRandomizer.GetRandomColorType();
                    GoveeLightControllers.SetGoveeLightControllers(
                        lightsCeiling:  (color, null),
                        lightsStanding: (color, null)
                    );
                    GoveeLightControllerCeiling.Instance.SetAndStoreLightColor(
                        colorType:           color, 
                        delayInMilliseconds: GoveeLightControllers.DelayInColorCycleInShortMilliseconds
                    );
                    GoveeLightControllerStanding.Instance.SetAndStoreLightColor(
                        colorType:           color, 
                        delayInMilliseconds: GoveeLightControllers.DelayInColorCycleInShortMilliseconds
                    );
                }
                else
                {
                    var sceneName = ServiceGoveeSceneRandomizer.GetRandomSceneName();
                    GoveeLightControllers.SetGoveeLightControllers(
                        lightsCeiling:  (null, sceneName),
                        lightsStanding: (null, sceneName)
                    );
                }
            }
            else
            {
                var lightStateA = ServiceGovee.GetRandomLightState();
                var lightStateB = ServiceGovee.GetRandomLightState();
                var swap        = new Random().Next(
                    maxValue: 2
                ) is 0;
                GoveeLightControllers.SetGoveeLightControllers(
                    lightsCeiling:  swap ? lightStateA : lightStateB,
                    lightsStanding: swap ? lightStateB : lightStateA
                );
            }
        }
    }
    
    private void SubscribeToServiceDatabaseEvents()
    {
        ServiceDatabaseTaskEvents.RetrievedGoveeData       += this.HandleServiceDatabaseRetrievedGoveeData;
        ServiceDatabaseTaskEvents.RetrievedListGoveeLights += this.HandleServiceDatabaseRetrievedListGoveeLights;
    }
}