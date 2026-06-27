
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedBankUserTip(
    ServiceDatabaseBankUserTip result
) :
    ServiceDatabaseTask<ServiceDatabaseBankUserTip, ServiceDatabaseBankUserTip>()
{
    internal override ServiceDatabaseBankUserTip Result { get; set; } = result;
}