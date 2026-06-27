
using Overlay.Core.Services.Databases.Models;

namespace Overlay.Core.Services.Databases.Tasks.Retrieves;

internal sealed class ServiceDatabaseTaskRetrievedAchievementUser(
    ServiceDatabaseAchievementUser result
) :
    ServiceDatabaseTask<ServiceDatabaseAchievementUser, ServiceDatabaseAchievementUser>()
{
    internal override ServiceDatabaseAchievementUser Result { get; set; } = result;
}