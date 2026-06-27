
using System;
using Godot;
using System.Collections.Generic;
using Overlay.Core.Contents;

namespace Overlay.Core.Services.Godots.BorderBurnControllers;

internal partial class ServiceGodotBorderBurnController() : 
	ServiceGodot()
{
	public override void _Process(
		double delta
	)
	{
		this.UpdateShaderResources(
			delta: (float) delta
		);
	}
	
	internal void AdjustBorderBurnSpeedByLovenseIntensity(
		float intensity
	)
	{
		this.m_burnSpeed = intensity * ServiceGodotBorderBurnController.c_borderBurnDefaultSpeed;
	}
	
	internal void ResetBorderBurnSpeed()
	{
		this.AdjustBorderBurnSpeedByLovenseIntensity(
			intensity: 1
		);
	}
	
	internal void RetrieveBorderBurnShaderMaterials()
	{
		this.RetrieveBorderBurnDownShaderMaterials();
		this.RetrieveBorderBurnLeftShaderMaterials();
		this.RetrieveBorderBurnRightShaderMaterials();
		this.RetrieveBorderBurnUpShaderMaterials();
	}
	
	private const float                   c_borderBurnDefaultSpeed         = 0.02f;
	private const string                  c_borderBurnDownGroupName        = "BorderBurnDown";
	private const string                  c_borderBurnLeftGroupName        = "BorderBurnLeft";
	private const string                  c_borderBurnRightGroupName       = "BorderBurnRight";
	private const string                  c_borderBurnUpGroupName          = "BorderBurnUp";
	private const string                  c_borderBurnSpeedPropertyName    = "noise_offset";

	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialDowns  = [];
	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialLefts  = [];
	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialRights = [];
	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialUps    = [];
	private Vector2                       m_noiseOffsetDown                = Vector2.Zero;
	private Vector2                       m_noiseOffsetLeft                = Vector2.Zero;
	private Vector2                       m_noiseOffsetRight               = Vector2.Zero;
	private Vector2                       m_noiseOffsetUp                  = Vector2.Zero;
	private float                         m_burnSpeed                      = ServiceGodotBorderBurnController.c_borderBurnDefaultSpeed;

	private void RetrieveBorderBurnDownShaderMaterials()
	{
		this.m_borderBurnShaderMaterialDowns.Clear();
		
		var tree            = this.GetTree();
		var borderBurnNodes = tree.GetNodesInGroup(
			group: ServiceGodotBorderBurnController.c_borderBurnDownGroupName
		);

		foreach (var borderBurnNode in borderBurnNodes)
		{
			if (
				borderBurnNode is ColorRect
				{
					Material: ShaderMaterial shaderMaterial 
				}
			)
			{
				this.m_borderBurnShaderMaterialDowns.Add(
					item: shaderMaterial
				);
			}
		}
	}
	
	private void RetrieveBorderBurnLeftShaderMaterials()
	{
		this.m_borderBurnShaderMaterialLefts.Clear();
		
		var tree            = this.GetTree();
		var borderBurnNodes = tree.GetNodesInGroup(
			group: ServiceGodotBorderBurnController.c_borderBurnLeftGroupName
		);

		foreach (var borderBurnNode in borderBurnNodes)
		{
			if (
				borderBurnNode is ColorRect
				{
					Material: ShaderMaterial shaderMaterial 
				}
			)
			{
				this.m_borderBurnShaderMaterialLefts.Add(
					item: shaderMaterial
				);
			}
		}
	}
	
	private void RetrieveBorderBurnRightShaderMaterials()
	{
		this.m_borderBurnShaderMaterialRights.Clear();
		
		var tree            = this.GetTree();
		var borderBurnNodes = tree.GetNodesInGroup(
			group: ServiceGodotBorderBurnController.c_borderBurnRightGroupName
		);

		foreach (var borderBurnNode in borderBurnNodes)
		{
			if (
				borderBurnNode is ColorRect
				{
					Material: ShaderMaterial shaderMaterial 
				}
			)
			{
				this.m_borderBurnShaderMaterialRights.Add(
					item: shaderMaterial
				);
			}
		}
	}
	
	private void RetrieveBorderBurnUpShaderMaterials()
	{
		this.m_borderBurnShaderMaterialUps.Clear();
		
		var tree            = this.GetTree();
		var borderBurnNodes = tree.GetNodesInGroup(
			group: ServiceGodotBorderBurnController.c_borderBurnUpGroupName
		);

		foreach (var borderBurnNode in borderBurnNodes)
		{
			if (
				borderBurnNode is ColorRect
				{
					Material: ShaderMaterial shaderMaterial 
				}
			)
			{
				this.m_borderBurnShaderMaterialUps.Add(
					item: shaderMaterial
				);
			}
		}
	}

	private void SetBorderBurnSpeedDown(
		float speed
	)
	{
		this.m_noiseOffsetDown.Y -= speed;
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialDowns)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: this.m_noiseOffsetDown
			);
		}
	}

	private void SetBorderBurnSpeedLeft(
		float speed
	)
	{
		this.m_noiseOffsetLeft.X += speed;
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialLefts)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: this.m_noiseOffsetLeft
			);
		}
	}

	private void SetBorderBurnSpeedRight(
		float speed
	)
	{
		this.m_noiseOffsetRight.X -= speed;
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialRights)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: this.m_noiseOffsetRight
			);
		}
	}

	private void SetBorderBurnSpeedUp(
		float speed
	)
	{
		this.m_noiseOffsetUp.Y += speed;
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialUps)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: this.m_noiseOffsetUp
			);
		}
	}

	private void UpdateShaderResources(
		float delta
	)
	{
		var intensity = SpectrumMusicAnalyzer.Intensity;
		var speed     = this.m_burnSpeed * (1.0f + intensity * 2f) * delta;
		
		this.SetBorderBurnSpeedDown(
			speed: speed
		);
		this.SetBorderBurnSpeedLeft(
			speed: speed
		);
		this.SetBorderBurnSpeedRight(
			speed: speed
		);
		this.SetBorderBurnSpeedUp(
			speed: speed
		);
	}
}
