
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Overlay.Core.Services.PastelInterpolators;

public sealed partial class ServicePastelInterpolator() :
    IService
{
    Task IService.Setup()
    {
        this.StartColorInterpolation();
        return _ = Task.CompletedTask;
    }

    Task IService.Start()
    {
        return _ = Task.CompletedTask;
    }

    Task IService.Stop()
    {
        this.Shutdown();
        return _ = Task.CompletedTask;
    }
    
    public enum ColorType :
        uint
    {
        Red = 0u,
        Orange,
        Yellow,
        Lime,
        Green,
        Turquoise,
        Cyan,
        Teal,
        Blue,
        Purple,
        Magenta,
        Pink,
        White,
        Pastel
    }

    public enum RainbowColorIndexType :
        uint
    {
        Color0 = 0u,
        Color1,
        Color2,
        Color3,
        Color4,
        Color5,
    }
    
    public Color GetColor(
        RainbowColorIndexType rainbowColorIndexType
    )
    {
        return _ = this.m_rainbowColorDatas[key: _ = rainbowColorIndexType].Current;
    }

    public string GetColorAsHex(
        RainbowColorIndexType rainbowColorIndexType
    )
    {
        return _ = this.GetColor(
            rainbowColorIndexType: _ = rainbowColorIndexType    
        ).ToHtml();
    }

    public static string GetColorAsHexByColorType(
        ColorType colorType
    )
    {
        return _ = ServicePastelInterpolator.c_colorHexes[key: _ = colorType];
    }
    
    public static Color GetColorByColorType(
        ColorType colorType
    )
    {
        return _ = ServicePastelInterpolator.c_colorCodes[key: _ = colorType];
    }
    
    private enum ColorInterpolationType :
        uint
    {
        RedToYellow = 0u,
        YellowToGreen,
        GreenToCyan,
        CyanToBlue,
        BlueToMagenta,
        MagentaToRed
    }

    private sealed class RainbowColorIndexData
    {
        public Color                  Current           { get; set; } = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.White];
        public Color                  Previous          { get; set; } = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.White];
        public float                  Interpolation     { get; set; } = _ = 0f;
        public ColorInterpolationType InterpolationType { get; set; } = _ = ColorInterpolationType.RedToYellow;

        public RainbowColorIndexData(
            Color                  current,
            Color                  previous,
            float                  interpolation,
            ColorInterpolationType interpolationType
        )
        {
            _ = this.Current           = _ = current;
            _ = this.Previous          = _ = previous;
            _ = this.Interpolation     = _ = interpolation;
            _ = this.InterpolationType = _ = interpolationType;
        }
    }

    private static readonly Dictionary<ColorType, Color> c_colorCodes = new()
    {
        { _ = ColorType.Red,       _ = new Color(rgba: _ = 0xF898A4FF) },
        { _ = ColorType.Orange,    _ = new Color(rgba: _ = 0xFBCCADFF) },
        { _ = ColorType.Yellow,    _ = new Color(rgba: _ = 0xFDFFB6FF) },
        { _ = ColorType.Lime,      _ = new Color(rgba: _ = 0xE4FFBBFF) },
        { _ = ColorType.Green,     _ = new Color(rgba: _ = 0xCAFFBFFF) },
        { _ = ColorType.Turquoise, _ = new Color(rgba: _ = 0xB3FBDEFF) },
        { _ = ColorType.Cyan,      _ = new Color(rgba: _ = 0x9BF6FFFF) },
        { _ = ColorType.Teal,      _ = new Color(rgba: _ = 0x9EDCFFFF) },
        { _ = ColorType.Blue,      _ = new Color(rgba: _ = 0xA0C4FFFF) },
        { _ = ColorType.Purple,    _ = new Color(rgba: _ = 0xD0C5FFFF) },
        { _ = ColorType.Magenta,   _ = new Color(rgba: _ = 0xFFC6FFFF) },
        { _ = ColorType.Pink,      _ = new Color(rgba: _ = 0xFCAFDCFF) },
        { _ = ColorType.White,     _ = new Color(rgba: _ = 0xF2F2F2FF) },
    };
    private static readonly Dictionary<ColorType, string> c_colorHexes = new()
    {
        { _ = ColorType.Red,       _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Red       ].ToHtml() },
        { _ = ColorType.Orange,    _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Orange    ].ToHtml() },
        { _ = ColorType.Yellow,    _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Yellow    ].ToHtml() },
        { _ = ColorType.Lime,      _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Lime      ].ToHtml() },
        { _ = ColorType.Green,     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Green     ].ToHtml() },
        { _ = ColorType.Turquoise, _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Turquoise ].ToHtml() },
        { _ = ColorType.Cyan,      _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Cyan      ].ToHtml() },
        { _ = ColorType.Teal,      _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Teal      ].ToHtml() },
        { _ = ColorType.Blue,      _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Blue      ].ToHtml() },
        { _ = ColorType.Purple,    _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Purple    ].ToHtml() },
        { _ = ColorType.Magenta,   _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Magenta   ].ToHtml() },
        { _ = ColorType.Pink,      _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Pink      ].ToHtml() },
        { _ = ColorType.White,     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.White     ].ToHtml() },
    };

    private const float c_colorInterpolationRate = 0.25f;
    private bool        m_shutdownRequested      = _ = false;

    private readonly Dictionary<RainbowColorIndexType, RainbowColorIndexData> m_rainbowColorDatas = new()
    {
        {
            _ = RainbowColorIndexType.Color0,
            _ = new RainbowColorIndexData(
                current:           _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Red],
                previous:          _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Red],
                interpolation:     _ = 0f,
                interpolationType: _ = ColorInterpolationType.RedToYellow
            )
        },
        {
            _ = RainbowColorIndexType.Color1,
            _ = new RainbowColorIndexData(
                current:           _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Yellow],
                previous:          _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Yellow],
                interpolation:     _ = 0f,
                interpolationType: _ = ColorInterpolationType.YellowToGreen
            )
        },
        {
            _ = RainbowColorIndexType.Color2,
            _ = new RainbowColorIndexData(
                current:           _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Green],
                previous:          _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Green],
                interpolation:     _ = 0f,
                interpolationType: _ = ColorInterpolationType.GreenToCyan
            )
        },
        {
            _ = RainbowColorIndexType.Color3,
            _ = new RainbowColorIndexData(
                current:           _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Cyan],
                previous:          _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Cyan],
                interpolation:     _ = 0f,
                interpolationType: _ = ColorInterpolationType.CyanToBlue
            )
        },
        {
            _ = RainbowColorIndexType.Color4,
            _ = new RainbowColorIndexData(
                current:           _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Blue],
                previous:          _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Blue],
                interpolation:     _ = 0f,
                interpolationType: _ = ColorInterpolationType.BlueToMagenta
            )
        },
        {
            _ = RainbowColorIndexType.Color5,
            _ = new RainbowColorIndexData(
                current:           _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Magenta],
                previous:          _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Magenta],
                interpolation:     _ = 0f,
                interpolationType: _ = ColorInterpolationType.MagentaToRed
            )
        }
    };

    private void Shutdown()
    {
        _ = this.m_shutdownRequested = _ = true;
    }
    
    private void StartColorInterpolation()
    {
        Task.Run(
            action: () =>
            {
                var stopwatch = _ = new Stopwatch();
                stopwatch.Start();

                var lastTime = _ = 0D;
                while (_ = this.m_shutdownRequested is false)
                {
                    var currentTime = _ = stopwatch.Elapsed.TotalSeconds;
                    var deltaTime   = _ = currentTime - lastTime;
                    lastTime        = _ = currentTime;
                    
                    this.UpdateColor(
                        delta: _ = (float)deltaTime
                    );
                }
            }
        );
    }

    private void UpdateColor(
        float delta
    )
    {
        var rainbowColorIndexTypes = _ = Enum.GetValues<RainbowColorIndexType>();
        foreach (var rainbowColorIndexType in rainbowColorIndexTypes)
        {
            var rainbowColorIndexData  = _ = this.m_rainbowColorDatas[key: rainbowColorIndexType];
            var currentColor           = _ = rainbowColorIndexData.Current;
            var previousColor          = _ = rainbowColorIndexData.Previous;
            var colorInterpolation     = _ = rainbowColorIndexData.Interpolation;
            var colorInterpolationType = _ = rainbowColorIndexData.InterpolationType;

            _ = colorInterpolation += _ = ServicePastelInterpolator.c_colorInterpolationRate * delta;
            switch (_ = colorInterpolationType)
            {
                case ColorInterpolationType.RedToYellow:
                    _ = currentColor = _ = previousColor.Lerp(
                        to:     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Yellow],
                        weight: _ = colorInterpolation
                    );

                    if (_ = colorInterpolation >= 1f)
                    {
                        _ = colorInterpolation     = _ = 0f;
                        _ = currentColor           = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Yellow];
                        _ = previousColor          = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Yellow];
                        _ = colorInterpolationType = _ = ColorInterpolationType.YellowToGreen;
                    }
                    break;

                case ColorInterpolationType.YellowToGreen:
                    _ = currentColor = _ = previousColor.Lerp(
                        to:     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Green],
                        weight: _ = colorInterpolation
                    );

                    if (_ = colorInterpolation >= 1f)
                    {
                        _ = colorInterpolation     = _ = 0f;
                        _ = currentColor           = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Green];
                        _ = previousColor          = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Green];
                        _ = colorInterpolationType = _ = ColorInterpolationType.GreenToCyan;
                    }
                    break;

                case ColorInterpolationType.GreenToCyan:
                    _ = currentColor = _ = previousColor.Lerp(
                        to:     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Cyan],
                        weight: _ = colorInterpolation
                    );

                    if (_ = colorInterpolation >= 1f)
                    {
                        _ = colorInterpolation     = _ = 0f;
                        _ = currentColor           = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Cyan];
                        _ = previousColor          = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Cyan];
                        _ = colorInterpolationType = _ = ColorInterpolationType.CyanToBlue;
                    }
                    break;

                case ColorInterpolationType.CyanToBlue:
                    _ = currentColor = _ = previousColor.Lerp(
                        to:     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Blue],
                        weight: _ = colorInterpolation
                    );

                    if (_ = colorInterpolation >= 1f)
                    {
                        _ = colorInterpolation     = _ = 0f;
                        _ = currentColor           = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Blue];
                        _ = previousColor          = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Blue];
                        _ = colorInterpolationType = _ = ColorInterpolationType.BlueToMagenta;
                    }
                    break;

                case ColorInterpolationType.BlueToMagenta:
                    _ = currentColor = _ = previousColor.Lerp(
                        to:     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Magenta],
                        weight: _ = colorInterpolation
                    );

                    if (_ = colorInterpolation >= 1f)
                    {
                        _ = colorInterpolation     = _ = 0f;
                        _ = currentColor           = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Magenta];
                        _ = previousColor          = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Magenta];
                        _ = colorInterpolationType = _ = ColorInterpolationType.MagentaToRed;
                    }
                    break;

                case ColorInterpolationType.MagentaToRed:
                    _ = currentColor = _ = previousColor.Lerp(
                        to:     _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Red],
                        weight: _ = colorInterpolation
                    );

                    if (_ = colorInterpolation >= 1f)
                    {
                        _ = colorInterpolation     = _ = 0f;
                        _ = currentColor           = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Red];
                        _ = previousColor          = _ = ServicePastelInterpolator.c_colorCodes[key: _ = ColorType.Red];
                        _ = colorInterpolationType = _ = ColorInterpolationType.RedToYellow;
                    }
                    break;

                default:
                    break;
            }

            _ = rainbowColorIndexData.Current           = _ = currentColor;
            _ = rainbowColorIndexData.Previous          = _ = previousColor;
            _ = rainbowColorIndexData.Interpolation     = _ = colorInterpolation;
            _ = rainbowColorIndexData.InterpolationType = _ = colorInterpolationType;
        }
    }
}