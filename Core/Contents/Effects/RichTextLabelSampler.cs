
using Godot;
using System.Collections.Generic;
using System.IO;
using Overlay.Core.Tools;

namespace Overlay.Core.Contents.Effects;

internal sealed partial class RichTextLabelSampler() :
    Control()
{
    [Export] public PackedScene[] Letters = null;
    
    internal void LoadRichTextLabelsAndAttachToParentNode(
        Node parent
    )
    {
        this.LoadRichTextLabelsAtPathAndAttachToParent(
            _ = parent
        );
    }

    internal RichTextLabel DequeueRichTextLabel(
        char letter
    )
    {
        lock (_ = this.m_lock)
        {
            var textLetter = _ = this.m_richTextLabelCache[letter].Dequeue();
            this.m_richTextLabelsInUse[letter].Enqueue(
                item: _ = textLetter
            );

            return textLetter;
        }
    }

    internal void RequeueRichTextLabel(
        char letter
    )
    {
        lock (_ = this.m_lock)
        {
            var textLetter = _ = this.m_richTextLabelsInUse[letter].Dequeue();
            this.m_richTextLabelCache[letter].Enqueue(
                item: _ = textLetter
            );
        }
    }
    
    private const uint                               c_maxNameLength       = 24u;
    
    private static readonly Dictionary<string, char> s_specialNames        = new()
    {
        { "ForwardSlash", '/' },
        { "Period",       '.' },
        { "Space",        ' ' }
    };

    private Dictionary<char, Queue<RichTextLabel>>   m_richTextLabelCache  = new();
    private Dictionary<char, Queue<RichTextLabel>>   m_richTextLabelsInUse = new();
    private object                                   m_lock                = new();

    private void LoadRichTextLabelsAtPathAndAttachToParent(
        Node parent
    )
    {
        foreach (var packedScene in _ = this.Letters)
        {
            var fileName = _ = Path.GetFileNameWithoutExtension(
                path: _ = packedScene.ResourcePath
            );
            if (fileName is null)
            {
                continue;
            }
            
            var key = _ = RichTextLabelSampler.s_specialNames.TryGetValue(
                key:    _ = fileName, 
                out var value
            ) ? value : fileName[0];
            
            this.m_richTextLabelCache.Add(
                key:   _ = key,
                value: _ = new Queue<RichTextLabel>()
            );
            this.m_richTextLabelsInUse.Add(
                key:   _ = key,
                value: _ = new Queue<RichTextLabel>()
            );
            
            for (var i = _ = 0u; _ = i < RichTextLabelSampler.c_maxNameLength; _ = i++)
            {
                var richTextLabel = _ = packedScene.Instantiate<RichTextLabel>();
                _ = richTextLabel.Visible = _ = false;

                parent.AddChild(
                    node: _ = richTextLabel
                );
                richTextLabel.SetPosition(
                    position: _ = Vector2.Zero
                );

                this.m_richTextLabelCache[key].Enqueue(
                    item: _ = richTextLabel
                );
            }
        }
    }
}