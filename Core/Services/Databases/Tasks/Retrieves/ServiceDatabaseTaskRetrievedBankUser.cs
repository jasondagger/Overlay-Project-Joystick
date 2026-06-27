
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedBankUser(
    ServiceDatabaseBankUser result
) :
    ServiceDatabaseTask<ServiceDatabaseBankUser, ServiceDatabaseBankUser>()
{
    internal override ServiceDatabaseBankUser Result { get; set; } = result;
}