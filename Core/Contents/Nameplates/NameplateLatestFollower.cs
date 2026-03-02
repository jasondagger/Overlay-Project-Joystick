
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
        ServiceJoystickWebSocketPayloadStreamEvents.Followed           += this.HandleWebSocketPayloadStreamEventFollowed;
        ServiceDatabaseTaskEvents.RetrievedListJoystickLatestFollowers += this.HandleRetrievedJoystickLatestFollowers;
    }
    
    private void HandleRetrievedJoystickLatestFollowers(
        ServiceDatabaseTaskRetrievedListJoystickLatestFollowers retrievedListJoystickLatestFollowers
    )
    {
        var result = retrievedListJoystickLatestFollowers.Result;

        var target = result.Count - 5;
        if (target < 0)
        {
            target = 0;
        }
        
        for (var i = result.Count - 1; i >= target; i--)
        {
            var joystickLatestFollower = result[i];
            this.m_names.Enqueue(
                item: joystickLatestFollower.JoystickLatest_Latest_Follower
            );
        }
        
        this.PlayNotification();
    }
    
    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        var name = messageMetadata.Who;
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.AddJoystickLatestFollower,
            serviceDatabaseTaskSqlParameters: 
            [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: $"{nameof(ServiceDatabaseJoystickLatestFollower.JoystickLatest_Latest_Follower)}",
                    value:         name
                )
            ]
        );
        
        this.m_pendingNames.Enqueue(
            item: name
        );
    }
}
