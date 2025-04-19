
namespace Overlay.Core.Tools.ResourcePaths;

internal static partial class ResourcePaths
{
    public const string StreamStates          = $"{ResourcePaths.Scenes}/{nameof(ResourcePaths.StreamStates)}";
    
    public const string StreamStateDefault    = $"{ResourcePaths.StreamStates}/{nameof(ResourcePaths.StreamStateDefault)}.tscn";
    public const string StreamStateLarge      = $"{ResourcePaths.StreamStates}/{nameof(ResourcePaths.StreamStateLarge)}.tscn";
	public const string StreamStateTransition = $"{ResourcePaths.StreamStates}/{nameof(ResourcePaths.StreamStateTransition)}.tscn";
}