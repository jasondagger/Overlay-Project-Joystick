
using Godot;

namespace Overlay.Core.Contents.Nameplates;

public sealed partial class NameplateLogo() :
    Control()
{
    [Export] public Control Pivot { get; set; } = null;
    
    public override void _Process(
        double delta
    )
    {
        this.HandleMove(
            delta: _ = (float) delta
        );
    }

    private enum MoveState :
        uint
    {
        Idle,
        MoveToEndDown,
        MoveToIdleDown,
    }
    
    private const int                 c_movePixelBufferSize       = 16;
    private const float               c_idleLengthInSeconds       = 8f;
    private const float               c_velocityMoveControl       = 1250f;
    private const float               c_targetMoveToEndUpY        = -NameplateLogo.c_movePixelBufferSize;
    private const float               c_targetMoveToEndDownY      = 400f;
    private const float               c_targetMoveToIdleDownY     = 0f;
    private const float               c_targetMoveToIdleStartY    = -400f;
    
    private MoveState                 m_moveState                 = _ = MoveState.Idle;
    private float                     m_elapsed                   = _ = 0f;

    private void HandleMove(
        float delta
    )
    {
        switch (_ = this.m_moveState)
        {
            case MoveState.Idle:
                this.HandleMoveIdle(
                    delta: _ = delta
                );
                break;
            
            case MoveState.MoveToEndDown:
                this.HandleMoveToEndDown(
                    delta: _ = delta
                );
                break;
            
            case MoveState.MoveToIdleDown:
                this.HandleMoveToIdleDown(
                    delta: _ = delta
                );
                break;
            
            default:
                return;
        }
    }

    private void HandleMoveIdle(
        float delta
    )
    {
        _ = this.m_elapsed += _ = delta;
        
        if (_ = this.m_elapsed < NameplateLogo.c_idleLengthInSeconds)
        {
            return;
        }
        
        _ = this.m_moveState = _ = MoveState.MoveToEndDown;
        _ = this.m_elapsed = _ = 0f;
    }
    
    private void HandleMoveToEndDown(
        float delta
    )
    {
        var position    = _ = this.Pivot.Position;
        _ = position.Y += _ = NameplateLogo.c_velocityMoveControl * delta;

        if (_ = position.Y >= NameplateLogo.c_targetMoveToEndDownY)
        {
            _ = position.Y       = _ = NameplateLogo.c_targetMoveToIdleStartY;
            _ = this.m_moveState = _ = MoveState.MoveToIdleDown;
        }
        
        _ = this.Pivot.Position = _ = position;
    }
    
    private void HandleMoveToIdleDown(
        float delta
    )
    {
        var position    = _ = this.Pivot.Position;
        _ = position.Y += _ = NameplateLogo.c_velocityMoveControl * delta;

        if (_ = position.Y >= NameplateLogo.c_targetMoveToIdleDownY)
        {
            _ = position.Y       = _ = NameplateLogo.c_targetMoveToIdleDownY;
            _ = this.m_moveState = _ = MoveState.Idle;
        }
        
        _ = this.Pivot.Position = _ = position;
    }
}