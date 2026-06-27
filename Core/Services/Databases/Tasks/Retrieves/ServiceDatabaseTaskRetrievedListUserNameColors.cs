
using System.Collections.Generic;
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListUserNameColors(
	List<ServiceDatabaseUserNameColor> result
) :
	ServiceDatabaseTaskRetrievedList<ServiceDatabaseUserNameColor>()
{
	internal override List<ServiceDatabaseUserNameColor> Result { get; set; } = result;
}