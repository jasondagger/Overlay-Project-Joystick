
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.TeamFortress2s;

namespace Overlay.Core.Contents;

public abstract partial class GoveeLightController<TInterpolator>() :
    Node()
    where TInterpolator : IServiceColorInterpolator
{
    public override void _Ready()
    {
        this.RetrieveResources();
    }
    
    internal void Reset()
    {
        switch (this.m_currentLightMode)
        {
            case LightMode.Color:
                if (
                    this.m_lastColorType != this.m_storedColorType ||
                    this.m_lastSceneName != string.Empty
                )
                {
                    this.SetLightColor(
                        colorType: this.m_storedColorType
                    );
                    GoveeLightControllers.StartColorCycleChange(
                        delayInMilliseconds: this.m_delayInColorCycleInMilliseconds,
                        customDelay:         GoveeLightControllers.GetRemainingTime()
                    );
                }
                break;
            
            case LightMode.Scene:
                if (this.m_lastSceneName != this.m_storedSceneName)
                {
                    this.SetLightScene(
                        sceneName: this.m_storedSceneName
                    );
                    GoveeLightControllers.StartColorCycleChange(
                        delayInMilliseconds: this.m_delayInColorCycleInMilliseconds,
                        customDelay:         GoveeLightControllers.GetRemainingTime()
                    );
                }
                break;
                
            default:
                break;
        }
    }
    
    internal void SetLightColor(
        IServiceColorInterpolatorDefinition.ColorType colorType
    )
    {
        this.SetGoveeLightsColor(
            colorType: colorType
        );

        this.m_lastColorType = colorType;
        this.m_lastSceneName = string.Empty;

        switch (colorType)
        {
            case IServiceColorInterpolatorDefinition.ColorType.Blue:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Blue
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.Cyan:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Cyan
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.Green:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Green
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.Magenta:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Magenta
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.Orange:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Orange
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.Purple:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Purple
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.Red:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Red
                );
                break;
            
            case IServiceColorInterpolatorDefinition.ColorType.White:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.White
                );
                break;
                
            case IServiceColorInterpolatorDefinition.ColorType.Yellow:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Yellow
                );
                break;
        }
    }

    internal void SetLightScene(
        string sceneName
    )
    {
        this.SetGoveeLightsScene(
            sceneName: sceneName
        );
        
        this.m_lastSceneName = sceneName;
        this.m_lastColorType = IServiceColorInterpolatorDefinition.ColorType.Off;

        switch (sceneName)
        {
            case ServiceGoveeSceneNames.BananaShake:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.BananaShake
                );
                break;
            
            case ServiceGoveeSceneNames.BlueRaspberry:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.BlueRaspberry
                );
                break;
            
            case ServiceGoveeSceneNames.CreamsicleBanana:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.CreamsicleBanana
                );
                break;
            
            case ServiceGoveeSceneNames.CreamsicleBlueberry:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.CreamsicleBlueberry
                );
                break;
            
            case ServiceGoveeSceneNames.CreamsicleDragonfruit:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.CreamsicleDragonfruit
                );
                break;
            
            case ServiceGoveeSceneNames.CreamsicleLime:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.CreamsicleLime
                );
                break;
            
            case ServiceGoveeSceneNames.CreamsicleOrange:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.CreamsicleOrange
                );
                break;
            
            case ServiceGoveeSceneNames.CreamsicleStrawberry:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.CreamsicleStrawberry
                );
                break;
            
            case ServiceGoveeSceneNames.Cyberpunk:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Cyberpunk
                );
                break;
            
            case ServiceGoveeSceneNames.Dinner:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Dinner
                );
                break;
            
            case ServiceGoveeSceneNames.ForestSunset:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.ForestSunset
                );
                break;
            
            case ServiceGoveeSceneNames.Heatwave:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Heatwave
                );
                break;
            
            case ServiceGoveeSceneNames.Icy:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Icy
                );
                break;
            
            case ServiceGoveeSceneNames.OrangePurple:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.OrangePurple
                );
                break;
            
            case ServiceGoveeSceneNames.PoweradeSlushie:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.PoweradeSlushie
                );
                break;
            
            case ServiceGoveeSceneNames.Rainbow:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Rainbow
                );
                break;
            
            case ServiceGoveeSceneNames.RedWhiteBlue:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.RedWhiteBlue
                );
                break;
            
            case ServiceGoveeSceneNames.ShowinSomeLove:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.ShowinSomeLove
                );
                break;
            
            case ServiceGoveeSceneNames.TF2KillStreak5:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.TeamFortress2KillStreak5
                );
                break;
            
            case ServiceGoveeSceneNames.TF2KillStreak10:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.TeamFortress2KillStreak10
                );
                break;
            
            case ServiceGoveeSceneNames.TF2KillStreak15:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.TeamFortress2KillStreak15
                );
                break;
            
            case ServiceGoveeSceneNames.TF2KillStreak20:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20
                );
                break;
            
            case ServiceGoveeSceneNames.TokeUp:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.TokeUp
                );
                break;
            
            case ServiceGoveeSceneNames.Toxic:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Toxic
                );
                break;
            
            case ServiceGoveeSceneNames.Vaporwave:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Vaporwave
                );
                break;
            
            case ServiceGoveeSceneNames.Watermelon:
                this.m_serviceColorInterpolator.SetColorMode(
                    serviceColorInterpolatorColorMode: ServiceColorInterpolatorColorMode.Watermelon
                );
                break;
        }
    }
    
    internal void SetAndStoreLightColor(
        IServiceColorInterpolatorDefinition.ColorType colorType,
        int                                           delayInMilliseconds    = GoveeLightControllers.DelayInColorCycleLongInMilliseconds,
        bool                                          ignoreColorChangeStart = false
    )
    {
        if (
            this.m_serviceTeamFortress2.IsBhopping is false &&
            this.m_serviceTeamFortress2.IsKillStreaking is false
        )
        {
            this.SetLightColor(
                colorType: colorType
            );
        }

        this.StoreColorType(
            colorType: colorType
        );
        this.StoreDelayInColorCycleInMilliseconds(
            delayInMilliseconds: delayInMilliseconds
        );
        
        if (ignoreColorChangeStart is false)
        {
            GoveeLightControllers.StartColorCycleChange(
                delayInMilliseconds: this.m_delayInColorCycleInMilliseconds
            );
        }
    }
    
    internal void SetAndStoreLightScene(
        string sceneName,
        int    delayInMilliseconds    = GoveeLightControllers.DelayInColorCycleLongInMilliseconds,
        bool   ignoreColorChangeStart = false
    )
    {
        if (
            this.m_serviceTeamFortress2.IsBhopping is false &&
            this.m_serviceTeamFortress2.IsKillStreaking is false
        )
        {
            this.SetLightScene(
                sceneName: sceneName
            );
        }

        this.StoreSceneName(
            sceneName: sceneName
        );
        this.StoreDelayInColorCycleInMilliseconds(
            delayInMilliseconds: delayInMilliseconds
        );

        if (ignoreColorChangeStart is false)
        {
            GoveeLightControllers.StartColorCycleChange(
                delayInMilliseconds: this.m_delayInColorCycleInMilliseconds
            );
        }
    }
    
    internal void StoreColorType(
        IServiceColorInterpolatorDefinition.ColorType colorType
    )
    {
        this.m_storedColorType  = colorType;
        this.m_storedSceneName  = string.Empty;
        this.m_currentLightMode = LightMode.Color;
    }
    
    internal void StoreDelayInColorCycleInMilliseconds(
        int delayInMilliseconds
    )
    {
        this.m_delayInColorCycleInMilliseconds = delayInMilliseconds;
    }
    
    internal void StoreSceneName(
        string sceneName
    )
    {
        this.m_storedColorType  = IServiceColorInterpolatorDefinition.ColorType.Off;
        this.m_storedSceneName  = sceneName;
        this.m_currentLightMode = LightMode.Scene;
    }

    internal void TurnOffLights()
    {
        this.m_serviceGovee.TurnOffLights();
        this.m_storedColorType = IServiceColorInterpolatorDefinition.ColorType.White;
    }
    
    internal void TurnOnLights()
    {
        this.SetLightColor(
            colorType: IServiceColorInterpolatorDefinition.ColorType.White
        );
        this.m_storedColorType  = IServiceColorInterpolatorDefinition.ColorType.White;
        this.m_currentLightMode = LightMode.Color;
    }
    
    protected IServiceColorInterpolator                   m_serviceColorInterpolator        = null;
    protected ServiceGovee                                m_serviceGovee                    = null;

    private enum LightMode
    {
        Scene = 0,
        Color = 1
    }
    
    private LightMode                                     m_currentLightMode                = LightMode.Scene;
    private int                                           m_delayInColorCycleInMilliseconds = GoveeLightControllers.DelayInColorCycleInShortMilliseconds;
    private IServiceColorInterpolatorDefinition.ColorType m_lastColorType                   = IServiceColorInterpolatorDefinition.ColorType.Off;
    private string                                        m_lastSceneName                   = string.Empty;
    private ServiceTeamFortress2                          m_serviceTeamFortress2            = null;
    private IServiceColorInterpolatorDefinition.ColorType m_storedColorType                 = IServiceColorInterpolatorDefinition.ColorType.Off;
    private string                                        m_storedSceneName                 = string.Empty;
    
    protected virtual void RetrieveResources()
    {
        this.m_serviceGovee         = Services.Services.GetService<ServiceGovee>();
        this.m_serviceTeamFortress2 = Services.Services.GetService<ServiceTeamFortress2>();
    }

    protected abstract void SetGoveeLightsColor(
        IServiceColorInterpolatorDefinition.ColorType colorType
    );

    protected abstract void SetGoveeLightsScene(
        string sceneName
    );
}