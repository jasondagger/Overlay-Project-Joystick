
using Godot;
using Godot.Collections;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Databases.Models;
using Overlay.Core.Services.Databases.Tasks;
using Overlay.Core.Services.Databases.Tasks.Retrieves;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatest() :
    Control()
{
    [Export] public Array<RichTextLabel> RichTextLabels { get; set; } = null;

    public override void _EnterTree()
    {
        this.RegisterForJoystickEvents();
    }

    private const string c_messageBBCode = "[outline_size=2][outline_color=#202020FF][font_size=32][font=res://Resources/Fonts/Roboto-Black.ttf][color=#F2F2F2FF]";

    private void HandleRetrievedJoystickLatest(
        ServiceDatabaseTaskRetrievedJoystickLatest retrievedJoystickLatest
    )
    {
        var result = _ = retrievedJoystickLatest.Result;

        this.UpdateLatestFollower(
            name: _ = result.JoystickLatest_Latest_Follower
        );
        this.UpdateLatestSubscriber(
            name: _ = result.JoystickLatest_Latest_Subscriber
        );
        this.UpdateLatestTipper(
            name: _ = result.JoystickLatest_Latest_Tipper
        );
    }
    
    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        this.UpdateLatestFollower(
            name: _ = messageMetadata.Who
        );
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        this.UpdateLatestSubscriber(
            name: _ = messageMetadata.Who
        );
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        this.UpdateLatestTipper(
            name: _ = messageMetadata.Who
        );
    }
    
    private void RegisterForJoystickEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed   += this.HandleWebSocketPayloadStreamEventFollowed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Subscribed += this.HandleWebSocketPayloadStreamEventSubscribed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped     += this.HandleWebSocketPayloadStreamEventTipped;

        _ = ServiceDatabaseTaskEvents.RetrievedJoystickLatest      += this.HandleRetrievedJoystickLatest;
        
        ServiceDatabase.ExecuteTaskQuery(
            serviceDatabaseTaskQueryType: _ = ServiceDatabaseTaskQueryType.RetrieveJoystickLatest
        );
    }

    private void SetLatestFollowerName(
        string name
    )
    {
        _ = this.RichTextLabels[0].Text = _ = $"{_ = NameplateLatest.c_messageBBCode}{_ = name}";
    }

    private void SetLatestSubscriberName(
        string name
    )
    {
        _ = this.RichTextLabels[1].Text = _ = $"{_ = NameplateLatest.c_messageBBCode}{_ = name}";
    }
    
    private void SetLatestTipperName(
        string name
    )
    {
        _ = this.RichTextLabels[2].Text = _ = $"{_ = NameplateLatest.c_messageBBCode}{_ = name}";
    
    }

    private void UpdateLatestFollower(
        string name
    )
    {
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.UpdateJoystickLatestFollower,
            serviceDatabaseTaskSqlParameters: [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Follower)}",
                    value:         _ = name
                )
            ]
        );
        
        this.CallDeferred(
            method: _ = $"{_ = nameof(NameplateLatest.SetLatestFollowerName)}", 
            args:   _ = name
        );
    }
    
    private void UpdateLatestSubscriber(
        string name
    )
    {
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.UpdateJoystickLatestSubscriber,
            serviceDatabaseTaskSqlParameters: [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Subscriber)}",
                    value:         _ = name
                )
            ]
        );
        
        this.CallDeferred(
            method: _ = $"{_ = nameof(NameplateLatest.SetLatestSubscriberName)}", 
            args:   _ = name
        );
    }
    
    private void UpdateLatestTipper(
        string name
    )
    {
        ServiceDatabase.ExecuteTaskNonQuery(
            serviceDatabaseTaskNonQueryType:  _ = ServiceDatabaseTaskNonQueryType.UpdateJoystickLatestTipper,
            serviceDatabaseTaskSqlParameters: [
                new ServiceDatabaseTaskNpgsqlParameter(
                    parameterName: _ = $"{_ = nameof(ServiceDatabaseJoystickLatest.JoystickLatest_Latest_Tipper)}",
                    value:         _ = name
                )
            ]
        );
        
        this.CallDeferred(
            method: _ = $"{_ = nameof(NameplateLatest.SetLatestTipperName)}", 
            args:   _ = name
        );
    }
}