
using System.Collections.Generic;
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListJoystickLatestTippers(
	List<ServiceDatabaseJoystickLatestTipper> result
) :
	ServiceDatabaseTaskRetrievedList<ServiceDatabaseJoystickLatestTipper>()
{
	internal override List<ServiceDatabaseJoystickLatestTipper> Result { get; set; } = _ = result;
}