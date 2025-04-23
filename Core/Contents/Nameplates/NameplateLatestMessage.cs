
using Godot;
using Overlay.Core.Contents.Effects;
using Overlay.Core.Services.Joysticks.Payloads;
using Overlay.Core.Services.Joysticks.Payloads.Metadatas;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatestMessage() :
    Control()
{
    [Export] public Control              ControlStart         = null;
    [Export] public Control              ControlEnd           = null;
    [Export] public RichTextLabelSampler RichTextLabelSampler = null;

    public override void _Ready()
    {
        base._Ready();
        
        this.RegisterForJoystickEvents();
    }
    
    private void RegisterForJoystickEvents()
    {
        _ = ServiceJoystickWebSocketPayloadStreamEvents.Followed   += this.HandleWebSocketPayloadStreamEventFollowed;
    }

    private void HandleWebSocketPayloadStreamEventFollowed(
        ServiceJoystickWebSocketPayloadMessageMetadataFollowed messageMetadata
    )
    {
        
    }
}