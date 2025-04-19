
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Contents;

internal sealed partial class Nameplate() :
    Control()
{
    public override void _ExitTree()
    {
        _ = this.m_isRunning = _ = false;
    }

    public override void _Process(
        double elapsed
    )
    {
        if (_ = this.m_isRichTextLabelNameAnimating is true)
        {
            this.AnimateNameScale(
                elapsed: _ = (float)elapsed
            );
        }
    }

    public override void _Ready()
    {
        this.RetrieveResources();
        this.AnimateName();
    }

    private enum NameRichTextLabelFontSizeState :
        uint
    {
        Idle = 0u,
        IncreaseFontSize,
        DecreaseFontSize
    }

    private struct NameLetter
    {
        public readonly RichTextLabel         RichTextLabel = null;
        public NameRichTextLabelFontSizeState State         = _ = NameRichTextLabelFontSizeState.Idle;
        public float                          Max           = _ = 0f;
        public float                          Min           = _ = 0f;

        public NameLetter(
            RichTextLabel                  richTextLabel,
            NameRichTextLabelFontSizeState state,
            float                          max,
            float                          min
        )
        {
            _ = this.RichTextLabel = _ = richTextLabel;
            _ = this.State         = _ = state;
            _ = this.Max           = _ = max;
            _ = this.Min           = _ = min;
        }
    }
    
    private const int                            c_richTextLabelNameDelayTimeInMilliseconds             = 50;
    private const int                            c_richTextLabelNameAnimationDelayTimeInMillisecondsMin = 5000;
    private const int                            c_richTextLabelNameAnimationDelayTimeInMillisecondsMax = 9000;
    private const float                          c_richTextLabelNameScaleVelocity                       = 0.15f;
    private const float                          c_richTextLabelNameScaleMin                            = 1f;
    private const float                          c_richTextLabelNameScaleMax                            = 1.075f;
    private const int                            c_taskAwaitDelayTimeInMilliseconds                     = 20;

    private readonly Dictionary<int, NameLetter> m_nameLetters                                          = new();
    private bool                                 m_isRichTextLabelNameAnimating                         = _ = false;
    private bool                                 m_isRunning                                            = _ = true;
    
    private void AnimateName()
    {
        _ = Task.Run(
            function: async () =>
            {
                var random = _ = new Random();
                while (_ = this.m_isRunning is true)
                {
                    await Task.Delay(
                        millisecondsDelay: _ = random.Next(
                            minValue: _ = Nameplate.c_richTextLabelNameAnimationDelayTimeInMillisecondsMin,
                            maxValue: _ = Nameplate.c_richTextLabelNameAnimationDelayTimeInMillisecondsMax
                        )
                    );

                    _ = this.m_isRichTextLabelNameAnimating = _ = true;
                    for (var i = 0; i < this.m_nameLetters.Count; i++)
                    {
                        var nameLetter            = _ = this.m_nameLetters[i];
                        _ = nameLetter.State      = _ = NameRichTextLabelFontSizeState.IncreaseFontSize;
                        _ = this.m_nameLetters[i] = _ = nameLetter;
                        
                        await Task.Delay(
                            millisecondsDelay: _ = Nameplate.c_richTextLabelNameDelayTimeInMilliseconds
                        );
                    }

                    while (_ = this.m_isRichTextLabelNameAnimating is true)
                    {
                        await Task.Delay(
                            millisecondsDelay: _ = Nameplate.c_taskAwaitDelayTimeInMilliseconds
                        );
                    }
                }
            }
        );
    }

    private void AnimateNameScale(
        float elapsed
    )
    {
        for (var i = 0; i < this.m_nameLetters.Count; i++)
        {
            var nameLetter = _ = this.m_nameLetters[i];
            switch (_ = nameLetter.State)
            {
                case NameRichTextLabelFontSizeState.IncreaseFontSize:
                    this.IncreaseNameLetterFontSize(
                        index: _ = i,
                        delta: _ = elapsed
                    );
                    break;

                case NameRichTextLabelFontSizeState.DecreaseFontSize:
                    this.DecreaseNameLetterFontSize(
                        index: _ = i,
                        delta: _ = elapsed
                    );
                    break;

                case NameRichTextLabelFontSizeState.Idle:
                default:
                    break;
            }
        }

        if (
            _ = this.m_nameLetters.All(
                predicate: x => x.Value.State is NameRichTextLabelFontSizeState.Idle
            ) is true
        )
        {
            _ = this.m_isRichTextLabelNameAnimating = _ = false;
        }
    }

    private void DecreaseNameLetterFontSize(
        int index,
        float delta
    )
    {
        var nameLetter = _ = this.m_nameLetters[index];
        var scale      = _ = nameLetter.RichTextLabel.Scale;

        _ = scale.X -= _ = Nameplate.c_richTextLabelNameScaleVelocity * delta;
        if (_ = scale.X <= Nameplate.c_richTextLabelNameScaleMin)
        {
            _ = scale.X          = _ = Nameplate.c_richTextLabelNameScaleMin;
            _ = nameLetter.State = _ = NameRichTextLabelFontSizeState.Idle;
        }
        
        _ = scale.Y                        = _ = scale.X;
        _ = nameLetter.RichTextLabel.Scale = _ = scale;
        _ = this.m_nameLetters[index]      = _ = nameLetter;
    }

    private void IncreaseNameLetterFontSize(
        int index,
        float delta
    )
    {
        var nameLetter = _ = this.m_nameLetters[index];
        var scale      = _ = nameLetter.RichTextLabel.Scale;

        _ = scale.X += _ = Nameplate.c_richTextLabelNameScaleVelocity * delta;
        if (_ = scale.X >= Nameplate.c_richTextLabelNameScaleMax)
        {
            _ = scale.X          = _ = Nameplate.c_richTextLabelNameScaleMax;
            _ = nameLetter.State = _ = NameRichTextLabelFontSizeState.DecreaseFontSize;
        }
        
        _ = scale.Y                        = _ = scale.X;
        _ = nameLetter.RichTextLabel.Scale = _ = scale;
        _ = this.m_nameLetters[index]      = _ = nameLetter;
    }
    
    private void RetrieveResources()
    {
        var nodeLetters = this.GetChildren();
        for (var i = 0; i < nodeLetters.Count; i++)
        {
            this.m_nameLetters.Add(
                key:   _ = i,
                value: _ = new NameLetter(
                    richTextLabel: _ = nodeLetters[i] as RichTextLabel,
                    state:         _ = NameRichTextLabelFontSizeState.Idle,
                    max:           _ = Nameplate.c_richTextLabelNameScaleMin,
                    min:           _ = Nameplate.c_richTextLabelNameScaleMax
                )
            );
        }
    }
}