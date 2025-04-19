
using Overlay.Core.Services.Databases.Models;
using System.Collections.Generic;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal abstract class ServiceDatabaseTaskRetrievedList<TServiceDatabaseModel>() :
    ServiceDatabaseTask<TServiceDatabaseModel, List<TServiceDatabaseModel>>()
    where TServiceDatabaseModel :
        ServiceDatabaseModel
{

}