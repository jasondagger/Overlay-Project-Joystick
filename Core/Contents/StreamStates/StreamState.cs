
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.BorderBurnControllers;

namespace Overlay.Core.Contents.StreamStates;

using Godot;

internal abstract partial class StreamState() :
	Node()
{
	internal static Node Node { get; private set; } = null;

	public override void _EnterTree()
	{
		base._EnterTree();

		StreamState.Node = this;
	}

	public override void _Ready()
	{
		StreamState.RetrieveBorderBurnShaderMaterials();
	}

	private static void RetrieveBorderBurnShaderMaterials()
	{
		var serviceGodots = Services.Services.GetService<ServiceGodots>();
		var serviceGodotBorderBurnController = serviceGodots.GetServiceGodot<ServiceGodotBorderBurnController>();
		
		serviceGodotBorderBurnController.RetrieveBorderBurnShaderMaterials();
	}
}