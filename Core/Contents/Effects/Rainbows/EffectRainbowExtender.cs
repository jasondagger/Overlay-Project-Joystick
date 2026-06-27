
using Godot;
using Overlay.Core.Tools;
using System;
using System.Collections.Generic;

namespace Overlay.Core.Contents.Effects.Rainbows;

public sealed partial class EffectRainbowExtender() : 
	Control()
{
	[Export] public ColorRect[] ColorRectBorders  = new ColorRect[2];
	[Export] public ColorRect[] ColorRectBurns    = new ColorRect[2];
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
		this.AddToEffectRainbowExtenders();
		this.RetrieveResources();
		this.SetEffectRainbowStripeEffects();
	}

	internal static void AdjustExtenderSpeeds(
		float intensity
	)
	{
		foreach (var effectRainbowExtender in EffectRainbowExtender.s_rainbowExtenders)
		{
			effectRainbowExtender.m_lovenseIntensityMultiplier = intensity;
		}
	}
	
	internal static void ResetExtenderSpeed()
	{
		EffectRainbowExtender.AdjustExtenderSpeeds(
			intensity: 1
		);
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

	private static readonly RandomNumberGenerator                       s_randomNumberGenerator      = new();
	private static readonly List<EffectRainbowExtender>                 s_rainbowExtenders           = [];
	
	private const float                                                 c_targetElapsedToShowMax     = 25f;
	private const float                                                 c_targetElapsedToShowMin     = 5f;
	private const float                                                 c_targetElapsedToStayHidden  = 2f;
	private const float                                                 c_delayTimerMax              = 0.025f;
	private const float                                                 c_delayTimerMin              = 0.075f;
	private const float                                                 c_colorRectSpeed             = 5000f;


	private readonly Dictionary<RainbowColorType, RainbowColorRect>     m_rainbowColorRects          = new()
	{
		{ RainbowColorType.Red,     new RainbowColorRect() },
		{ RainbowColorType.Yellow,  new RainbowColorRect() },
		{ RainbowColorType.Green,   new RainbowColorRect() },
		{ RainbowColorType.Cyan,    new RainbowColorRect() },
		{ RainbowColorType.Blue,    new RainbowColorRect() },
		{ RainbowColorType.Magenta, new RainbowColorRect() },
	};
	private readonly Dictionary<RainbowColorType, EffectRainbowStripe> m_effectRainbowStripes       = new();
	private readonly RainbowColorRect[]                                m_rainbowColorRectBorders    = 
	[
		new(),
		new()
	];
	private RainbowColorType[]                                         m_rainbowColorAnimationOrder = EnumHelper.GetRandomizedValues<RainbowColorType>();
	private RainbowStateType                                           m_currentRainbowState        = RainbowStateType.Shown;
	private float                                                      m_elapsed                    = 0f;
	private float                                                      m_targetElapsed              = 3;
	private float                                                      m_lovenseIntensityMultiplier = 1f;
	private int                                                        m_lengthOfColorRect          = 0;
	
	private static float GenerateRandomTargetElapsedTimeForShown()
	{
		return EffectRainbowExtender.s_randomNumberGenerator.RandfRange(
			from: EffectRainbowExtender.c_targetElapsedToShowMin,
			to:   EffectRainbowExtender.c_targetElapsedToShowMax
		);
	}
	
	private static float GetMusicIntensity()
	{
		return 1f + SpectrumMusicAnalyzer.Intensity;
	}

	private void AddToEffectRainbowExtenders()
	{
		EffectRainbowExtender.s_rainbowExtenders.Add(
			item: this
		);
	}

	private void HandleRainbowHidden(
		float delta
	)
	{
		this.m_elapsed += delta * this.m_lovenseIntensityMultiplier * EffectRainbowExtender.GetMusicIntensity();
		if (this.m_elapsed < this.m_targetElapsed)
		{
			return;
		}
		
		this.m_currentRainbowState        = RainbowStateType.Show;
		this.m_elapsed                    = 0f;
		this.m_rainbowColorAnimationOrder = EnumHelper.GetRandomizedValues<RainbowColorType>();

		this.SetEffectRainbowStripeEffects();
		this.RandomizeColorRectDelayTimes();
	}
	
	private void HandleRainbowHide(
		float delta
	)
	{
		this.m_elapsed += delta;
		var offset = delta * EffectRainbowExtender.c_colorRectSpeed * this.m_lovenseIntensityMultiplier * EffectRainbowExtender.GetMusicIntensity();
		
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
		for (var i = 0; i < this.m_rainbowColorRectBorders.Length; i++)
		{
			var rainbowColorRectBorder = this.m_rainbowColorRectBorders[i];
			
			if (this.m_elapsed < rainbowColorRectBorder.Delay)
			{
				continue;
			}
			
			// move border
			var colorRect = rainbowColorRectBorder.ColorRect;
			var position  = colorRect.Position;

			position.Y += offset;
				
			colorRect.Position = position;
			
			// move burn
			var rainbowColorRectBurn = this.ColorRectBurns[i];
			colorRect = rainbowColorRectBurn;
			position  = colorRect.Position;

			position.Y += offset;
				
			colorRect.Position = position;
		}

		var colorRectBorder = this.ColorRectBorders[1];
		var colorRectBorderPosition = colorRectBorder.Position;
		if (colorRectBorderPosition.Y < this.m_lengthOfColorRect)
		{
			return;
		}
		
		// reset positions & sizes
		foreach (var colorRect in this.ColorRectBorders)
		{
			var position = colorRect.Position;
			var size     = colorRect.Size;

			position.Y = -this.m_lengthOfColorRect;
				
			colorRect.Position = position;
			colorRect.Size     = size;
		}
		foreach (var colorRect in this.ColorRectBurns)
		{
			var position = colorRect.Position;
			var size     = colorRect.Size;

			position.Y = -this.m_lengthOfColorRect;
				
			colorRect.Position = position;
			colorRect.Size     = size;
		}
		foreach (var colorRect in this.ColorRectRainbows)
		{
			var position = colorRect.Position;
			var size     = colorRect.Size;

			position.Y = -this.m_lengthOfColorRect;
				
			colorRect.Position = position;
			colorRect.Size     = size;
		}

		// move to hidden state
		this.m_currentRainbowState = RainbowStateType.Hidden;
		this.m_elapsed             = 0f;
		this.m_targetElapsed       = EffectRainbowExtender.c_targetElapsedToStayHidden;
	}
	
	private void HandleRainbowShow(float delta)
	{
		this.m_elapsed += delta;
		var offset = delta * EffectRainbowExtender.c_colorRectSpeed * this.m_lovenseIntensityMultiplier * EffectRainbowExtender.GetMusicIntensity();
    
		foreach (var rainbowColorType in this.m_rainbowColorAnimationOrder)
		{
			var rainbowColorRect = this.m_rainbowColorRects[key: rainbowColorType];
			if (this.m_elapsed < rainbowColorRect.Delay)
			{
				continue;
			}
        
			var position                       = rainbowColorRect.ColorRect.Position;
			position.Y                         = Math.Min(
				val1: 0,
				val2: position.Y + offset
			); 
			rainbowColorRect.ColorRect.Position = position;
		}
    
		for (var i = 0; i < this.m_rainbowColorRectBorders.Length; i++)
		{
			var rainbowColorRectBorder = this.m_rainbowColorRectBorders[i];
			if (this.m_elapsed < rainbowColorRectBorder.Delay)
			{
				continue;
			}
        
			var position                              = rainbowColorRectBorder.ColorRect.Position;
			position.Y                                = Math.Min(
				val1: 0,
				val2: position.Y + offset
			);
			rainbowColorRectBorder.ColorRect.Position = position;
        
			var colorRectBurnPositions      = this.ColorRectBurns[i].Position;
			colorRectBurnPositions.Y        = Math.Min(
				val1: 0, 
				val2: colorRectBurnPositions.Y + offset
			);
			this.ColorRectBurns[i].Position = colorRectBurnPositions;
		}

		if (this.ColorRectBorders[1].Position.Y < 0)
		{
			return;
		}
    
		this.m_currentRainbowState = RainbowStateType.Shown;
		this.m_elapsed = 0f;
		this.m_targetElapsed = EffectRainbowExtender.GenerateRandomTargetElapsedTimeForShown();
	}

	private void HandleRainbowShown(
		float delta
	)
	{
		this.m_elapsed += delta * this.m_lovenseIntensityMultiplier * EffectRainbowExtender.GetMusicIntensity();
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
			this.m_effectRainbowStripes.Add(
				key:   rainbowColorType,
				value: colorRect as EffectRainbowStripe
			);
		}

		for (var i = 0; i < this.ColorRectBorders.Length; i++)
		{
			this.m_rainbowColorRectBorders[i].ColorRect = this.ColorRectBorders[i];
		}

		this.m_lengthOfColorRect = (int)this.m_rainbowColorRectBorders[0].ColorRect.Size.X;
	}

	private void SetEffectRainbowStripeEffects()
	{
		for (var i = 0; i < 5; i++)
		{
			var effectIndex0 = GD.RandRange(
				from: 0, 
				to:   49
			);
			this.m_effectRainbowStripes[key: RainbowColorType.Red].SetEffectSlotEffect(
				slotIndex:   i,
				effectIndex: effectIndex0
			);
			this.m_effectRainbowStripes[key: RainbowColorType.Green].SetEffectSlotEffect(
				slotIndex:   i,
				effectIndex: effectIndex0
			);
			this.m_effectRainbowStripes[key: RainbowColorType.Blue].SetEffectSlotEffect(
				slotIndex:   i,
				effectIndex: effectIndex0
			);
			
			var effectIndex1 = GD.RandRange(
				from: 0, 
				to:   49
			);
			this.m_effectRainbowStripes[key: RainbowColorType.Yellow].SetEffectSlotEffect(
				slotIndex:   i,
				effectIndex: effectIndex1
			);
			this.m_effectRainbowStripes[key: RainbowColorType.Cyan].SetEffectSlotEffect(
				slotIndex:   i,
				effectIndex: effectIndex1
			);
			this.m_effectRainbowStripes[key: RainbowColorType.Magenta].SetEffectSlotEffect(
				slotIndex:   i,
				effectIndex: effectIndex1
			);
		}
	}
}
