
using System.Threading.Tasks;
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
        Waiting,
        Idle,
        MoveToBottom,
        MoveToIdle,
    }
    
    private const int             c_idleLengthInMillisecondsMax  = 8000;
    private const int             c_idleLengthInMillisecondsMin  = 14000;
    private const int             c_entryLengthInMillisecondsMax = 1000;
    private const int             c_entryLengthInMillisecondsMin = 2000;
    private const float           c_velocityMoveControl          = 1500f;
    private const float           c_targetMoveToBottomY          = 400f;
    private const float           c_targetMoveToIdleY            = 0f;
    private const float           c_targetMoveToIdleStartY       = -400f;
    
    private RandomNumberGenerator m_random                       = new();
    private MoveState             m_moveState                    = _ = MoveState.Idle;
    private float                 m_elapsed                      = _ = 0f;
    private float                 m_idleLength                   = _ = 8f;

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
            
            case MoveState.MoveToBottom:
                this.HandleMoveToBottom(
                    delta: _ = delta
                );
                break;
            
            case MoveState.MoveToIdle:
                this.HandleMoveToIdle(
                    delta: _ = delta
                );
                break;
            
            case MoveState.Waiting:
            default:
                return;
        }
    }
    
    private void HandleMoveIdle(
        float delta
    )
    {
        _ = this.m_elapsed += _ = delta;
        
        if (_ = this.m_elapsed < this.m_idleLength)
        {
            return;
        }
        
        _ = this.m_moveState = _ = MoveState.MoveToBottom;
        _ = this.m_elapsed   = _ = 0f;
    }
    
    private void HandleMoveToBottom(
        float delta
    )
    {
        var position    = _ = this.Pivot.Position;
        _ = position.Y += _ = NameplateLogo.c_velocityMoveControl * delta;

        if (_ = position.Y >= NameplateLogo.c_targetMoveToBottomY)
        {
            _ = position.Y       = _ = NameplateLogo.c_targetMoveToIdleStartY;
            _ = this.m_moveState = _ = MoveState.Waiting;

            Task.Run(
                function:
                async () =>
                {
                    var delay = _ = this.m_random.RandiRange(
                        from: _ = NameplateLogo.c_entryLengthInMillisecondsMin,
                        to:   _ = NameplateLogo.c_entryLengthInMillisecondsMax
                    );
                    await Task.Delay(
                        millisecondsDelay: _ = delay
                    );
                    
                    _ = this.m_moveState = _ = MoveState.MoveToIdle;
                }
            );
        }
        
        _ = this.Pivot.Position = _ = position;
    }
    
    private void HandleMoveToIdle(
        float delta
    )
    {
        var position    = _ = this.Pivot.Position;
        _ = position.Y += _ = NameplateLogo.c_velocityMoveControl * delta;

        if (_ = position.Y >= NameplateLogo.c_targetMoveToIdleY)
        {
            _ = position.Y       = _ = NameplateLogo.c_targetMoveToIdleY;
            _ = this.m_moveState = _ = MoveState.Idle;
            
            _ = this.m_idleLength = _ = this.m_random.RandiRange(
                from: _ = NameplateLogo.c_idleLengthInMillisecondsMin,
                to:   _ = NameplateLogo.c_idleLengthInMillisecondsMax
            ) / 1000f;
        }
        
        _ = this.Pivot.Position = _ = position;
    }
}