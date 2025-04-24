
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Overlay.Core.Contents.Chats;
using Overlay.Core.Contents.Effects;
using Vector2 = Godot.Vector2;

namespace Overlay.Core.Contents.Nameplates;

internal abstract partial class NameplateLatestEvent() :
    Control()
{
    [Export] public RichTextLabelSampler RichTextLabelSampler = null;
    [Export] public ColorRect            ColorRectIcon        = null;
    [Export] public Control              ControlPivot         = null;

    public override void _Process(
        double elapsed
    )
    {
        if (_ = this.m_move)
        {
            var position    = _ = this.ControlPivot.Position;
            _ = position.X -= _ = NameplateLatestEvent.c_velocityScrollControl * (float)elapsed;

            if (_ = position.X <= this.m_targetX)
            {
                _ = position.X  = _ = this.m_targetX;
                _ = this.m_move = _ = false;
            }

            _ = this.ControlPivot.Position = _ = position;
        }

        if (_ = this.m_isRichTextLabelScrolling)
        {
            this.AnimateScroll(
                elapsed: _ = (float)elapsed
            );
        }

        this.AnimateIcon(
            elapsed: _ = (float)elapsed
        );

        if (_ = this.m_reset)
        {
            this.ResetToInitialPosition();
        }
    }
    
    public override void _EnterTree()
    {
        base._EnterTree();
        
        this.RegisterForJoystickEvents();
        this.RichTextLabelSampler.LoadRichTextLabelsAndAttachToParentNode(
            parent: _ = this.ControlPivot
        );
    }
    
    protected Queue<string>          m_names        = new();
    protected readonly Queue<string> m_pendingNames = new();

    protected void PlayNotification() 
    {
        Task.Run(
            function:
            async () =>
            {
                for (var i = _ = 0; _ = i < this.m_names.Count; _ = i++)
                {
                    _ = this.Text = _ = this.m_names.ElementAt(
                        index: _ = i
                    );
                    
                    this.CallDeferred(
                        method: _ = $"{_ = nameof(NameplateLatestEvent.CreateTextLetters)}"
                    );

                    await Task.Delay(
                        millisecondsDelay: _ = NameplateLatestEvent.c_richTextLabelTitleDelayInMilliseconds
                    );

                    this.StartScrollToCenter();
                    this.SetImageIconState(
                        imageIconAnimationState: _ = ImageIconAnimationState.Showing
                    );
 
                    if (_ = this.m_distanceOffScreen > 0f)
                    {
                        var animationDelay = Mathf.RoundToInt(
                            s: _ = this.m_distanceOffScreen / NameplateLatestEvent.c_velocityScrollControlInMilliseconds
                        );
                        var remainingScreenDuration = _ = NameplateLatestEvent.c_richTextLabelOnScreenDurationMax - animationDelay;
                        var halfDelay = _ = Mathf.RoundToInt(
                            s: _ = remainingScreenDuration / 2f
                        );

                        await Task.Delay(
                            millisecondsDelay: _ = halfDelay
                        );

                        _ = this.m_move = _ = true;

                        await Task.Delay(
                            millisecondsDelay: _ = halfDelay
                        );
                    }
                    else
                    {
                        await Task.Delay(
                            millisecondsDelay: _ = NameplateLatestEvent.c_richTextLabelOnScreenDurationMax
                        );
                    }
                    
                    await Task.Delay(
                        millisecondsDelay: _ = NameplateLatestEvent.c_richTextLabelTitleDelayInMilliseconds
                    );
                    
                    this.StartScrollToEnd();
                    this.SetImageIconState(
                        imageIconAnimationState: _ = ImageIconAnimationState.Hiding
                    );

                    await Task.Delay(
                        millisecondsDelay: _ = NameplateLatestEvent.c_richTextLabelTitleDelayInMillisecondsCompletion
                    );

                    this.DestroyTextLetters();
                    this.Reset();
                }

                if (_ = this.m_pendingNames.Count > 0u)
                {
                    var names = _ = new Queue<string>();
                    names.Enqueue(
                        item: _ = this.m_pendingNames.Dequeue()
                    );
                    while (_ = this.m_names.Count > 1u)
                    {
                        names.Enqueue(
                            item: _ = this.m_names.Dequeue()
                        );
                    }
                    _ = this.m_names = _ = names;
                }
                
                this.QueueNotification();
            }
        );
    }

    protected abstract void RegisterForJoystickEvents();
    
    private enum ImageIconAnimationState :
        uint
    {
        Showing = 0u,
        Hiding,
        Idle
    }

    private enum TextLetterScrollState :
        uint
    {
        Idle = 0u,
        ScrollCenterToEnd,
        ScrollStartToCenter
    }
    
    private struct TextLetter(
        RichTextLabel         richTextLabel,
        TextLetterScrollState textLetterScrollState,
        float                 center,
        float                 start,
        float                 end
    )
    {
        public readonly RichTextLabel RichTextLabel         = _ = richTextLabel;
        public TextLetterScrollState  TextLetterScrollState = _ = textLetterScrollState;
        public readonly float         Center                = _ = center;
        public readonly float         Start                 = _ = start;
        public readonly float         End                   = _ = end;
    }
    
    private const float                          c_scrollDistance                                  = 480f;
    private const uint                           c_maxNameCount                                    = 5u;

    private const int                            c_richTextLabelOnScreenDurationMax                = 8000;
    private const int                            c_richTextLabelTitleDelayInMilliseconds           = 500;
    private const int                            c_richTextLabelTitleDelayInMillisecondsCompletion = 2000;
    private const float                          c_velocityScrollControl                           = 250f;
    private const float                          c_velocityScrollControlInMilliseconds             = NameplateLatestEvent.c_velocityScrollControl * 1000f;

    private const int                            c_richTextLabelTitleDelayInMillisecondsScroll     = 35;
    private const float                          c_velocityScrollLetter                            = 900f;

    private const float                          c_imageIconSpeed                                  = 2f;
    private const float                          c_imageIconRotation                               = -2160f;
    
    private bool                                 m_isRichTextLabelScrolling                        = false;
    private ImageIconAnimationState              m_imageIconAnimationState                         = ImageIconAnimationState.Idle;
    private float                                m_imageIconElapsed                                = 0f;
    
    private string                               Text                                              { get; set; } = string.Empty;
    private readonly Dictionary<int, TextLetter> m_textLetters                                     = new();
    private Vector2                              m_initialPosition                                 = Vector2.Zero;
    private RandomNumberGenerator                m_random                                          = new();
    private bool                                 m_reset                                           = false;
    private bool                                 m_move                                            = false;
    private float                                m_distanceOffScreen                               = 0f;
    private float                                m_targetX                                         = 0f;

    private void AnimateScroll(
        float elapsed
    )
    {
        for (var i = _ = 0; _ = i < this.m_textLetters.Count; _ = i++)
        {
            var textLetter = _ = this.m_textLetters[i];
            switch (_ = textLetter.TextLetterScrollState)
            {
                case TextLetterScrollState.ScrollCenterToEnd:
                    this.ScrollCenterToEnd(
                        index:   _ = i,
                        elapsed: _ = elapsed
                    );
                    break;
                case TextLetterScrollState.ScrollStartToCenter:
                    this.ScrollStartToCenter(
                        index:   _ = i,
                        elapsed: _ = elapsed
                    );
                    break;

                case TextLetterScrollState.Idle:
                default:
                    break;
            }
        }

        if (
            _ = this.m_textLetters.All(
                predicate: x => 
                    x.Value.TextLetterScrollState is TextLetterScrollState.Idle
            ) is true
        )
        {
            _ = this.m_isRichTextLabelScrolling = _ = false;
        }
    }
    
    private void AnimateIcon(
        float elapsed
    )
    {
        switch (_ = this.m_imageIconAnimationState)
        {
            case ImageIconAnimationState.Showing:
                this.ShowIcon(
                    delta: elapsed
                );
                break;

            case ImageIconAnimationState.Hiding:
                this.HideIcon(
                    delta: elapsed
                );
                break;

            case ImageIconAnimationState.Idle:
            default:
                break;
        }
    }
    
    private void CreateTextLetters()
    {
        var positionX = 0f;
        for (var i = 0; i < Text.Length; i++)
        {
            var letter = Text[i];
            var richTextLabel = RichTextLabelSampler.DequeueRichTextLabel(
                letter: letter
            );
            richTextLabel.Position = new Vector2(
                x: positionX,
                y: 0f
            );

            m_textLetters.Add(
                key: i,
                value: new(
                    richTextLabel: richTextLabel,
                    textLetterScrollState: TextLetterScrollState.Idle,
                    center: richTextLabel.Position.X,
                    start: richTextLabel.Position.X + c_scrollDistance,
                    end: richTextLabel.Position.X - c_scrollDistance - positionX
                )
            );

            var position = m_textLetters[i].RichTextLabel.Position;
            position.X = m_textLetters[i].Start;
            m_textLetters[i].RichTextLabel.Position = position;

            positionX += richTextLabel.GetContentWidth();
        }

        m_distanceOffScreen = positionX - c_scrollDistance;
        m_targetX = m_initialPosition.X - m_distanceOffScreen;
    }
    
    private void DestroyTextLetters()
    {
        foreach (var t in _ = Text)
        {
            this.RichTextLabelSampler.RequeueRichTextLabel(
                letter: _ = t
            );
        }

        this.m_textLetters.Clear();
    }
    
    private void HideIcon(
        float delta
    )
    {
        const float startRotation = c_imageIconRotation;
        const float endRotation = 0f;

        m_imageIconElapsed += c_imageIconSpeed * delta;
        ColorRectIcon.RotationDegrees = Mathf.Lerp(
            from: startRotation,
            to: endRotation,
            weight: m_imageIconElapsed
        );
        ColorRectIcon.Scale = Vector2.One.Lerp(
            to: Vector2.Zero,
            weight: m_imageIconElapsed
        );

        if (m_imageIconElapsed >= 1f)
        {
            m_imageIconAnimationState = ImageIconAnimationState.Idle;
            ColorRectIcon.RotationDegrees = endRotation;
            ColorRectIcon.Scale = Vector2.Zero;
            ColorRectIcon.Visible = false;
            m_imageIconElapsed = 0f;
        }
    }
    
    private bool IsFooterTextCentered()
    {
        foreach (var footerTextLetter in m_textLetters)
        {
            var textLetter = footerTextLetter.Value;
            if (textLetter.TextLetterScrollState is not TextLetterScrollState.Idle)
            {
                return false;
            }
        }
        return true;
    }

    private void QueueNotification()
    {
        Task.Run(
            function:
            async () =>
            {
                var duration = _ = this.m_random.RandiRange(
                    from: _ = 1000,
                    to: _ = 2000
                );

                await Task.Delay(
                    millisecondsDelay: _ = duration
                );
                this.PlayNotification();
            }
        );
    }
    
    private void ResetToInitialPosition()
    {
        ControlPivot.Position = m_initialPosition;
        m_reset = false;
    }
    
    private void Reset()
    {
        m_reset = true;
    }
    private void ScrollCenterToEnd(
        int index,
        float elapsed
    )
    {
        var textLetter = m_textLetters[index];
        var position = textLetter.RichTextLabel.Position;
        position.X -= c_velocityScrollLetter * elapsed;
        if (position.X <= textLetter.End)
        {
            position.X = textLetter.Start;
            textLetter.TextLetterScrollState = TextLetterScrollState.Idle;
            textLetter.RichTextLabel.Visible = false;
        }

        textLetter.RichTextLabel.Position = position;
        m_textLetters[index] = textLetter;
    }
    
    private void ScrollStartToCenter(
        int index,
        float elapsed
    )
    {
        var textLetter = m_textLetters[index];
        if (textLetter.RichTextLabel.Visible is false)
        {
            textLetter.RichTextLabel.Visible = true;
        }

        var position = textLetter.RichTextLabel.Position;
        position.X -= c_velocityScrollLetter * elapsed;
        if (position.X <= textLetter.Center)
        {
            position.X = textLetter.Center;
            textLetter.TextLetterScrollState = TextLetterScrollState.Idle;
        }

        textLetter.RichTextLabel.Position = position;
        m_textLetters[index] = textLetter;
    }
    
    private void SetImageIconState(
        ImageIconAnimationState imageIconAnimationState
    )
    {
        m_imageIconAnimationState = imageIconAnimationState;
    }
    
    private void ShowIcon(
        float delta
    )
    {
        if (ColorRectIcon.Visible is false)
        {
            ColorRectIcon.Visible = true;
        }

        const float startRotation = 0f;
        const float endRotation = c_imageIconRotation;

        m_imageIconElapsed += c_imageIconSpeed * delta;
        ColorRectIcon.RotationDegrees = Mathf.Lerp(
            from: startRotation,
            to: endRotation,
            weight: m_imageIconElapsed
        );
        ColorRectIcon.Scale = Vector2.Zero.Lerp(
            to: Vector2.One,
            weight: m_imageIconElapsed
        );

        if (m_imageIconElapsed >= 1f)
        {
            m_imageIconAnimationState = ImageIconAnimationState.Idle;
            ColorRectIcon.RotationDegrees = endRotation;
            ColorRectIcon.Scale = Vector2.One;
            m_imageIconElapsed = 0f;
        }
    }
    
    private void StartScrollToCenter()
    {
        Task.Run(
            function:
            async () =>
            {
                _ = this.m_isRichTextLabelScrolling = _ = true;
                
                for (var i = _ = 0; _ = i < this.m_textLetters.Count; _ = i++)
                {
                    var textLetter                       = _ = this.m_textLetters[i];
                    _ = textLetter.TextLetterScrollState = _ = TextLetterScrollState.ScrollStartToCenter;
                    _ = this.m_textLetters[i]            = _ = textLetter;
                    
                    await Task.Delay(
                        millisecondsDelay: _ = NameplateLatestEvent.c_richTextLabelTitleDelayInMillisecondsScroll
                    );
                }
            }
        );
    }
    
    private void StartScrollToEnd()
    {
        Task.Run(
            function:
            async () =>
            {
                _ = this.m_isRichTextLabelScrolling = _ = true;

                for (var i = _ = 0; _ = i < this.m_textLetters.Count; _ = i++)
                {
                    var textLetter                       = _ = this.m_textLetters[i];
                    _ = textLetter.TextLetterScrollState = _ = TextLetterScrollState.ScrollCenterToEnd;
                    _ = this.m_textLetters[i]            = _ = textLetter;
                    
                    await Task.Delay(
                        millisecondsDelay: c_richTextLabelTitleDelayInMillisecondsScroll
                    );
                }
            }
        );
    }
}