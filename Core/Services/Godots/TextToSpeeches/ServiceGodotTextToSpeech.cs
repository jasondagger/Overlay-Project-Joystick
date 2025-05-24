
using Godot;

namespace Overlay.Core.Services.Godots.TextToSpeeches;

internal sealed partial class ServiceGodotTextToSpeech() :
    ServiceGodot()
{
    internal static void Speak(
        string message
    )
    {
        OS.Execute(
            path:      "espeak",
            arguments: ["-a", "200", message]
        );
    }
}