
using Godot;
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundStars() :
    ColorRect()
{
    public static EffectBackgroundStars Instance { get; private set; } = null;
    
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
        this.SetShaderMaterial();
        this.SetInstance();
    }
    
    internal void AdjustStarsSpeed(
        float intensity
    )
    {
        this.m_speedScale = intensity;
    }
    
    internal void ResetStarsSpeed()
    {
        this.AdjustStarsSpeed(
            intensity: 1f
        );
    }
    
    private float                    m_currentSpeed             = 1.0f;
    private ShaderMaterial           m_material                 = null;
    private float                    m_phase                    = 0.0f;
    private float                    m_phaseSlow                = 0.0f;
    private float                    m_speedScale               = 1f;
    
    private void SetInstance()
    {
        EffectBackgroundStars.Instance = this;
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
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
        this.m_phaseSlow += delta * this.m_currentSpeed * 0.5f;

        this.m_material.SetShaderParameter(
            param: "at_time", 
            value: this.m_phase
        );
        
        this.m_material.SetShaderParameter(
            param: "at_time_slow", 
            value: this.m_phaseSlow
        );
        
        this.m_material.SetShaderParameter(
            param: "drift_offset", 
            value: this.m_phaseSlow * 0.05f
        );
    }
}