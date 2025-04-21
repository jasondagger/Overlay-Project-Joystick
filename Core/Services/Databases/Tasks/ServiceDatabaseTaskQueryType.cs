
namespace Overlay.Core.Services.Databases.Tasks;

internal enum ServiceDatabaseTaskQueryType :
    uint
{
    Start                     = 0U,

    RetrieveJoystickData,
    RetrieveJoystickLatest,
    RetrieveListJoystickUsers,
}