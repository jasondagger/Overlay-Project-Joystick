
using Godot;
using Overlay.Core.Contents.Effects;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.Effects.Rainbows;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.BorderBurnControllers;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EffectBackgroundKaleidoscope = Overlay.Core.Contents.Effects.Backgrounds.EffectBackgroundKaleidoscope;
using EffectBackgroundSnow         = Overlay.Core.Contents.Effects.Backgrounds.EffectBackgroundSnow;
using EffectBackgroundVaporwave    = Overlay.Core.Contents.Effects.Backgrounds.EffectBackgroundVaporwave;

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
        this.StartAutomaticLovenseCommands();
        return Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        this.StopProcessingCommands();
        return Task.CompletedTask;
    }
    
    internal void All(
        int       intensity, 
        double    timeInSeconds,
        EventType eventType
    )
    {
        var command = new ServiceLovenseCommand(
            action:        $"All:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity,
            eventType:     eventType
        );
    }
    
    internal void Oscillate(
        int       intensity, 
        double    timeInSeconds,
        EventType eventType
    )
    {
        var command = new ServiceLovenseCommand(
            action:        $"Oscillate:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity,
            eventType:     eventType
        );
    }

    internal void Vibrate(
        int       intensity, 
        double    timeInSeconds,
        EventType eventType
    )
    {
        var command = new ServiceLovenseCommand(
            action:        $"Vibrate:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity,
            eventType:     eventType
        );
    }

    internal enum EventType
    {
        None,
        DroppedIn,
        Followed,
        MilestoneCompleted,
        Subscribed,
        TF2_BigEarner,
        TF2_Death,
        TF2_DiamondBack,
        TF2_Pan,
        TF2_GrenadeLauncher,
        TF2_StickyLauncher,
        Tipped,
    }
    
    private sealed class ServiceLovenseCommandData(
        ServiceLovenseCommand command,
        int                   timeInMilliseconds,
        int                   intensity,
        EventType             eventType
    )
    {
        public readonly ServiceLovenseCommand Command            = command;
        public readonly int                   TimeInMilliseconds = timeInMilliseconds;
        public readonly int                   Intensity          = intensity;
        public readonly EventType             EventType          = eventType;
    }
    
    private const int                                 c_automaticTriggerIntensityMaximum             = 20;
    private const int                                 c_automaticTriggerIntensityMinimum             = 5;
    private const int                                 c_automaticTriggerTimeMaximumInSeconds         = 5;
    private const int                                 c_automaticTriggerTimeMinimumInSeconds         = 2;
    private const string                              c_lovenseDomain                                = "http://192.168.88.3";
    private const string                              c_lovensePort                                  = "20010";
    private const int                                 c_timeDelayForAutoTriggerInMillisecondsMinimum = 20000;
    private const int                                 c_timeDelayForAutoTriggerInMillisecondsMaximum = 300000;
    private const int                                 c_fadeEffectDuration                           = 350;
    
    private string                                    m_apiIv                                        = string.Empty;
    private string                                    m_apiKey                                       = string.Empty;
    private string                                    m_apiToken                                     = string.Empty;
    private readonly RandomNumberGenerator            m_random                                       = new();
    private ServiceGodotBorderBurnController          m_serviceGodotBorderBurnController             = null;
    private ServiceColorInterpolatorNormal            m_serviceColorInterpolatorNormal               = null;
    private ServiceColorInterpolatorInverse           m_serviceColorInterpolatorInverse              = null;
    private ServiceGodotHttp                          m_serviceGodotHttp                             = null;
    private readonly Queue<ServiceLovenseCommandData> m_queueCommandDatas                            = new();
    private readonly object                           m_lock                                         = new();
    private bool                                      m_shutdown                                     = false;

    private static void ApplySnowTextureUpdate(
        EventType eventType
    )
    {
        if (EffectBackgroundSnow.Instance is null)
        {
            return;
        }

        switch (eventType)
        {
            case EventType.DroppedIn:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.Plane
                );
                break;
                        
            case EventType.Followed:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.Heart
                );
                break;
            
            case EventType.MilestoneCompleted:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.Star
                );
                break;
                        
            case EventType.Subscribed:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.Star
                );
                break;
            
            case EventType.TF2_BigEarner:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.TF2_BigEarner
                );
                break;
            
            case EventType.TF2_Death:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.TF2_Death
                );
                break;
            
            case EventType.TF2_DiamondBack:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.TF2_DiamondBack
                );
                break;
            
            case EventType.TF2_GrenadeLauncher:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.TF2_GrenadeLauncher
                );
                break;
            
            case EventType.TF2_Pan:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.TF2_Pan
                );
                break;
            
            case EventType.TF2_StickyLauncher:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.TF2_StickyLauncher
                );
                break;
            
            case EventType.Tipped:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.MoneyBag
                );
                break;
            
            case EventType.None:
                EffectBackgroundSnow.Instance.SetParticleTexture(
                    particleTexture: EffectBackgroundSnow.ParticleTexture.Penis
                );
                break;

            default:
                throw new NotImplementedException();
        }
    }
    
    private void AddCommandToQueue(
        ServiceLovenseCommand command,
        double                timeInSeconds,
        int                   intensity,
        EventType             eventType
    )
    {
        var timeInMilliseconds = (int)(timeInSeconds * 1000);
        
        var commandData = new ServiceLovenseCommandData(
            command:            command,
            timeInMilliseconds: timeInMilliseconds,
            intensity:          intensity,
            eventType:          eventType
        );
        
        lock (this.m_lock)
        {
            this.m_queueCommandDatas.Enqueue(
                item: commandData
            );
        }
    }
    
    private void ApplyEffects(
        float normalizedIntensity
    )
    {
        var intensity = Mathf.Remap(
            value:   normalizedIntensity,
            inFrom:  0f,
            inTo:    1f,
            outFrom: 1f,
            outTo:   2f
        );
        
        this.m_serviceGodotBorderBurnController.AdjustBorderBurnSpeedByLovenseIntensity(
            intensity: intensity
        );
        this.m_serviceColorInterpolatorNormal.AdjustColorInterpolationSpeedByLovenseIntensity(
            intensity: intensity
        );
        this.m_serviceColorInterpolatorInverse.AdjustColorInterpolationSpeedByLovenseIntensity(
            intensity: intensity
        );
        
        EffectBackgroundConfetti.Instance.AdjustConfettiSpeed(
            intensity: intensity
        );
        EffectBackgroundAvatar.AdjustDancerSpeed(
            intensity: intensity
        );
        EffectBackgroundDancerInverse.AdjustDancerSpeed(
            intensity: intensity
        );
        EffectBackgroundGeometry.AdjustGeometrySpeed(
            intensity: intensity
        );
        EffectBackgroundGeometryVaporwave.AdjustGeometrySpeed(
            intensity: intensity
        );
        EffectBackgroundGodRays.Instance.AdjustGodRaysSpeed(
            intensity: intensity
        );
        EffectBackgroundKaleidoscope.Instance.AdjustKaleidoscopeSpeed(
            intensity: intensity
        );
        EffectRainbowStripe.AdjustStripeScrollSpeed(
            intensity: intensity
        );
        EffectBackgroundStars.Instance.AdjustStarsSpeed(
            intensity: intensity
        );
        EffectBackgroundVaporwave.AdjustVaporwaveSpeed(
            intensity: intensity
        );
        EffectBackgroundSnow.Instance.AdjustScrollSpeed(
            intensity: intensity
        );
        EffectRainbowExtender.AdjustExtenderSpeeds(
            intensity: intensity
        );
    }
    
    private void HandleServiceDatabaseRetrievedLovenseData(
        ServiceDatabaseTaskRetrievedLovenseData lovenseData
    )
    {
        var result      = lovenseData.Result;
        
        this.m_apiIv    = result.LovenseData_Api_Iv;
        this.m_apiKey   = result.LovenseData_Api_Key;
        this.m_apiToken = result.LovenseData_Api_Token;
    }
    
    private void ResetEffects()
    {
        this.m_serviceGodotBorderBurnController.ResetBorderBurnSpeed();
        this.m_serviceColorInterpolatorNormal.ResetColorInterpolationSpeed();
        this.m_serviceColorInterpolatorInverse.ResetColorInterpolationSpeed();
                    
        EffectBackgroundConfetti.Instance.ResetConfettiSpeed();
        EffectBackgroundAvatar.ResetDancerSpeed();
        EffectBackgroundDancerInverse.ResetDancerSpeed();
        EffectBackgroundGeometry.ResetGeometrySpeed();
        EffectBackgroundGeometryVaporwave.ResetGeometrySpeed();
        EffectBackgroundGodRays.Instance.ResetGodRaysSpeed();
        EffectBackgroundKaleidoscope.Instance.ResetKaleidoscopeSpeed();
        EffectBackgroundStars.Instance.ResetStarsSpeed();
        EffectBackgroundVaporwave.ResetVaporwaveSpeed();
        EffectBackgroundSnow.Instance.ResetScrollSpeed();
        EffectBackgroundSnow.Instance.ResetParticleTexture();
        EffectRainbowExtender.ResetExtenderSpeed();
        EffectRainbowStripe.ResetStripeScrollSpeed();
    }
    
    private void RetrieveResources()
    {
        var serviceGodots                       = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp                 = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
        this.m_serviceGodotBorderBurnController = serviceGodots.GetServiceGodot<ServiceGodotBorderBurnController>();
        
        this.m_serviceColorInterpolatorNormal   = Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse  = Services.GetService<ServiceColorInterpolatorInverse>();
    }

    private void StartAutomaticLovenseCommands()
    {
        Task.Run(
            function: async () =>
            {
                var serviceJoystickBot = Services.GetService<ServiceJoystickBot>();
                var serviceGodots      = Services.GetService<ServiceGodots>();
                var serviceGodotAudio  = serviceGodots.GetServiceGodot<ServiceGodotAudio>();
                while (this.m_shutdown is false)
                {
                    var timeDelayInMilliseconds = this.m_random.RandiRange(
                        from: ServiceLovense.c_timeDelayForAutoTriggerInMillisecondsMinimum,
                        to:   ServiceLovense.c_timeDelayForAutoTriggerInMillisecondsMaximum
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: timeDelayInMilliseconds
                    );
                    
                    var intensity     = this.m_random.RandiRange(
                        from: ServiceLovense.c_automaticTriggerIntensityMinimum,
                        to:   ServiceLovense.c_automaticTriggerIntensityMaximum
                    );
                    var timeInSeconds = this.m_random.RandiRange(
                        from: ServiceLovense.c_automaticTriggerTimeMinimumInSeconds,
                        to:   ServiceLovense.c_automaticTriggerTimeMaximumInSeconds
                    );
                    
                    this.All(
                        intensity:     intensity,
                        timeInSeconds: timeInSeconds,
                        eventType:     EventType.None
                    );
                    serviceGodotAudio.PlaySoundAlert(
                        soundAlertType: ServiceGodotAudio.SoundAlertType.Lovense
                    );
                    serviceJoystickBot.SendChatMessageSilently(
                        message: $"🕹️ Toys activated."
                    );
                }
            }
        );
    }
    
    private void StartProcessCommands()
    {
        Task.Run(
            function: async () =>
            {
                while (this.m_shutdown is false)
                {
                    await Task.Delay(
                        millisecondsDelay: 16
                    );
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
                                    $"{nameof(ServiceLovense.All)}() " +
                                    $"EXCEPTION: {responseCode} error."
                                );
                            }
                        }
                    );

                    ServiceLovense.ApplySnowTextureUpdate(
                        eventType: commandData.EventType
                    );

                    var stayDuration = Math.Max(
                        val1: 0, 
                        val2: commandData.TimeInMilliseconds - 2 * ServiceLovense.c_fadeEffectDuration
                    );

                    for (var elapsed = 0; elapsed <= ServiceLovense.c_fadeEffectDuration; elapsed += 10) {
                        var normalizedIntensity = (float)elapsed / ServiceLovense.c_fadeEffectDuration;
                        this.ApplyEffects(
                            normalizedIntensity: normalizedIntensity
                        );
                        await Task.Delay(
                            millisecondsDelay: 10
                        );
                    }

                    await Task.Delay(
                        millisecondsDelay: stayDuration
                    );

                    for (var elapsed = ServiceLovense.c_fadeEffectDuration; elapsed >= 0; elapsed -= 10) {
                        var normalizedIntensity = (float)elapsed / ServiceLovense.c_fadeEffectDuration;
                        this.ApplyEffects(
                            normalizedIntensity: normalizedIntensity
                        );
                        await Task.Delay(
                            millisecondsDelay: 10
                        );
                    }
                    
                    this.ResetEffects();
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