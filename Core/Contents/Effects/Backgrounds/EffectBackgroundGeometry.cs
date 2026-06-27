
using System;
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundGeometry() :
    ColorRect()
{
    public override void _Process(
        double delta
    )
    {
        this.UpdateShaderResources(
            delta: (float) delta
        );
    }
    
    public override void _Ready()
    {
        this.AddInstance();
        this.RetrieveResources();
        this.SetShaderMaterial();
    }
    
    internal static void SetInitialGeometryColorInverse(
        ServiceColorInterpolatorColorMode color
    )
    {
        EffectBackgroundGeometry.s_initialColorInverse = color;
    }
    
    internal static void SetInitialGeometryColorNormal(
        ServiceColorInterpolatorColorMode color
    )
    {
        EffectBackgroundGeometry.s_initialColorNormal = color;
    }
    
    internal static void UpdateGeometryColorInverse(
        ServiceColorInterpolatorColorMode color
    )
    {
        foreach (var instance in EffectBackgroundGeometry.Instances)
        {
            lock (instance.m_lock)
            {
                instance.m_colorInverse = color;
            }
        }
    }
    
    internal static void UpdateGeometryColorNormal(
        ServiceColorInterpolatorColorMode color
    )
    {
        foreach (var instance in EffectBackgroundGeometry.Instances)
        {
            lock (instance.m_lock)
            {
                instance.m_colorNormal = color;
            }
        }
    }
    
    internal static void AdjustGeometrySpeed(
        float intensity
    )
    {
        foreach (var instance in EffectBackgroundGeometry.Instances)
        {
            lock (instance.m_lock)
            {
                instance.m_speedScale = intensity;
            }
        }
    }
    
    internal static void ResetGeometrySpeed()
    {
        EffectBackgroundGeometry.AdjustGeometrySpeed(
            intensity: 1
        );
    }
    
    private const float                                    c_randomizeTime                   = 2f;
    
    private static readonly List<EffectBackgroundGeometry> Instances                         = [];
    private static ServiceColorInterpolatorColorMode       s_initialColorInverse             = ServiceColorInterpolatorColorMode.Transition;
    private static ServiceColorInterpolatorColorMode       s_initialColorNormal              = ServiceColorInterpolatorColorMode.Transition;
    
    private readonly object                                m_lock                            = new();

    private ServiceColorInterpolatorColorMode              m_colorInverse                    = ServiceColorInterpolatorColorMode.Transition;
    private ServiceColorInterpolatorColorMode              m_colorNormal                     = ServiceColorInterpolatorColorMode.Transition;
    private int[]                                          m_effectsCurrent                  = new int[5];
    private int[]                                          m_effectsNext                     = new int[5];
    private float                                          m_gridOffset                      = 0f;
    private ShaderMaterial                                 m_material                        = null;
    private ServiceColorInterpolatorInverse                m_serviceColorInterpolatorInverse = null;
    private ServiceColorInterpolatorNormal                 m_serviceColorInterpolatorNormal  = null;
    private float                                          m_speedScale                      = 1f;
    private float                                          m_timeUntilNextRandomization      = EffectBackgroundGeometry.c_randomizeTime;
    private float                                          m_transitionProgress              = 1f;
    
    private void AddInstance()
        {
            EffectBackgroundGeometry.Instances.Add(
                item: this
            );
        }
    
    private void RandomizeEffectSlots()
    {
        Array.Copy(
            sourceArray:      this.m_effectsNext, 
            destinationArray: this.m_effectsCurrent, 
            length:           5
        );
        
        for (var i = 0; i < 5; i++)
        {
            this.m_material.SetShaderParameter(
                param: $"current_slot_{i}", 
                value: this.m_effectsCurrent[i]
            );
            
            this.m_effectsNext[i] = GD.RandRange(
                from: 0,
                to:   49
            );
            this.m_material.SetShaderParameter(
                param: $"next_slot_{i}", 
                value: this.m_effectsNext[i]
            );
        }
        
        this.m_transitionProgress = 0f;
        this.m_material.SetShaderParameter(
            param: $"transition_weight", 
            value: this.m_transitionProgress
        );
    }
    
    private void RandomizeInitialEffectSlots()
    {
        for (var i = 0; i < 5; i++)
        {
            this.m_effectsNext[i] = GD.RandRange(
                from: 0,
                to:   49
            );
        }
        
        Array.Copy(
            sourceArray:      this.m_effectsNext, 
            destinationArray: this.m_effectsCurrent, 
            length:           5
        );

        for (var i = 0; i < 5; i++)
        {
            this.m_material.SetShaderParameter(
                param: $"current_slot_{i}", 
                value: this.m_effectsCurrent[i]
            );
            this.m_material.SetShaderParameter(
                param: $"next_slot_{i}", 
                value: this.m_effectsNext[i]
            );
        }
        
        this.m_transitionProgress = 0f;
        this.m_material.SetShaderParameter(
            param: $"transition_weight", 
            value: this.m_transitionProgress
        );
    }
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal  = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }
    
    private void SetShaderMaterial()
    {
        this.m_material     = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_colorInverse = EffectBackgroundGeometry.s_initialColorInverse;
        this.m_colorNormal  = EffectBackgroundGeometry.s_initialColorNormal;
        
        this.RandomizeInitialEffectSlots();
        this.UpdateShaderResources(
            delta: 0f
        );
    }
    
    private void UpdateShaderResources(
        float delta
    )
    {
        this.m_timeUntilNextRandomization -= delta * (1 + SpectrumMusicAnalyzer.Intensity);
        if (this.m_timeUntilNextRandomization <= 0f)
        {
            this.RandomizeEffectSlots();
            this.m_timeUntilNextRandomization = EffectBackgroundGeometry.c_randomizeTime;
        }
        
        if (this.m_transitionProgress < 1f)
        {
            this.m_transitionProgress += delta * .5f;
            this.m_material.SetShaderParameter(
                param: $"transition_weight", 
                value: this.m_transitionProgress
            );
        }
        
        var intensity    = SpectrumMusicAnalyzer.Intensity;
        var currentSpeed = this.m_speedScale * (1.0f + intensity * 2.0f);
    
        this.m_gridOffset += delta * currentSpeed;

        this.m_material.SetShaderParameter(
            param: "music_intensity", 
            value: intensity
        );
        this.m_material.SetShaderParameter(
            param: "audio_offset", 
            value: this.m_gridOffset
        );
        this.m_material.SetShaderParameter(
            param: "audio_time", 
            value: this.m_gridOffset
        );
        
        lock (this.m_lock)
        {
            if (this.m_colorInverse is ServiceColorInterpolatorColorMode.Transition)
            {
                this.m_material.SetShaderParameter(
                    param: $"color",
                    value: this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                        colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
                    )
                );
            }
            else
            {
                this.m_material.SetShaderParameter(
                    param: $"color",
                    value: this.m_serviceColorInterpolatorInverse.GetColorByMode(
                        colorMode:      this.m_colorInverse,
                        colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
                    )
                );
            }
            
            if (this.m_colorNormal is ServiceColorInterpolatorColorMode.Transition)
            {
                this.m_material.SetShaderParameter(
                    param: $"alt_color",
                    value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                        colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
                    )
                );
            }
            else
            {
                this.m_material.SetShaderParameter(
                    param: $"alt_color",
                    value: this.m_serviceColorInterpolatorNormal.GetColorByMode(
                        colorMode:      this.m_colorNormal,
                        colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
                    )
                );
            }
        }
    }
}