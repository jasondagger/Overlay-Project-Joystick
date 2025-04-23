
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatestFollower() :
    NameplateLatestEvent()
{
    protected override void RegisterForJoystickEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed += this.HandleWebSocketPayloadStreamEventFollowed;
        _ = ServiceDatabaseTaskEvents.RetrievedJoystickLatest    += this.HandleRetrievedJoystickLatest;
    }
    
    private void HandleRetrievedJoystickLatest(
        ServiceDatabaseTaskRetrievedJoystickLatest retrievedJoystickLatest
    )
    {
        var result = _ = retrievedJoystickLatest.Result;
        this.m_names.Enqueue(
            item: _ = result.JoystickLatest_Latest_Follower
        );
        
        this.PlayNotification();
    }
    
    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        var name = _ = messageMetadata.Who;
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.UpdateJoystickLatestFollower,
            serviceDatabaseTaskSqlParameters: 
            [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Follower)}",
                    value:         _ = name
                )
            ]
        );
        
        this.m_pendingNames.Enqueue(
            item: _ = name
        );
    }
}
