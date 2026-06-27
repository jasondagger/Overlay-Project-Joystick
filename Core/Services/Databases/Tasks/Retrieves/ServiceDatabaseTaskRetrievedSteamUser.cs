
#nullable enable
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedSteamUser(
    ServiceDatabaseSteamUser result
) :
    ServiceDatabaseTask<ServiceDatabaseSteamUser, ServiceDatabaseSteamUser>()
{
    internal override ServiceDatabaseSteamUser Result { get; set; } = result;
}