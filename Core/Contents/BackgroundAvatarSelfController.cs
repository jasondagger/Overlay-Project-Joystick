
using Godot;
using Overlay.Core.Contents.Effects.Backgrounds;
using Overlay.Core.Contents.Effects.Rainbows;
using Overlay.Core.Contents.Nameplates;
using Overlay.Core.Services.ColorInterpolators;
using System;

namespace Overlay.Core.Contents;

public sealed partial class BackgroundAvatarSelfController() : 
	Node()
{
	public override void _Ready()
	{
		base._Ready();
		
		this.RetrieveResources();
		this.UpdateAvatar();
	}

	public override void _Process(
		double delta
	)
	{
		base._Process(
			delta: delta
		);
		
		this.m_elapsedTime += (float) delta;
		if (this.m_elapsedTime >= this.m_targetElapsedTimeInSeconds)
		{
			this.UpdateAvatar();
			this.CalculateNextElapsedTime();
			this.ResetElapsedTime();
		}
	}

	private const float                                          c_initialTargetElapsedDelayInSeconds = 3f;
	private const float                                          c_maximumDelayTimeInSeconds          = 90f;
	private const float                                          c_minimumDelayTimeInSeconds          = 30f;
	private const string                                         c_username                           = "SmoothDagger";
	
	private static readonly EffectBackgroundAvatarShaderEffect[] s_avatarShaderEffects                = Enum.GetValues<EffectBackgroundAvatarShaderEffect>();
	private static readonly EffectBackgroundAvatarShaderModel[]  s_avatarShaderModels                 = Enum.GetValues<EffectBackgroundAvatarShaderModel>();
	private static readonly EffectBackgroundAvatarShaderSlot[]   s_avatarShaderSlots                  = Enum.GetValues<EffectBackgroundAvatarShaderSlot>();
	private static readonly ServiceColorInterpolatorColorMode[]  s_availableColorModes                = Enum.GetValues<ServiceColorInterpolatorColorMode>();

	private BackgroundAvatarsController                          m_backgroundAvatarsController        = null;
	private float                                                m_elapsedTime                        = 0f;
	private float                                                m_targetElapsedTimeInSeconds         = BackgroundAvatarSelfController.c_initialTargetElapsedDelayInSeconds;

	private void CalculateNextElapsedTime()
	{
		this.m_targetElapsedTimeInSeconds = (float) GD.RandRange(
			from: BackgroundAvatarSelfController.c_minimumDelayTimeInSeconds,
			to:   BackgroundAvatarSelfController.c_maximumDelayTimeInSeconds
		);
	}

	private void ResetElapsedTime()
	{
		this.m_elapsedTime = 0f;
	}

	private void RetrieveResources()
	{
		this.m_backgroundAvatarsController = BackgroundAvatarsController.Instance;
	}

	private void SelectRandomBadgeColor()
	{
		var randomIndex = Random.Shared.Next(
			maxValue: BackgroundAvatarSelfController.s_availableColorModes.Length
		);
		var randomColor = BackgroundAvatarSelfController.s_availableColorModes[randomIndex];
		
		this.m_backgroundAvatarsController.UpdateAvatarNameplateBadgeColor(
			username:                          BackgroundAvatarSelfController.c_username,
			serviceColorInterpolatorColorMode: randomColor
		);
		
		NameplateIcon.UpdateIconColor(
			color: randomColor
		);
		EffectBackgroundGeometry.UpdateGeometryColorInverse(
			color: randomColor
		);
		EffectRainbowStripe.UpdateGlobalColorInverse(
			color: randomColor
		);
	}
	
	private void SelectRandomBaseColor()
	{
		var randomIndex = Random.Shared.Next(
			maxValue: BackgroundAvatarSelfController.s_availableColorModes.Length
		);
		var randomColor = BackgroundAvatarSelfController.s_availableColorModes[randomIndex];
		
		this.m_backgroundAvatarsController.UpdateAvatarBase(
			username:                          BackgroundAvatarSelfController.c_username,
			serviceColorInterpolatorColorMode: randomColor
		);
	}
	
	private void SelectRandomEffects()
	{
		foreach (var avatarShaderSlot in BackgroundAvatarSelfController.s_avatarShaderSlots)
		{
			var randomEffectIndex = Random.Shared.Next(
				maxValue: BackgroundAvatarSelfController.s_avatarShaderEffects.Length
			);
			var randomEffect = BackgroundAvatarSelfController.s_avatarShaderEffects[randomEffectIndex];
			this.m_backgroundAvatarsController.UpdateAvatarEffect(
				username:                           BackgroundAvatarSelfController.c_username,
				effectBackgroundAvatarShaderSlot:   avatarShaderSlot,
				effectBackgroundAvatarShaderEffect: randomEffect
			);
			
			var randomColorIndex = Random.Shared.Next(
				maxValue: BackgroundAvatarSelfController.s_availableColorModes.Length
			);
			var randomColor = BackgroundAvatarSelfController.s_availableColorModes[randomColorIndex];
			this.m_backgroundAvatarsController.UpdateAvatarEffectColor(
				username:                          BackgroundAvatarSelfController.c_username,
				effectBackgroundAvatarShaderSlot:  avatarShaderSlot,
				serviceColorInterpolatorColorMode: randomColor
			);
		}
	}
	
	private void SelectRandomModel()
	{
		var randomIndex = Random.Shared.Next(
			maxValue: BackgroundAvatarSelfController.s_avatarShaderModels.Length
		);
		var randomModel = BackgroundAvatarSelfController.s_avatarShaderModels[randomIndex];
		
		this.m_backgroundAvatarsController.UpdateAvatarModel(
			username:                          BackgroundAvatarSelfController.c_username,
			effectBackgroundAvatarShaderModel: randomModel
		);
	}
	
	private void SelectRandomNameColor()
	{
		var randomIndex = Random.Shared.Next(
			maxValue: BackgroundAvatarSelfController.s_availableColorModes.Length
		);
		var randomColor = BackgroundAvatarSelfController.s_availableColorModes[randomIndex];
		
		this.m_backgroundAvatarsController.UpdateAvatarNameplateNameColor(
			username:                          BackgroundAvatarSelfController.c_username,
			serviceColorInterpolatorColorMode: randomColor
		);
		
		Nameplate.UpdateNameColor(
			color: randomColor
		);
		EffectBackgroundGeometry.UpdateGeometryColorNormal(
			color: randomColor
		);
		EffectRainbowStripe.UpdateGlobalColorNormal(
			color: randomColor
		);
	}
	
	private void SelectRandomOutlineColor()
	{
		var randomIndex = Random.Shared.Next(
			maxValue: BackgroundAvatarSelfController.s_availableColorModes.Length
		);
		var randomColor = BackgroundAvatarSelfController.s_availableColorModes[randomIndex];
		
		this.m_backgroundAvatarsController.UpdateAvatarOutline(
			username:                          BackgroundAvatarSelfController.c_username,
			serviceColorInterpolatorColorMode: randomColor
		);
	}
	
	private void UpdateAvatar()
	{
		this.SelectRandomBadgeColor();
		this.SelectRandomBaseColor();
		this.SelectRandomEffects();
		this.SelectRandomModel();
		this.SelectRandomNameColor();
		this.SelectRandomOutlineColor();
	}
}
