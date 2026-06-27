
using Godot;
using Overlay.Core.Services.ColorInterpolators;

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
    
    private ServiceColorInterpolatorNormal m_serviceColorInterpolatorNormal = null;
    private ShaderMaterial                 m_material                       = null;
    private readonly Color                 m_color                          = new(
        code: "F2F2F2"
    );
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal = Services.Services.GetService<ServiceColorInterpolatorNormal>();
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_color
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_color
        );
    }
    
    private void UpdateShaderResources()
    {
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_color
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_color
        );
    }
}