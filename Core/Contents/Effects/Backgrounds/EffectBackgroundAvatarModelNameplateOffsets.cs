
using System;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Backgrounds;

internal static class EffectBackgroundAvatarModelNameplateOffsets
{
    internal static float GetYOffsetForModel(
        EffectBackgroundAvatarShaderModel model
    )
    {
        if (
            EffectBackgroundAvatarModelNameplateOffsets.s_modelYOffsets.TryGetValue(
                key:   model,
                value: out var yOffset
            ) is true
        )
        {
            return yOffset;
        }

        throw new NotImplementedException();
    }

    private static readonly Dictionary<EffectBackgroundAvatarShaderModel, float> s_modelYOffsets = new()
    {
        { EffectBackgroundAvatarShaderModel.Airplane,         136.0f },
        { EffectBackgroundAvatarShaderModel.Asteroid,         144.0f },
        { EffectBackgroundAvatarShaderModel.Banana,           144.0f },
        { EffectBackgroundAvatarShaderModel.Bone,             124.0f },
        { EffectBackgroundAvatarShaderModel.Boobs,            124.0f },
        { EffectBackgroundAvatarShaderModel.Brain,            124.0f },
        { EffectBackgroundAvatarShaderModel.Branch,           144.0f },
        { EffectBackgroundAvatarShaderModel.Bread,            144.0f },
        { EffectBackgroundAvatarShaderModel.Cloud,            156.0f },
        { EffectBackgroundAvatarShaderModel.CompanionCube,    140.0f },
        { EffectBackgroundAvatarShaderModel.DeepSeaJellyfish, 152.0f },
        { EffectBackgroundAvatarShaderModel.Die,              120.0f },
        { EffectBackgroundAvatarShaderModel.Dinosaur,         128.0f },
        { EffectBackgroundAvatarShaderModel.Donut,            144.0f },
        { EffectBackgroundAvatarShaderModel.DoubleHelix,      132.0f },
        { EffectBackgroundAvatarShaderModel.Dugtrio,          124.0f },
        { EffectBackgroundAvatarShaderModel.Egg,              144.0f },
        { EffectBackgroundAvatarShaderModel.Flask,            132.0f },
        { EffectBackgroundAvatarShaderModel.FryingPan,        152.0f },
        { EffectBackgroundAvatarShaderModel.Gears,            128.0f },
        { EffectBackgroundAvatarShaderModel.Ghost,            148.0f },
        { EffectBackgroundAvatarShaderModel.GLADoS,           208.0f },
        { EffectBackgroundAvatarShaderModel.Gun,              124.0f },
        { EffectBackgroundAvatarShaderModel.Hand,             124.0f },
        { EffectBackgroundAvatarShaderModel.HatsuneMiku,      128.0f },
        { EffectBackgroundAvatarShaderModel.Heart,            128.0f },
        { EffectBackgroundAvatarShaderModel.Human,            160.0f },
        { EffectBackgroundAvatarShaderModel.Jellyfish,        112.0f },
        { EffectBackgroundAvatarShaderModel.Katana,           200.0f },
        { EffectBackgroundAvatarShaderModel.Mushroom,         124.0f },
        { EffectBackgroundAvatarShaderModel.Octopus,          144.0f },
        { EffectBackgroundAvatarShaderModel.Penis,            148.0f },
        { EffectBackgroundAvatarShaderModel.Pikmin,           120.0f },
        { EffectBackgroundAvatarShaderModel.Pokeball,         136.0f },
        { EffectBackgroundAvatarShaderModel.Potato,           148.0f },
        { EffectBackgroundAvatarShaderModel.Robot,            152.0f },
        { EffectBackgroundAvatarShaderModel.Rocket,           164.0f },
        { EffectBackgroundAvatarShaderModel.Rook,             132.0f },
        { EffectBackgroundAvatarShaderModel.Sentry,           112.0f },
        { EffectBackgroundAvatarShaderModel.Snowman,          164.0f },
        { EffectBackgroundAvatarShaderModel.SolarSystem,      120.0f },
        { EffectBackgroundAvatarShaderModel.Spider,           112.0f },
        { EffectBackgroundAvatarShaderModel.Squid,            156.0f },
        { EffectBackgroundAvatarShaderModel.Star,             164.0f },
        { EffectBackgroundAvatarShaderModel.StickyBomb,       144.0f },
        { EffectBackgroundAvatarShaderModel.Tank,             124.0f },
        { EffectBackgroundAvatarShaderModel.TieFighter,       136.0f },
        { EffectBackgroundAvatarShaderModel.Tree,             140.0f },
        { EffectBackgroundAvatarShaderModel.Triangle,         144.0f },
        { EffectBackgroundAvatarShaderModel.UFO,              136.0f },
        { EffectBackgroundAvatarShaderModel.XWing,            136.0f },
    };
}