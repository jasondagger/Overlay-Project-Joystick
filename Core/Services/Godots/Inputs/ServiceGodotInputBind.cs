
namespace Overlay.Core.Services.Godots.Inputs;

using Godot;
using System;
using System.Text.Json.Serialization;

[Serializable()]
internal struct ServiceGodotInputBind
{
    internal ServiceGodotInputBind(
        Key         key,
        MouseButton mouseButton
    )
    {
        _ = this.Key         = _ = key;
        _ = this.MouseButton = _ = mouseButton;
    }

    [JsonPropertyName(
        name: $"{nameof(this.Key)}"
    )]
    internal Key                            Key         { get; set; } = _ = Key.None;

    [JsonPropertyName(
        name: $"{nameof(this.MouseButton)}"
    )]
    internal MouseButton                    MouseButton { get; set; } = _ = MouseButton.None;
}