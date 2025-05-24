
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
            text:  message,
            voice: "English (America)+Alex"
        );
    }
}