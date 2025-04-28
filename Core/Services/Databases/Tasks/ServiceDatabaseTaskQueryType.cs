
namespace Overlay.Core.Services.Databases.Tasks;

internal enum ServiceDatabaseTaskQueryType :
    uint
{
    Start                     = 0U,

    RetrieveGoveeData,
    RetrieveJoystickData,
    RetrieveListGoveeLights,
    RetrieveListJoystickLatestFollowers,
    RetrieveListJoystickLatestSubscribers,
    RetrieveListJoystickLatestTippers,
    RetrieveListJoystickUsers,
    RetrieveLovenseData,
}