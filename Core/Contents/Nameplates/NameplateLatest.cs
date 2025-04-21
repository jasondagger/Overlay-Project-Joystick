
using Godot;
using Godot.Collections;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Contents.Nameplates;

public partial class NameplateLatest() :
    Control()
{
    [Export] public Array<RichTextLabel> RichTextLabels { get; set; } = null;

    public override void _Ready()
    {
        this.RegisterForJoystickEvents();
    }

    private const string c_messageBBCode = "[outline_size=2][outline_color=#202020FF][font_size=32][font=res://Resources/Fonts/Roboto-Black.ttf][color=#F2F2F2FF]";

    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        this.CallDeferred($"{_ = nameof(SetStreamEventFollowedName)}", $"{_ = NameplateLatest.c_messageBBCode}{_ = messageMetadata.Who}");
    }
    
    private void HandleWebSocketPayloadStreamEventSubscribed(
        ServiceJoystickWebSocketPayloadMessageMetadataSubscribed messageMetadata
    )
    {
        this.CallDeferred($"{_ = nameof(SetStreamEventSubscriberName)}", $"{_ = NameplateLatest.c_messageBBCode}{_ = messageMetadata.Who}");
    }
    
    private void HandleWebSocketPayloadStreamEventTipped(
        ServiceJoystickWebSocketPayloadMessageMetadataTipped messageMetadata
    )
    {
        this.CallDeferred($"{_ = nameof(SetStreamEventTippedName)}", $"{_ = NameplateLatest.c_messageBBCode}{_ = messageMetadata.Who}");
    }
    
    private void RegisterForJoystickEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed   += this.HandleWebSocketPayloadStreamEventFollowed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Subscribed += this.HandleWebSocketPayloadStreamEventSubscribed;
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Tipped     += this.HandleWebSocketPayloadStreamEventTipped;
    }

    private void SetStreamEventFollowedName(
        string message
    )
    {
        _ = this.RichTextLabels[0].Text = _ = $"{_ = message}";
    }

    private void SetStreamEventSubscriberName(
        string message
    )
    {
        _ = this.RichTextLabels[1].Text = _ = $"{_ = message}";
    }
    
    private void SetStreamEventTippedName(
        string message
    )
    {
        _ = this.RichTextLabels[2].Text = _ = $"{_ = message}";
    
    }
}