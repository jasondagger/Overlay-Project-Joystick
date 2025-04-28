
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedLovenseData(
	ServiceDatabaseLovenseData result
) :
	ServiceDatabaseTask<ServiceDatabaseLovenseData, ServiceDatabaseLovenseData>()
{
	internal override ServiceDatabaseLovenseData Result { get; set; } = _ = result;
}