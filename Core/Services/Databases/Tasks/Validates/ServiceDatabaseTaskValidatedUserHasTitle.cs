
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Validates;

internal sealed class ServiceDatabaseTaskValidatedUserHasTitle(
    ServiceDatabaseValidateUserHas result
) :
    ServiceDatabaseTask<ServiceDatabaseValidateUserHas, ServiceDatabaseValidateUserHas>()
{
    internal override ServiceDatabaseValidateUserHas Result { get; set; } = result;
}