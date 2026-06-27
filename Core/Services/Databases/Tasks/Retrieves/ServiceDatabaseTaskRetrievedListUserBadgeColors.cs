
using System.Collections.Generic;
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListUserBadgeColors(
	List<ServiceDatabaseUserBadgeColor> result
) :
	ServiceDatabaseTaskRetrievedList<ServiceDatabaseUserBadgeColor>()
{
	internal override List<ServiceDatabaseUserBadgeColor> Result { get; set; } = result;
}