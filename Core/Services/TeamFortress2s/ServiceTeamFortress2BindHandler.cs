
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Godots.Audios;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Overlay.Core.Services.TeamFortress2s;

internal static class ServiceTeamFortress2BindHandler
{
    internal static void CloseGame()
    {
        Process.Start(
            fileName:  $"pkill", 
            arguments: $"-f tf_linux64"
        );
    }
    
    internal static void Explode()
    {
        ServiceTeamFortress2BindHandler.SendKeyPress(
            key: $"p"
        );
    }
    
    internal static void Jump()
    {
        ServiceTeamFortress2BindHandler.SendKeyPress(
            key: $"space"
        );
    }

    internal static void Kill()
    {
        ServiceTeamFortress2BindHandler.SendKeyPress(
            key: $"o"
        );
    }
    
    internal static void MoveForward()
    {
        ServiceTeamFortress2BindHandler.SendKeyPresses(
            key: $"w"
        );
    }
    
    internal static void OpenEmoteMenu()
    {
        ServiceTeamFortress2BindHandler.SendKeyPress(
            key: $"g"
        );
    }
    
    internal static void SelectTaunt(
        int index
    )
    {
        ServiceTeamFortress2BindHandler.SendKeyPress(
            key: $"{index}"
        );
    }

    private const int c_sendKeyCommandPressAmount              = 50;
    private const int c_sendKeyCommandPressDelayInMilliseconds = 1;

    private static void SendKeyPress(
        string key
    )
    {
        Process.Start(
            fileName:  $"/bin/bash",
            arguments: $"-c \"xdotool key --window $(wmctrl -l | grep 'Team Fortress 2' | awk '{{print $1}}') {key}\""
        );
    }

    private static void SendKeyPresses(
        string key
    )
    {
        Task.Run(
            function: async () =>
            {
                for (var i = 0; i < ServiceTeamFortress2BindHandler.c_sendKeyCommandPressAmount; i++)
                {
                    Process.Start(
                        fileName:  $"/bin/bash",
                        arguments: $"-c \"xdotool key --window $(wmctrl -l | grep 'Team Fortress 2' | awk '{{print $1}}') {key}\""
                    );
                    
                    await Task.Delay(
                        millisecondsDelay: ServiceTeamFortress2BindHandler.c_sendKeyCommandPressDelayInMilliseconds
                    );
                }
            }
        );
    }
}