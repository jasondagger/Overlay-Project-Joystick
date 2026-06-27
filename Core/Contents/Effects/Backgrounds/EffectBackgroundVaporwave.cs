
using System;
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundVaporwave() :
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
    
    internal static void AdjustVaporwaveSpeed(
        float intensity
    )
    {
        foreach (var instance in EffectBackgroundVaporwave.Instances)
        {
            instance.m_speedScale = intensity;
        }
    }
    
    internal static void ResetVaporwaveSpeed()
    {
        EffectBackgroundVaporwave.AdjustVaporwaveSpeed(
            intensity: 1
        );
    }
    
    private const float                                     c_randomizeTime                   = 2f;
    
    private static readonly List<EffectBackgroundVaporwave> Instances                         = [];
    
    private int[]                                           m_effectsCurrent                  = new int[5];
    private int[]                                           m_effectsNext                     = new int[5];
    private float                                           m_gridOffset                      = 0f;
    private ShaderMaterial                                  m_material                        = null;
    private ServiceColorInterpolatorInverse                 m_serviceColorInterpolatorInverse = null;
    private ServiceColorInterpolatorNormal                  m_serviceColorInterpolatorNormal  = null;
    private float                                           m_speedScale                      = 1f;
    private float                                           m_timeUntilNextRandomization      = EffectBackgroundVaporwave.c_randomizeTime;
    private float                                           m_transitionProgress              = 1f;
    
    private void RandomizeEffectSlots()
        {
            System.Array.Copy(
                sourceArray:      this.m_effectsNext, 
                destinationArray: this.m_effectsCurrent, 
                length:           5
            );
            
            var randomIndex = GD.RandRange(
                from: 0,
                to:   4
            );
        
            this.m_effectsNext[randomIndex] = GD.RandRange(
                from: 0,
                to:   49
            );
            
            this.m_material.SetShaderParameter(
                param: $"current_slot_{randomIndex}", 
                value: this.m_effectsCurrent[randomIndex]
            );
            this.m_material.SetShaderParameter(
                param: $"next_slot_{randomIndex}", 
                value: this.m_effectsNext[randomIndex]
            );
            
            this.m_transitionProgress = 0f;
        }
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal  = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }
    
    private void AddInstance()
    {
        EffectBackgroundVaporwave.Instances.Add(
            item: this
        );
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
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
            this.m_timeUntilNextRandomization = EffectBackgroundVaporwave.c_randomizeTime;
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
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
    }
}