
using Overlay.Core.Tools.ResourcePaths;

namespace Overlay.Core.Services.StreamStates;

using Overlay.Core.Tools.ResourcePaths;

internal sealed class ServiceStreamStateDefault() :
	ServiceStreamState(),
	IServiceStreamState
{
	void IServiceStreamState.Load()
	{
		base.AddStreamStateSceneToStreamStatesScene(
			filePathStreamState: _ = ResourcePaths.StreamStateDefault
		);
	}

	void IServiceStreamState.Unload()
	{
		base.RemoveStreamStateSceneFromMainScene();
	}
}