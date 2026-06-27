
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents;

public sealed partial class GoveeLightControllerStanding() :
    GoveeLightController<ServiceColorInterpolatorNormal>
{
    internal static GoveeLightControllerStanding Instance { get; private set; }
    
    protected override void RetrieveResources()
    {
        GoveeLightControllerStanding.Instance = this;
        this.m_serviceColorInterpolator       = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        
        base.RetrieveResources();
    }

    protected override void SetGoveeLightsColor(
        IServiceColorInterpolatorDefinition.ColorType colorType
    )
    {
        var color = ServiceColorInterpolatorNormal.GetColorCodeByColorType(
            colorType: colorType
        );
        this.m_serviceGovee.SetLightColorForStandingLights(
            color: color
        );
    }

    protected override void SetGoveeLightsScene(
        string sceneName
    )
    {
        this.m_serviceGovee.SetLightSceneForStandingLights(
            sceneName: sceneName
        );
    }
}