
#nullable enable
using Overlay.Core.Contents.Effects.Backgrounds;

namespace Overlay.Core.Contents.Mappings;

internal static class MappingModelNames
{
    internal static EffectBackgroundAvatarShaderModel? GetModelByModelName(
        string modelName
    )
    {
        return modelName switch
        {
            "human"              => EffectBackgroundAvatarShaderModel.Human,
            "airplane"           => EffectBackgroundAvatarShaderModel.Airplane,
            "asteroid"           => EffectBackgroundAvatarShaderModel.Asteroid,
            "banana"             => EffectBackgroundAvatarShaderModel.Banana,
            "bone"               => EffectBackgroundAvatarShaderModel.Bone,
            "boobs"              => EffectBackgroundAvatarShaderModel.Boobs,
            "brain"              => EffectBackgroundAvatarShaderModel.Brain,
            "branch"             => EffectBackgroundAvatarShaderModel.Branch,
            "bread"              => EffectBackgroundAvatarShaderModel.Bread,
            "cloud"              => EffectBackgroundAvatarShaderModel.Cloud,
            "companion cube"     => EffectBackgroundAvatarShaderModel.CompanionCube,
            "deep sea jellyfish" => EffectBackgroundAvatarShaderModel.DeepSeaJellyfish,
            "die"                => EffectBackgroundAvatarShaderModel.Die,
            "dinosaur"           => EffectBackgroundAvatarShaderModel.Dinosaur,
            "donut"              => EffectBackgroundAvatarShaderModel.Donut,
            "double helix"       => EffectBackgroundAvatarShaderModel.DoubleHelix,
            "dugtrio"            => EffectBackgroundAvatarShaderModel.Dugtrio,
            "egg"                => EffectBackgroundAvatarShaderModel.Egg,
            "flask"              => EffectBackgroundAvatarShaderModel.Flask,
            "frying pan"         => EffectBackgroundAvatarShaderModel.FryingPan,
            "gears"              => EffectBackgroundAvatarShaderModel.Gears,
            "ghost"              => EffectBackgroundAvatarShaderModel.Ghost,
            "glados"             => EffectBackgroundAvatarShaderModel.GLADoS,
            "gun"                => EffectBackgroundAvatarShaderModel.Gun,
            "hand"               => EffectBackgroundAvatarShaderModel.Hand,
            "hatsune miku"       => EffectBackgroundAvatarShaderModel.HatsuneMiku,
            "heart"              => EffectBackgroundAvatarShaderModel.Heart,
            "jellyfish"          => EffectBackgroundAvatarShaderModel.Jellyfish,
            "katana"             => EffectBackgroundAvatarShaderModel.Katana,
            "mushroom"           => EffectBackgroundAvatarShaderModel.Mushroom,
            "octopus"            => EffectBackgroundAvatarShaderModel.Octopus,
            "penis"              => EffectBackgroundAvatarShaderModel.Penis,
            "pikmin"             => EffectBackgroundAvatarShaderModel.Pikmin,
            "pokeball"           => EffectBackgroundAvatarShaderModel.Pokeball,
            "potato"             => EffectBackgroundAvatarShaderModel.Potato,
            "robot"              => EffectBackgroundAvatarShaderModel.Robot,
            "rocket"             => EffectBackgroundAvatarShaderModel.Rocket,
            "rook"               => EffectBackgroundAvatarShaderModel.Rook,
            "sentry"             => EffectBackgroundAvatarShaderModel.Sentry,
            "snowman"            => EffectBackgroundAvatarShaderModel.Snowman,
            "solar system"       => EffectBackgroundAvatarShaderModel.SolarSystem,
            "spider"             => EffectBackgroundAvatarShaderModel.Spider,
            "squid"              => EffectBackgroundAvatarShaderModel.Squid,
            "star"               => EffectBackgroundAvatarShaderModel.Star,
            "sticky bomb"        => EffectBackgroundAvatarShaderModel.StickyBomb,
            "tank"               => EffectBackgroundAvatarShaderModel.Tank,
            "tie fighter"        => EffectBackgroundAvatarShaderModel.TieFighter,
            "tree"               => EffectBackgroundAvatarShaderModel.Tree,
            "triangle"           => EffectBackgroundAvatarShaderModel.Triangle,
            "ufo"                => EffectBackgroundAvatarShaderModel.UFO,
            "xwing"              => EffectBackgroundAvatarShaderModel.XWing,
            _                    => null
        };
    }
    
    internal static string? GetModelNameByModel(
        EffectBackgroundAvatarShaderModel model
    )
    {
        return model switch
        {
            EffectBackgroundAvatarShaderModel.Human            => "human",
            EffectBackgroundAvatarShaderModel.Airplane         => "airplane",
            EffectBackgroundAvatarShaderModel.Asteroid         => "asteroid",
            EffectBackgroundAvatarShaderModel.Banana           => "banana",
            EffectBackgroundAvatarShaderModel.Bone             => "bone",
            EffectBackgroundAvatarShaderModel.Boobs            => "boobs",
            EffectBackgroundAvatarShaderModel.Brain            => "brain",
            EffectBackgroundAvatarShaderModel.Branch           => "branch",
            EffectBackgroundAvatarShaderModel.Bread            => "bread",
            EffectBackgroundAvatarShaderModel.Cloud            => "cloud",
            EffectBackgroundAvatarShaderModel.CompanionCube    => "companion cube",
            EffectBackgroundAvatarShaderModel.DeepSeaJellyfish => "deep sea jellyfish",
            EffectBackgroundAvatarShaderModel.Die              => "die",
            EffectBackgroundAvatarShaderModel.Dinosaur         => "dinosaur",
            EffectBackgroundAvatarShaderModel.Donut            => "donut",
            EffectBackgroundAvatarShaderModel.DoubleHelix      => "double helix",
            EffectBackgroundAvatarShaderModel.Dugtrio          => "dugtrio",
            EffectBackgroundAvatarShaderModel.Egg              => "egg",
            EffectBackgroundAvatarShaderModel.Flask            => "flask",
            EffectBackgroundAvatarShaderModel.FryingPan        => "frying pan",
            EffectBackgroundAvatarShaderModel.Gears            => "gears",
            EffectBackgroundAvatarShaderModel.Ghost            => "ghost",
            EffectBackgroundAvatarShaderModel.GLADoS           => "glados",
            EffectBackgroundAvatarShaderModel.Gun              => "gun",
            EffectBackgroundAvatarShaderModel.Hand             => "hand",
            EffectBackgroundAvatarShaderModel.HatsuneMiku      => "hatsune miku",
            EffectBackgroundAvatarShaderModel.Heart            => "heart",
            EffectBackgroundAvatarShaderModel.Jellyfish        => "jellyfish",
            EffectBackgroundAvatarShaderModel.Katana           => "katana",
            EffectBackgroundAvatarShaderModel.Mushroom         => "mushroom",
            EffectBackgroundAvatarShaderModel.Octopus          => "octopus",
            EffectBackgroundAvatarShaderModel.Penis            => "penis",
            EffectBackgroundAvatarShaderModel.Pikmin           => "pikmin",
            EffectBackgroundAvatarShaderModel.Pokeball         => "pokeball",
            EffectBackgroundAvatarShaderModel.Potato           => "potato",
            EffectBackgroundAvatarShaderModel.Robot            => "robot",
            EffectBackgroundAvatarShaderModel.Rocket           => "rocket",
            EffectBackgroundAvatarShaderModel.Rook             => "rook",
            EffectBackgroundAvatarShaderModel.Sentry           => "sentry",
            EffectBackgroundAvatarShaderModel.Snowman          => "snowman",
            EffectBackgroundAvatarShaderModel.SolarSystem      => "solar system",
            EffectBackgroundAvatarShaderModel.Spider           => "spider",
            EffectBackgroundAvatarShaderModel.Squid            => "squid",
            EffectBackgroundAvatarShaderModel.Star             => "star",
            EffectBackgroundAvatarShaderModel.StickyBomb       => "sticky bomb",
            EffectBackgroundAvatarShaderModel.Tank             => "tank",
            EffectBackgroundAvatarShaderModel.TieFighter       => "tie fighter",
            EffectBackgroundAvatarShaderModel.Tree             => "tree",
            EffectBackgroundAvatarShaderModel.Triangle         => "triangle",
            EffectBackgroundAvatarShaderModel.UFO              => "ufo",
            EffectBackgroundAvatarShaderModel.XWing            => "xwing",
            _                                                  => null
        };
    }
}