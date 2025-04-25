
using System.Threading.Tasks;
using Godot;

namespace Overlay.Core.Contents.Nameplates;

public sealed partial class NameplateLogo() :
    Control()
{
    [Export] public Control   Pivot { get; set; } = null;
    [Export] public ColorRect Icon  { get; set; } = null;
    
    public override void _Process(
        double delta
    )
    {
        this.HandleMove(
            delta: _ = (float) delta
        );
        this.HandleScale(
            delta: _ = (float) delta
        );
    }

    private enum MoveState :
        uint
    {
        MoveWaitingAtTop,
        MoveWaitingAtMiddle,
        MoveToBottom,
        MoveToMiddle,
    }

    private enum ScaleState :
        uint
    {
        ScaleUp,
        ScaleWaiting,
        ScaleDown,
    }
    
    private const int             c_entryLengthInMillisecondsMax          = 1000;
    private const int             c_entryLengthInMillisecondsMin          = 2000;   
    private const int             c_middleDurationInMillisecondsMax       = 5000;
    private const int             c_middleDurationInMillisecondsMin       = 9000;
    private const float           c_moveVelocity                          = 1500f;
    private const float           c_scaleVelocity                         = 0.1f;
    private const int             c_scaleWaitingDurationInMillisecondsMax = 8000;
    private const int             c_scaleWaitingDurationInMillisecondsMin = 14000;
    private const float           c_targetMoveToBottomY                   = 400f;
    private const float           c_targetMoveToIdleStartY                = -400f;
    private const float           c_targetMoveToIdleY                     = 0f;
    private const float           c_targetScaleMaxY                       = 1.04f;
    private const float           c_targetScaleMinY                       = 1f;

    private RandomNumberGenerator m_random                                = new();
    private float                 m_moveElapsed                           = _ = 0f;
    private float                 m_moveMiddleDurationInSeconds           = _ = 8f;
    private MoveState             m_moveState                             = _ = MoveState.MoveWaitingAtMiddle;
    private float                 m_scaleElapsed                          = _ = 0f;
    private ScaleState            m_scaleState                            = _ = ScaleState.ScaleWaiting;
    private float                 m_scaleWaitingDurationInSeconds         = _ = 8f;


    private void HandleMove(
        float delta
    )
    {
        switch (_ = this.m_moveState)
        {
            case MoveState.MoveWaitingAtMiddle:
                this.HandleMoveWaitingAtMiddle(
                    delta: _ = delta
                );
                break;
            
            case MoveState.MoveToBottom:
                this.HandleMoveToBottom(
                    delta: _ = delta
                );
                break;
            
            case MoveState.MoveToMiddle:
                this.HandleMoveToMiddle(
                    delta: _ = delta
                );
                break;
            
            case MoveState.MoveWaitingAtTop:
            default:
                return;
        }
    }
    
    private void HandleMoveWaitingAtMiddle(
        float delta
    )
    {
        _ = this.m_moveElapsed += _ = delta;
        
        if (_ = this.m_moveElapsed < this.m_moveMiddleDurationInSeconds)
        {
            return;
        }
        
        _ = this.m_moveState   = _ = MoveState.MoveToBottom;
        _ = this.m_moveElapsed = _ = 0f;
    }
    
    private void HandleMoveToBottom(
        float delta
    )
    {
        var position    = _ = this.Pivot.Position;
        _ = position.Y += _ = NameplateLogo.c_moveVelocity * delta;

        if (_ = position.Y >= NameplateLogo.c_targetMoveToBottomY)
        {
            _ = position.Y       = _ = NameplateLogo.c_targetMoveToIdleStartY;
            _ = this.m_moveState = _ = MoveState.MoveWaitingAtTop;

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
                    
                    _ = this.m_moveState = _ = MoveState.MoveToMiddle;
                }
            );
        }
        
        _ = this.Pivot.Position = _ = position;
    }
    
    private void HandleMoveToMiddle(
        float delta
    )
    {
        var position    = _ = this.Pivot.Position;
        _ = position.Y += _ = NameplateLogo.c_moveVelocity * delta;

        if (_ = position.Y >= NameplateLogo.c_targetMoveToIdleY)
        {
            _ = position.Y       = _ = NameplateLogo.c_targetMoveToIdleY;
            _ = this.m_moveState = _ = MoveState.MoveWaitingAtMiddle;
            
            _ = this.m_moveMiddleDurationInSeconds = _ = this.m_random.RandiRange(
                from: _ = NameplateLogo.c_middleDurationInMillisecondsMin,
                to:   _ = NameplateLogo.c_middleDurationInMillisecondsMax
            ) / 1000f;
        }
        
        _ = this.Pivot.Position = _ = position;
    }

    private void HandleScale(
        float delta
    )
    {
        switch (_ = this.m_scaleState)
        {
            case ScaleState.ScaleDown:
                this.HandleScaleDown(
                    delta: _ = delta
                );
                break;
            
            case ScaleState.ScaleUp:
                this.HandleScaleUp(
                    delta: _ = delta
                );
                break;
                
            case ScaleState.ScaleWaiting:
                this.HandleScaleWaiting(
                    delta: _ = delta
                );
                break;
            
            default:
                return;
        }
    }
    
    private void HandleScaleDown(
        float delta
    )
    {
        var scale    = _ = this.Icon.Scale;
        _ = scale.Y -= _ = NameplateLogo.c_scaleVelocity * delta;

        if (_ = scale.Y <= NameplateLogo.c_targetScaleMinY)
        {
            _ = scale.Y           = _ = NameplateLogo.c_targetScaleMinY;
            _ = this.m_scaleState = _ = ScaleState.ScaleWaiting;
        }

        _ = scale.X          = _ = scale.Y;
        _ = this.Icon.Scale = _ = scale;
    }

    private void HandleScaleUp(
        float delta
    )
    {
        var scale    = _ = this.Icon.Scale;
        _ = scale.Y += _ = NameplateLogo.c_scaleVelocity * delta;

        if (_ = scale.Y >= NameplateLogo.c_targetScaleMaxY)
        {
            _ = scale.Y           = _ = NameplateLogo.c_targetScaleMaxY;
            _ = this.m_scaleState = _ = ScaleState.ScaleDown;
        }

        _ = scale.X          = _ = scale.Y;
        _ = this.Icon.Scale = _ = scale;
    }

    private void HandleScaleWaiting(
        float delta
    )
    {
        _ = this.m_scaleElapsed += _ = delta;
        
        if (_ = this.m_scaleElapsed < this.m_scaleWaitingDurationInSeconds)
        {
            return;
        }
        
        _ = this.m_scaleState   = _ = ScaleState.ScaleUp;
        _ = this.m_scaleElapsed = _ = 0f;
        _ = this.m_scaleWaitingDurationInSeconds = _ = this.m_random.RandiRange(
            from: _ = NameplateLogo.c_scaleWaitingDurationInMillisecondsMin,
            to:   _ = NameplateLogo.c_scaleWaitingDurationInMillisecondsMax
        ) / 1000f;;
    }
}