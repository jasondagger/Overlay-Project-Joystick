
using System;
using Godot;
using Overlay.Core.Services.PastelInterpolators;

namespace Overlay.Core.Contents.Effects;

public sealed partial class EffectBackgroundKaleidoscope() :
    ColorRect()
{
    public static EffectBackgroundKaleidoscope Instance { get; private set; } = null;
    
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

    internal void AdjustKaleidoscopeSpeed(
        int intensity
    )
    {
        var intensityClamped = Math.Clamp(
            value: intensity, 
            min:   EffectBackgroundKaleidoscope.c_minimumIntensity, 
            max:   EffectBackgroundKaleidoscope.c_maximumIntensity
        );
        var speed = intensityClamped * EffectBackgroundKaleidoscope.c_kaleidoscopeDefaultSpeed;
        
        this.m_material.SetShaderParameter(
            param: $"animate_speed",
            value: speed
        );
    }
    
    internal void ResetKaleidoscopeSpeed()
    {
        this.AdjustKaleidoscopeSpeed(
            intensity: 1
        );
    }
    
    private const int				  c_maximumIntensity          = 20;
    private const int				  c_minimumIntensity          = 1;
    private const float               c_kaleidoscopeDefaultSpeed  = 0.1f;
    
    private ServicePastelInterpolator m_servicePastelInterpolator = null;
    private ShaderMaterial            m_material                  = null;
    
    private void RetrieveResources()
    {
        this.m_servicePastelInterpolator = Services.Services.GetService<ServicePastelInterpolator>();
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