
using Overlay.Core.Services.Govee;
using System;

namespace Overlay.Core.Services.TeamFortress2s;

internal static class ServiceTeamFortress2LightRandomizer
{
    internal static string GetRandomLightSceneName()
    {
        var index = ServiceTeamFortress2LightRandomizer.s_random.Next(
            ServiceTeamFortress2LightRandomizer.s_lightSceneNames.Length
        );
        return ServiceTeamFortress2LightRandomizer.s_lightSceneNames[index];
    }
    
    internal static string GetRandomLightSceneNameWithGold()
    {
        var index = ServiceTeamFortress2LightRandomizer.s_random.Next(
            ServiceTeamFortress2LightRandomizer.s_lightSceneNamesWithGold.Length
        );
        return ServiceTeamFortress2LightRandomizer.s_lightSceneNamesWithGold[index];
    }
    
    private static readonly Random   s_random                 = new();
    private static readonly string[] s_lightSceneNames        =
    [
        ServiceGoveeSceneNames.TF2KillStreak5,
        ServiceGoveeSceneNames.TF2KillStreak10,
        ServiceGoveeSceneNames.TF2KillStreak15,
    ];
    private static readonly string[] s_lightSceneNamesWithGold =
    [
        ServiceGoveeSceneNames.TF2KillStreak5,
        ServiceGoveeSceneNames.TF2KillStreak10,
        ServiceGoveeSceneNames.TF2KillStreak15,
        ServiceGoveeSceneNames.TF2KillStreak20,
    ];
}
