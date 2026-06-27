
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
            delta: (float) delta
        );
        this.HandleScale(
            delta: (float) delta
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
        ScaleDown,
        ScaleUp,
        ScaleLingering,
        ScaleWaiting,
    }
    
    private const int             c_entryLengthInMillisecondsMax          = 2000;
    private const int             c_entryLengthInMillisecondsMin          = 1000;   
    private const int             c_middleDurationInMillisecondsMax       = 12000;
    private const int             c_middleDurationInMillisecondsMin       = 8000;
    private const float           c_moveVelocity                          = 1500f;
    private const float           c_scaleVelocity                         = 0.1f;
    private const int             c_scaleWaitingDurationInMillisecondsMax = 9000;
    private const int             c_scaleWaitingDurationInMillisecondsMin = 3000;
    private const float           c_targetMoveToBottomY                   = 400f;
    private const float           c_targetMoveToIdleStartY                = -400f;
    private const float           c_targetMoveToIdleY                     = 0f;
    private const float           c_targetScaleMaxY                       = 1.03f;
    private const float           c_targetScaleMinY                       = 1f;

    private RandomNumberGenerator m_random                                = new();
    private float                 m_moveElapsed                           = 0f;
    private float                 m_moveMiddleDurationInSeconds           = 8f;
    private MoveState             m_moveState                             = MoveState.MoveWaitingAtMiddle;
    private float                 m_scaleElapsed                          = 0f;
    private ScaleState            m_scaleState                            = ScaleState.ScaleLingering;
    private float                 m_scaleWaitingDurationInSeconds         = 4f;


    private void HandleMove(
        float delta
    )
    {
        switch (this.m_moveState)
        {
            case MoveState.MoveWaitingAtMiddle:
                this.HandleMoveWaitingAtMiddle(
                    delta: delta
                );
                break;
            
            case MoveState.MoveToBottom:
                this.HandleMoveToBottom(
                    delta: delta
                );
                break;
            
            case MoveState.MoveToMiddle:
                this.HandleMoveToMiddle(
                    delta: delta
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
        this.m_moveElapsed += delta;
        
        if (this.m_moveElapsed < this.m_moveMiddleDurationInSeconds)
        {
            return;
        }
        
        this.m_moveState   = MoveState.MoveToBottom;
        this.m_moveElapsed = 0f;
    }
    
    private void HandleMoveToBottom(
        float delta
    )
    {
        var position    = this.Pivot.Position;
        position.Y += NameplateLogo.c_moveVelocity * delta;

        if (position.Y >= NameplateLogo.c_targetMoveToBottomY)
        {
            position.Y       = NameplateLogo.c_targetMoveToIdleStartY;
            this.m_moveState = MoveState.MoveWaitingAtTop;

            Task.Run(
                function:
                async () =>
                {
                    var delay = this.m_random.RandiRange(
                        from: NameplateLogo.c_entryLengthInMillisecondsMin,
                        to:   NameplateLogo.c_entryLengthInMillisecondsMax
                    );
                    await Task.Delay(
                        millisecondsDelay: delay
                    );
                    
                    this.m_moveState = MoveState.MoveToMiddle;
                }
            );
        }
        
        this.Pivot.Position = position;
    }
    
    private void HandleMoveToMiddle(
        float delta
    )
    {
        var position    = this.Pivot.Position;
        position.Y += NameplateLogo.c_moveVelocity * delta;

        if (position.Y >= NameplateLogo.c_targetMoveToIdleY)
        {
            position.Y       = NameplateLogo.c_targetMoveToIdleY;
            this.m_moveState = MoveState.MoveWaitingAtMiddle;

            var durationInMilliseconds = this.m_random.RandiRange(
                from: NameplateLogo.c_middleDurationInMillisecondsMin,
                to:   NameplateLogo.c_middleDurationInMillisecondsMax
            );
            this.m_moveMiddleDurationInSeconds =  durationInMilliseconds / 1000f;

            Task.Run(
                function:
                async () =>
                {
                    await Task.Delay(
                        millisecondsDelay: durationInMilliseconds
                    );
                    
                    this.m_scaleState = ScaleState.ScaleWaiting;
                }
            );
        }
        
        this.Pivot.Position = position;
    }

    private void HandleScale(
        float delta
    )
    {
        switch (this.m_scaleState)
        {
            case ScaleState.ScaleDown:
                this.HandleScaleDown(
                    delta: delta
                );
                break;
            
            case ScaleState.ScaleUp:
                this.HandleScaleUp(
                    delta: delta
                );
                break;
                
            case ScaleState.ScaleWaiting:
                this.HandleScaleWaiting(
                    delta: delta
                );
                break;
            
            case ScaleState.ScaleLingering:
            default:
                return;
        }
    }
    
    private void HandleScaleDown(
        float delta
    )
    {
        var scale    = this.Icon.Scale;
        scale.Y -= NameplateLogo.c_scaleVelocity * delta;

        if (scale.Y <= NameplateLogo.c_targetScaleMinY)
        {
            scale.Y           = NameplateLogo.c_targetScaleMinY;
            this.m_scaleState = ScaleState.ScaleLingering;
        }

        scale.X         = scale.Y;
        this.Icon.Scale = scale;
    }

    private void HandleScaleUp(
        float delta
    )
    {
        var scale    = this.Icon.Scale;
        scale.Y += NameplateLogo.c_scaleVelocity * delta;

        if (scale.Y >= NameplateLogo.c_targetScaleMaxY)
        {
            scale.Y           = NameplateLogo.c_targetScaleMaxY;
            this.m_scaleState = ScaleState.ScaleDown;
        }

        scale.X         = scale.Y;
        this.Icon.Scale = scale;
    }

    private void HandleScaleWaiting(
        float delta
    )
    {
        this.m_scaleElapsed += delta;
        
        if (this.m_scaleElapsed < this.m_scaleWaitingDurationInSeconds)
        {
            return;
        }
        
        this.m_scaleState   = ScaleState.ScaleUp;
        this.m_scaleElapsed = 0f;
        this.m_scaleWaitingDurationInSeconds = this.m_random.RandiRange(
            from: NameplateLogo.c_scaleWaitingDurationInMillisecondsMin,
            to:   NameplateLogo.c_scaleWaitingDurationInMillisecondsMax
        ) / 1000f;
    }
}