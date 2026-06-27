
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundSnow() :
    ColorRect()
{
    internal static EffectBackgroundSnow Instance { get; private set; }

    public override void _EnterTree()
    {
        this.SetInstance();
    }
    
    public override void _ExitTree()
    {
        this.m_cancellationTokenSource.Cancel();
        this.m_cancellationTokenSource.Dispose();
    }
    
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
        this.LoadTextures();
        this.SetShaderMaterial();
    }

    internal enum ParticleTexture
    {
        Banana,
        Drink,
        Heart,
        MarijuanaLeaf,
        MoneyBag,
        Penis,
        Plane,
        Snowflake,
        Star,
        TF2_BigEarner,
        TF2_Death,
        TF2_DiamondBack,
        TF2_GrenadeLauncher,
        TF2_Pan,
        TF2_StickyLauncher,
        Turkey,
    }

    internal void AdjustScrollSpeed(
        float intensity
    )
    {
        this.m_lovenseIntensity = intensity;
    }

    internal void ResetParticleTexture()
    {
        this.SetParticleTexture(
            particleTexture: this.m_storedParticleTexture
        );
    }

    internal void ResetScrollSpeed()
    {
        this.AdjustScrollSpeed(
            intensity: 1
        );
    }

    internal void SetStoreAndStartResetTimerForParticleTexture(
        ParticleTexture particleTexture
    )
    {
        this.SetAndStoreParticleTexture(
            particleTexture: particleTexture
        );
        this.StartResetTimer();
    }
    
    internal void SetAndStoreParticleTexture(
        ParticleTexture particleTexture
    )
    {
        this.SetParticleTexture(
            particleTexture: particleTexture
        );
        this.m_storedParticleTexture = particleTexture;
        this.m_cancellationTokenSource.Cancel();
    }
    
    internal void SetParticleTexture(
        ParticleTexture particleTexture
    )
    {
        this.m_material.SetShaderParameter(
            param: $"particle_texture",
            value: this.m_particleTextures[key: particleTexture]
        );
    }

    private const string                                    c_pathParticleTextureBanana              = "res://Resources/Textures/Icons/Icon_Banana.png";
    private const string                                    c_pathParticleTextureDrink               = "res://Resources/Textures/Icons/Icon_Drink.png";
    private const string                                    c_pathParticleTextureHeart               = "res://Resources/Textures/Icons/Icon_Heart.png";
    private const string                                    c_pathParticleTextureMarijuanaLeaf       = "res://Resources/Textures/Icons/Icon_MarijuanaLeaf.png";
    private const string                                    c_pathParticleTextureMoneyBag            = "res://Resources/Textures/Icons/Icon_MoneyBag.png";
    private const string                                    c_pathParticleTexturePenis               = "res://Resources/Textures/Icons/Icon_Penis.png";
    private const string                                    c_pathParticleTexturePlane               = "res://Resources/Textures/Icons/Icon_Plane.png";
    private const string                                    c_pathParticleTextureSnowflake           = "res://Resources/Textures/Icons/Icon_Snowflake.png";
    private const string                                    c_pathParticleTextureStar                = "res://Resources/Textures/Icons/Icon_Star.png";
    private const string                                    c_pathParticleTextureTF2_BigEarner       = "res://Resources/Textures/Icons/Icon_TF2_BigEarner.png";
    private const string                                    c_pathParticleTextureTF2_Death           = "res://Resources/Textures/Icons/Icon_TF2_Death.png";
    private const string                                    c_pathParticleTextureTF2_DiamondBack     = "res://Resources/Textures/Icons/Icon_TF2_DiamondBack.png";
    private const string                                    c_pathParticleTextureTF2_GrenadeLauncher = "res://Resources/Textures/Icons/Icon_TF2_GrenadeLauncher.png";
    private const string                                    c_pathParticleTextureTF2_Pan             = "res://Resources/Textures/Icons/Icon_TF2_Pan.png";
    private const string                                    c_pathParticleTextureTF2_StickyLauncher  = "res://Resources/Textures/Icons/Icon_TF2_StickyLauncher.png";
    private const string                                    c_pathParticleTextureTurkey              = "res://Resources/Textures/Icons/Icon_Turkey.png";
    
    private const int                                       c_timerDelayInMilliseconds               = 900000;
    
    private float                                           m_audioTime                              = 1.0f;
    private CancellationTokenSource                         m_cancellationTokenSource                = new();
    private float                                           m_lovenseIntensity                       = 1.0f;
    private ShaderMaterial                                  m_material                               = null;
    private readonly Dictionary<ParticleTexture, Texture2D> m_particleTextures                       = new();
    private ServiceColorInterpolatorNormal                  m_serviceColorInterpolatorNormal         = null;
    private ServiceColorInterpolatorInverse                 m_serviceColorInterpolatorInverse        = null;
    private ParticleTexture                                 m_storedParticleTexture                  = ParticleTexture.Snowflake;
    
    private void LoadTextures()
    {
        this.m_particleTextures.Add(
            key:   ParticleTexture.Banana,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureBanana
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Drink,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureDrink
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Heart,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureHeart
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.MarijuanaLeaf,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureMarijuanaLeaf
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.MoneyBag,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureMoneyBag
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Penis,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTexturePenis
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Plane,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTexturePlane
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Snowflake,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureSnowflake
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Star,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureStar
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.TF2_BigEarner,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTF2_BigEarner
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.TF2_Death,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTF2_Death
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.TF2_DiamondBack,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTF2_DiamondBack
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.TF2_GrenadeLauncher,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTF2_GrenadeLauncher
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.TF2_Pan,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTF2_Pan
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.TF2_StickyLauncher,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTF2_StickyLauncher
            )
        );
        this.m_particleTextures.Add(
            key:   ParticleTexture.Turkey,
            value: GD.Load<Texture2D>(
                path: EffectBackgroundSnow.c_pathParticleTextureTurkey
            )
        );
    }
    
    internal void ResetToDefault()
    {
        this.SetAndStoreParticleTexture(
            particleTexture: ParticleTexture.Snowflake
        );
    }
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal  = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }

    private void SetInstance()
    {
        EffectBackgroundSnow.Instance = this;
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
            )
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
            )
        );
    }
    
    private void StartResetTimer()
    {
        this.m_cancellationTokenSource.Cancel();
        this.m_cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken          = this.m_cancellationTokenSource.Token;

        Task.Run(
            function: async () =>
            {
                await Task.Delay(
                    millisecondsDelay: EffectBackgroundSnow.c_timerDelayInMilliseconds, 
                    cancellationToken: cancellationToken
                );
    
                if (cancellationToken.IsCancellationRequested is false)
                {
                    this.CallDeferred(
                        method: nameof(this.ResetToDefault)
                    );
                }
            }, 
            cancellationToken: cancellationToken
        );
    }
    
    private void UpdateShaderResources(
        float delta
    )
    {
        var audioIntensity  = 1f + SpectrumMusicAnalyzer.Intensity * 0.5f; 
        this.m_audioTime   += delta * audioIntensity * this.m_lovenseIntensity;
        
        if (this.m_audioTime > 10000f) 
        {
            this.m_audioTime -= 10000f;
        }
        
        this.m_material.SetShaderParameter(
            param: $"audio_time",
            value: this.m_audioTime
        );
        this.m_material.SetShaderParameter(
            param: $"alt_color",
            value: this.m_serviceColorInterpolatorNormal.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
            )
        );
        this.m_material.SetShaderParameter(
            param: $"color",
            value: this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
            )
        );
    }
}