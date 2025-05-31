
using Godot;

namespace Overlay.Core.Services.Godots.TextToSpeeches;

internal sealed partial class ServiceGodotTextToSpeech() :
    ServiceGodot()
{
    internal static void Speak(
        string message
    )
    {
        var voices = DisplayServer.TtsGetVoicesForLanguage("en");
        DisplayServer.TtsSpeak(
            text:   message,
            voice:  "english-mb-en1",
            volume: 100
        );
    }
}