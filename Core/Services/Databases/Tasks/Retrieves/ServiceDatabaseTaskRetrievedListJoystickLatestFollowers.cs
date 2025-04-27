
using System.Collections.Generic;
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListJoystickLatestFollowers(
	List<ServiceDatabaseJoystickLatestFollower> result
) :
	ServiceDatabaseTaskRetrievedList<ServiceDatabaseJoystickLatestFollower>()
{
	internal override List<ServiceDatabaseJoystickLatestFollower> Result { get; set; } = _ = result;
}