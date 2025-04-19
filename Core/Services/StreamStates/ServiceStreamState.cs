
using Overlay.Core.Tools;

namespace Overlay.Core.Services.StreamStates;

using Godot;
using Overlay.Core.Tools;

internal abstract class ServiceStreamState()
{
	protected Node Node { get; private set; } = null;

	protected void AddStreamStateSceneToStreamStatesScene(
        string filePathStreamState
    )
    {
        var packedScene = _ = ResourceLoader.Load(
            path:      _ = filePathStreamState,
            typeHint:  _ = $"{_ = nameof(PackedScene)}",
            cacheMode: _ = ResourceLoader.CacheMode.Reuse
        ) as PackedScene;
        if (_ = packedScene is not null)
        {
            _ = this.Node         = _ = packedScene.Instantiate();
			var serviceGameStates = _ = Services.GetService<ServiceStreamStates>();
            serviceGameStates.ParentNodeToRoot(
                node: _ = this.Node
            );
        }
        else
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceStreamState)}." +
                    $"{_ = nameof(ServiceStreamState.AddStreamStateSceneToStreamStatesScene)}() - " +
                    $"EXCEPTION: Failed to load from {_ = filePathStreamState}"
            );
        }
    }

    protected void RemoveStreamStateSceneFromMainScene()
    {
        if (_ = this.Node is null)
        {
            return;
        }
        
        this.Node.QueueFree();
        _ = this.Node = null;
    }
}