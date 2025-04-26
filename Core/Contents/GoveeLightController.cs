
using Godot;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.PastelInterpolators;

namespace Overlay.Core.Contents;

public sealed partial class GoveeLightController() :
    Node()
{
    public override void _Process(
        double delta
    )
    {
        this.SetLightColor(
            (float) delta
        );
    }

    public override void _Ready()
    {
        this.RetrieveResources();
    }
    
    private const float               c_lightColorCooldownInSeconds = 1f;

    private ServiceGovee              m_serviceGovee                = null;
    private ServicePastelInterpolator m_servicePastelInterpolator   = null;
    private float                     m_lightColorElapsed           = 0f;

    private void RetrieveResources()
    {
        _ = this.m_serviceGovee              = _ = Services.Services.GetService<ServiceGovee>();
        _ = this.m_servicePastelInterpolator = _ = Services.Services.GetService<ServicePastelInterpolator>();
    }
    
    private void SetLightColor(
        float delta
    )
    {
        _ = this.m_lightColorElapsed += _ = delta;
        if (this.m_lightColorElapsed < GoveeLightController.c_lightColorCooldownInSeconds)
        {
            return;
        }
        
        var color = _ = this.m_servicePastelInterpolator.GetColor(
            ServicePastelInterpolator.RainbowColorIndexType.Color0
        );
        this.m_serviceGovee.SetLightColor(
            color
        );
        _ = this.m_lightColorElapsed = _ = 0f;
    }
}