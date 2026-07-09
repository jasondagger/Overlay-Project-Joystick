
#nullable enable
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Mappings;

internal static class MappingColorNames
{
    internal static ServiceColorInterpolatorColorMode? GetColorByColorName(
        string colorName
    )
    {
        return colorName switch
        {
            "blue"                   => ServiceColorInterpolatorColorMode.Blue,
            "blue raspberry"         => ServiceColorInterpolatorColorMode.BlueRaspberry,
            "creamsicle banana"      => ServiceColorInterpolatorColorMode.CreamsicleBanana,
            "creamsicle blueberry"   => ServiceColorInterpolatorColorMode.CreamsicleBlueberry,
            "creamsicle dragonfruit" => ServiceColorInterpolatorColorMode.CreamsicleDragonfruit,
            "creamsicle lime"        => ServiceColorInterpolatorColorMode.CreamsicleLime,
            "creamsicle orange"      => ServiceColorInterpolatorColorMode.CreamsicleOrange,
            "creamsicle strawberry"  => ServiceColorInterpolatorColorMode.CreamsicleStrawberry,
            "cyan"                   => ServiceColorInterpolatorColorMode.Cyan,
            "cyberpunk"              => ServiceColorInterpolatorColorMode.Cyberpunk,
            "forest sunset"          => ServiceColorInterpolatorColorMode.ForestSunset,
            "green"                  => ServiceColorInterpolatorColorMode.Green,
            "heatwave"               => ServiceColorInterpolatorColorMode.Heatwave,
            "icy"                    => ServiceColorInterpolatorColorMode.Icy,
            "magenta"                => ServiceColorInterpolatorColorMode.Magenta,
            "orange"                 => ServiceColorInterpolatorColorMode.Orange,
            "orange purple"          => ServiceColorInterpolatorColorMode.OrangePurple,
            "purple"                 => ServiceColorInterpolatorColorMode.Purple,
            "rainbow"                => ServiceColorInterpolatorColorMode.Rainbow,
            "red"                    => ServiceColorInterpolatorColorMode.Red,
            "red white blue"         => ServiceColorInterpolatorColorMode.RedWhiteBlue,
            "toxic"                  => ServiceColorInterpolatorColorMode.Toxic,
            "vaporwave"              => ServiceColorInterpolatorColorMode.Vaporwave,
            "watermelon"             => ServiceColorInterpolatorColorMode.Watermelon,
            "white"                  => ServiceColorInterpolatorColorMode.White,
            "yellow"                 => ServiceColorInterpolatorColorMode.Yellow,
            _                        => null
        };
    }
    
    internal static string? GetColorNameByColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        return color switch
        {
            ServiceColorInterpolatorColorMode.Blue                  => "blue",
            ServiceColorInterpolatorColorMode.BlueRaspberry         => "blue raspberry",
            ServiceColorInterpolatorColorMode.CreamsicleBanana      => "creamsicle banana",
            ServiceColorInterpolatorColorMode.CreamsicleBlueberry   => "creamsicle blueberry",
            ServiceColorInterpolatorColorMode.CreamsicleDragonfruit => "creamsicle dragonfruit",
            ServiceColorInterpolatorColorMode.CreamsicleLime        => "creamsicle lime",
            ServiceColorInterpolatorColorMode.CreamsicleOrange      => "creamsicle orange",
            ServiceColorInterpolatorColorMode.CreamsicleStrawberry  => "creamsicle strawberry",
            ServiceColorInterpolatorColorMode.Cyan                  => "cyan",
            ServiceColorInterpolatorColorMode.Cyberpunk             => "cyberpunk",
            ServiceColorInterpolatorColorMode.ForestSunset          => "forest sunset",
            ServiceColorInterpolatorColorMode.Green                 => "green",
            ServiceColorInterpolatorColorMode.Heatwave              => "heatwave",
            ServiceColorInterpolatorColorMode.Icy                   => "icy",
            ServiceColorInterpolatorColorMode.Magenta               => "magenta",
            ServiceColorInterpolatorColorMode.Orange                => "orange",
            ServiceColorInterpolatorColorMode.OrangePurple          => "orange purple",
            ServiceColorInterpolatorColorMode.Purple                => "purple",
            ServiceColorInterpolatorColorMode.Rainbow               => "rainbow",
            ServiceColorInterpolatorColorMode.Red                   => "red",
            ServiceColorInterpolatorColorMode.RedWhiteBlue          => "red white blue",
            ServiceColorInterpolatorColorMode.Toxic                 => "toxic",
            ServiceColorInterpolatorColorMode.Vaporwave             => "vaporwave",
            ServiceColorInterpolatorColorMode.Watermelon            => "watermelon",
            ServiceColorInterpolatorColorMode.White                 => "white",
            ServiceColorInterpolatorColorMode.Yellow                => "yellow",
            _                                                       => null
        };
    }
}