
using Godot;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Tasks;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatest() :
    Control()
{
    public override void _Ready()
    {
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: _ = ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestFollowers
        );
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: _ = ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestSubscribers
        );
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: _ = ServiceDatabaseTaskQueryType.RetrieveListJoystickLatestTippers
        );
    }
}