
using System;
using Godot;
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Effects;

public sealed partial class EffectBackgroundConfetti() :
    ColorRect()
{
    public static EffectBackgroundConfetti Instance { get; private set; } = null;
    
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

    internal void AdjustConfettiSpeed(
        float intensity
    )
    {
        this.m_speedScale = intensity;
    }
    
    internal void ResetConfettiSpeed()
    {
        this.AdjustConfettiSpeed(
            intensity: 1
        );
    }
    
    private ServiceColorInterpolatorNormal  m_serviceColorInterpolatorNormal  = null;
    private ServiceColorInterpolatorInverse m_serviceColorInterpolatorInverse = null;
    private ShaderMaterial                  m_material                        = null;
    private float                           m_speedScale                      = 1.0f;
    private float                           m_audioOffset                     = 0.0f;
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal  = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }

    private void SetInstance()
    {
        EffectBackgroundConfetti.Instance = this;
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"confetti_color",
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
    
    private void UpdateShaderResources(
        float delta
    )
    {
        var intensity    = SpectrumMusicAnalyzer.Intensity;
        var currentSpeed = this.m_speedScale * (1.0f + intensity * 2.0f);
    
        this.m_audioOffset += delta * currentSpeed;
        this.m_material.SetShaderParameter(
            param: $"audio_offset",
            value: this.m_audioOffset
        );
        
        this.m_material.SetShaderParameter(
            param: $"confetti_color",
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