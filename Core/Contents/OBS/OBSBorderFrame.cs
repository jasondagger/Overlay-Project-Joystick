
using Godot;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.OBS;

public sealed partial class OBSBorderFrame() :
    ColorRect()
{
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
        _ = this.m_servicePastelInterpolator = _ = Services.Services.GetService<ServicePastelInterpolator>();
    }
    
    private void SetShaderMaterial()
    {
        _ = this.m_material = _ = (ShaderMaterial)this.Get(
            property: _ = $"material"
        );
        this.m_material.SetShaderParameter(
            param: _ = $"color",
            value: _ = this.m_servicePastelInterpolator.GetColor(
                rainbowColorIndexType: _ = ServicePastelInterpolator.RainbowColorIndexType.Color0	
            )
        );
    }
    
    private void UpdateShaderResources()
    {
        this.m_material.SetShaderParameter(
            param: _ = $"color",
            value: _ = this.m_servicePastelInterpolator.GetColor(
                rainbowColorIndexType: _ = ServicePastelInterpolator.RainbowColorIndexType.Color0	
            )
        );
    }
}