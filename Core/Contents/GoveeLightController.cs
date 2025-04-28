
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
        this.HandleLightColorPastel(
            (float) delta
        );
    }

    public override void _Ready()
    {
        this.RetrieveResources();
    }

    internal void SetLightColor(
        ServicePastelInterpolator.ColorType colorType
    )
    {
        _ = this.m_currentColorType = _ = colorType;
        switch (_ = this.m_currentColorType)
        {
            case ServicePastelInterpolator.ColorType.Pastel:
                break;
            
            case ServicePastelInterpolator.ColorType.Red:
            case ServicePastelInterpolator.ColorType.Orange:
            case ServicePastelInterpolator.ColorType.Yellow:
            case ServicePastelInterpolator.ColorType.Lime:
            case ServicePastelInterpolator.ColorType.Green:
            case ServicePastelInterpolator.ColorType.Turquoise:
            case ServicePastelInterpolator.ColorType.Cyan:
            case ServicePastelInterpolator.ColorType.Teal:
            case ServicePastelInterpolator.ColorType.Blue:
            case ServicePastelInterpolator.ColorType.Purple:
            case ServicePastelInterpolator.ColorType.Magenta:
            case ServicePastelInterpolator.ColorType.Pink:
            case ServicePastelInterpolator.ColorType.White:
            default:
                var color = _ = ServicePastelInterpolator.GetColorByColorType(
                    colorType: _ = this.m_currentColorType
                );
                this.m_serviceGovee.SetLightColor(
                    color: _ = color
                );
                _ = this.m_lightColorElapsed = _ = 0f;
                break;
        }
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
    
    private const float                         c_lightColorCooldownInSeconds = 20f;

    private ServiceGovee                        m_serviceGovee                = null;
    private ServicePastelInterpolator           m_servicePastelInterpolator   = null;
    private ServicePastelInterpolator.ColorType m_currentColorType            = _ = ServicePastelInterpolator.ColorType.Pastel;
    private float                               m_lightColorElapsed           = _ = 0f;

    private void RetrieveResources()
    {
        _ = GoveeLightController.Instance    = _ = this;
        
        _ = this.m_serviceGovee              = _ = Services.Services.GetService<ServiceGovee>();
        _ = this.m_servicePastelInterpolator = _ = Services.Services.GetService<ServicePastelInterpolator>();
    }
    
    private void HandleLightColorPastel(
        float delta
    )
    {
        if (_ = this.m_currentColorType is not ServicePastelInterpolator.ColorType.Pastel)
        {
            return;
        }
        
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