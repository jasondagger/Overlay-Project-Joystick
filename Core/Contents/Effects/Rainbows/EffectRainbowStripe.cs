using Godot;
using Overlay.Core.Services.PastelInterpolators;

namespace Overlay.Core.Contents.Effects;

public partial class EffectRainbowStripe() :
    ColorRect()
{
    [Export] public ServicePastelInterpolator.RainbowColorIndexType RainbowColorIndex = ServicePastelInterpolator.RainbowColorIndexType.Color0;
    
    public override void _Process(
        double delta
    )
    {
        this.UpdateShaderResources();
    }
    
    public override void _Ready()
    {
        this.RetrieveResources();
        this.SetShaderMaterial();
    }
    
    private ServicePastelInterpolator m_servicePastelInterpolator = null;
    private ShaderMaterial            m_material                  = null;
    
    private void RetrieveResources()
    {
        this.m_servicePastelInterpolator = Services.Services.GetService<ServicePastelInterpolator>();
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_servicePastelInterpolator.GetColor(
                rainbowColorIndexType: this.RainbowColorIndex
            )
        );
    }
    
    private void UpdateShaderResources()
    {
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_servicePastelInterpolator.GetColor(
                rainbowColorIndexType: this.RainbowColorIndex
            )
        );
    }
}