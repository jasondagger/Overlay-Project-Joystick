
using Godot;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.PastelInterpolators;

namespace Overlay.Core.Contents;

public sealed partial class GoveeLightController() :
    Node()
{
    public override void _Ready()
    {
        this.RetrieveResources();
    }

    internal void SetLightColor(
        ServicePastelInterpolator.ColorType colorType
    )
    {
        _ = this.m_currentColorType = _ = colorType;
        var color = _ = ServicePastelInterpolator.GetColorByColorType(
            colorType: _ = this.m_currentColorType
        );
        this.m_serviceGovee.SetLightColor(
            color: _ = color
        );
        _ = this.m_lightColorElapsed = _ = 0f;
    }

    internal void SetLightScene(
        string sceneName
    )
    {
        this.m_serviceGovee.SetLightScene(
            sceneName: _ = sceneName
        );
    }

    internal void TurnOffLights()
    {
        _ = this.m_lightColorElapsed = _ = 0f;
        _ = this.m_currentColorType  = _ = ServicePastelInterpolator.ColorType.White;

        this.m_serviceGovee.TurnOffLights();
    }
    
    internal void TurnOnLights()
    {
        _ = this.m_lightColorElapsed = _ = 0f;
        _ = this.m_currentColorType  = _ = ServicePastelInterpolator.ColorType.White;
        
        var color = _ = ServicePastelInterpolator.GetColorByColorType(
            colorType: _ = this.m_currentColorType
        );
        this.m_serviceGovee.SetLightColor(
            color: _ = color
        );
    }
    
    internal static GoveeLightController        Instance                      { get; private set; }
    
    private ServiceGovee                        m_serviceGovee                = null;
    private ServicePastelInterpolator.ColorType m_currentColorType            = _ = ServicePastelInterpolator.ColorType.White;
    private float                               m_lightColorElapsed           = _ = 0f;

    private void RetrieveResources()
    {
        _ = GoveeLightController.Instance = _ = this;
        _ = this.m_serviceGovee           = _ = Services.Services.GetService<ServiceGovee>();
    }
}