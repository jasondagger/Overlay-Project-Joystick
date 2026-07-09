
using Godot;
using Overlay.Core.Contents.Effects;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vector2 = Godot.Vector2;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class NameplateLatestMessage() :
    Control()
{
    [Export] public RichTextLabelSampler RichTextLabelSampler = null;
    [Export] public ColorRect            ColorRectIcon        = null;
    [Export] public Control              ControlPivot         = null;

    public override void _Process(
        double elapsed
    )
    {
        if (this.m_move)
        {
            var position    = this.ControlPivot.Position;
            position.X -= NameplateLatestMessage.c_velocityScrollControl * (float)elapsed;

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

        this.RichTextLabelSampler.LoadRichTextLabelsAndAttachToParentNode(
            parent: this.ControlPivot
        );
        this.PlayNotification();
    }
    
    public override void _ExitTree()
    {
        this.m_cancellationTokenSourceScroll.Cancel();
        this.m_cancellationTokenSourceScroll.Dispose();
        
        base._ExitTree();
    }

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
    ) : IEquatable<TextLetter>
    {
        public readonly RichTextLabel RichTextLabel         = richTextLabel;
        public TextLetterScrollState  TextLetterScrollState = textLetterScrollState;
        public readonly float         Center                = center;
        public readonly float         Start                 = start;
        public readonly float         End                   = end;

        public bool Equals(
            TextLetter other
        )
        {
            return object.Equals(
                objA: this.RichTextLabel,
                objB: other.RichTextLabel
            ) && 
           this.TextLetterScrollState == other.TextLetterScrollState && 
           this.Center.Equals(
               obj: other.Center
            ) && 
           this.Start.Equals(
               obj: other.Start
            ) && 
           this.End.Equals(
               obj: other.End
            );
        }

        public override bool Equals(
            object obj
        )
        {
            return obj is TextLetter other && this.Equals(
                other: other
            );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                value1: this.RichTextLabel, 
                value2: (int) this.TextLetterScrollState, 
                value3: this.Center, 
                value4: this.Start, 
                value5: this.End
            );
        }
    }
    
    private const float                                    c_scrollDistance                                                = 480f;
    
    private const int                                      c_richTextLabelOnScreenDurationMax                              = 8000;
    private const int                                      c_richTextLabelTitleDelayInMilliseconds                         = 500;
    private const int                                      c_richTextLabelTitleDelayInMillisecondsCompletion               = 2000;
    private const float                                    c_velocityScrollControl                                         = 250f;
    private const float                                    c_velocityScrollControlInMilliseconds                           = NameplateLatestMessage.c_velocityScrollControl * 1000f;
    
    private const int                                      c_richTextLabelTitleDelayInMillisecondsScroll                   = 35;
    private const float                                    c_velocityScrollLetter                                          = 900f;
    
    private const float                                    c_imageIconSpeed                                                = 2f;
    private const float                                    c_imageIconRotation                                             = -2160f;
    
    private static readonly string[]                       s_messages                                                      =
    [
        $"Thank you for hanging out here!",
        $"Feel free to lurk or chat!",
        $"Make sure you're following the rules.",
        $"Add SmoothDagger on Steam to play!",
        $"Stay hydrated!",
    ];
    
    private bool                                           m_isRichTextLabelScrolling                                      = false;
    private ImageIconAnimationState                        m_imageIconAnimationState                                       = ImageIconAnimationState.Idle;
    private float                                          m_imageIconElapsed                                              = 0f;
    
    private string                                         Text                                              { get; set; } = string.Empty;
    private readonly ConcurrentDictionary<int, TextLetter> m_textLetters                                                   = new();
    private Vector2                                        m_initialPosition                                               = Vector2.Zero;
    private RandomNumberGenerator                          m_random                                                        = new();
    private bool                                           m_reset                                                         = false;
    private bool                                           m_move                                                          = false;
    private float                                          m_distanceOffScreen                                             = 0f;
    private float                                          m_targetX                                                       = 0f;
    private int                                            m_lastMessageIndex                                              = -1;
    private CancellationTokenSource                        m_cancellationTokenSourceScroll                                 = new();

    private void AnimateScroll(
        float elapsed
    )
    {
        var isStillScrolling = false;
        for (var i = 0; i < this.m_textLetters.Count; i++)
        {
            var hasValue = this.m_textLetters.TryGetValue(
                key:   i, 
                value: out var textLetter
            );
            if (hasValue)
            {
                switch (textLetter.TextLetterScrollState)
                {
                    case TextLetterScrollState.ScrollCenterToEnd:
                        this.ScrollCenterToEnd(
                            index:   i,
                            elapsed: elapsed
                        );
                        isStillScrolling = true;
                        break;
                    case TextLetterScrollState.ScrollStartToCenter:
                        this.ScrollStartToCenter(
                            index:   i,
                            elapsed: elapsed
                        );
                        isStillScrolling = true;
                        break;

                    case TextLetterScrollState.Idle:
                    default:
                        break;
                }
            }
        }

        this.m_isRichTextLabelScrolling = isStillScrolling;
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
        for (var i = 0; i < this.Text.Length; i++)
        {
            var letter        = this.Text[i];
            var richTextLabel = this.RichTextLabelSampler.DequeueRichTextLabel(
                letter: letter
            );
            
            richTextLabel.Position = new Vector2(
                x: positionX,
                y: 0f
            );
            
            var newLetter = new TextLetter(
                richTextLabel:         richTextLabel,
                textLetterScrollState: TextLetterScrollState.Idle,
                center:                richTextLabel.Position.X,
                start:                 richTextLabel.Position.X + NameplateLatestMessage.c_scrollDistance,
                end:                   richTextLabel.Position.X - NameplateLatestMessage.c_scrollDistance - positionX
            );
            this.m_textLetters.AddOrUpdate(
                key:                i,
                addValue:           newLetter,
                updateValueFactory: (key, oldValue) => newLetter
            );

            var position                                 = this.m_textLetters[i].RichTextLabel.Position;
            position.X                                   = this.m_textLetters[i].Start;
            this.m_textLetters[i].RichTextLabel.Position = position;

            positionX += richTextLabel.GetContentWidth();
        }

        this.m_distanceOffScreen = positionX - NameplateLatestMessage.c_scrollDistance;
        this.m_targetX           = this.m_initialPosition.X - this.m_distanceOffScreen;
    }
    
    private void DestroyTextLetters()
    {
        this.m_cancellationTokenSourceScroll.Cancel();
        this.m_cancellationTokenSourceScroll = new CancellationTokenSource();
        
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
        const float startRotation = NameplateLatestMessage.c_imageIconRotation;
        const float endRotation   = 0f;

        this.m_imageIconElapsed += NameplateLatestMessage.c_imageIconSpeed * delta;
        this.ColorRectIcon.RotationDegrees = Mathf.Lerp(
            from:   startRotation,
            to:     endRotation,
            weight: this.m_imageIconElapsed
        );
        this.ColorRectIcon.Scale = Vector2.One.Lerp(
            to:     Vector2.Zero,
            weight: this.m_imageIconElapsed
        );

        if (this.m_imageIconElapsed >= 1f)
        {
            this.m_imageIconAnimationState     = ImageIconAnimationState.Idle;
            this.ColorRectIcon.RotationDegrees = endRotation;
            this.ColorRectIcon.Scale           = Vector2.Zero;
            this.ColorRectIcon.Visible         = false;
            this.m_imageIconElapsed            = 0f;
        }
    }
    
    private bool IsFooterTextCentered()
    {
        return this.m_textLetters.Select(
            selector: footerTextLetter => footerTextLetter.Value
        ).All(
            predicate: textLetter => textLetter.TextLetterScrollState is TextLetterScrollState.Idle
        );
    }
    
    private void PlayNotification() 
    {
        Task.Run(
            function:
            async () =>
            {
                var index = this.m_lastMessageIndex;
                while (index == this.m_lastMessageIndex)
                {
                    index = this.m_random.RandiRange(
                        from: 0,
                        to:   NameplateLatestMessage.s_messages.Length - 1
                    );
                }
                this.m_lastMessageIndex = index;
                
                this.Text = NameplateLatestMessage.s_messages.ElementAt(
                    index: index
                );
                
                this.CallDeferred(
                    method: $"{nameof(NameplateLatestMessage.CreateTextLetters)}"
                );

                await Task.Delay(
                    millisecondsDelay: NameplateLatestMessage.c_richTextLabelTitleDelayInMilliseconds
                );

                this.StartScrollToCenter();
                this.SetImageIconState(
                    imageIconAnimationState: ImageIconAnimationState.Showing
                );

                if (this.m_distanceOffScreen > 0f)
                {
                    var animationDelay = Mathf.RoundToInt(
                        s: this.m_distanceOffScreen / NameplateLatestMessage.c_velocityScrollControlInMilliseconds
                    );
                    var remainingScreenDuration = NameplateLatestMessage.c_richTextLabelOnScreenDurationMax - animationDelay;
                    var halfDelay = Mathf.RoundToInt(
                        s: remainingScreenDuration / 2f
                    );

                    await Task.Delay(
                        millisecondsDelay: halfDelay
                    );

                    this.m_move = true;

                    await Task.Delay(
                        millisecondsDelay: halfDelay
                    );
                }
                else
                {
                    await Task.Delay(
                        millisecondsDelay: NameplateLatestMessage.c_richTextLabelOnScreenDurationMax
                    );
                }
                
                await Task.Delay(
                    millisecondsDelay: NameplateLatestMessage.c_richTextLabelTitleDelayInMilliseconds
                );
                
                this.StartScrollToEnd();
                this.SetImageIconState(
                    imageIconAnimationState: ImageIconAnimationState.Hiding
                );

                await Task.Delay(
                    millisecondsDelay: NameplateLatestMessage.c_richTextLabelTitleDelayInMillisecondsCompletion
                );

                this.DestroyTextLetters();
                this.Reset();
                this.QueueNotification();
            }
        );
    }

    private void QueueNotification()
    {
        Task.Run(
            function:
            async () =>
            {
                var duration = this.m_random.RandiRange(
                    from: 1000,
                    to:   2000
                );

                await Task.Delay(
                    millisecondsDelay: duration
                );
                this.PlayNotification();
            }
        );
    }
    
    private void ResetToInitialPosition()
    {
        this.ControlPivot.Position = this.m_initialPosition;
        this.m_reset               = false;
    }
    
    private void Reset()
    {
        this.m_reset = true;
    }
    private void ScrollCenterToEnd(
        int   index,
        float elapsed
    )
    {
        if (
            this.m_textLetters.TryGetValue(
                key:   index, 
                value: out var textLetter
            ) is false
        )
        {
            return;
        }
        
        var position =  textLetter.RichTextLabel.Position;
        position.X   -= NameplateLatestMessage.c_velocityScrollLetter * elapsed;
        if (position.X <= textLetter.End)
        {
            position.X = textLetter.Start;
            textLetter.TextLetterScrollState = TextLetterScrollState.Idle;
            textLetter.RichTextLabel.Visible = false;
        }

        textLetter.RichTextLabel.Position = position;
        this.m_textLetters[key: index]    = textLetter;
    }
    
    private void ScrollStartToCenter(
        int   index,
        float elapsed
    )
    {
        if (
            this.m_textLetters.TryGetValue(
                key:   index, 
                value: out var textLetter
            ) is false
        )
        {
            return;
        }
        
        if (textLetter.RichTextLabel.Visible is false)
        {
            textLetter.RichTextLabel.Visible = true;
        }

        var position =  textLetter.RichTextLabel.Position;
        position.X   -= NameplateLatestMessage.c_velocityScrollLetter * elapsed;
        if (position.X <= textLetter.Center)
        {
            position.X = textLetter.Center;
            textLetter.TextLetterScrollState = TextLetterScrollState.Idle;
        }

        textLetter.RichTextLabel.Position = position;
        this.m_textLetters[key: index] = textLetter;
    }
    
    private void SetImageIconState(
        ImageIconAnimationState imageIconAnimationState
    )
    {
        this.m_imageIconAnimationState = imageIconAnimationState;
    }
    
    private void ShowIcon(
        float delta
    )
    {
        if (this.ColorRectIcon.Visible is false)
        {
            this.ColorRectIcon.Visible = true;
        }

        const float startRotation = 0f;

        this.m_imageIconElapsed            += NameplateLatestMessage.c_imageIconSpeed * delta;
        this.ColorRectIcon.RotationDegrees =  Mathf.Lerp(
            from:   startRotation,
            to:     NameplateLatestMessage.c_imageIconRotation,
            weight: this.m_imageIconElapsed
        );
        this.ColorRectIcon.Scale = Vector2.Zero.Lerp(
            to:     Vector2.One,
            weight: this.m_imageIconElapsed
        );

        if (this.m_imageIconElapsed >= 1f)
        {
            this.m_imageIconAnimationState     = ImageIconAnimationState.Idle;
            this.ColorRectIcon.RotationDegrees = NameplateLatestMessage.c_imageIconRotation;
            this.ColorRectIcon.Scale           = Vector2.One;
            this.m_imageIconElapsed            = 0f;
        }
    }
    
    private void StartScrollToCenter()
    {
        var cancellationToken = this.m_cancellationTokenSourceScroll.Token;
        Task.Run(
            function:
            async () =>
            {
                this.m_isRichTextLabelScrolling = true;
                
                for (var i = 0; i < this.m_textLetters.Count; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    if (
                        this.m_textLetters.TryGetValue(
                            key:   i, 
                            value: out var textLetter
                        ) is true
                    )
                    {
                        textLetter.TextLetterScrollState = TextLetterScrollState.ScrollStartToCenter;
                        this.m_textLetters[key: i]       = textLetter;
                    }
                    
                    await Task.Delay(
                        millisecondsDelay: NameplateLatestMessage.c_richTextLabelTitleDelayInMillisecondsScroll,
                        cancellationToken
                    );
                }
            },
            cancellationToken: cancellationToken
        );
    }
    
    private void StartScrollToEnd()
    {
        var cancellationToken = this.m_cancellationTokenSourceScroll.Token;
        Task.Run(
            function:
            async () =>
            {
                this.m_isRichTextLabelScrolling = true;

                for (var i = 0; i < this.m_textLetters.Count; i++)
                {
                    if (
                        this.m_textLetters.TryGetValue(
                            key:   i, 
                            value: out var textLetter
                        ) is true
                    )
                    {
                        textLetter.TextLetterScrollState = TextLetterScrollState.ScrollCenterToEnd;
                        this.m_textLetters[key: i]       = textLetter;
                    }
                    
                    await Task.Delay(
                        millisecondsDelay: NameplateLatestMessage.c_richTextLabelTitleDelayInMillisecondsScroll,
                        cancellationToken: cancellationToken
                    );
                }
            },
            cancellationToken: cancellationToken
        );
    }
}