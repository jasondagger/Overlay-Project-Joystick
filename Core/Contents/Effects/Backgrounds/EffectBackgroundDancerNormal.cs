
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundDancerNormal() :
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
    
    internal static void AdjustDancerSpeed(
        float intensity
    )
    {
        foreach (var instance in EffectBackgroundDancerNormal.Instances)
        {
            instance.m_speedScale = intensity;
        }
    }
    
    internal static void ResetDancerSpeed()
    {
        EffectBackgroundDancerNormal.AdjustDancerSpeed(
            intensity: 1
        );
    }
    
    private static readonly List<EffectBackgroundDancerNormal> Instances                         = [];
    
    [Export] private ServiceColorInterpolatorColorMode         m_colorModeInverse                = ServiceColorInterpolatorColorMode.BananaShake;
    [Export] private ServiceColorInterpolatorColorMode         m_colorModeNormal                 = ServiceColorInterpolatorColorMode.BananaShake;
    
    private ServiceColorInterpolatorNormal                     m_serviceColorInterpolatorNormal  = null;
    private ServiceColorInterpolatorInverse                    m_serviceColorInterpolatorInverse = null;
    private ShaderMaterial                                     m_material                        = null;
    private float                                              m_speedScale                      = 1.0f;
    private float                                              m_gridOffset                      = 0.0f;

    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal  = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }
    
    private void AddInstance()
    {
        EffectBackgroundDancerNormal.Instances.Add(
            item: this
        );
    }
    
    private void SetShaderMaterial()
    {
        this.Material   = (ShaderMaterial)this.Material.Duplicate();
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByMode(
                colorMode:      this.m_colorModeNormal,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByMode(
                colorMode:      this.m_colorModeInverse,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: "seed", 
            value: this.GetHashCode() % 100
        );
    }
    
    private void UpdateShaderResources(
        float delta
    )
    {
        var intensity = SpectrumMusicAnalyzer.Intensity;

        // Base speed is 1.0. High intensity music makes them move up to 2.5x faster.
        var tempoMultiplier = 1.0f + (intensity * 1.5f * this.m_speedScale);
        this.m_gridOffset += delta * tempoMultiplier;
        
        this.m_material.SetShaderParameter(
            param: "audio_time", 
            value: this.m_gridOffset
        );
        this.m_material.SetShaderParameter(
            param: "music_intensity", 
            value: intensity
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByMode(
                colorMode:      this.m_colorModeNormal,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByMode(
                colorMode:      this.m_colorModeInverse,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
        this.m_material.SetShaderParameter(
            param: "seed", 
            value: this.GetHashCode() % 100
        );
    }
}