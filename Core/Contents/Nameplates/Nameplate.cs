
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Nameplates;

internal sealed partial class Nameplate() :
    Control()
{
    public override void _ExitTree()
    {
        this.m_isRunning = false;
        this.RemoveInstance();
    }

    public override void _Process(
        double elapsed
    )
    {
        if (this.m_isRichTextLabelNameAnimating is true)
        {
            this.AnimateNameScale(
                elapsed: (float)elapsed
            );
        }

        this.AnimateNameColor();
    }

    public override void _Ready()
    {
        this.AddInstance();
        this.RetrieveResources();
        this.AnimateName();
    }
    
    internal static void SetInitialNameColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        Nameplate.s_initialColor = color;
    }
    
    internal static void UpdateNameColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        foreach (var instance in Nameplate.Instances)
        {
            lock (instance.m_lock)
            {
                instance.m_color = color;
            }
        }
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
        public readonly string                Text          = string.Empty;
        public NameRichTextLabelFontSizeState State         = NameRichTextLabelFontSizeState.Idle;
        public float                          Max           = 0f;
        public float                          Min           = 0f;

        public NameLetter(
            RichTextLabel                  richTextLabel,
            string                         text,
            NameRichTextLabelFontSizeState state,
            float                          max,
            float                          min
        )
        {
            this.RichTextLabel = richTextLabel;
            this.Text          = text;
            this.State         = state;
            this.Max           = max;
            this.Min           = min;
        }
    }

    private const string                             c_labelColorInterpolator                               = "F2F2F2FF";
    private const int                                c_richTextLabelNameDelayTimeInMilliseconds             = 50;
    private const int                                c_richTextLabelNameAnimationDelayTimeInMillisecondsMin = 5000;
    private const int                                c_richTextLabelNameAnimationDelayTimeInMillisecondsMax = 9000;
    private const float                              c_richTextLabelNameScaleVelocity                       = 0.15f;
    private const float                              c_richTextLabelNameScaleMin                            = 1f;
    private const float                              c_richTextLabelNameScaleMax                            = 1.075f;
    private const int                                c_taskAwaitDelayTimeInMilliseconds                     = 20;
    
    private static readonly List<Nameplate>          Instances                                              = [];
    private static ServiceColorInterpolatorColorMode s_initialColor                                         = ServiceColorInterpolatorColorMode.Transition;
    
    private readonly object                          m_lock                                                 = new();
    private readonly Dictionary<int, NameLetter>     m_nameLetters                                          = new();
    
    private ServiceColorInterpolatorColorMode        m_color                                                = ServiceColorInterpolatorColorMode.Transition;
    private bool                                     m_isRichTextLabelNameAnimating                         = false;
    private bool                                     m_isRunning                                            = true;
    private ServiceColorInterpolatorNormal           m_serviceColorInterpolatorNormal                       = null;
    
    private void AddInstance()
    {
        Nameplate.Instances.Add(
            item: this
        );
    }
    
    private void AnimateName()
    {
        Task.Run(
            function: async () =>
            {
                var random = new Random();
                while (this.m_isRunning is true)
                {
                    await Task.Delay(
                        millisecondsDelay: random.Next(
                            minValue: Nameplate.c_richTextLabelNameAnimationDelayTimeInMillisecondsMin,
                            maxValue: Nameplate.c_richTextLabelNameAnimationDelayTimeInMillisecondsMax
                        )
                    );

                    this.m_isRichTextLabelNameAnimating = true;
                    for (var i = 0; i < this.m_nameLetters.Count; i++)
                    {
                        var nameLetter            = this.m_nameLetters[i];
                        nameLetter.State      = NameRichTextLabelFontSizeState.IncreaseFontSize;
                        this.m_nameLetters[i] = nameLetter;
                        
                        await Task.Delay(
                            millisecondsDelay: Nameplate.c_richTextLabelNameDelayTimeInMilliseconds
                        );
                    }

                    while (this.m_isRichTextLabelNameAnimating is true)
                    {
                        await Task.Delay(
                            millisecondsDelay: Nameplate.c_taskAwaitDelayTimeInMilliseconds
                        );
                    }
                }
            }
        );
    }

    private void AnimateNameColor()
    {
        string color;
        lock (this.m_lock)
        {
            if (this.m_color is ServiceColorInterpolatorColorMode.Transition)
            {
                color = this.m_serviceColorInterpolatorNormal.GetColorByCurrentModeAsHex(
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
                );
            }
            else
            {
                color = this.m_serviceColorInterpolatorNormal.GetColorByModeAsHex(
                    colorMode:      this.m_color,
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
                );
            }
        }
        
        for (var i = 0; i < this.m_nameLetters.Count; i++)
        {
            var nameLetter    = this.m_nameLetters[i];
            var richTextLabel = nameLetter.RichTextLabel;
            var text          = nameLetter.Text;
            
            richTextLabel.Text = text.Replace(
                oldValue: Nameplate.c_labelColorInterpolator,
                newValue: color
            );
        }
    }

    private void AnimateNameScale(
        float elapsed
    )
    {
        for (var i = 0; i < this.m_nameLetters.Count; i++)
        {
            var nameLetter = this.m_nameLetters[i];
            switch (nameLetter.State)
            {
                case NameRichTextLabelFontSizeState.IncreaseFontSize:
                    this.IncreaseNameLetterFontSize(
                        index: i,
                        delta: elapsed
                    );
                    break;

                case NameRichTextLabelFontSizeState.DecreaseFontSize:
                    this.DecreaseNameLetterFontSize(
                        index: i,
                        delta: elapsed
                    );
                    break;

                case NameRichTextLabelFontSizeState.Idle:
                default:
                    break;
            }
        }

        if (
            this.m_nameLetters.All(
                predicate: x => x.Value.State is NameRichTextLabelFontSizeState.Idle
            ) is true
        )
        {
            this.m_isRichTextLabelNameAnimating = false;
        }
    }

    private void DecreaseNameLetterFontSize(
        int index,
        float delta
    )
    {
        var nameLetter = this.m_nameLetters[index];
        var scale      = nameLetter.RichTextLabel.Scale;

        scale.X -= Nameplate.c_richTextLabelNameScaleVelocity * delta;
        if (scale.X <= Nameplate.c_richTextLabelNameScaleMin)
        {
            scale.X          = Nameplate.c_richTextLabelNameScaleMin;
            nameLetter.State = NameRichTextLabelFontSizeState.Idle;
        }
        
        scale.Y                        = scale.X;
        nameLetter.RichTextLabel.Scale = scale;
        this.m_nameLetters[index]      = nameLetter;
    }

    private void IncreaseNameLetterFontSize(
        int index,
        float delta
    )
    {
        var nameLetter = this.m_nameLetters[index];
        var scale      = nameLetter.RichTextLabel.Scale;

        scale.X += Nameplate.c_richTextLabelNameScaleVelocity * delta;
        if (scale.X >= Nameplate.c_richTextLabelNameScaleMax)
        {
            scale.X          = Nameplate.c_richTextLabelNameScaleMax;
            nameLetter.State = NameRichTextLabelFontSizeState.DecreaseFontSize;
        }
        
        scale.Y                        = scale.X;
        nameLetter.RichTextLabel.Scale = scale;
        this.m_nameLetters[index]      = nameLetter;
    }
    
    private void RemoveInstance()
        {
            Nameplate.Instances.Remove(
                item: this
            );
        }
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorNormal = Services.Services.GetService<ServiceColorInterpolatorNormal>();
        this.m_color                          = Nameplate.s_initialColor;
        
        var nodeLetters = this.GetChildren();
        for (var i = 0; i < nodeLetters.Count; i++)
        {
            var richTextLabel = nodeLetters[i] as RichTextLabel;
            if (richTextLabel is null)
            {
                continue;
            }
            
            this.m_nameLetters.Add(
                key:   i,
                value: new NameLetter(
                    richTextLabel: richTextLabel,
                    text:          richTextLabel.Text,
                    state:         NameRichTextLabelFontSizeState.Idle,
                    max:           Nameplate.c_richTextLabelNameScaleMin,
                    min:           Nameplate.c_richTextLabelNameScaleMax
                )
            );
        }
    }
}