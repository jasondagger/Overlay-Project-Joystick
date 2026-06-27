
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedBankUserTimeLimit(
    ServiceDatabaseBankUserTimeLimit result
) :
    ServiceDatabaseTask<ServiceDatabaseBankUserTimeLimit, ServiceDatabaseBankUserTimeLimit>()
{
    internal override ServiceDatabaseBankUserTimeLimit Result { get; set; } = result;
}