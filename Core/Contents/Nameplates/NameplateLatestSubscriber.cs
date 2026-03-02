
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatestSubscriber() :
    NameplateLatestEvent()
{
    protected override void RegisterForJoystickEvents()
    {
        ServiceJoystickWebSocketPayloadStreamEvents.Subscribed           += this.HandleWebSocketPayloadStreamEventSubscribed;
        ServiceDatabaseTaskEvents.RetrievedListJoystickLatestSubscribers += this.HandleRetrievedJoystickLatestSubscribers;
    }
    
    private void HandleRetrievedJoystickLatestSubscribers(
        ServiceDatabaseTaskRetrievedListJoystickLatestSubscribers retrievedListJoystickLatestSubscribers
    )
    {
        var result = retrievedListJoystickLatestSubscribers.Result;

        var target = result.Count - 5;
        if (target < 0)
        {
            target = 0;
        }
        
        for (var i = result.Count - 1; i >= target; i--)
        {
            var joystickLatestSubscriber = result[i];
            this.m_names.Enqueue(
                item: joystickLatestSubscriber.JoystickLatest_Latest_Subscriber
            );
        }
        
        this.PlayNotification();
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        var name = messageMetadata.Who;
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  ServiceDatabaseTaskNonQueryType.AddJoystickLatestSubscriber,
            serviceDatabaseTaskSqlParameters: 
            [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: $"{nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber)}",
                    value:         name
                )
            ]
        );
        
        this.m_pendingNames.Enqueue(
            item: name
        );
    }
}
