
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedSpotifyData(
	ServiceDatabaseSpotifyData result
) :
	ServiceDatabaseTask<ServiceDatabaseSpotifyData, ServiceDatabaseSpotifyData>()
{
	internal override ServiceDatabaseSpotifyData Result { get; set; } = _ = result;
}