
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents;

public sealed partial class GoveeLightControllerCeiling() :
    GoveeLightController<ServiceColorInterpolatorNormal>
{
    internal static GoveeLightControllerCeiling Instance { get; private set; }
    
    protected override void RetrieveResources()
    {
        GoveeLightControllerCeiling.Instance = this;
        this.m_serviceColorInterpolator      = Services.Services.GetService<ServiceColorInterpolatorInverse>();
        
        base.RetrieveResources();
    }

    protected override void SetGoveeLightsColor(
        IServiceColorInterpolatorDefinition.ColorType colorType
    )
    {
        var color = ServiceColorInterpolatorNormal.GetColorCodeByColorType(
            colorType: colorType
        );
        this.m_serviceGovee.SetLightColorForCeilingLights(
            color: color
        );
    }

    protected override void SetGoveeLightsScene(
        string sceneName
    )
    {
        this.m_serviceGovee.SetLightSceneForCeilingLights(
            sceneName: sceneName
        );
    }
}