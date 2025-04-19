
using Godot;

namespace Overlay.Core.Contents.Effects.Controls;

internal sealed partial class EffectControlRotateSimple() : 
    Control()
{
    [Export] public float AngularVelocity = _ = 1f;

    public override void _Process(
        double delta
    )
    {
        this.Rotate(
            delta: _ = delta
        );
    }

    private void Rotate(
        double delta 
    )
    {
        var angularVelocity = _ = Mathf.DegToRad(
            deg: _ = (float)delta * this.AngularVelocity
        );
        _ = this.Rotation += _ = angularVelocity;
    }
}