
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Rainbows;

public partial class EffectRainbowStripe() :
    ColorRect()
{
    [Export] public IServiceColorInterpolatorDefinition.ColorIndexType ColorIndex = IServiceColorInterpolatorDefinition.ColorIndexType.Color0;
    
    public override void _Process(
        double delta
    )
    {
        this.UpdateShaderResources(
            delta: (float) delta
        );
    }
    
    public override void _Ready()
    {
        this.RetrieveResources();
        this.SetShaderMaterial();
    }
    
    internal static void AdjustStripeScrollSpeed(
        float intensity
    )
    {
        lock (EffectRainbowStripe.s_lock)
        {
            EffectRainbowStripe.s_speedScale = intensity;
        }
    }
    
    internal static void ResetStripeScrollSpeed()
    {
        EffectRainbowStripe.AdjustStripeScrollSpeed(
            intensity: 1
        );
    }
    
    internal static void UpdateGlobalColorInverse(
        ServiceColorInterpolatorColorMode color
    )
    {
        lock (EffectRainbowStripe.s_lock)
        {
            EffectRainbowStripe.s_colorInverse = color;
        }
    }
    
    internal static void UpdateGlobalColorNormal(
        ServiceColorInterpolatorColorMode color
    )
    {
        lock (EffectRainbowStripe.s_lock)
        {
            EffectRainbowStripe.s_colorNormal = color;
        }
    }

    internal static void UpdateGlobalPhase(
        float delta
    )
    {
        var musicMultiplier = 0.2f + SpectrumMusicAnalyzer.Intensity;
        
        lock (EffectRainbowStripe.s_lock)
        {
            var step                          = delta * EffectRainbowStripe.s_sharedSpeedOffset * EffectRainbowStripe.s_speedScale * musicMultiplier;
            EffectRainbowStripe.s_globalPhase = Mathf.PosMod(
                a: EffectRainbowStripe.s_globalPhase + step, 
                b: 3600f
            );
        }
    }

    internal void SetEffectSlotEffect(
        int slotIndex,
        int effectIndex
    )
    {
        this.m_material.SetShaderParameter(
            param: $"effect_slot_{slotIndex}", 
            value: effectIndex
        );
    }
    
    private static ServiceColorInterpolatorColorMode  s_colorInverse                    = ServiceColorInterpolatorColorMode.Transition;
    private static ServiceColorInterpolatorColorMode  s_colorNormal                     = ServiceColorInterpolatorColorMode.Transition;
    private static float                              s_globalPhase                     = 0f;
    private static readonly object                    s_lock                            = new();
    private static readonly float                     s_sharedSpeedOffset               = (float)GD.RandRange(
        from: EffectRainbowStripe.c_speedMinimum, 
        to:   EffectRainbowStripe.c_speedMaximum
    );
    private static float                              s_speedScale                      = 1f;
    
    private const float                               c_speedMaximum                    = 1.05f;
    private const float                               c_speedMinimum                    = 1f;
    
    private readonly int[]                            m_activeEffectSlots               = new int[5];
    private ShaderMaterial                            m_material                        = null;
    private readonly Color[]                          m_paletteCache                    = new Color[6];
    private readonly Color[]                          m_paletteAltCache                 = new Color[6];
    private ServiceColorInterpolatorNormal            m_serviceColorInterpolator        = null;
    private ServiceColorInterpolatorInverse           m_serviceColorInterpolatorInverse = null;
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolator        = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.UpdateShaderResources(
            delta: 0f
        );
    }
    
    private void UpdateShaderResources(
        float delta
    )
    {
        ServiceColorInterpolatorColorMode globalColorInverse;
        ServiceColorInterpolatorColorMode globalColorNormal;
        float globalPhase;
        lock (EffectRainbowStripe.s_lock)
        {
            globalColorInverse = EffectRainbowStripe.s_colorInverse;
            globalColorNormal  = EffectRainbowStripe.s_colorNormal;
            globalPhase        = EffectRainbowStripe.s_globalPhase;
        }
        
        for (var i = 0; i < 6; i++) {
            var colorIndexType = (IServiceColorInterpolatorDefinition.ColorIndexType)i;
            if (EffectRainbowStripe.s_colorNormal is ServiceColorInterpolatorColorMode.Transition)
            {
                this.m_paletteCache[i]    = this.m_serviceColorInterpolator.GetColorByCurrentMode(
                    colorIndexType: colorIndexType
                );
            }
            else
            {
                this.m_paletteCache[i]    = this.m_serviceColorInterpolator.GetColorByMode(
                    colorIndexType: colorIndexType,
                    colorMode:      globalColorNormal
                );
            }

            if (EffectRainbowStripe.s_colorInverse is ServiceColorInterpolatorColorMode.Transition)
            {
                this.m_paletteAltCache[i] = this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                    colorIndexType: colorIndexType
                );
            }
            else
            {
                this.m_paletteAltCache[i] = this.m_serviceColorInterpolatorInverse.GetColorByMode(
                    colorIndexType: colorIndexType,
                    colorMode:      globalColorInverse
                );
            }
        }
        
        this.m_material.SetShaderParameter(
            param: "palette", 
            value: this.m_paletteCache
        );
        this.m_material.SetShaderParameter(
            param: "palette_alt", 
            value: this.m_paletteAltCache
        );
        this.m_material.SetShaderParameter(
            param: "band_id", 
            value: (int)this.ColorIndex
        );
        this.m_material.SetShaderParameter(
            param: "audio_time", 
            value: Time.GetTicksMsec() / 1000.0f
        );
        this.m_material.SetShaderParameter(
            param: "music_intensity", 
            value: SpectrumMusicAnalyzer.Intensity
        );
        this.m_material.SetShaderParameter(
            param: "global_phase", 
            value: globalPhase
        );
    }
}