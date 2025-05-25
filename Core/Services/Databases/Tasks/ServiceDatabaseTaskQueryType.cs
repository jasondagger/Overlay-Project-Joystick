﻿
namespace Overlay.Core.Services.Databases.Tasks;

internal enum ServiceDatabaseTaskQueryType :
    uint
{
    Start                     = 0U,

    RetrieveGoogleData,
    RetrieveGoveeData,
    RetrieveJoystickData,
    RetrieveListGoveeLights,
    RetrieveListJoystickLatestFollowers,
    RetrieveListJoystickLatestSubscribers,
    RetrieveListJoystickLatestTippers,
    RetrieveListJoystickUsers,
    RetrieveLovenseData,
    RetrieveSpotifyData,
}