
using Godot;
using System.Threading.Tasks;

namespace Overlay.Core.Services;

internal abstract class ServiceNode() :
	IService
{
	public virtual Task Setup()
	{
		this.AddServiceNodeToMainScene();
		return _ = Task.CompletedTask;
	}

	public virtual Task Start()
	{
		return _ = Task.CompletedTask;
	}

	public virtual Task Stop()
	{
		return _ = Task.CompletedTask;
	}

	internal Node Node { get; private set; } = null;

	internal void ParentNodeToRoot(
		Node node
	)
	{
		Main.AddNodeToNode(
			node:   _ = node,
			target: _ = this.Node
		);
	}

	private void AddServiceNodeToMainScene()
	{
		var type      = _ = this.GetType();
		_ = this.Node = _ = new Node()
		{
			Name = _ = $"{_ = type.Name}"
		};

		Main.Node.AddChild(
			node: _ = this.Node
		);
	}
}