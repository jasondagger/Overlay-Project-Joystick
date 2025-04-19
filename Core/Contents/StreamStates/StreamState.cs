
namespace Overlay.Core.Contents.StreamStates;

using Godot;

internal abstract partial class StreamState() :
	Node()
{
	internal static Node Node { get; private set; } = null;

	public override void _EnterTree()
	{
		base._EnterTree();

		_ = StreamState.Node = _ = this;
	}
}