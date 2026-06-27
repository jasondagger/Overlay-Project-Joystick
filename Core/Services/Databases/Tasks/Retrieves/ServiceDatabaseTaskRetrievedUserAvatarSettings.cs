
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedUserAvatarSettings(
    ServiceDatabaseUserAvatarSettings result
) :
    ServiceDatabaseTask<ServiceDatabaseUserAvatarSettings, ServiceDatabaseUserAvatarSettings>()
{
    internal override ServiceDatabaseUserAvatarSettings Result { get; set; } = result;
}