
using Godot;
using Overlay.Core.Services;
using System;
using System.Collections.Generic;

namespace Overlay;

internal sealed partial class Main() :
	Node()
{
	public override async void _EnterTree()
	{
#if DEBUG
		if (Main.Node is not null)
		{
			throw new Exception(
				message:
					$"EXCEPTION: " +
					$"{nameof(Main)}." +
					$"{nameof(Main.Node)} - " +
					$"Duplicate '{nameof(Main)}' detected."
			);
		}
#endif

		Main.Node = this;
		
		await Services.Start();
		
		DisplayServer.WindowSetMode(
			mode: DisplayServer.WindowMode.Windowed
		);
	}

	public override void _Process(
		double delta
	)
	{
		Main.ProcessNodeTargets();
	}

	internal static Node Node { get; private set; } = null;

	internal static void AddNodeToNode(
		Node node,
		Node target
	)
	{
		lock (Main.s_lock)
		{
			Main.s_nodeTargets.Enqueue(
				item: new NodeTarget(
					node:   node,
					target: target
				)
			);
		}
	}

	private sealed class NodeTarget(
		Node node,
		Node target
	)
	{
		internal Node Node   { get; set; } = node;
		internal Node Target { get; set; } = target;
	}

	private static readonly Queue<NodeTarget> s_nodeTargets = new();
	private static readonly object            s_lock        = new();

	private static void ProcessNodeTargets()
	{
		NodeTarget nodeTarget;
		lock (Main.s_lock)
		{
			if (Main.s_nodeTargets.Count > 0U)
			{
				nodeTarget = Main.s_nodeTargets.Dequeue();
			}
			else
			{
				return;
			}
		}
		
		var node   = nodeTarget.Node;
		var target = nodeTarget.Target;
		
		target.AddChild(
			node: node
		);
	}
}