
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedGoveeData(
	ServiceDatabaseGoveeData result
) :
	ServiceDatabaseTask<ServiceDatabaseGoveeData, ServiceDatabaseGoveeData>()
{
	internal override ServiceDatabaseGoveeData Result { get; set; } = _ = result;
}