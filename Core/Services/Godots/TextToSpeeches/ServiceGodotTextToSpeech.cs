
using Godot;

namespace Overlay.Core.Services.Godots.TextToSpeeches;

internal sealed partial class ServiceGodotTextToSpeech() :
    ServiceGodot()
{
    internal static void Speak(
        string message
    )
    {
        var voices = DisplayServer.TtsGetVoices();
        DisplayServer.TtsSpeak(
            text:   message,
            voice:  "serena",
            volume: 100
        );
    }
}