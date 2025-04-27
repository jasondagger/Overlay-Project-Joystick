
using Overlay.Core.Services.Databases.Models;
using System.Collections.Generic;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedListGoveeLights(
	List<ServiceDatabaseGoveeLight> result
) :
    ServiceDatabaseTaskRetrievedList<ServiceDatabaseGoveeLight>()
{
    internal override List<ServiceDatabaseGoveeLight> Result { get; set; } = _ = result;
}