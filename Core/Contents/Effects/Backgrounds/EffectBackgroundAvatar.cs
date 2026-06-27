
using Godot;
using Overlay.Core.Services.ColorInterpolators;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Overlay.Core.Contents.Effects.Backgrounds;

public sealed partial class EffectBackgroundAvatar() :
    ColorRect()
{
    [Export] public RichTextLabel NameplateName;
    [Export] public ColorRect     NameplateIcon;
    [Export] public Control       NameplatePivot;
    
    public override void _ExitTree()
    {
        this.RemoveInstance();
    }
    
    public override void _Process(
        double delta
    )
    {
        this.HandleTextAnimation();
        this.UpdateShaderResources(
            delta: (float) delta
        );
    }
    
    public override void _EnterTree()
    {
        this.AddInstance();
        this.RetrieveResources();
        this.SetShaderMaterial();
    }
    
    internal static void AdjustDancerSpeed(
        float intensity
    )
    {
        foreach (var instance in EffectBackgroundAvatar.Instances)
        {
            lock (instance.m_lock)
            {
                instance.m_speedScale = intensity;
            }
        }
    }
    
    internal static void ResetDancerSpeed()
    {
        EffectBackgroundAvatar.AdjustDancerSpeed(
            intensity: 1
        );
    }
    
    internal void SetNameplateBadgeColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        lock (this.m_lock)
        {
            this.m_nameplateBadgeColor = color;
        }
    }
    
    internal void SetNameplateTitle(
        string title
    )
    {
        lock (this.m_lock)
        {
            this.m_textTitle = title != string.Empty ? $"\n[outline_size=10][font_size=20]<{title}>" : string.Empty;
        }   
    }
    
    internal void SetNameplateUsername(
        string username
    )
    {
        lock (this.m_lock)
        {
            this.m_text = $"[font=res://Resources/Fonts/Roboto-Black.ttf][outline_color=#f2f2f2FF][color=#{EffectBackgroundAvatar.c_labelInterpolatorColor}][outline_size=16][font_size=32]{username}";
        }
    }
    
    internal void SetNameplateUsernameColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        lock (this.m_lock)
        {
            this.m_nameplateUsernameColor = color;
        }
    }


    internal void SetShaderColorBase(
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            this.m_colorModeBase = serviceColorInterpolatorColorMode;
        }
    }
    
    internal void SetShaderColorOutline(
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            this.m_colorModeOutline = serviceColorInterpolatorColorMode;
        }
    }
    
    internal void SetShaderEffectSlotColor(
        EffectBackgroundAvatarShaderSlot  effectBackgroundAvatarShaderSlot,
        ServiceColorInterpolatorColorMode serviceColorInterpolatorColorMode
    )
    {
        lock (this.m_lock)
        {
            switch (effectBackgroundAvatarShaderSlot)
            {
                case EffectBackgroundAvatarShaderSlot.ShaderSlot0:
                    this.m_colorModeEffectSlot0 = serviceColorInterpolatorColorMode;
                    break;
            
                case EffectBackgroundAvatarShaderSlot.ShaderSlot1:
                    this.m_colorModeEffectSlot1 = serviceColorInterpolatorColorMode;
                    break;
            
                case EffectBackgroundAvatarShaderSlot.ShaderSlot2:
                    this.m_colorModeEffectSlot2 = serviceColorInterpolatorColorMode;
                    break;
            
                case EffectBackgroundAvatarShaderSlot.ShaderSlot3:
                    this.m_colorModeEffectSlot3 = serviceColorInterpolatorColorMode;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(
                        paramName:   nameof(effectBackgroundAvatarShaderSlot), 
                        actualValue: effectBackgroundAvatarShaderSlot, 
                        message:     null
                    );
            }
        }
    }
    
    internal void SetShaderEffectSlotEffect(
        EffectBackgroundAvatarShaderSlot   effectBackgroundAvatarShaderSlot,
        EffectBackgroundAvatarShaderEffect effectBackgroundAvatarShaderEffect
    )
    {
        lock (this.m_lock)
        {
            this.m_material.SetShaderParameter(
                param: $"effect_slot_{(int) effectBackgroundAvatarShaderSlot}",
                value: (int) effectBackgroundAvatarShaderEffect
            );
        }
    }

    internal void SetShaderModel(
        EffectBackgroundAvatarShaderModel effectBackgroundAvatarShaderModel
    )
    {
        lock (this.m_lock)
        {
            this.m_material.SetShaderParameter(
                param: "model", 
                value: (int) effectBackgroundAvatarShaderModel
            );
            this.m_modelYOffset = EffectBackgroundAvatarModelNameplateOffsets.GetYOffsetForModel(
                model: effectBackgroundAvatarShaderModel
            );
        }
    }

    internal void ShowNameplate()
    {
        this.m_cancellationTokenSource.Cancel();
        this.m_cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken          = this.m_cancellationTokenSource.Token;
        
        Task.Run(
            function:          async () =>
            {
                this.NameplatePivot.CallDeferred(
                    method: CanvasItem.MethodName.SetVisible,
                    args: [ true ]
                );   
                
                await Task.Delay(
                    millisecondsDelay: EffectBackgroundAvatar.c_nameplateShowTimeInMilliseconds,
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                this.NameplatePivot.CallDeferred(
                    method: CanvasItem.MethodName.SetVisible,
                    args: [ false ]
                );   
            },
            cancellationToken: cancellationToken
        );
    }
    
    internal void ShowNameplateForFirstTime()
    {
        this.m_cancellationTokenSource.Cancel();
        this.m_cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken          = this.m_cancellationTokenSource.Token;
        
        Task.Run(
            function:          async () =>
            {
                this.NameplatePivot.CallDeferred(
                    method: CanvasItem.MethodName.SetVisible,
                    args: [ true ]
                );   
                
                await Task.Delay(
                    millisecondsDelay: EffectBackgroundAvatar.c_nameplateFirstTimeInMilliseconds,
                    cancellationToken: cancellationToken
                );

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                this.NameplatePivot.CallDeferred(
                    method: CanvasItem.MethodName.SetVisible,
                    args: [ false ]
                );   
            },
            cancellationToken: cancellationToken
        );
    }
    
    private const int                                    c_nameplateShowTimeInMilliseconds  = 12000;
    private const int                                    c_nameplateFirstTimeInMilliseconds = 24000;
    private const float                                  c_nameplateBasePositionX           = 512f;
    private const float                                  c_nameplateBasePositionY           = 512f;
    private const string                                 c_labelInterpolatorColor           = "00000000";
    
    private static readonly List<EffectBackgroundAvatar> Instances                          = [];
    
    private ServiceColorInterpolatorColorMode            m_colorModeBase                    = ServiceColorInterpolatorColorMode.CreamsicleBanana;
    private ServiceColorInterpolatorColorMode            m_colorModeEffectSlot0             = ServiceColorInterpolatorColorMode.CreamsicleBlueberry;
    private ServiceColorInterpolatorColorMode            m_colorModeEffectSlot1             = ServiceColorInterpolatorColorMode.CreamsicleDragonfruit;
    private ServiceColorInterpolatorColorMode            m_colorModeEffectSlot2             = ServiceColorInterpolatorColorMode.CreamsicleLime;
    private ServiceColorInterpolatorColorMode            m_colorModeEffectSlot3             = ServiceColorInterpolatorColorMode.CreamsicleOrange;
    private ServiceColorInterpolatorColorMode            m_colorModeOutline                 = ServiceColorInterpolatorColorMode.CreamsicleStrawberry;
    
    private float                                        m_audioTime                        = 0f;
    private ShaderMaterial                               m_badgeMaterial                    = null;
    private CancellationTokenSource                      m_cancellationTokenSource          = new();
    private readonly object                              m_lock                             = new();
    private ShaderMaterial                               m_material                         = null;
    private float                                        m_modelYOffset                     = 0f;
    private ServiceColorInterpolatorColorMode            m_nameplateBadgeColor              = ServiceColorInterpolatorColorMode.Transition;
    private ServiceColorInterpolatorColorMode            m_nameplateUsernameColor           = ServiceColorInterpolatorColorMode.Transition;
    private ServiceColorInterpolatorNormal               m_serviceColorInterpolatorNormal   = null;
    private ServiceColorInterpolatorInverse              m_serviceColorInterpolatorInverse  = null;
    private float                                        m_speedScale                       = 1f;
    private string                                       m_text                             = string.Empty;
    private string                                       m_textTitle                        = string.Empty;
    private float                                        m_timeOffset                       = 0f;

    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal  = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }
    
    private void AddInstance()
    {
        EffectBackgroundAvatar.Instances.Add(
            item: this
        );
    }

    private void HandleTextAnimation()
    {
        lock (this.m_lock)
        {
            var color = this.m_nameplateUsernameColor is ServiceColorInterpolatorColorMode.Transition ? 
                this.m_serviceColorInterpolatorNormal.GetColorByCurrentModeAsHex(
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0    
                ) :
                this.m_serviceColorInterpolatorNormal.GetColorByModeAsHex(
                    colorMode:      this.m_nameplateUsernameColor,
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0    
                );
            
            this.NameplateName.Text = this.m_text.Replace(
                oldValue: EffectBackgroundAvatar.c_labelInterpolatorColor,
                newValue: color
            ) + this.m_textTitle;
        }
    }
    
    private void RemoveInstance()
    {
        EffectBackgroundAvatar.Instances.Remove(
            item: this
        );
    }
    
    private void SetShaderMaterial()
    {
        this.Material     = (ShaderMaterial)this.Material.Duplicate();
        this.m_material   = (ShaderMaterial)this.Get(
            property: $"material"
        );
        this.m_timeOffset = GD.Randf() * 100f;
        
        this.NameplateIcon.Material = (ShaderMaterial)this.NameplateIcon.Material.Duplicate();
        this.m_badgeMaterial        = (ShaderMaterial)this.NameplateIcon.Get(
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
        var intensity = SpectrumMusicAnalyzer.Intensity;
        var seed      = this.GetHashCode() % 100;
        
        lock (this.m_lock)
        {
            var tempoMultiplier =  1f + (intensity * 1.5f * this.m_speedScale);
            this.m_audioTime    += delta * tempoMultiplier;
            
            var colorBase        = this.m_serviceColorInterpolatorNormal.GetColorByMode(
                colorMode:      this.m_colorModeBase,
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
            );
            var colorOutline     = this.m_serviceColorInterpolatorInverse.GetColorByMode(
                colorMode:      this.m_colorModeOutline,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            );
            var colorEffectSlot0 = this.m_serviceColorInterpolatorNormal.GetColorByMode(
                colorMode:      this.m_colorModeEffectSlot0,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            );
            var colorEffectSlot1 = this.m_serviceColorInterpolatorInverse.GetColorByMode(
                colorMode:      this.m_colorModeEffectSlot1,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            );
            var colorEffectSlot2 = this.m_serviceColorInterpolatorNormal.GetColorByMode(
                colorMode:      this.m_colorModeEffectSlot2,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            );
            var colorEffectSlot3 = this.m_serviceColorInterpolatorInverse.GetColorByMode(
                colorMode:      this.m_colorModeEffectSlot3,          
                colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0	
            );
            
            this.m_material.SetShaderParameter(
                param: "audio_time", 
                value: this.m_audioTime + this.m_timeOffset
            );
            this.m_material.SetShaderParameter(
                param: "music_intensity", 
                value: intensity
            );
            this.m_material.SetShaderParameter(
                param: $"color_base",
                value: colorBase
            );
            this.m_material.SetShaderParameter(
                param: $"color_outline",
                value: colorOutline
            );
            this.m_material.SetShaderParameter(
                param: $"color_effect_slot_0",
                value: colorEffectSlot0
            );
            this.m_material.SetShaderParameter(
                param: $"color_effect_slot_1",
                value: colorEffectSlot1
            );
            this.m_material.SetShaderParameter(
                param: $"color_effect_slot_2",
                value: colorEffectSlot2
            );
            this.m_material.SetShaderParameter(
                param: $"color_effect_slot_3",
                value: colorEffectSlot3
            );
            this.m_material.SetShaderParameter(
                param: "seed",
                value: seed
            );
            
            var colorBadge = this.m_nameplateBadgeColor is ServiceColorInterpolatorColorMode.Transition ? 
                this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0    
                ) :
                this.m_serviceColorInterpolatorInverse.GetColorByMode(
                    colorMode:      this.m_nameplateBadgeColor,
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0    
                );
            this.m_badgeMaterial.SetShaderParameter(
                param: "color",
                value: colorBadge
            );

            var position     = this.Position;
            var scaleTrigger = Math.Clamp(
                value: (position.Y - 1100f) / (1550f - 1100f), 
                min:   0f, 
                max:   1f
            );
            var sizeScalar   = 0.425f + 0.075f * scaleTrigger * 550;

            this.NameplatePivot.Position = new Vector2(
                x: EffectBackgroundAvatar.c_nameplateBasePositionX,
                y: EffectBackgroundAvatar.c_nameplateBasePositionY - this.m_modelYOffset - sizeScalar
            );
        }
    }
}