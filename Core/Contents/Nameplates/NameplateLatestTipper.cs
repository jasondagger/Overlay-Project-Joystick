
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
        ServiceJoystickWebSocketPayloadStreamEvents.Tipped           += this.HandleWebSocketPayloadStreamEventTipped;
        ServiceDatabaseTaskEvents.RetrievedListJoystickLatestTippers += this.HandleRetrievedJoystickLatestTippers;
    }
    
    private void HandleRetrievedJoystickLatestTippers(
        ServiceDatabaseTaskRetrievedListJoystickLatestTippers retrievedListJoystickLatestTippers
    )
    {
        var result = retrievedListJoystickLatestTippers.Result;

        var target = result.Count - 5;
        if (target < 0)
        {
            target = 0;
        }
        
        for (var i = result.Count - 1; i >= target; i--)
        {
            var joystickLatestTipper = result[i];
            this.m_names.Enqueue(
                item: joystickLatestTipper.JoystickLatest_Latest_Tipper
            );
        }
        
        this.CallDeferred(
            method: $"{nameof(NameplateLatestTipper.Play)}"
        );
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var name = messageMetadata.Who;

        if (this.SendDatabaseUpdate is true)
        {
            ServiceDatabase.ExecuteTaskNonQuery(
                serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.AddJoystickLatestTipper,
                serviceDatabaseTaskSqlParameters: 
                [
                    new ServiceDatabaseTaskNpgsqlParameter(
                        parameterName: $"{nameof(ServiceDatabaseJoystickLatestTipper.JoystickLatest_Latest_Tipper)}",
                        value:         name
                    )
                ]
            );
        }
        
        this.m_pendingNames.Enqueue(
            item: name
        );
    }
}
