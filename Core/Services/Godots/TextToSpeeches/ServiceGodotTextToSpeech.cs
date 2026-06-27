
using Godot;

namespace Overlay.Core.Services.Godots.TextToSpeeches;

internal sealed partial class ServiceGodotTextToSpeech() :
    ServiceGodot()
{
    internal static void Speak(
        string message
    )
    {
        DisplayServer.TtsSpeak(
            text:      message,
            voice:     ServiceGodotTextToSpeech.c_voice,
            volume:    100
        );
    }
    
    internal static void SpeakWithInterrupt(
        string message
    )
    {
        DisplayServer.TtsSpeak(
            text:      message,
            voice:     ServiceGodotTextToSpeech.c_voice,
            volume:    100,
            interrupt: true
        );
    }

    private const string c_voice = "serena";
}