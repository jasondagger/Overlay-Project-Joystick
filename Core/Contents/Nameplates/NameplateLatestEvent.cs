
using Godot;
using Overlay.Core.Contents.Effects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vector2 = Godot.Vector2;

namespace Overlay.Core.Contents.Nameplates;

internal abstract partial class NameplateLatestEvent() :
    Control()
{
    [Export] public RichTextLabelSampler RichTextLabelSampler = null;
    [Export] public ColorRect            ColorRectIcon        = null;
    [Export] public Control              ControlPivot         = null;
    [Export] public float                ScrollDistance       = 480f;
    [Export] public bool                 SendDatabaseUpdate   = true;

    public override void _Process(
        double elapsed
    )
    {
        if (this.m_move)
        {
            var position    = this.ControlPivot.Position;
            position.X -= NameplateLatestEvent.c_velocityScrollControl * (float)elapsed;

            if (position.X <= this.m_targetX)
            {
                position.X  = this.m_targetX;
                this.m_move = false;
            }

            this.ControlPivot.Position = position;
        }

        if (this.m_isRichTextLabelScrolling)
        {
            this.AnimateScroll(
                elapsed: (float)elapsed
            );
        }

        this.AnimateIcon(
            elapsed: (float)elapsed
        );

        if (this.m_reset)
        {
            this.ResetToInitialPosition();
        }
    }
    
    public override void _EnterTree()
    {
        base._EnterTree();
        
        this.RegisterForJoystickEvents();
        this.RichTextLabelSampler.LoadRichTextLabelsAndAttachToParentNode(
            parent: this.ControlPivot
        );
    }
    
    protected readonly Queue<string> m_names        = new();
    protected readonly Queue<string> m_pendingNames = new();

    protected void Play()
    {
        _ = this.PlayNotification();
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
        public readonly RichTextLabel RichTextLabel         = richTextLabel;
        public TextLetterScrollState  TextLetterScrollState = textLetterScrollState;
        public readonly float         Center                = center;
        public readonly float         Start                 = start;
        public readonly float         End                   = end;
    }
    
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
        for (var i = 0; i < this.m_textLetters.Count; i++)
        {
            if (
                this.m_textLetters.TryGetValue(
                    key:   i,
                    value: out var textLetter
                ) is false
            )
            {
                continue;
            }
                
            switch (textLetter.TextLetterScrollState)
            {
                case TextLetterScrollState.ScrollCenterToEnd:
                    this.ScrollCenterToEnd(
                        index:   i,
                        elapsed: elapsed
                    );
                    break;
                case TextLetterScrollState.ScrollStartToCenter:
                    this.ScrollStartToCenter(
                        index:   i,
                        elapsed: elapsed
                    );
                    break;

                case TextLetterScrollState.Idle:
                default:
                    break;
            }
        }

        if (
            this.m_textLetters.Values.All(
                predicate: x => 
                    x.TextLetterScrollState is TextLetterScrollState.Idle
            ) is true
        )
        {
            this.m_isRichTextLabelScrolling = false;
        }
    }
    
    private void AnimateIcon(
        float elapsed
    )
    {
        switch (this.m_imageIconAnimationState)
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
                value: new TextLetter(
                    richTextLabel: richTextLabel,
                    textLetterScrollState: TextLetterScrollState.Idle,
                    center: richTextLabel.Position.X,
                    start: richTextLabel.Position.X + ScrollDistance,
                    end: richTextLabel.Position.X - ScrollDistance - positionX
                )
            );

            var position = m_textLetters[i].RichTextLabel.Position;
            position.X = m_textLetters[i].Start;
            m_textLetters[i].RichTextLabel.Position = position;

            positionX += richTextLabel.GetContentWidth();
        }

        m_distanceOffScreen = positionX - ScrollDistance;
        m_targetX = m_initialPosition.X - m_distanceOffScreen;
    }
    
    private void DestroyTextLetters()
    {
        foreach (var t in Text)
        {
            this.RichTextLabelSampler.RequeueRichTextLabel(
                letter: t
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
        this.RunSafe(            
            taskFactory: async () =>
            {
                var duration = this.m_random.RandiRange(
                    from: 1000,
                    to: 2000
                );
    
                var tree  = this.GetTree();
                var timer = tree.CreateTimer(
                    timeSec: duration / 1000f
                );
                await this.ToSignal(
                    source: timer,
                    signal: "timeout"
                );

                this.Play();
            }
        );
    }
    
    private async Task PlayNotification() 
    {
        var tree  = this.GetTree();
        for (var i = 0; i < this.m_names.Count; i++)
        {
            this.Text = this.m_names.ElementAt(
                index: i
            );

            this.CreateTextLetters();

            var timer = tree.CreateTimer(
                timeSec: NameplateLatestEvent.c_richTextLabelTitleDelayInMilliseconds / 1000f
            );
            await this.ToSignal(
                source: timer,
                signal: "timeout"
            );

            await this.StartScrollToCenter();
            this.SetImageIconState(
                imageIconAnimationState: ImageIconAnimationState.Showing
            );

            if (this.m_distanceOffScreen > 0f)
            {
                var animationDelay = Mathf.RoundToInt(
                    s: this.m_distanceOffScreen / NameplateLatestEvent.c_velocityScrollControlInMilliseconds
                );
                var remainingScreenDuration = NameplateLatestEvent.c_richTextLabelOnScreenDurationMax - animationDelay;
                var halfDelay = remainingScreenDuration / 2000f;

                timer = tree.CreateTimer(
                    timeSec: halfDelay
                );
                await this.ToSignal(
                    source: timer,
                    signal: "timeout"
                );

                this.m_move = true;

                timer = tree.CreateTimer(
                    timeSec: halfDelay
                );
                await this.ToSignal(
                    source: timer,
                    signal: "timeout"
                );
            }
            else
            {
                timer = tree.CreateTimer(
                    timeSec: NameplateLatestEvent.c_richTextLabelOnScreenDurationMax / 1000f
                );
                await this.ToSignal(
                    source: timer,
                    signal: "timeout"
                );
            }

            timer = tree.CreateTimer(
                timeSec: NameplateLatestEvent.c_richTextLabelTitleDelayInMilliseconds / 1000f
            );
            await this.ToSignal(
                source: timer,
                signal: "timeout"
            );
            
            await this.StartScrollToEnd();
            this.SetImageIconState(
                imageIconAnimationState: ImageIconAnimationState.Hiding
            );

            timer = tree.CreateTimer(
                timeSec: NameplateLatestEvent.c_richTextLabelTitleDelayInMillisecondsCompletion / 1000f
            );
            await this.ToSignal(
                source: timer,
                signal: "timeout"
            );

            this.DestroyTextLetters();
            this.Reset();
        }

        while (this.m_pendingNames.Count > 0)
        {
            this.m_names.Enqueue(this.m_pendingNames.Dequeue());
        }
        
        this.QueueNotification();
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
    
    private async void RunSafe(
        System.Func<Task> taskFactory
    )
    {
        try
        {
            await taskFactory();
        }
        catch
        {
            this.m_isRichTextLabelScrolling = false;
            this.m_move = false;
            this.DestroyTextLetters();
        }
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
    
    private async Task StartScrollToCenter()
    {
        this.m_isRichTextLabelScrolling = true;
                
        for (var i = 0; i < this.m_textLetters.Count; i++)
        {
            var textLetter                   = this.m_textLetters[i];
            textLetter.TextLetterScrollState = TextLetterScrollState.ScrollStartToCenter;
            this.m_textLetters[i]            = textLetter;

            var tree  = this.GetTree();
            var timer = tree.CreateTimer(
                timeSec: NameplateLatestEvent.c_richTextLabelTitleDelayInMillisecondsScroll / 1000f
            );
            await this.ToSignal(
                source: timer, 
                signal: "timeout"
            );
        }
    }
    
    private async Task StartScrollToEnd()
    {
        this.m_isRichTextLabelScrolling = true;

        for (var i = 0; i < this.m_textLetters.Count; i++)
        {
            var textLetter                   = this.m_textLetters[i];
            textLetter.TextLetterScrollState = TextLetterScrollState.ScrollCenterToEnd;
            this.m_textLetters[i]            = textLetter;
                    
            var tree  = this.GetTree();
            var timer = tree.CreateTimer(
                timeSec: NameplateLatestEvent.c_richTextLabelTitleDelayInMillisecondsScroll / 1000f
            );
            await this.ToSignal(
                source: timer, 
                signal: "timeout"
            );
        }
    }
}