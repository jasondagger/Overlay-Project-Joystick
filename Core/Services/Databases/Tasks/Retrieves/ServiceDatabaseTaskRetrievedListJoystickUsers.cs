
using Overlay.Core.Services.Databases.Models;
using System.Collections.Generic;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListJoystickUsers(
	List<ServiceDatabaseJoystickUser> result
) :
    ServiceDatabaseTaskRetrievedList<ServiceDatabaseJoystickUser>()
{
    internal override List<ServiceDatabaseJoystickUser> Result { get; set; } = _ = result;
}