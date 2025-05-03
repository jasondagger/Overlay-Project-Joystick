
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using System;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskEvents
{
    internal static Action<ServiceDatabaseTaskRetrievedGoveeData>                     RetrievedGoveeData                     { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedJoystickData>                  RetrievedJoystickData                  { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedListGoveeLights>               RetrievedListGoveeLights               { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedListJoystickUsers>             RetrievedListJoystickUsers             { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedListJoystickLatestFollowers>   RetrievedListJoystickLatestFollowers   { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedListJoystickLatestSubscribers> RetrievedListJoystickLatestSubscribers { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedListJoystickLatestTippers>     RetrievedListJoystickLatestTippers     { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedLovenseData>                   RetrievedLovenseData                   { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedSpotifyData>                   RetrievedSpotifyData                   { get; set; } = null;
}