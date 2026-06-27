
using Godot;
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundGodRays() :
    ColorRect()
{
    public static EffectBackgroundGodRays Instance { get; private set; } = null;
    
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
        this.RetrieveResources();
        this.SetShaderMaterial();
        this.SetInstance();
    }
    
    internal void AdjustGodRaysSpeed(
        float intensity
    )
    {
        this.m_speedScale = intensity;
    }
    
    internal void ResetGodRaysSpeed()
    {
        this.AdjustGodRaysSpeed(
            intensity: 1f
        );
    }
    
    private float                          m_currentSpeed             = 1.0f;
    private ShaderMaterial                 m_material                 = null;
    private float                          m_phase                    = 0.0f;
    private ServiceColorInterpolatorNormal m_serviceColorInterpolatorNormal = null;
    private float                          m_speedScale               = 1f;
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal = Services.Services.GetService<ServiceColorInterpolatorNormal>();
    }
    
    private void SetInstance()
    {
        EffectBackgroundGodRays.Instance = this;
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
    }
    
    private void UpdateShaderResources(
        float delta
    )
    {
        var targetIntensity = SpectrumMusicAnalyzer.Intensity;
        var targetSpeed     = this.m_speedScale * (1.0f + targetIntensity * 2.0f);
        this.m_currentSpeed = Mathf.MoveToward(
            from:   this.m_currentSpeed, 
            to:     targetSpeed, 
            delta:  delta
        );
        
        this.m_phase += delta * this.m_currentSpeed;

        this.m_material.SetShaderParameter(
            param: "at_time", 
            value: this.m_phase
        );
        
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            )
        );
    }
}