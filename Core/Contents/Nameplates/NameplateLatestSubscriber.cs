
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
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Subscribed           += this.HandleWebSocketPayloadStreamEventSubscribed;
        _ = ServiceDatabaseTaskEvents.RetrievedListJoystickLatestSubscribers += this.HandleRetrievedJoystickLatestSubscribers;
    }
    
    private void HandleRetrievedJoystickLatestSubscribers(
        ServiceDatabaseTaskRetrievedListJoystickLatestSubscribers retrievedListJoystickLatestSubscribers
    )
    {
        var result = _ = retrievedListJoystickLatestSubscribers.Result;

        var target = _ = result.Count - 5;
        if (target < 0)
        {
            target = 0;
        }
        
        for (var i = result.Count - 1; i >= target; i--)
        {
            var joystickLatestSubscriber = _ = result[i];
            this.m_names.Enqueue(
                item: _ = joystickLatestSubscriber.JoystickLatest_Latest_Subscriber
            );
        }
        
        this.PlayNotification();
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        var name = _ = messageMetadata.Who;
        
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.AddJoystickLatestSubscriber,
            serviceDatabaseTaskSqlParameters: 
            [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatestSubscriber.JoystickLatest_Latest_Subscriber)}",
                    value:         _ = name
                )
            ]
        );
        
        this.m_pendingNames.Enqueue(
            item: _ = name
        );
    }
}
