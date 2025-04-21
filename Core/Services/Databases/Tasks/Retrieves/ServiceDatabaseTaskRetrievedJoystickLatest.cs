
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedJoystickLatest(
	ServiceDatabaseJoystickLatest result
) :
	ServiceDatabaseTask<ServiceDatabaseJoystickData, ServiceDatabaseJoystickLatest>()
{
	internal override ServiceDatabaseJoystickLatest Result { get; set; } = _ = result;
}