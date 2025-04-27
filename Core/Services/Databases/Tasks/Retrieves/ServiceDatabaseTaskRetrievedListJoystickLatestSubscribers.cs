
using System.Collections.Generic;
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListJoystickLatestSubscribers(
	List<ServiceDatabaseJoystickLatestSubscriber> result
) :
	ServiceDatabaseTaskRetrievedList<ServiceDatabaseJoystickLatestSubscriber>()
{
	internal override List<ServiceDatabaseJoystickLatestSubscriber> Result { get; set; } = _ = result;
}