
using Godot;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;
using Overlay.Core.Contents.Effects;
using Overlay.Core.Services.Godots.BorderBurnControllers;
using Overlay.Core.Services.PastelInterpolators;

namespace Overlay.Core.Services.Lovenses;

internal sealed class ServiceLovense() :
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
        this.StartProcessCommands();
        return Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        this.StopProcessingCommands();
        return Task.CompletedTask;
    }
    
    internal void Oscillate(
        int    intensity, 
        double timeInSeconds
    )
    {
        var command = new ServiceLovenseCommand(
            action:        $"Oscillate:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity
        );
    }

    internal void Vibrate(
        int    intensity, 
        double timeInSeconds
    )
    {
        var command = new ServiceLovenseCommand(
            action:        $"Vibrate:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity
        );
    }
    
    private sealed class ServiceLovenseCommandData(
        ServiceLovenseCommand command,
        int                   timeInMilliseconds,
        int                   intensity
    )
    {
        public readonly ServiceLovenseCommand Command            = command;
        public readonly int                   TimeInMilliseconds = timeInMilliseconds;
        public readonly int                   Intensity          = intensity;
    }
    
    private const string                              c_lovenseAddress                   = "https://api.lovense-api.com/";
    private const string                              c_lovenseDomain                    = "http://192.168.88.3";
    private const string                              c_lovensePort                      = "20011";
                                  
    private string                                    m_apiIv                            = string.Empty;
    private string                                    m_apiKey                           = string.Empty;
    private string                                    m_apiToken                         = string.Empty;
    private ServiceGodotBorderBurnController          m_serviceGodotBorderBurnController = null;
    private ServicePastelInterpolator                 m_servicePastelInterpolator        = null;
    private ServiceGodotHttp                          m_serviceGodotHttp                 = null;
    private readonly Queue<ServiceLovenseCommandData> m_queueCommandDatas                = new();
    private readonly object                           m_lock                             = new();
    private bool                                      m_shutdown                         = false;

    private void AddCommandToQueue(
        ServiceLovenseCommand command,
        double                timeInSeconds,
        int                   intensity
    )
    {
        var timeInMilliseconds = (int)(timeInSeconds * 1000);
        
        var commandData = new ServiceLovenseCommandData(
            command:            command,
            timeInMilliseconds: timeInMilliseconds,
            intensity:          intensity
        );
        
        lock (this.m_lock)
        {
            this.m_queueCommandDatas.Enqueue(
                item: commandData
            );
        }
    }
    
    private void HandleServiceDatabaseRetrievedLovenseData(
        ServiceDatabaseTaskRetrievedLovenseData lovenseData    
    )
    {
        var result       = lovenseData.Result;
        
        this.m_apiIv     = result.LovenseData_Api_Iv;
        this.m_apiKey    = result.LovenseData_Api_Key;
        this.m_apiToken  = result.LovenseData_Api_Token;
    }
    
    private void RetrieveResources()
    {
        var serviceGodots                       = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp                 = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
        this.m_serviceGodotBorderBurnController = serviceGodots.GetServiceGodot<ServiceGodotBorderBurnController>();
        
        this.m_servicePastelInterpolator        =  Services.GetService<ServicePastelInterpolator>();
    }
    
    private void StartProcessCommands()
        {
            Task.Run(
                function: async () =>
                {
                    while (this.m_shutdown is false)
                    {
                        ServiceLovenseCommandData commandData;
                        lock (this.m_lock)
                        {
                            if (this.m_queueCommandDatas.Count > 0U)
                            {
                                commandData = this.m_queueCommandDatas.Dequeue();
                            }
                            else
                            {
                                continue;
                            }
                        }
    
                        var json = System.Text.Json.JsonSerializer.Serialize(
                            value: commandData.Command
                        );
                        this.m_serviceGodotHttp.SendHttpRequest(
                            url:                     $"{ServiceLovense.c_lovenseDomain}:{ServiceLovense.c_lovensePort}/command",
                            headers:                 [
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
                                        $"{nameof(ServiceLovense)}." +
                                        $"{nameof(ServiceLovense.Vibrate)}() " +
                                        $"EXCEPTION: {responseCode} error."
                                    );
                                }
                            }
                        );

                        this.m_serviceGodotBorderBurnController.AdjustBorderBurnSpeedByLovenseIntensity(
                            intensity: commandData.Intensity
                        );
                        this.m_servicePastelInterpolator.AdjustColorInterpolationSpeedByLovenseIntensity(
                            intensity: commandData.Intensity
                        );

                        EffectBackgroundKaleidoscope.Instance.AdjustKaleidoscopeSpeed(
                            intensity: commandData.Intensity
                        );
                        EffectBackgroundVaporwave.Instance.AdjustVaporwaveSpeed(
                            intensity: commandData.Intensity
                        );
                        
                        await Task.Delay(
                            millisecondsDelay: commandData.TimeInMilliseconds
                        );
                        
                        this.m_serviceGodotBorderBurnController.ResetBorderBurnSpeed();
                        this.m_servicePastelInterpolator.ResetColorInterpolationSpeed();
                        
                        EffectBackgroundKaleidoscope.Instance.ResetKaleidoscopeSpeed();
                        EffectBackgroundVaporwave.Instance.ResetVaporwaveSpeed();
                    }
                }
            );
        }
    
    private void StopProcessingCommands()
    {
        this.m_shutdown = true;
    }
    
    private void SubscribeToServiceDatabaseEvents()
    {
        ServiceDatabaseTaskEvents.RetrievedLovenseData += this.HandleServiceDatabaseRetrievedLovenseData;
    }
}