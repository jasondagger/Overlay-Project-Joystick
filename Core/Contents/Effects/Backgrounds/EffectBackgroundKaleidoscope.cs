
using System;
using Godot;
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundKaleidoscope() :
    ColorRect()
{
    public static EffectBackgroundKaleidoscope Instance { get; private set; } = null;
    
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
        this.SetInstance();
        this.RetrieveResources();
        this.SetShaderMaterial();
    }

    internal void AdjustKaleidoscopeSpeed(
        float intensity
    )
    {
        this.m_currentSpeed = intensity * EffectBackgroundKaleidoscope.c_kaleidoscopeDefaultSpeed;
    }
    
    internal void ResetKaleidoscopeSpeed()
    {
        this.AdjustKaleidoscopeSpeed(
            intensity: 1
        );
    }
    
    private const float                     c_kaleidoscopeDefaultSpeed        = 0.1f;
    private const float                     c_randomizeTime                   = 2f;
    
    private float                           m_currentSpeed                    = EffectBackgroundKaleidoscope.c_kaleidoscopeDefaultSpeed;
    private int[]                           m_effectsCurrent                  = new int[5];
    private int[]                           m_effectsNext                     = new int[5];
    private float                           m_internalTime                    = 0f;
    private ShaderMaterial                  m_material                        = null;
    private ServiceColorInterpolatorInverse m_serviceColorInterpolatorInverse = null;
    private ServiceColorInterpolatorNormal  m_serviceColorInterpolatorNormal  = null;
    private float                           m_timeUntilNextRandomization      = EffectBackgroundKaleidoscope.c_randomizeTime;
    private float                           m_transitionProgress              = 1f;
    
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

    private void SetInstance()
    {
        EffectBackgroundKaleidoscope.Instance = this;
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );

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
            this.m_timeUntilNextRandomization = EffectBackgroundKaleidoscope.c_randomizeTime;
        }
        
        if (this.m_transitionProgress < 1f)
        {
            this.m_transitionProgress += delta * .5f;
            this.m_material.SetShaderParameter(
                param: $"transition_weight", 
                value: this.m_transitionProgress
            );
        }
        
        this.m_internalTime += delta * this.m_currentSpeed * (1 + SpectrumMusicAnalyzer.Intensity);
        this.m_material.SetShaderParameter(
            param: $"internal_time",
            value: this.m_internalTime
        );
        this.m_material.SetShaderParameter(
            param: "music_intensity",
            value: SpectrumMusicAnalyzer.Intensity
        );
        
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
            )
        );
    }
}