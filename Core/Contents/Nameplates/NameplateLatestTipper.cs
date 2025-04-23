
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
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped += this.HandleWebSocketPayloadStreamEventTipped;
        _ = ServiceDatabaseTaskEvents.RetrievedJoystickLatest  += this.HandleRetrievedJoystickLatest;
    }
    
    private void HandleRetrievedJoystickLatest(
        ServiceDatabaseTaskRetrievedJoystickLatest retrievedJoystickLatest
    )
    {
        var result = _ = retrievedJoystickLatest.Result;
        this.m_names.Enqueue(
            item: _ = result.JoystickLatest_Latest_Tipper
        );
        
        this.PlayNotification();
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        var name = _ = messageMetadata.Who;
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.UpdateJoystickLatestTipper,
            serviceDatabaseTaskSqlParameters: 
            [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Tipper)}",
                    value:         _ = name
                )
            ]
        );
        
        this.m_pendingNames.Enqueue(
            item: _ = name
        );
    }
}
