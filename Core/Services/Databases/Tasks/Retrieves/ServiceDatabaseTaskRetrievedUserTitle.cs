
#nullable enable
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedUserTitle(
    ServiceDatabaseUserTitle? result
) :
    ServiceDatabaseTask<ServiceDatabaseUserTitle, ServiceDatabaseUserTitle>()
{
    internal override ServiceDatabaseUserTitle Result         { get; set; } = result ?? new ServiceDatabaseUserTitle();
    internal ServiceDatabaseUserTitle?         NullableResult { get; set; } = result;
}