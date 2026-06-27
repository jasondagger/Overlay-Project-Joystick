
using Godot;
using Overlay.Core.Contents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Services.ColorInterpolators;

public abstract partial class ServiceColorInterpolator<TDefinition>() :
    IServiceColorInterpolator 
    where TDefinition : IServiceColorInterpolatorDefinition
{
    Task IService.Setup()
    {
        this.StartColorInterpolation();
        return Task.CompletedTask;
    }

    Task IService.Start()
    {
        return Task.CompletedTask;
    }

    Task IService.Stop()
    {
        this.Shutdown();
        return Task.CompletedTask;
    }
    
    public void SetColorMode(
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            var colorTypesTarget = ServiceColorInterpolator<TDefinition>.GetColorTypesByColorMode(
                serviceColorInterpolatorColorMode: serviceColorInterpolatorColorMode
            );
        
            ServiceColorInterpolator<TDefinition>.UpdateColorTransitionCodes(
                colorTypes: colorTypesTarget
            );
            this.UpdateColorTransitionDatas();
        
            this.m_serviceColorInterpolatorColorMode       = ServiceColorInterpolatorColorMode.Transition;
            this.m_serviceColorInterpolatorColorModeTarget = serviceColorInterpolatorColorMode;
        }
    }
    
    internal void AdjustColorInterpolationSpeedByLovenseIntensity(
        float intensity
    )
    {
        lock (this.m_lock)
        {
            this.m_lovenseIntensity = intensity;
        }
    }
    
    internal Color GetColorByCurrentMode(
        IServiceColorInterpolatorDefinition.ColorIndexType colorIndexType
    )
    {
        lock (this.m_lock)
        {
            return this.m_serviceColorInterpolatorColorMode switch
            {
                ServiceColorInterpolatorColorMode.BananaShake               => this.m_colorDatasBananaShake               [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Blue                      => this.m_colorDatasBlue                      [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.BlueRaspberry             => this.m_colorDatasBlueRaspberry             [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleBanana          => this.m_colorDatasCreamsicleBanana          [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleBlueberry       => this.m_colorDatasCreamsicleBlueberry       [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleDragonfruit     => this.m_colorDatasCreamsicleDragonfruit     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleLime            => this.m_colorDatasCreamsicleLime            [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleOrange          => this.m_colorDatasCreamsicleOrange          [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleStrawberry      => this.m_colorDatasCreamsicleStrawberry      [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Cyan                      => this.m_colorDatasCyan                      [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Cyberpunk                 => this.m_colorDatasCyberpunk                 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Dinner                    => this.m_colorDatasDinner                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.ForestSunset              => this.m_colorDatasForestSunset              [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Green                     => this.m_colorDatasGreen                     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Heatwave                  => this.m_colorDatasHeatwave                  [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Icy                       => this.m_colorDatasIcy                       [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Magenta                   => this.m_colorDatasMagenta                   [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Orange                    => this.m_colorDatasOrange                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.OrangePurple              => this.m_colorDatasOrangePurple              [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.PoweradeSlushie           => this.m_colorDatasPoweradeSlushie           [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Purple                    => this.m_colorDatasPurple                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Rainbow                   => this.m_colorDatasRainbow                   [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Red                       => this.m_colorDatasRed                       [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.RedWhiteBlue              => this.m_colorDatasRedWhiteBlue              [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.ShowinSomeLove            => this.m_colorDatasShowinSomeLove            [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak5  => this.m_colorDatasTeamFortress2KillStreak5  [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak10 => this.m_colorDatasTeamFortress2KillStreak10 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak15 => this.m_colorDatasTeamFortress2KillStreak15 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20 => this.m_colorDatasTeamFortress2KillStreak20 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TokeUp                    => this.m_colorDatasTokeUp                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Toxic                     => this.m_colorDatasToxic                     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Transition                => this.m_colorDatasTransition                [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Vaporwave                 => this.m_colorDatasVaporwave                 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Watermelon                => this.m_colorDatasWatermelon                [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.White                     => this.m_colorDatasWhite                     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Yellow                    => this.m_colorDatasYellow                    [key: colorIndexType].Current,
                _                                                           => throw new NotImplementedException()
            };
        }
    }

    internal string GetColorByCurrentModeAsHex(
        IServiceColorInterpolatorDefinition.ColorIndexType colorIndexType
    )
    {
        return this.GetColorByCurrentMode(
            colorIndexType: colorIndexType    
        ).ToHtml();
    }
    
    internal static Color GetColorCodeByColorType(
        IServiceColorInterpolatorDefinition.ColorType colorType
    )
    {
        return ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorType];
    }
    
    internal Color GetColorByMode(
        ServiceColorInterpolatorColorMode                  colorMode,
        IServiceColorInterpolatorDefinition.ColorIndexType colorIndexType
    )
    {
        lock (this.m_lock)
        {
            return colorMode switch
            {
                ServiceColorInterpolatorColorMode.BananaShake               => this.m_colorDatasBananaShake               [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Blue                      => this.m_colorDatasBlue                      [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.BlueRaspberry             => this.m_colorDatasBlueRaspberry             [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleBanana          => this.m_colorDatasCreamsicleBanana          [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleBlueberry       => this.m_colorDatasCreamsicleBlueberry       [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleDragonfruit     => this.m_colorDatasCreamsicleDragonfruit     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleLime            => this.m_colorDatasCreamsicleLime            [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleOrange          => this.m_colorDatasCreamsicleOrange          [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.CreamsicleStrawberry      => this.m_colorDatasCreamsicleStrawberry      [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Cyan                      => this.m_colorDatasCyan                      [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Cyberpunk                 => this.m_colorDatasCyberpunk                 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Dinner                    => this.m_colorDatasDinner                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.ForestSunset              => this.m_colorDatasForestSunset              [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Green                     => this.m_colorDatasGreen                     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Heatwave                  => this.m_colorDatasHeatwave                  [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Icy                       => this.m_colorDatasIcy                       [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Magenta                   => this.m_colorDatasMagenta                   [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Orange                    => this.m_colorDatasOrange                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.OrangePurple              => this.m_colorDatasOrangePurple              [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.PoweradeSlushie           => this.m_colorDatasPoweradeSlushie           [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Purple                    => this.m_colorDatasPurple                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Rainbow                   => this.m_colorDatasRainbow                   [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Red                       => this.m_colorDatasRed                       [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.RedWhiteBlue              => this.m_colorDatasRedWhiteBlue              [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.ShowinSomeLove            => this.m_colorDatasShowinSomeLove            [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak5  => this.m_colorDatasTeamFortress2KillStreak5  [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak10 => this.m_colorDatasTeamFortress2KillStreak10 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak15 => this.m_colorDatasTeamFortress2KillStreak15 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20 => this.m_colorDatasTeamFortress2KillStreak20 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.TokeUp                    => this.m_colorDatasTokeUp                    [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Toxic                     => this.m_colorDatasToxic                     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Transition                => this.m_colorDatasTransition                [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Vaporwave                 => this.m_colorDatasVaporwave                 [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Watermelon                => this.m_colorDatasWatermelon                [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.White                     => this.m_colorDatasWhite                     [key: colorIndexType].Current,
                ServiceColorInterpolatorColorMode.Yellow                    => this.m_colorDatasYellow                    [key: colorIndexType].Current,
                _                                                           => throw new NotImplementedException()
            };
        }
    }
    
    internal string GetColorByModeAsHex(
        ServiceColorInterpolatorColorMode                  colorMode,
        IServiceColorInterpolatorDefinition.ColorIndexType colorIndexType
    )
    {
        return this.GetColorByMode(
            colorMode:      colorMode,
            colorIndexType: colorIndexType    
        ).ToHtml();
    }

    internal Color GetColorForBorder()
    {
        lock (this.m_lock)
        {
            return this.m_colorDatasBorder[key: IServiceColorInterpolatorDefinition.ColorIndexType.Color0].Current;
        }
    }
    
    internal void ResetColorInterpolationSpeed()
    {
        this.AdjustColorInterpolationSpeedByLovenseIntensity(
            intensity: 1
        );
    }
    
    private const float                         c_colorInterpolationRateMinimum                  = 0.49f;
    private const float                         c_colorInterpolationRateMaximum                  = 0.51f;
    private const float                         c_colorTransitionSpeed                           = 1f;
    
    private static Dictionary<IServiceColorInterpolatorDefinition.ColorType, Color> s_colorCodes => TDefinition.ColorCodes;
    
    private ServiceColorInterpolatorColorMode   m_serviceColorInterpolatorColorMode              = ServiceColorInterpolatorColorMode.Blue;
    private ServiceColorInterpolatorColorMode   m_serviceColorInterpolatorColorModeTarget        = ServiceColorInterpolatorColorMode.Blue;
    private readonly object                     m_lock                                           = new();
    private float                               m_lovenseIntensity                               = 1f;
    private readonly Random                     m_random                                         = new();
    private bool                                m_shutdownRequested                              = false;

    private static IServiceColorInterpolatorDefinition.ColorType[] GetColorTypesByColorMode(
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        var colorTypes = new IServiceColorInterpolatorDefinition.ColorType[6];
        switch (serviceColorInterpolatorColorMode)
        {
            case ServiceColorInterpolatorColorMode.BananaShake:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.BananaShake0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.BananaShake1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.BananaShake2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.BananaShake3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.BananaShake4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.BananaShake5;
                break;
            
            case ServiceColorInterpolatorColorMode.Blue:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Blue0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Blue1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Blue2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Blue3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Blue4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Blue5;
                break;
            
            case ServiceColorInterpolatorColorMode.BlueRaspberry:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry5;
                break;
            
            case ServiceColorInterpolatorColorMode.CreamsicleBanana:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana5;
                break;
            
            case ServiceColorInterpolatorColorMode.CreamsicleBlueberry:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry5;
                break;
            
            case ServiceColorInterpolatorColorMode.CreamsicleDragonfruit:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit5;
                break;
            
            case ServiceColorInterpolatorColorMode.CreamsicleLime:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime5;
                break;
            
            case ServiceColorInterpolatorColorMode.CreamsicleOrange:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange5;
                break;
            
            case ServiceColorInterpolatorColorMode.CreamsicleStrawberry:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry5;
                break;
            
            case ServiceColorInterpolatorColorMode.Cyan:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Cyan0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Cyan1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Cyan2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Cyan3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Cyan4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Cyan5;
                break;
            
            case ServiceColorInterpolatorColorMode.Cyberpunk:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Cyberpunk0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Cyberpunk1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Cyberpunk2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Cyberpunk3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Cyberpunk4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Cyberpunk5;
                break;
            
            case ServiceColorInterpolatorColorMode.Dinner:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Dinner0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Dinner1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Dinner2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Dinner3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Dinner4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Dinner5;
                break;
            
            case ServiceColorInterpolatorColorMode.ForestSunset:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.ForestSunset0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.ForestSunset1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.ForestSunset2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.ForestSunset3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.ForestSunset4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.ForestSunset5;
                break;

            case ServiceColorInterpolatorColorMode.Green:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Green0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Green1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Green2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Green3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Green4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Green5;
                break;

            case ServiceColorInterpolatorColorMode.Heatwave:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Heatwave0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Heatwave1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Heatwave2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Heatwave3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Heatwave4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Heatwave5;
                break;

            case ServiceColorInterpolatorColorMode.Icy:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Icy0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Icy1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Icy2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Icy3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Icy4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Icy5;
                break;

            case ServiceColorInterpolatorColorMode.Magenta:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Magenta0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Magenta1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Magenta2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Magenta3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Magenta4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Magenta5;
                break;
            
            case ServiceColorInterpolatorColorMode.Orange:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Orange0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Orange1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Orange2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Orange3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Orange4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Orange5;
                break;
            
            case ServiceColorInterpolatorColorMode.OrangePurple:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.OrangePurple0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.OrangePurple1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.OrangePurple2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.OrangePurple3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.OrangePurple4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.OrangePurple5;
                break;

            case ServiceColorInterpolatorColorMode.PoweradeSlushie:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie5;
                break;
            
            case ServiceColorInterpolatorColorMode.Purple:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Purple0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Purple1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Purple2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Purple3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Purple4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Purple5;
                break;

            case ServiceColorInterpolatorColorMode.Rainbow:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Red;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Yellow;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Green;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Cyan;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Blue;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Magenta;
                break;

            case ServiceColorInterpolatorColorMode.Red:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Red0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Red1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Red2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Red3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Red4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Red5;
                break;
            
            case ServiceColorInterpolatorColorMode.RedWhiteBlue:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue5;
                break;
            
            case ServiceColorInterpolatorColorMode.ShowinSomeLove:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove5;
                break;

            case ServiceColorInterpolatorColorMode.TeamFortress2KillStreak5:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_5;
                break;

            case ServiceColorInterpolatorColorMode.TeamFortress2KillStreak10:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_5;
                break;

            case ServiceColorInterpolatorColorMode.TeamFortress2KillStreak15:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_5;
                break;

            case ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_5;
                break;
            
            case ServiceColorInterpolatorColorMode.TokeUp:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.TokeUp0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.TokeUp1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.TokeUp2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.TokeUp3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.TokeUp4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.TokeUp5;
                break;

            case ServiceColorInterpolatorColorMode.Toxic:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Toxic0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Toxic1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Toxic2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Toxic3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Toxic4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Toxic5;
                break;
            
            case ServiceColorInterpolatorColorMode.Transition:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Transition0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Transition1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Transition2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Transition3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Transition4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Transition5;
                break;
            
            case ServiceColorInterpolatorColorMode.Vaporwave:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Vaporwave0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Vaporwave1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Vaporwave2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Vaporwave3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Vaporwave4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Vaporwave5;
                break;
            
            case ServiceColorInterpolatorColorMode.Watermelon:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Watermelon0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Watermelon1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Watermelon2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Watermelon3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Watermelon4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Watermelon5;
                break;
            
            case ServiceColorInterpolatorColorMode.White:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.White0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.White1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.White2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.White3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.White4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.White5;
                break;

            case ServiceColorInterpolatorColorMode.Yellow:
                colorTypes[0] = IServiceColorInterpolatorDefinition.ColorType.Yellow0;
                colorTypes[1] = IServiceColorInterpolatorDefinition.ColorType.Yellow1;
                colorTypes[2] = IServiceColorInterpolatorDefinition.ColorType.Yellow2;
                colorTypes[3] = IServiceColorInterpolatorDefinition.ColorType.Yellow3;
                colorTypes[4] = IServiceColorInterpolatorDefinition.ColorType.Yellow4;
                colorTypes[5] = IServiceColorInterpolatorDefinition.ColorType.Yellow5;
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(serviceColorInterpolatorColorMode), serviceColorInterpolatorColorMode, null);
        }

        return colorTypes;
    }

    private void FinalizeTransition()
    {
        var targetDatas = this.m_serviceColorInterpolatorColorModeTarget switch {
            ServiceColorInterpolatorColorMode.BananaShake               => this.m_colorDatasBananaShake,
            ServiceColorInterpolatorColorMode.Blue                      => this.m_colorDatasBlue,
            ServiceColorInterpolatorColorMode.BlueRaspberry             => this.m_colorDatasBlueRaspberry,
            ServiceColorInterpolatorColorMode.CreamsicleBanana          => this.m_colorDatasCreamsicleBanana,
            ServiceColorInterpolatorColorMode.CreamsicleBlueberry       => this.m_colorDatasCreamsicleBlueberry,
            ServiceColorInterpolatorColorMode.CreamsicleDragonfruit     => this.m_colorDatasCreamsicleDragonfruit,
            ServiceColorInterpolatorColorMode.CreamsicleLime            => this.m_colorDatasCreamsicleLime,
            ServiceColorInterpolatorColorMode.CreamsicleOrange          => this.m_colorDatasCreamsicleOrange,
            ServiceColorInterpolatorColorMode.CreamsicleStrawberry      => this.m_colorDatasCreamsicleStrawberry,
            ServiceColorInterpolatorColorMode.Cyan                      => this.m_colorDatasCyan,
            ServiceColorInterpolatorColorMode.Cyberpunk                 => this.m_colorDatasCyberpunk,
            ServiceColorInterpolatorColorMode.Dinner                    => this.m_colorDatasDinner,
            ServiceColorInterpolatorColorMode.ForestSunset              => this.m_colorDatasForestSunset,
            ServiceColorInterpolatorColorMode.Green                     => this.m_colorDatasGreen,
            ServiceColorInterpolatorColorMode.Heatwave                  => this.m_colorDatasHeatwave,
            ServiceColorInterpolatorColorMode.Icy                       => this.m_colorDatasIcy,
            ServiceColorInterpolatorColorMode.Magenta                   => this.m_colorDatasMagenta,
            ServiceColorInterpolatorColorMode.Orange                    => this.m_colorDatasOrange,
            ServiceColorInterpolatorColorMode.OrangePurple              => this.m_colorDatasOrangePurple,
            ServiceColorInterpolatorColorMode.PoweradeSlushie           => this.m_colorDatasPoweradeSlushie,
            ServiceColorInterpolatorColorMode.Purple                    => this.m_colorDatasPurple,
            ServiceColorInterpolatorColorMode.Rainbow                   => this.m_colorDatasRainbow,
            ServiceColorInterpolatorColorMode.Red                       => this.m_colorDatasRed,
            ServiceColorInterpolatorColorMode.RedWhiteBlue              => this.m_colorDatasRedWhiteBlue,
            ServiceColorInterpolatorColorMode.ShowinSomeLove            => this.m_colorDatasShowinSomeLove,
            ServiceColorInterpolatorColorMode.TeamFortress2KillStreak5  => this.m_colorDatasTeamFortress2KillStreak5,
            ServiceColorInterpolatorColorMode.TeamFortress2KillStreak10 => this.m_colorDatasTeamFortress2KillStreak10,
            ServiceColorInterpolatorColorMode.TeamFortress2KillStreak15 => this.m_colorDatasTeamFortress2KillStreak15,
            ServiceColorInterpolatorColorMode.TeamFortress2KillStreak20 => this.m_colorDatasTeamFortress2KillStreak20,
            ServiceColorInterpolatorColorMode.TokeUp                    => this.m_colorDatasTokeUp,
            ServiceColorInterpolatorColorMode.Toxic                     => this.m_colorDatasToxic,
            ServiceColorInterpolatorColorMode.Transition                => this.m_colorDatasTransition,
            ServiceColorInterpolatorColorMode.Vaporwave                 => this.m_colorDatasVaporwave,
            ServiceColorInterpolatorColorMode.Watermelon                => this.m_colorDatasWatermelon,
            ServiceColorInterpolatorColorMode.White                     => this.m_colorDatasWhite,
            ServiceColorInterpolatorColorMode.Yellow                    => this.m_colorDatasYellow,
            _                                   => throw new ArgumentOutOfRangeException()
        };

        var colorIndexTypes = Enum.GetValues<IServiceColorInterpolatorDefinition.ColorIndexType>();
        foreach (var colorIndex in colorIndexTypes)
        {
            var transitionEndColor = this.m_colorDatasTransition[colorIndex].Current;
            var targetData         = targetDatas[colorIndex];

            targetData.Previous      = transitionEndColor;
            targetData.Current       = transitionEndColor;
            targetData.Interpolation = 0f;
        }

        this.m_serviceColorInterpolatorColorMode = this.m_serviceColorInterpolatorColorModeTarget;
    }
    
    private float GetRandomInterpolationRate()
    {
        lock (this.m_random)
        {
            return (float) (this.m_random.NextDouble() * (
                ServiceColorInterpolator<TDefinition>.c_colorInterpolationRateMaximum -
                ServiceColorInterpolator<TDefinition>.c_colorInterpolationRateMinimum
            ) + ServiceColorInterpolator<TDefinition>.c_colorInterpolationRateMinimum);
        }
    }
    
    private void Shutdown()
    {
        this.m_shutdownRequested = true;
    }
    
    private void StartColorInterpolation()
    {
        Task.Run(
            function: async () =>
            {
                try
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
    
                    var lastTime = 0D;
                    while (this.m_shutdownRequested is false)
                    {
                        lock (this.m_lock)
                        {
                            var interpolationRate = this.GetRandomInterpolationRate() * this.m_lovenseIntensity;
                            var currentTime       = stopwatch.Elapsed.TotalSeconds;
                            var deltaTime         = currentTime - lastTime;
                            var speed             = interpolationRate * deltaTime * (1 + SpectrumMusicAnalyzer.Intensity);
                            lastTime              = currentTime;
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasBananaShake,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.BananaShake0,
                                    IServiceColorInterpolatorDefinition.ColorType.BananaShake1,
                                    IServiceColorInterpolatorDefinition.ColorType.BananaShake2,
                                    IServiceColorInterpolatorDefinition.ColorType.BananaShake3,
                                    IServiceColorInterpolatorDefinition.ColorType.BananaShake4,
                                    IServiceColorInterpolatorDefinition.ColorType.BananaShake5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasBlueRaspberry,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry0,
                                    IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry1,
                                    IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry2,
                                    IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry3,
                                    IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry4,
                                    IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasBlue,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Blue0,
                                    IServiceColorInterpolatorDefinition.ColorType.Blue1,
                                    IServiceColorInterpolatorDefinition.ColorType.Blue2,
                                    IServiceColorInterpolatorDefinition.ColorType.Blue3,
                                    IServiceColorInterpolatorDefinition.ColorType.Blue4,
                                    IServiceColorInterpolatorDefinition.ColorType.Blue5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCreamsicleBanana,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana0,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana1,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana2,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana3,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana4,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCreamsicleBlueberry,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry0,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry1,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry2,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry3,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry4,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCreamsicleDragonfruit,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit0,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit1,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit2,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit3,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit4,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCreamsicleLime,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime0,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime1,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime2,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime3,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime4,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCreamsicleOrange,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange0,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange1,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange2,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange3,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange4,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCreamsicleStrawberry,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry0,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry1,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry2,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry3,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry4,
                                    IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCyan,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan0,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan1,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan2,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan3,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan4,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasCyberpunk,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Cyberpunk0,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyberpunk1,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyberpunk2,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyberpunk3,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyberpunk4,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyberpunk5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasDinner,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Dinner0,
                                    IServiceColorInterpolatorDefinition.ColorType.Dinner1,
                                    IServiceColorInterpolatorDefinition.ColorType.Dinner2,
                                    IServiceColorInterpolatorDefinition.ColorType.Dinner3,
                                    IServiceColorInterpolatorDefinition.ColorType.Dinner4,
                                    IServiceColorInterpolatorDefinition.ColorType.Dinner5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasForestSunset,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.ForestSunset0,
                                    IServiceColorInterpolatorDefinition.ColorType.ForestSunset1,
                                    IServiceColorInterpolatorDefinition.ColorType.ForestSunset2,
                                    IServiceColorInterpolatorDefinition.ColorType.ForestSunset3,
                                    IServiceColorInterpolatorDefinition.ColorType.ForestSunset4,
                                    IServiceColorInterpolatorDefinition.ColorType.ForestSunset5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasGreen,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Green0,
                                    IServiceColorInterpolatorDefinition.ColorType.Green1,
                                    IServiceColorInterpolatorDefinition.ColorType.Green2,
                                    IServiceColorInterpolatorDefinition.ColorType.Green3,
                                    IServiceColorInterpolatorDefinition.ColorType.Green4,
                                    IServiceColorInterpolatorDefinition.ColorType.Green5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasHeatwave,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Heatwave0,
                                    IServiceColorInterpolatorDefinition.ColorType.Heatwave1,
                                    IServiceColorInterpolatorDefinition.ColorType.Heatwave2,
                                    IServiceColorInterpolatorDefinition.ColorType.Heatwave3,
                                    IServiceColorInterpolatorDefinition.ColorType.Heatwave4,
                                    IServiceColorInterpolatorDefinition.ColorType.Heatwave5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasIcy,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Icy0,
                                    IServiceColorInterpolatorDefinition.ColorType.Icy1,
                                    IServiceColorInterpolatorDefinition.ColorType.Icy2,
                                    IServiceColorInterpolatorDefinition.ColorType.Icy3,
                                    IServiceColorInterpolatorDefinition.ColorType.Icy4,
                                    IServiceColorInterpolatorDefinition.ColorType.Icy5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasMagenta,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta0,
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta1,
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta2,
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta3,
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta4,
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasOrange,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Orange0,
                                    IServiceColorInterpolatorDefinition.ColorType.Orange1,
                                    IServiceColorInterpolatorDefinition.ColorType.Orange2,
                                    IServiceColorInterpolatorDefinition.ColorType.Orange3,
                                    IServiceColorInterpolatorDefinition.ColorType.Orange4,
                                    IServiceColorInterpolatorDefinition.ColorType.Orange5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasOrangePurple,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.OrangePurple0,
                                    IServiceColorInterpolatorDefinition.ColorType.OrangePurple1,
                                    IServiceColorInterpolatorDefinition.ColorType.OrangePurple2,
                                    IServiceColorInterpolatorDefinition.ColorType.OrangePurple3,
                                    IServiceColorInterpolatorDefinition.ColorType.OrangePurple4,
                                    IServiceColorInterpolatorDefinition.ColorType.OrangePurple5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasPoweradeSlushie,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie0,
                                    IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie1,
                                    IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie2,
                                    IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie3,
                                    IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie4,
                                    IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasPurple,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Purple0,
                                    IServiceColorInterpolatorDefinition.ColorType.Purple1,
                                    IServiceColorInterpolatorDefinition.ColorType.Purple2,
                                    IServiceColorInterpolatorDefinition.ColorType.Purple3,
                                    IServiceColorInterpolatorDefinition.ColorType.Purple4,
                                    IServiceColorInterpolatorDefinition.ColorType.Purple5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasRainbow,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Red,
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow,
                                    IServiceColorInterpolatorDefinition.ColorType.Green,
                                    IServiceColorInterpolatorDefinition.ColorType.Cyan,
                                    IServiceColorInterpolatorDefinition.ColorType.Blue,
                                    IServiceColorInterpolatorDefinition.ColorType.Magenta,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasRed,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Red0,
                                    IServiceColorInterpolatorDefinition.ColorType.Red1,
                                    IServiceColorInterpolatorDefinition.ColorType.Red2,
                                    IServiceColorInterpolatorDefinition.ColorType.Red3,
                                    IServiceColorInterpolatorDefinition.ColorType.Red4,
                                    IServiceColorInterpolatorDefinition.ColorType.Red5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasRedWhiteBlue,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue0,
                                    IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue1,
                                    IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue2,
                                    IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue3,
                                    IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue4,
                                    IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasShowinSomeLove,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove0,
                                    IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove1,
                                    IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove2,
                                    IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove3,
                                    IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove4,
                                    IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasTeamFortress2KillStreak5,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_0,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_1,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_2,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_3,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_4,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasTeamFortress2KillStreak10,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_0,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_1,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_2,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_3,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_4,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasTeamFortress2KillStreak15,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_0,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_1,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_2,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_3,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_4,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasTeamFortress2KillStreak20,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_0,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_1,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_2,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_3,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_4,
                                    IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasTokeUp,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.TokeUp0,
                                    IServiceColorInterpolatorDefinition.ColorType.TokeUp1,
                                    IServiceColorInterpolatorDefinition.ColorType.TokeUp2,
                                    IServiceColorInterpolatorDefinition.ColorType.TokeUp3,
                                    IServiceColorInterpolatorDefinition.ColorType.TokeUp4,
                                    IServiceColorInterpolatorDefinition.ColorType.TokeUp5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasToxic,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Toxic0,
                                    IServiceColorInterpolatorDefinition.ColorType.Toxic1,
                                    IServiceColorInterpolatorDefinition.ColorType.Toxic2,
                                    IServiceColorInterpolatorDefinition.ColorType.Toxic3,
                                    IServiceColorInterpolatorDefinition.ColorType.Toxic4,
                                    IServiceColorInterpolatorDefinition.ColorType.Toxic5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasVaporwave,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Vaporwave0,
                                    IServiceColorInterpolatorDefinition.ColorType.Vaporwave1,
                                    IServiceColorInterpolatorDefinition.ColorType.Vaporwave2,
                                    IServiceColorInterpolatorDefinition.ColorType.Vaporwave3,
                                    IServiceColorInterpolatorDefinition.ColorType.Vaporwave4,
                                    IServiceColorInterpolatorDefinition.ColorType.Vaporwave5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasWatermelon,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Watermelon0,
                                    IServiceColorInterpolatorDefinition.ColorType.Watermelon1,
                                    IServiceColorInterpolatorDefinition.ColorType.Watermelon2,
                                    IServiceColorInterpolatorDefinition.ColorType.Watermelon3,
                                    IServiceColorInterpolatorDefinition.ColorType.Watermelon4,
                                    IServiceColorInterpolatorDefinition.ColorType.Watermelon5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasWhite,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.White0,
                                    IServiceColorInterpolatorDefinition.ColorType.White1,
                                    IServiceColorInterpolatorDefinition.ColorType.White2,
                                    IServiceColorInterpolatorDefinition.ColorType.White3,
                                    IServiceColorInterpolatorDefinition.ColorType.White4,
                                    IServiceColorInterpolatorDefinition.ColorType.White5,
                                ]
                            );
                            
                            ServiceColorInterpolator<TDefinition>.UpdateColor(
                                speed:      (float)speed,
                                colorDatas: this.m_colorDatasYellow,
                                colorTypes: [
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow0,
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow1,
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow2,
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow3,
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow4,
                                    IServiceColorInterpolatorDefinition.ColorType.Yellow5,
                                ]
                            );

                            if (this.m_serviceColorInterpolatorColorMode is ServiceColorInterpolatorColorMode.Transition)
                            {
                                this.UpdateColorTransition(
                                    speed: (float)speed
                                );
                            }
                            
                            this.UpdateColorBorder(
                                speed: (float)speed
                            );
                        }

                        await Task.Delay(
                            millisecondsDelay: 16
                        );
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(
                        value: $"{nameof(ServiceColorInterpolator<TDefinition>)} - Exception: {exception.Message}"
                    );
                    throw;
                }
            }
        );
    }
    
    private static void UpdateColor(
        float                                                                          speed,
        Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> colorDatas,
        IServiceColorInterpolatorDefinition.ColorType[]                                colorTypes
    )
    {
        var colorIndexTypes = Enum.GetValues<IServiceColorInterpolatorDefinition.ColorIndexType>();
        foreach (var colorIndexType in colorIndexTypes)
        {
            var colorIndexData         = colorDatas[key: colorIndexType];
            var currentColor           = colorIndexData.Current;
            var previousColor          = colorIndexData.Previous;
            var colorInterpolation     = colorIndexData.Interpolation;
            var colorInterpolationType = colorIndexData.InterpolatorColorInterpolationType;

            colorInterpolation += speed;
            switch (colorInterpolationType)
            {
                case ServiceColorInterpolatorColorInterpolationType.Color0ToColor1:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[0]],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[0]];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[0]];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color1ToColor2;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color1ToColor2:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[1]],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[1]];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[1]];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color2ToColor3;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color2ToColor3:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[2]],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[2]];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[2]];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color3ToColor4;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color3ToColor4:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[3]],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[3]];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[3]];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color4ToColor5;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color4ToColor5:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[4]],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[4]];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[4]];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color5ToColor0;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color5ToColor0:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[5]],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[5]];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: colorTypes[5]];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color0ToColor1;
                    }
                    break;

                default:
                    break;
            }

            colorIndexData.Current           = currentColor;
            colorIndexData.Previous          = previousColor;
            colorIndexData.Interpolation     = colorInterpolation;
            colorIndexData.InterpolatorColorInterpolationType = colorInterpolationType;
        }
    }
    
    private void UpdateColorBorder(
        float speed
    )
    {
        var colorIndexTypes = Enum.GetValues<IServiceColorInterpolatorDefinition.ColorIndexType>();
        foreach (var colorIndexType in colorIndexTypes)
        {
            var colorIndexData         = this.m_colorDatasBorder[key: colorIndexType];
            var currentColor           = colorIndexData.Current;
            var previousColor          = colorIndexData.Previous;
            var colorInterpolation     = colorIndexData.Interpolation;
            var colorInterpolationType = colorIndexData.InterpolatorColorInterpolationType;

            colorInterpolation += speed;
            switch (colorInterpolationType)
            {
                case ServiceColorInterpolatorColorInterpolationType.Color0ToColor1:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border1],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border1];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border1];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color1ToColor2;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color1ToColor2:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border2],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border2];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border2];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color2ToColor3;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color2ToColor3:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border3],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border3];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border3];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color3ToColor4;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color3ToColor4:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border4],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border4];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border4];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color4ToColor5;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color4ToColor5:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border5],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border5];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border5];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color5ToColor0;
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color5ToColor0:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border0],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border0];
                        previousColor          = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border0];
                        colorInterpolationType = ServiceColorInterpolatorColorInterpolationType.Color0ToColor1;
                    }
                    break;

                default:
                    break;
            }

            colorIndexData.Current                            = currentColor;
            colorIndexData.Previous                           = previousColor;
            colorIndexData.Interpolation                      = colorInterpolation;
            colorIndexData.InterpolatorColorInterpolationType = colorInterpolationType;
        }
    }
    
    private void UpdateColorTransition(
        float speed
    )
    {
        var colorIndexTypes = Enum.GetValues<IServiceColorInterpolatorDefinition.ColorIndexType>();
        foreach (var colorIndexType in colorIndexTypes)
        {
            var colorIndexData         = this.m_colorDatasTransition[key: colorIndexType];
            var currentColor           = colorIndexData.Current;
            var previousColor          = colorIndexData.Previous;
            var colorInterpolation     = colorIndexData.Interpolation;
            var colorInterpolationType = colorIndexData.InterpolatorColorInterpolationType;

            colorInterpolation += speed * ServiceColorInterpolator<TDefinition>.c_colorTransitionSpeed;
            switch (colorInterpolationType)
            {
                case ServiceColorInterpolatorColorInterpolationType.Color0ToColor1:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition1],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation = 1f;
                        currentColor       = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition1];
                        previousColor      = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition1];
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color1ToColor2:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition2],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation = 1f;
                        currentColor       = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition2];
                        previousColor      = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition2];
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color2ToColor3:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition3],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation = 1f;
                        currentColor       = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition3];
                        previousColor      = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition3];
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color3ToColor4:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition4],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation = 1f;
                        currentColor       = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition4];
                        previousColor      = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition4];
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color4ToColor5:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition5],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation = 1f;
                        currentColor       = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition5];
                        previousColor      = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition5];
                    }
                    break;

                case ServiceColorInterpolatorColorInterpolationType.Color5ToColor0:
                    currentColor = previousColor.Lerp(
                        to:     ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition0],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation = 1f;
                        currentColor       = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition0];
                        previousColor      = ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition0];
                    }
                    break;

                default:
                    break;
            }

            colorIndexData.Current                            = currentColor;
            colorIndexData.Previous                           = previousColor;
            colorIndexData.Interpolation                      = colorInterpolation;
            colorIndexData.InterpolatorColorInterpolationType = colorInterpolationType;
        }

        var areAllInterpolationsCompleted = colorIndexTypes.Select(
            selector:  colorIndexType => this.m_colorDatasTransition[key: colorIndexType]
        ).Select(
            selector:  colorIndexData => colorIndexData.Interpolation
        ).All(
            predicate: colorInterpolation => !(colorInterpolation < 1f)
        );
        if (areAllInterpolationsCompleted is true)
        {
            this.FinalizeTransition();
        }
    }
    
    private static void UpdateColorTransitionCodes(
        IServiceColorInterpolatorDefinition.ColorType[] colorTypes
    )
    {
        ServiceColorInterpolator<TDefinition>.s_colorCodes[IServiceColorInterpolatorDefinition.ColorType.Transition0] = ServiceColorInterpolator<TDefinition>.s_colorCodes[colorTypes[0]];
        ServiceColorInterpolator<TDefinition>.s_colorCodes[IServiceColorInterpolatorDefinition.ColorType.Transition1] = ServiceColorInterpolator<TDefinition>.s_colorCodes[colorTypes[1]];
        ServiceColorInterpolator<TDefinition>.s_colorCodes[IServiceColorInterpolatorDefinition.ColorType.Transition2] = ServiceColorInterpolator<TDefinition>.s_colorCodes[colorTypes[2]];
        ServiceColorInterpolator<TDefinition>.s_colorCodes[IServiceColorInterpolatorDefinition.ColorType.Transition3] = ServiceColorInterpolator<TDefinition>.s_colorCodes[colorTypes[3]];
        ServiceColorInterpolator<TDefinition>.s_colorCodes[IServiceColorInterpolatorDefinition.ColorType.Transition4] = ServiceColorInterpolator<TDefinition>.s_colorCodes[colorTypes[4]];
        ServiceColorInterpolator<TDefinition>.s_colorCodes[IServiceColorInterpolatorDefinition.ColorType.Transition5] = ServiceColorInterpolator<TDefinition>.s_colorCodes[colorTypes[5]];
    }
    
    private void UpdateColorTransitionDatas()
    {
        var colorIndexTypes = Enum.GetValues(
            enumType: typeof(IServiceColorInterpolatorDefinition.ColorIndexType)
        );
        foreach (IServiceColorInterpolatorDefinition.ColorIndexType colorIndexType in colorIndexTypes)
        {
            var currentActiveColor = this.GetColorByCurrentMode(
                colorIndexType: colorIndexType
            ); 
            var transitionData     = this.m_colorDatasTransition[key: colorIndexType];
            
            transitionData.Previous      = currentActiveColor;
            transitionData.Current       = currentActiveColor;
            transitionData.Interpolation = 0f;
        }
    }
}