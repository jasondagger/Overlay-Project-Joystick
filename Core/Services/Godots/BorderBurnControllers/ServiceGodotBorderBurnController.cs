
using System;
using Godot;
using System.Collections.Generic;

namespace Overlay.Core.Services.Godots.BorderBurnControllers;

internal partial class ServiceGodotBorderBurnController() : 
	ServiceGodot()
{
	internal void AdjustBorderBurnSpeedByLovenseIntensity(
		int intensity
	)
	{
		var intensityClamped = Math.Clamp(
			value: intensity, 
			min:   ServiceGodotBorderBurnController.c_minimumIntensity, 
			max:   ServiceGodotBorderBurnController.c_maximumIntensity
		);
		var speed = intensityClamped * ServiceGodotBorderBurnController.c_borderBurnDefaultSpeed;
		
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
	private const string                  c_borderBurnSpeedPropertyName    = "burnSpeed";
	
	private const int				      c_maximumIntensity               = 20;
	private const int				      c_minimumIntensity               = 1;

	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialDowns  = [];
	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialLefts  = [];
	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialRights = [];
	private readonly List<ShaderMaterial> m_borderBurnShaderMaterialUps    = [];

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
		var value = new Vector2(
			x: 0f, 
			y: -speed
		);
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialDowns)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: value
			);
		}
	}

	private void SetBorderBurnSpeedLeft(
		float speed
	)
	{
		var value = new Vector2(
			x: speed, 
			y: 0f
		);
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialLefts)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: value
			);
		}
	}

	private void SetBorderBurnSpeedRight(
		float speed
	)
	{
		var value = new Vector2(
			x: -speed,
			y: 0f
		);
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialRights)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: value
			);
		}
	}

	private void SetBorderBurnSpeedUp(
		float speed
	)
	{
		var value = new Vector2(
			x: 0f, 
			y: speed
		);
		foreach (var borderBurnShaderMaterial in this.m_borderBurnShaderMaterialUps)
		{
			borderBurnShaderMaterial.SetShaderParameter(
				param: ServiceGodotBorderBurnController.c_borderBurnSpeedPropertyName,
				value: value
			);
		}
	}
}
