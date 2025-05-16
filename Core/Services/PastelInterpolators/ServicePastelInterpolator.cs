
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Overlay.Core.Services.PastelInterpolators;

public sealed class ServicePastelInterpolator() :
    IService
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
    
    public enum ColorType :
        uint
    {
        Red = 0u,
        Yellow,
        Green,
        Cyan,
        Blue,
        Magenta,
        White
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
        return this.m_rainbowColorDatas[key: rainbowColorIndexType].Current;
    }

    public string GetColorAsHex(
        RainbowColorIndexType rainbowColorIndexType
    )
    {
        return this.GetColor(
            rainbowColorIndexType: rainbowColorIndexType    
        ).ToHtml();
    }

    public static string GetColorAsHexByColorType(
        ColorType colorType
    )
    {
        return ServicePastelInterpolator.c_colorHexes[key: colorType];
    }
    
    public static Color GetColorByColorType(
        ColorType colorType
    )
    {
        return ServicePastelInterpolator.c_colorCodes[key: colorType];
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
        public Color                  Current           { get; set; } = ServicePastelInterpolator.c_colorCodes[key: ColorType.White];
        public Color                  Previous          { get; set; } = ServicePastelInterpolator.c_colorCodes[key: ColorType.White];
        public float                  Interpolation     { get; set; } = 0f;
        public ColorInterpolationType InterpolationType { get; set; } = ColorInterpolationType.RedToYellow;

        public RainbowColorIndexData(
            Color                  current,
            Color                  previous,
            float                  interpolation,
            ColorInterpolationType interpolationType
        )
        {
            this.Current           = current;
            this.Previous          = previous;
            this.Interpolation     = interpolation;
            this.InterpolationType = interpolationType;
        }
    }

    private static readonly Dictionary<ColorType, Color> c_colorCodes = new()
    {
        { ColorType.Red,       new Color(rgba: 0xFF0000FF) },
        { ColorType.Yellow,    new Color(rgba: 0xFFFF00FF) },
        { ColorType.Green,     new Color(rgba: (uint) 0x00FF00FF) },
        { ColorType.Cyan,      new Color(rgba: (uint) 0x00FFFFFF) },
        { ColorType.Blue,      new Color(rgba: (uint) 0x0000FFFF) },
        { ColorType.Magenta,   new Color(rgba: 0xFF00FFFF) },
        { ColorType.White,     new Color(rgba: 0xF2F2F2FF) },
    };
    private static readonly Dictionary<ColorType, string> c_colorHexes = new()
    {
        { ColorType.Red,       ServicePastelInterpolator.c_colorCodes[key: ColorType.Red       ].ToHtml() },
        { ColorType.Yellow,    ServicePastelInterpolator.c_colorCodes[key: ColorType.Yellow    ].ToHtml() },
        { ColorType.Green,     ServicePastelInterpolator.c_colorCodes[key: ColorType.Green     ].ToHtml() },
        { ColorType.Cyan,      ServicePastelInterpolator.c_colorCodes[key: ColorType.Cyan      ].ToHtml() },
        { ColorType.Blue,      ServicePastelInterpolator.c_colorCodes[key: ColorType.Blue      ].ToHtml() },
        { ColorType.Magenta,   ServicePastelInterpolator.c_colorCodes[key: ColorType.Magenta   ].ToHtml() },
        { ColorType.White,     ServicePastelInterpolator.c_colorCodes[key: ColorType.White     ].ToHtml() },
    };

    private const float c_colorInterpolationRate = 0.5f;
    private bool        m_shutdownRequested      = false;

    private readonly Dictionary<RainbowColorIndexType, RainbowColorIndexData> m_rainbowColorDatas = new()
    {
        {
            RainbowColorIndexType.Color0,
            new RainbowColorIndexData(
                current:           ServicePastelInterpolator.c_colorCodes[key: ColorType.Red],
                previous:          ServicePastelInterpolator.c_colorCodes[key: ColorType.Red],
                interpolation:     0f,
                interpolationType: ColorInterpolationType.RedToYellow
            )
        },
        {
            RainbowColorIndexType.Color1,
            new RainbowColorIndexData(
                current:           ServicePastelInterpolator.c_colorCodes[key: ColorType.Yellow],
                previous:          ServicePastelInterpolator.c_colorCodes[key: ColorType.Yellow],
                interpolation:     0f,
                interpolationType: ColorInterpolationType.YellowToGreen
            )
        },
        {
            RainbowColorIndexType.Color2,
            new RainbowColorIndexData(
                current:           ServicePastelInterpolator.c_colorCodes[key: ColorType.Green],
                previous:          ServicePastelInterpolator.c_colorCodes[key: ColorType.Green],
                interpolation:     0f,
                interpolationType: ColorInterpolationType.GreenToCyan
            )
        },
        {
            RainbowColorIndexType.Color3,
            new RainbowColorIndexData(
                current:           ServicePastelInterpolator.c_colorCodes[key: ColorType.Cyan],
                previous:          ServicePastelInterpolator.c_colorCodes[key: ColorType.Cyan],
                interpolation:     0f,
                interpolationType: ColorInterpolationType.CyanToBlue
            )
        },
        {
            RainbowColorIndexType.Color4,
            new RainbowColorIndexData(
                current:           ServicePastelInterpolator.c_colorCodes[key: ColorType.Blue],
                previous:          ServicePastelInterpolator.c_colorCodes[key: ColorType.Blue],
                interpolation:     0f,
                interpolationType: ColorInterpolationType.BlueToMagenta
            )
        },
        {
            RainbowColorIndexType.Color5,
            new RainbowColorIndexData(
                current:           ServicePastelInterpolator.c_colorCodes[key: ColorType.Magenta],
                previous:          ServicePastelInterpolator.c_colorCodes[key: ColorType.Magenta],
                interpolation:     0f,
                interpolationType: ColorInterpolationType.MagentaToRed
            )
        }
    };

    private void Shutdown()
    {
        this.m_shutdownRequested = true;
    }
    
    private void StartColorInterpolation()
    {
        Task.Run(
            action: () =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var lastTime = 0D;
                while (this.m_shutdownRequested is false)
                {
                    var currentTime = stopwatch.Elapsed.TotalSeconds;
                    var deltaTime   = currentTime - lastTime;
                    lastTime        = currentTime;
                    
                    this.UpdateColor(
                        delta: (float)deltaTime
                    );
                }
            }
        );
    }

    private void UpdateColor(
        float delta
    )
    {
        var rainbowColorIndexTypes = Enum.GetValues<RainbowColorIndexType>();
        foreach (var rainbowColorIndexType in rainbowColorIndexTypes)
        {
            var rainbowColorIndexData  = this.m_rainbowColorDatas[key: rainbowColorIndexType];
            var currentColor           = rainbowColorIndexData.Current;
            var previousColor          = rainbowColorIndexData.Previous;
            var colorInterpolation     = rainbowColorIndexData.Interpolation;
            var colorInterpolationType = rainbowColorIndexData.InterpolationType;

            colorInterpolation += ServicePastelInterpolator.c_colorInterpolationRate * delta;
            switch (colorInterpolationType)
            {
                case ColorInterpolationType.RedToYellow:
                    currentColor = previousColor.Lerp(
                        to:     ServicePastelInterpolator.c_colorCodes[key: ColorType.Yellow],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServicePastelInterpolator.c_colorCodes[key: ColorType.Yellow];
                        previousColor          = ServicePastelInterpolator.c_colorCodes[key: ColorType.Yellow];
                        colorInterpolationType = ColorInterpolationType.YellowToGreen;
                    }
                    break;

                case ColorInterpolationType.YellowToGreen:
                    currentColor = previousColor.Lerp(
                        to:     ServicePastelInterpolator.c_colorCodes[key: ColorType.Green],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServicePastelInterpolator.c_colorCodes[key: ColorType.Green];
                        previousColor          = ServicePastelInterpolator.c_colorCodes[key: ColorType.Green];
                        colorInterpolationType = ColorInterpolationType.GreenToCyan;
                    }
                    break;

                case ColorInterpolationType.GreenToCyan:
                    currentColor = previousColor.Lerp(
                        to:     ServicePastelInterpolator.c_colorCodes[key: ColorType.Cyan],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServicePastelInterpolator.c_colorCodes[key: ColorType.Cyan];
                        previousColor          = ServicePastelInterpolator.c_colorCodes[key: ColorType.Cyan];
                        colorInterpolationType = ColorInterpolationType.CyanToBlue;
                    }
                    break;

                case ColorInterpolationType.CyanToBlue:
                    currentColor = previousColor.Lerp(
                        to:     ServicePastelInterpolator.c_colorCodes[key: ColorType.Blue],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServicePastelInterpolator.c_colorCodes[key: ColorType.Blue];
                        previousColor          = ServicePastelInterpolator.c_colorCodes[key: ColorType.Blue];
                        colorInterpolationType = ColorInterpolationType.BlueToMagenta;
                    }
                    break;

                case ColorInterpolationType.BlueToMagenta:
                    currentColor = previousColor.Lerp(
                        to:     ServicePastelInterpolator.c_colorCodes[key: ColorType.Magenta],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServicePastelInterpolator.c_colorCodes[key: ColorType.Magenta];
                        previousColor          = ServicePastelInterpolator.c_colorCodes[key: ColorType.Magenta];
                        colorInterpolationType = ColorInterpolationType.MagentaToRed;
                    }
                    break;

                case ColorInterpolationType.MagentaToRed:
                    currentColor = previousColor.Lerp(
                        to:     ServicePastelInterpolator.c_colorCodes[key: ColorType.Red],
                        weight: colorInterpolation
                    );

                    if (colorInterpolation >= 1f)
                    {
                        colorInterpolation     = 0f;
                        currentColor           = ServicePastelInterpolator.c_colorCodes[key: ColorType.Red];
                        previousColor          = ServicePastelInterpolator.c_colorCodes[key: ColorType.Red];
                        colorInterpolationType = ColorInterpolationType.RedToYellow;
                    }
                    break;

                default:
                    break;
            }

            rainbowColorIndexData.Current           = currentColor;
            rainbowColorIndexData.Previous          = previousColor;
            rainbowColorIndexData.Interpolation     = colorInterpolation;
            rainbowColorIndexData.InterpolationType = colorInterpolationType;
        }
    }
}