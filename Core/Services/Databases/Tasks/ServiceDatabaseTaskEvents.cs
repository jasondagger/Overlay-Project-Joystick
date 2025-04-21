
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using System;

namespace Overlay.Core.Services.Databases.Tasks;

internal static class ServiceDatabaseTaskEvents
{
    internal static Action<ServiceDatabaseTaskRetrievedListJoystickUsers> RetrievedListJoystickUsers { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedJoystickLatest>    RetrievedJoystickLatest    { get; set; } = null;
    internal static Action<ServiceDatabaseTaskRetrievedJoystickData>      RetrievedJoystickData      { get; set; } = null;
}