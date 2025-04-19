
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedJoystickData(
	ServiceDatabaseJoystickData result
) :
	ServiceDatabaseTask<ServiceDatabaseJoystickData, ServiceDatabaseJoystickData>()
{
	internal override ServiceDatabaseJoystickData Result { get; set; } = _ = result;
}