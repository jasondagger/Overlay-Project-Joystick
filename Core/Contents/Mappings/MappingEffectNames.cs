
#nullable enable
using Overlay.Core.Contents.Effects.Backgrounds;

namespace Overlay.Core.Contents.Mappings;

internal static class MappingEffectNames
{
    internal static EffectBackgroundAvatarShaderEffect? GetEffectByEffectName(
        string effectName
    )
    {
        return effectName switch
        {
            "base"               => EffectBackgroundAvatarShaderEffect.Base,
            "atomic particles"   => EffectBackgroundAvatarShaderEffect.AtomicParticles,
            "binary rain"        => EffectBackgroundAvatarShaderEffect.BinaryRain,
            "bio sparks"         => EffectBackgroundAvatarShaderEffect.BioSparks,
            "cellular armor"     => EffectBackgroundAvatarShaderEffect.CellularArmor,
            "circuit flow"       => EffectBackgroundAvatarShaderEffect.CircuitFlow,
            "clockwise sweep"    => EffectBackgroundAvatarShaderEffect.ClockwiseSweep,
            "data stream"        => EffectBackgroundAvatarShaderEffect.DataStream,
            "diagonal rain"      => EffectBackgroundAvatarShaderEffect.DiagonalRain,
            "diamond pulse"      => EffectBackgroundAvatarShaderEffect.DiamondPulse,
            "dot matrix"         => EffectBackgroundAvatarShaderEffect.DotMatrix,
            "electric cracks"    => EffectBackgroundAvatarShaderEffect.ElectricCracks,
            "energy scan"        => EffectBackgroundAvatarShaderEffect.EnergyScan,
            "falling shards"     => EffectBackgroundAvatarShaderEffect.FallingShards,
            "fractal zoom"       => EffectBackgroundAvatarShaderEffect.FractalZoom,
            "glitch slices"      => EffectBackgroundAvatarShaderEffect.GlitchSlices,
            "hex shield"         => EffectBackgroundAvatarShaderEffect.HexShield,
            "honeycomb pulse"    => EffectBackgroundAvatarShaderEffect.HoneycombPulse,
            "interference bars"  => EffectBackgroundAvatarShaderEffect.InterferenceBars,
            "laser sweep"        => EffectBackgroundAvatarShaderEffect.LaserSweep,
            "lava bubbles"       => EffectBackgroundAvatarShaderEffect.LavaBubbles,
            "matrix stripes"     => EffectBackgroundAvatarShaderEffect.MatrixStripes,
            "moire lines"        => EffectBackgroundAvatarShaderEffect.MoireLines,
            "moving arcs"        => EffectBackgroundAvatarShaderEffect.MovingArcs,
            "neural flash"       => EffectBackgroundAvatarShaderEffect.NeuralFlash,
            "outward ripples"    => EffectBackgroundAvatarShaderEffect.OutwardRipples,
            "overdrive bars"     => EffectBackgroundAvatarShaderEffect.OverdriveBars,
            "plasma drift"       => EffectBackgroundAvatarShaderEffect.PlasmaDrift,
            "power grid"         => EffectBackgroundAvatarShaderEffect.PowerGrid,
            "pulse wave"         => EffectBackgroundAvatarShaderEffect.PulseWave,
            "radar ring"         => EffectBackgroundAvatarShaderEffect.RadarRing,
            "rgb shift"          => EffectBackgroundAvatarShaderEffect.RgbShift,
            "rolling magma"      => EffectBackgroundAvatarShaderEffect.RollingMagma,
            "rotating cubes"     => EffectBackgroundAvatarShaderEffect.RotatingCubes,
            "scrolling vines"    => EffectBackgroundAvatarShaderEffect.ScrollingVines,
            "silk threads"       => EffectBackgroundAvatarShaderEffect.SilkThreads,
            "solar orbits"       => EffectBackgroundAvatarShaderEffect.SolarOrbits,
            "sonar pings"        => EffectBackgroundAvatarShaderEffect.SonarPings,
            "spinning vortex"    => EffectBackgroundAvatarShaderEffect.SpinningVortex,
            "square tiling"      => EffectBackgroundAvatarShaderEffect.SquareTiling,
            "static glitch"      => EffectBackgroundAvatarShaderEffect.StaticGlitch,
            "swarming nanites"   => EffectBackgroundAvatarShaderEffect.SwarmingNanites,
            "topo lines"         => EffectBackgroundAvatarShaderEffect.TopoLines,
            "tracking lines"     => EffectBackgroundAvatarShaderEffect.TrackingLines,
            "tunnel starfield"   => EffectBackgroundAvatarShaderEffect.TunnelStarfield,
            "vapor grid"         => EffectBackgroundAvatarShaderEffect.VaporGrid,
            "vertical bitstream" => EffectBackgroundAvatarShaderEffect.VerticalBitstream,
            "vertical drift"     => EffectBackgroundAvatarShaderEffect.VerticalDrift,
            "wind streaks"       => EffectBackgroundAvatarShaderEffect.WindStreaks,
            "zebra sweep"        => EffectBackgroundAvatarShaderEffect.ZebraSweep,
            _                    => null
        };
    }
    
    internal static string? GetEffectNameByEffect(
        EffectBackgroundAvatarShaderEffect effect
    )
    {
        return effect switch
        {
            EffectBackgroundAvatarShaderEffect.Base              => "base",
            EffectBackgroundAvatarShaderEffect.AtomicParticles   => "atomic particles",
            EffectBackgroundAvatarShaderEffect.BinaryRain        => "binary rain",
            EffectBackgroundAvatarShaderEffect.BioSparks         => "bio sparks",
            EffectBackgroundAvatarShaderEffect.CellularArmor     => "cellular armor",
            EffectBackgroundAvatarShaderEffect.CircuitFlow       => "circuit flow",
            EffectBackgroundAvatarShaderEffect.ClockwiseSweep    => "clockwise sweep",
            EffectBackgroundAvatarShaderEffect.DataStream        => "data stream",
            EffectBackgroundAvatarShaderEffect.DiagonalRain      => "diagonal rain",
            EffectBackgroundAvatarShaderEffect.DiamondPulse      => "diamond pulse",
            EffectBackgroundAvatarShaderEffect.DotMatrix         => "dot matrix",
            EffectBackgroundAvatarShaderEffect.ElectricCracks    => "electric cracks",
            EffectBackgroundAvatarShaderEffect.EnergyScan        => "energy scan",
            EffectBackgroundAvatarShaderEffect.FallingShards     => "falling shards",
            EffectBackgroundAvatarShaderEffect.FractalZoom       => "fractal zoom",
            EffectBackgroundAvatarShaderEffect.GlitchSlices      => "glitch slices",
            EffectBackgroundAvatarShaderEffect.HexShield         => "hex shield",
            EffectBackgroundAvatarShaderEffect.HoneycombPulse    => "honeycomb pulse",
            EffectBackgroundAvatarShaderEffect.InterferenceBars  => "interference bars",
            EffectBackgroundAvatarShaderEffect.LaserSweep        => "laser sweep",
            EffectBackgroundAvatarShaderEffect.LavaBubbles       => "lava bubbles",
            EffectBackgroundAvatarShaderEffect.MatrixStripes     => "matrix stripes",
            EffectBackgroundAvatarShaderEffect.MoireLines        => "moire lines",
            EffectBackgroundAvatarShaderEffect.MovingArcs        => "moving arcs",
            EffectBackgroundAvatarShaderEffect.NeuralFlash       => "neural flash",
            EffectBackgroundAvatarShaderEffect.OutwardRipples    => "outward ripples",
            EffectBackgroundAvatarShaderEffect.OverdriveBars     => "overdrive bars",
            EffectBackgroundAvatarShaderEffect.PlasmaDrift       => "plasma drift",
            EffectBackgroundAvatarShaderEffect.PowerGrid         => "power grid",
            EffectBackgroundAvatarShaderEffect.PulseWave         => "pulse wave",
            EffectBackgroundAvatarShaderEffect.RadarRing         => "radar ring",
            EffectBackgroundAvatarShaderEffect.RgbShift          => "rgb shift",
            EffectBackgroundAvatarShaderEffect.RollingMagma      => "rolling magma",
            EffectBackgroundAvatarShaderEffect.RotatingCubes     => "rotating cubes",
            EffectBackgroundAvatarShaderEffect.ScrollingVines    => "scrolling vines",
            EffectBackgroundAvatarShaderEffect.SilkThreads       => "silk threads",
            EffectBackgroundAvatarShaderEffect.SolarOrbits       => "solar orbits",
            EffectBackgroundAvatarShaderEffect.SonarPings        => "sonar pings",
            EffectBackgroundAvatarShaderEffect.SpinningVortex    => "spinning vortex",
            EffectBackgroundAvatarShaderEffect.SquareTiling      => "square tiling",
            EffectBackgroundAvatarShaderEffect.StaticGlitch      => "static glitch",
            EffectBackgroundAvatarShaderEffect.SwarmingNanites   => "swarming nanites",
            EffectBackgroundAvatarShaderEffect.TopoLines         => "topo lines",
            EffectBackgroundAvatarShaderEffect.TrackingLines     => "tracking lines",
            EffectBackgroundAvatarShaderEffect.TunnelStarfield   => "tunnel starfield",
            EffectBackgroundAvatarShaderEffect.VaporGrid         => "vapor grid",
            EffectBackgroundAvatarShaderEffect.VerticalBitstream => "vertical bitstream",
            EffectBackgroundAvatarShaderEffect.VerticalDrift     => "vertical drift",
            EffectBackgroundAvatarShaderEffect.WindStreaks       => "wind streaks",
            EffectBackgroundAvatarShaderEffect.ZebraSweep        => "zebra sweep",
            _                                                    => null
        };
    }
}