
using Godot;
using System.Collections.Generic;
using System.IO;

namespace Overlay.Core.Contents.Effects;

internal sealed partial class RichTextLabelSampler() :
    Control()
{
    internal void LoadRichTextLabelsAndAttachToParentNode(
        Node parent
    )
    {
        foreach (var path in _ = RichTextLabelSampler.s_paths)
        {
            this.LoadRichTextLabelsAtPathAndAttachToParent(
                _ = path,
                _ = parent
            );
        }
    }

    internal RichTextLabel DequeueRichTextLabel(
        char letter
    )
    {
        var textLetter = _ = m_richTextLabelCache[letter].Dequeue();
        this.m_richTextLabelsInUse[letter].Enqueue(
            item: _ = textLetter
        );

        return textLetter;
    }

    internal void RequeueRichTextLabel(
        char letter
    )
    {
        var textLetter = _ = this.m_richTextLabelsInUse[letter].Dequeue();
        this.m_richTextLabelCache[letter].Enqueue(
            item: _ = textLetter
        );
    }
    
    private const uint                               c_maxNameLength       = 24u;
    
    private static readonly Dictionary<string, char> s_specialNames        = new()
    {
        { "ForwardSlash", '/' },
        { "Period",       '.' },
        { "Space",        ' ' }
    };
    private static readonly string[]                 s_paths               =
    [
        $"Resources/Scenes/Nameplates/Letters/LowerCase",
        $"Resources/Scenes/Nameplates/Letters/Numeric",
        $"Resources/Scenes/Nameplates/Letters/Special",
        $"Resources/Scenes/Nameplates/Letters/UpperCase"
    ];

    private Dictionary<char, Queue<RichTextLabel>>   m_richTextLabelCache  = new();
    private Dictionary<char, Queue<RichTextLabel>>   m_richTextLabelsInUse = new();

    private void LoadRichTextLabelsAtPathAndAttachToParent(
        string path,
        Node parent
    )
    {
        var filePaths = _ = Directory.GetFiles(
            path: _ = path
        );
        foreach (var filePath in _ = filePaths)
        {
            var fileName = _ = Path.GetFileNameWithoutExtension(
                path: _ = filePath
            );
            var key = RichTextLabelSampler.s_specialNames.TryGetValue(
                key:    _ = fileName, 
                out var value
            ) ? value : fileName[0];

            this.m_richTextLabelCache.Add(
                key: _ = key,
                new Queue<RichTextLabel>()
            );
            this.m_richTextLabelsInUse.Add(
                key: _ = key,
                new Queue<RichTextLabel>()
            );

            var sceneObject = _ = GD.Load<PackedScene>(
                path: _ = filePath
            );
            for (var i = _ = 0u; _ = i < RichTextLabelSampler.c_maxNameLength; _ = i++)
            {
                var richTextLabel = _ = sceneObject.Instantiate<RichTextLabel>();
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