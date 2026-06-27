
using Godot;
using System.Collections.Generic;

namespace Overlay.Core.Services.ColorInterpolators;

public abstract partial class ServiceColorInterpolator<TDefinition> 
    where TDefinition : IServiceColorInterpolatorDefinition
{
    private sealed class ColorIndexData
        {
            public Color                  Current           { get; set; }
            public Color                  Previous          { get; set; }
            public float                  Interpolation     { get; set; } = 0f;
            public ServiceColorInterpolatorColorInterpolationType InterpolatorColorInterpolationType { get; set; }
    
            public ColorIndexData(
                Color                  current,
                Color                  previous,
                float                  interpolation,
                ServiceColorInterpolatorColorInterpolationType interpolatorColorInterpolationType
            )
            {
                this.Current           = current;
                this.Previous          = previous;
                this.Interpolation     = interpolation;
                this.InterpolatorColorInterpolationType = interpolatorColorInterpolationType;
            }
        }
    
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasBananaShake               = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BananaShake5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasBlue                      = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasBlueRaspberry             = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.BlueRaspberry5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasBorder                    = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Border5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCreamsicleBanana          = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBanana5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCreamsicleBlueberry       = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleBlueberry5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCreamsicleDragonfruit     = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleDragonfruit5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCreamsicleLime            = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleLime5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCreamsicleOrange          = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleOrange5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCreamsicleStrawberry      = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.CreamsicleStrawberry5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCyan                      = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasCyberpunk                 = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyberpunk5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasDinner                    = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Dinner5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasForestSunset              = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ForestSunset5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasGreen                     = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasHeatwave                  = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Heatwave5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasIcy                       = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Icy5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasMagenta                   = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasOrange                    = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Orange5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasOrangePurple              = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.OrangePurple5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasPoweradeSlushie           = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.PoweradeSlushie5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasPurple                    = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Purple5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasRainbow                   = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Green],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Cyan],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Blue],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Magenta],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasRed                       = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Red5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasRedWhiteBlue              = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.RedWhiteBlue5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasShowinSomeLove            = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.ShowinSomeLove5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasTeamFortress2KillStreak5  = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak5_5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasTeamFortress2KillStreak10 = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak10_5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasTeamFortress2KillStreak15 = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak15_5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasTeamFortress2KillStreak20 = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TeamFortress2KillStreak20_5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasTokeUp                    = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.TokeUp5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasToxic                     = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Toxic5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasTransition                = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Transition5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasVaporwave                 = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Vaporwave5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasWatermelon                = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Watermelon5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasWhite                     = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.White5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
    private readonly Dictionary<IServiceColorInterpolatorDefinition.ColorIndexType, ColorIndexData> m_colorDatasYellow                    = new()
    {
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color0,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow0],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow0],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color0ToColor1
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color1,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow1],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow1],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color1ToColor2
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color2,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow2],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow2],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color2ToColor3
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color3,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow3],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow3],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color3ToColor4
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color4,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow4],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow4],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color4ToColor5
            )
        },
        {
            IServiceColorInterpolatorDefinition.ColorIndexType.Color5,
            new ColorIndexData(
                current:           ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow5],
                previous:          ServiceColorInterpolator<TDefinition>.s_colorCodes[key: IServiceColorInterpolatorDefinition.ColorType.Yellow5],
                interpolation:     0f,
                interpolatorColorInterpolationType: ServiceColorInterpolatorColorInterpolationType.Color5ToColor0
            )
        }
    };
}