
using Godot;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Rainbows;

public sealed partial class EffectRainbowExtender() : 
	Control()
{
	[Export] public ColorRect[] ColorRectBorders  = new ColorRect[2];
	[Export] public ColorRect[] ColorRectRainbows = new ColorRect[6];

	public override void _Process(
		double delta
	)
	{
		this.ProcessRainbowState(
			delta: (float)delta
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
	private RainbowColorType[]                                      m_rainbowColorAnimationOrder       = EnumHelper.GetRandomizedValues<RainbowColorType>();
	private RainbowStateType                                        m_currentRainbowState              = RainbowStateType.Shown;
	private float                                                   m_elapsed                          = 0f;
	private float                                                   m_targetElapsed                    = 3;
	
	private static float GenerateRandomTargetElapsedTimeForShown()
	{
		return EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
			from: EffectRainbowExtender.c_targetElapsedToShowMin,
			to:   EffectRainbowExtender.c_targetElapsedToShowMax
		);
	}

	private void HandleRainbowHidden(
		float delta
	)
	{
		this.m_elapsed += delta;
		if (this.m_elapsed < this.m_targetElapsed)
		{
			return;
		}
		
		this.m_currentRainbowState        = RainbowStateType.Show;
		this.m_elapsed                    = 0f;
		this.m_rainbowColorAnimationOrder = EnumHelper.GetRandomizedValues<RainbowColorType>();
		
		this.RandomizeColorRectDelayTimes();
	}
	
	private void HandleRainbowHide(
		float delta
	)
	{
		this.m_elapsed += delta;
		var offset = delta * EffectRainbowExtender.c_colorRectSpeed;
		
		// update each color bar position
		foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
		{
			var rainbowColorRect = this.m_rainbowColorRects[key: rainbowColorType];

			if (this.m_elapsed < rainbowColorRect.Delay)
			{
				continue;
			}
			
			var colorRect = rainbowColorRect.ColorRect;
			var position  = colorRect.Position;

			position.Y += offset;
				
			colorRect.Position = position;
		}
		
		// update each white border position
		foreach (var rainbowColorRectBorder in this.m_rainbowColorRectBorders)
		{
			if (this.m_elapsed < rainbowColorRectBorder.Delay)
			{
				continue;
			}
			
			var colorRect = rainbowColorRectBorder.ColorRect;
			var position  = colorRect.Position;

			position.Y += offset;
				
			colorRect.Position = position;
		}

		var colorRectBorder = this.ColorRectBorders[1];
		var colorRectBorderPosition = colorRectBorder.Position;
		if (colorRectBorderPosition.Y < EffectRainbowExtender.c_lengthOfColorRect)
		{
			return;
		}
		
		// reset positions & sizes
		foreach (var colorRect in this.ColorRectBorders)
		{
			var position  = colorRect.Position;
			var size      = colorRect.Size;

			position.Y = 0f;
			size.X     = 0f;
				
			colorRect.Position = position;
			colorRect.Size     = size;
		}
		foreach (var colorRect in this.ColorRectRainbows)
		{
			var position  = colorRect.Position;
			var size      = colorRect.Size;

			position.Y = 0f;
			size.X     = 0f;
				
			colorRect.Position = position;
			colorRect.Size     = size;
		}

		// move to hidden state
		this.m_currentRainbowState = RainbowStateType.Hidden;
		this.m_elapsed             = 0f;
		this.m_targetElapsed       = EffectRainbowExtender.c_targetElapsedToStayHidden;
	}
	
	private void HandleRainbowShow(
		float delta
	)
	{
		this.m_elapsed += delta;
		var offset = delta * EffectRainbowExtender.c_colorRectSpeed;
		
		// update each bar size
		foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
		{
			var rainbowColorRect = this.m_rainbowColorRects[key: rainbowColorType];

			if (this.m_elapsed < rainbowColorRect.Delay)
			{
				continue;
			}
			
			var colorRect = rainbowColorRect.ColorRect;
			var size      = colorRect.Size;

			size.X += offset;
				
			colorRect.Size = size;
		}
		
		// update each white border size
		foreach (var rainbowColorRectBorder in this.m_rainbowColorRectBorders)
		{
			if (this.m_elapsed < rainbowColorRectBorder.Delay)
			{
				continue;
			}
			
			var colorRect = rainbowColorRectBorder.ColorRect;
			var size      = colorRect.Size;

			size.X += offset;
				
			colorRect.Size = size;
		}

		var colorRectBorder     = this.ColorRectBorders[1];
		var colorRectBorderSize = colorRectBorder.Size;
		if (colorRectBorderSize.X < EffectRainbowExtender.c_lengthOfColorRect)
		{
			return;
		}
		
		this.m_currentRainbowState = RainbowStateType.Shown;
		this.m_elapsed             = 0f;
		this.m_targetElapsed       = EffectRainbowExtender.GenerateRandomTargetElapsedTimeForShown();
	}

	private void HandleRainbowShown(
		float delta
	)
	{
		this.m_elapsed += delta;
		if (this.m_elapsed < this.m_targetElapsed)
		{
			return;
		}
		
		this.m_currentRainbowState        = RainbowStateType.Hide;
		this.m_elapsed                    = 0f;
		this.m_rainbowColorAnimationOrder = EnumHelper.GetRandomizedValues<RainbowColorType>();

		this.RandomizeColorRectDelayTimes();
	}

	private void ProcessRainbowState(
		float delta
	)
	{
		switch (this.m_currentRainbowState)
		{
			case RainbowStateType.Hidden:
				this.HandleRainbowHidden(
					delta: delta
				);
				break;
			
			case RainbowStateType.Hide:
				this.HandleRainbowHide(
					delta: delta
				);
				break;
			
			case RainbowStateType.Show:
				this.HandleRainbowShow(
					delta: delta
				);
				break;
			
			case RainbowStateType.Shown:
				this.HandleRainbowShown(
					delta: delta
				);
				break;
			
			default:
				return;
		}
	}
	
	private void RandomizeColorRectDelayTimes()
	{
		var offset = 0f;
		foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
		{
			this.m_rainbowColorRects[key: rainbowColorType].Delay = offset;
			
			offset += EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
				from: EffectRainbowExtender.c_delayTimerMin,
				to:   EffectRainbowExtender.c_delayTimerMax
			);
		}

		foreach (var rainbowColorRectBorder in this.m_rainbowColorRectBorders)
		{
			rainbowColorRectBorder.Delay = offset;
			
			offset += EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
				from: EffectRainbowExtender.c_delayTimerMin,
				to:   EffectRainbowExtender.c_delayTimerMax
			);
		}
	}

	private void RetrieveResources()
	{
		var rainbowColorTypes = Enum.GetValues<RainbowColorType>();
		foreach (var rainbowColorType in rainbowColorTypes)
		{
			var index     = (int) rainbowColorType;
			var colorRect = this.ColorRectRainbows[index];
			
			this.m_rainbowColorRects[
				key: rainbowColorType
			].ColorRect = colorRect;
		}

		for (var i = 0; i < this.ColorRectBorders.Length; i++)
		{
			this.m_rainbowColorRectBorders[i].ColorRect = this.ColorRectBorders[i];
		}
	}
}
