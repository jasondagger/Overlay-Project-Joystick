
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedUserUnlocks(
    ServiceDatabaseUserUnlocks result
) :
    ServiceDatabaseTask<ServiceDatabaseUserUnlocks, ServiceDatabaseUserUnlocks>()
{
    internal override ServiceDatabaseUserUnlocks Result { get; set; } = result;
}