
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedGoogleData(
	ServiceDatabaseGoogleData result
) :
	ServiceDatabaseTask<ServiceDatabaseGoogleData, ServiceDatabaseGoogleData>()
{
	internal override ServiceDatabaseGoogleData Result { get; set; } = result;
}