
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Validates;

internal sealed class ServiceDatabaseTaskValidatedUserUnlockEffect(
    ServiceDatabaseValidateUserUnlock result
) :
    ServiceDatabaseTask<ServiceDatabaseValidateUserUnlock, ServiceDatabaseValidateUserUnlock>()
{
    internal override ServiceDatabaseValidateUserUnlock Result { get; set; } = result;
}