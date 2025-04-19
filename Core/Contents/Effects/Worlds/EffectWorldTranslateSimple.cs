
using Godot;

namespace Overlay.Core.Contents.Effects.Worlds;

internal sealed partial class EffectWorldTranslateSimple() : 
    Node3D()
{
    [Export] public Vector3 Velocity = _ = Vector3.Zero;

    public override void _Process(
        double delta
    )
    {
        this.Translate(
            delta: _ = delta
        );
    }

    private void Translate(
        double delta 
    )
    {
        var forward = _ = this.Transform.Basis.Z;
        forward = _ = forward.Normalized();
        
        var velocity = _ = new Vector3(
            x: _ = Mathf.DegToRad(
                deg: _ = (float)delta * this.Velocity.X
            ),
            y: _ = Mathf.DegToRad(
                deg: _ = (float)delta * this.Velocity.Y
            ),
            z: _ = Mathf.DegToRad(
                deg: _ = (float)delta * this.Velocity.Z
            )
        );
        
        var movement = _ = forward * Velocity.Length() * (float)delta;
        
        _ = this.Position += _ = movement;
    }
}