
using Godot;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Rainbows;

public sealed partial class EffectRainbowExtender() : 
    Control()
{
    [Export] public ColorRect[] ColorRectBorders  = _ = new ColorRect[2];
    [Export] public ColorRect[] ColorRectRainbows = _ = new ColorRect[6];

    public override void _Process(
        double delta
    )
    {
        this.ProcessRainbowState(
            delta: _ = (float)delta
        );
    }

    public override void _Ready()
    {
        this.RetrieveResources();
    }

    private enum RainbowColorType :
        uint
    {
        Red = 0U,
        Yellow,
        Green,
        Cyan,
        Blue,
        Magenta,
    }

    private enum RainbowStateType :
        uint
    {
        Hidden = 0U,
        Hide,
        Show,
        Shown,
    }

    private sealed class RainbowColorRect()
    {
        internal ColorRect ColorRect = null;
        internal float     Delay     = 0f;
    }

    private static readonly RandomNumberGenerator                   s_randomNumberGenerator            = new();
    
    private const float                                             c_targetElapsedToShowMax           = 25f;
    private const float                                             c_targetElapsedToShowMin           = 5f;
    private const float                                             c_targetElapsedToStayHidden        = 2f;
    private const float                                             c_delayTimerMax                    = 0.025f;
    private const float                                             c_delayTimerMin                    = 0.075f;
    private const float                                             c_colorRectSpeed                   = 5000f;
    private const int                                               c_lengthOfColorRect                = 2816;

    private readonly Dictionary<RainbowColorType, RainbowColorRect> m_rainbowColorRects                = new()
    {
        { RainbowColorType.Red,     new RainbowColorRect() },
        { RainbowColorType.Yellow,  new RainbowColorRect() },
        { RainbowColorType.Green,   new RainbowColorRect() },
        { RainbowColorType.Cyan,    new RainbowColorRect() },
        { RainbowColorType.Blue,    new RainbowColorRect() },
        { RainbowColorType.Magenta, new RainbowColorRect() },
    };
    private readonly RainbowColorRect[]                             m_rainbowColorRectBorders          = 
    [
        new RainbowColorRect(),
        new RainbowColorRect()
    ];
    private RainbowColorType[]                                      m_rainbowColorAnimationOrder       = _ = EnumHelper.GetRandomizedValues<RainbowColorType>();
    private RainbowStateType                                        m_currentRainbowState              = _ = RainbowStateType.Shown;
    private float                                                   m_elapsed                          = _ = 0f;
    private float                                                   m_targetElapsed                    = _ = 3;
    
    private static float GenerateRandomTargetElapsedTimeForShown()
    {
        return _ = EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
            from: _ = EffectRainbowExtender.c_targetElapsedToShowMin,
            to:   _ = EffectRainbowExtender.c_targetElapsedToShowMax
        );
    }

    private void HandleRainbowHidden(
        float delta
    )
    {
        _ = this.m_elapsed += _ = delta;
        if (_ = this.m_elapsed < this.m_targetElapsed)
        {
            return;
        }
        
        _ = this.m_currentRainbowState        = _ = RainbowStateType.Show;
        _ = this.m_elapsed                    = _ = 0f;
        _ = this.m_rainbowColorAnimationOrder = _ = EnumHelper.GetRandomizedValues<RainbowColorType>();
        
        this.RandomizeColorRectDelayTimes();
    }
    
    private void HandleRainbowHide(
        float delta
    )
    {
        _ = this.m_elapsed += _ = delta;
        var offset = _ = delta * EffectRainbowExtender.c_colorRectSpeed;
        
        // update each color bar position
        foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
        {
            var rainbowColorRect = _ = this.m_rainbowColorRects[key: _ = rainbowColorType];

            if (_ = this.m_elapsed < rainbowColorRect.Delay)
            {
                continue;
            }
            
            var colorRect = _ = rainbowColorRect.ColorRect;
            var position  = _ = colorRect.Position;

            _ = position.Y += _ = offset;
                
            _ = colorRect.Position = _ = position;
        }
        
        // update each white border position
        foreach (var rainbowColorRectBorder in this.m_rainbowColorRectBorders)
        {
            if (_ = this.m_elapsed < rainbowColorRectBorder.Delay)
            {
                continue;
            }
            
            var colorRect = _ = rainbowColorRectBorder.ColorRect;
            var position  = _ = colorRect.Position;

            _ = position.Y += _ = offset;
                
            _ = colorRect.Position = _ = position;
        }

        var colorRectBorder = _ = this.ColorRectBorders[1];
        var colorRectBorderPosition = _ = colorRectBorder.Position;
        if (_ = colorRectBorderPosition.Y < EffectRainbowExtender.c_lengthOfColorRect)
        {
            return;
        }
        
        // reset positions & sizes
        foreach (var colorRect in this.ColorRectBorders)
        {
            var position  = _ = colorRect.Position;
            var size      = _ = colorRect.Size;

            position.Y = _ = 0f;
            size.X     = _ = 0f;
                
            _ = colorRect.Position = _ = position;
            _ = colorRect.Size     = _ = size;
        }
        foreach (var colorRect in this.ColorRectRainbows)
        {
            var position  = _ = colorRect.Position;
            var size      = _ = colorRect.Size;

            position.Y = _ = 0f;
            size.X     = _ = 0f;
                
            _ = colorRect.Position = _ = position;
            _ = colorRect.Size     = _ = size;
        }

        // move to hidden state
        _ = this.m_currentRainbowState = _ = RainbowStateType.Hidden;
        _ = this.m_elapsed             = _ = 0f;
        _ = this.m_targetElapsed       = _ = EffectRainbowExtender.c_targetElapsedToStayHidden;
    }
    
    private void HandleRainbowShow(
        float delta
    )
    {
        _ = this.m_elapsed += _ = delta;
        var offset = _ = delta * EffectRainbowExtender.c_colorRectSpeed;
        
        // update each bar size
        foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
        {
            var rainbowColorRect = _ = this.m_rainbowColorRects[key: _ = rainbowColorType];

            if (_ = this.m_elapsed < rainbowColorRect.Delay)
            {
                continue;
            }
            
            var colorRect = _ = rainbowColorRect.ColorRect;
            var size      = _ = colorRect.Size;

            _ = size.X += _ = offset;
                
            _ = colorRect.Size = _ = size;
        }
        
        // update each white border size
        foreach (var rainbowColorRectBorder in this.m_rainbowColorRectBorders)
        {
            if (_ = this.m_elapsed < rainbowColorRectBorder.Delay)
            {
                continue;
            }
            
            var colorRect = _ = rainbowColorRectBorder.ColorRect;
            var size      = _ = colorRect.Size;

            _ = size.X += _ = offset;
                
            _ = colorRect.Size = _ = size;
        }

        var colorRectBorder     = _ = this.ColorRectBorders[1];
        var colorRectBorderSize = _ = colorRectBorder.Size;
        if (_ = colorRectBorderSize.X < EffectRainbowExtender.c_lengthOfColorRect)
        {
            return;
        }
        
        _ = this.m_currentRainbowState = _ = RainbowStateType.Shown;
        _ = this.m_elapsed             = _ = 0f;
        _ = this.m_targetElapsed       = _ = EffectRainbowExtender.GenerateRandomTargetElapsedTimeForShown();
    }

    private void HandleRainbowShown(
        float delta
    )
    {
        _ = this.m_elapsed += _ = delta;
        if (_ = this.m_elapsed < this.m_targetElapsed)
        {
            return;
        }
        
        _ = this.m_currentRainbowState        = _ = RainbowStateType.Hide;
        _ = this.m_elapsed                    = _ = 0f;
        _ = this.m_rainbowColorAnimationOrder = _ = EnumHelper.GetRandomizedValues<RainbowColorType>();

        this.RandomizeColorRectDelayTimes();
    }

    private void ProcessRainbowState(
        float delta
    )
    {
        switch (_ = this.m_currentRainbowState)
        {
            case RainbowStateType.Hidden:
                this.HandleRainbowHidden(
                    delta: _ = delta
                );
                break;
            
            case RainbowStateType.Hide:
                this.HandleRainbowHide(
                    delta: _ = delta
                );
                break;
            
            case RainbowStateType.Show:
                this.HandleRainbowShow(
                    delta: _ = delta
                );
                break;
            
            case RainbowStateType.Shown:
                this.HandleRainbowShown(
                    delta: _ = delta
                );
                break;
            
            default:
                return;
        }
    }
    
    private void RandomizeColorRectDelayTimes()
    {
        var offset = _ = 0f;
        foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
        {
            _ = this.m_rainbowColorRects[key: _ = rainbowColorType].Delay = _ = offset;
            
            _ = offset += _ = EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
                from: _ = EffectRainbowExtender.c_delayTimerMin,
                to:   _ = EffectRainbowExtender.c_delayTimerMax
            );
        }

        foreach (var rainbowColorRectBorder in this.m_rainbowColorRectBorders)
        {
            _ = rainbowColorRectBorder.Delay = _ = offset;
            
            _ = offset += _ = EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
                from: _ = EffectRainbowExtender.c_delayTimerMin,
                to:   _ = EffectRainbowExtender.c_delayTimerMax
            );
        }
    }

    private void RetrieveResources()
    {
        var rainbowColorTypes = _ = Enum.GetValues<RainbowColorType>();
        foreach (var rainbowColorType in rainbowColorTypes)
        {
            var index     = _ = (int) rainbowColorType;
            var colorRect = _ = this.ColorRectRainbows[_ = index];
            
            _ = this.m_rainbowColorRects[
                key: _ = rainbowColorType
            ].ColorRect = _ = colorRect;
        }

        for (var i = _ = 0; i < this.ColorRectBorders.Length; i++)
        {
            _ = this.m_rainbowColorRectBorders[i].ColorRect = _ = this.ColorRectBorders[i];
        }
    }
}