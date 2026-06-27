
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
            parent
        );
    }

    internal RichTextLabel DequeueRichTextLabel(
        char letter
    )
    {
        lock (this.m_lock)
        {
            var textLetter = this.m_richTextLabelCache[letter].Dequeue();
            this.m_richTextLabelsInUse[letter].Enqueue(
                item: textLetter
            );

            return textLetter;
        }
    }

    internal void RequeueRichTextLabel(
        char letter
    )
    {
        lock (this.m_lock)
        {
            var textLetter = this.m_richTextLabelsInUse[letter].Dequeue();
            this.m_richTextLabelCache[letter].Enqueue(
                item: textLetter
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
        foreach (var packedScene in this.Letters)
        {
            var fileName = Path.GetFileNameWithoutExtension(
                path: packedScene.ResourcePath
            );
            if (fileName is null)
            {
                continue;
            }
            
            var key = RichTextLabelSampler.s_specialNames.TryGetValue(
                key:    fileName, 
                out var value
            ) ? value : fileName[0];
            
            this.m_richTextLabelCache.Add(
                key:   key,
                value: new Queue<RichTextLabel>()
            );
            this.m_richTextLabelsInUse.Add(
                key:   key,
                value: new Queue<RichTextLabel>()
            );
            
            for (var i = 0u; i < RichTextLabelSampler.c_maxNameLength; i++)
            {
                var richTextLabel = packedScene.Instantiate<RichTextLabel>();
                richTextLabel.Visible = false;

                parent.AddChild(
                    node: richTextLabel
                );
                richTextLabel.SetPosition(
                    position: Vector2.Zero
                );

                this.m_richTextLabelCache[key].Enqueue(
                    item: richTextLabel
                );
            }
        }
    }
}