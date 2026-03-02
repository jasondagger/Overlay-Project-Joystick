
using System;
using Godot;
using Overlay.Core.Services.PastelInterpolators;

namespace Overlay.Core.Contents.Effects;

public sealed partial class EffectBackgroundVaporwave() :
    ColorRect()
{
    public static EffectBackgroundVaporwave Instance { get; private set; } = null;
    
    public override void _Process(
        double delta
    )
    {
        this.UpdateShaderResources();
    }
    
    public override void _Ready()
    {
        this.SetInstance();
        this.RetrieveResources();
        this.SetShaderMaterial();
    }
    
    internal void AdjustVaporwaveSpeed(
        int intensity
    )
    {
        var intensityClamped = Math.Clamp(
            value: intensity, 
            min:   EffectBackgroundVaporwave.c_minimumIntensity, 
            max:   EffectBackgroundVaporwave.c_maximumIntensity
        );

        this.m_material.SetShaderParameter(
            param: $"speed_scale",
            value: intensityClamped
        );
    }
    
    internal void ResetVaporwaveSpeed()
    {
        this.AdjustVaporwaveSpeed(
            intensity: 1
        );
    }
    
    private const int				  c_maximumIntensity          = 20;
    private const int				  c_minimumIntensity          = 1;
    
    private ServicePastelInterpolator m_servicePastelInterpolator = null;
    private ShaderMaterial            m_material                  = null;
    
    private void RetrieveResources()
    {
        this.m_servicePastelInterpolator = Services.Services.GetService<ServicePastelInterpolator>();
    }
    
    private void SetInstance()
    {
        EffectBackgroundVaporwave.Instance = this;
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_servicePastelInterpolator.GetColor(
                rainbowColorIndexType: ServicePastelInterpolator.RainbowColorIndexType.Color0	
            )
        );
    }
    
    private void UpdateShaderResources()
    {
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_servicePastelInterpolator.GetColor(
                rainbowColorIndexType: ServicePastelInterpolator.RainbowColorIndexType.Color0	
            )
        );
    }
}