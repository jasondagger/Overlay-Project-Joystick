
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatestTipper() :
    NameplateLatestEvent()
{
    protected override void RegisterForJoystickEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped           += this.HandleWebSocketPayloadStreamEventTipped;
        _ = ServiceDatabaseTaskEvents.RetrievedListJoystickLatestTippers += this.HandleRetrievedJoystickLatestTippers;
    }
    
    private void HandleRetrievedJoystickLatestTippers(
        ServiceDatabaseTaskRetrievedListJoystickLatestTippers retrievedListJoystickLatestTippers
    )
    {
        var result = _ = retrievedListJoystickLatestTippers.Result;

        var target = _ = result.Count - 5;
        if (target < 0)
        {
            target = 0;
        }
        
        for (var i = result.Count - 1; i >= target; i--)
        {
            var joystickLatestTipper = _ = result[i];
            this.m_names.Enqueue(
                item: _ = joystickLatestTipper.JoystickLatest_Latest_Tipper
            );
        }
        
        this.PlayNotification();
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var name = _ = messageMetadata.Who;
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.AddJoystickLatestTipper,
            serviceDatabaseTaskSqlParameters: 
            [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper)}",
                    value:         _ = name
                )
            ]
        );
        
        this.m_pendingNames.Enqueue(
            item: _ = name
        );
    }
}
