
using Godot;

namespace Overlay.Core.Contents.Effects.Worlds;

internal sealed partial class EffectWorldRotateSimple() : 
    Node3D()
{
    [Export] public Vector3 AngularVelocity = _ = Vector3.Zero;

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
        var angularVelocity = _ = new Vector3(
            x: _ = Mathf.DegToRad(
                deg: _ = (float)delta * this.AngularVelocity.X
            ),
            y: _ = Mathf.DegToRad(
                deg: _ = (float)delta * this.AngularVelocity.Y
            ),
            z: _ = Mathf.DegToRad(
                deg: _ = (float)delta * this.AngularVelocity.Z
            )
        );
        
        _ = this.Rotation += _ = angularVelocity;
    }
}