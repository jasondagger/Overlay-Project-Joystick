
namespace Overlay.Core.Services.Databases.Tasks;

internal enum ServiceDatabaseTaskNonQueryType :
    uint
{
    AddJoystickUser = 0U,
    AddJoystickLatestFollower,
    AddJoystickLatestSubscriber,
    AddJoystickLatestTipper,
    UpdateJoystickUsernameColor,
}