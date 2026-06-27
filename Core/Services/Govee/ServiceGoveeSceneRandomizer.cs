
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overlay.Core.Services.Govee;

internal static class ServiceGoveeSceneRandomizer
{
    internal static string GetRandomSceneName()
    {
        if (ServiceGoveeSceneRandomizer.s_activeScenePool.Count == 0)
        {
            ServiceGoveeSceneRandomizer.s_activeScenePool = ServiceGoveeSceneRandomizer.s_availableScenes.Where(
                predicate: s => s != ServiceGoveeSceneRandomizer.s_lastScene
            ).ToList();
        }

        var index = Random.Shared.Next(
            maxValue: ServiceGoveeSceneRandomizer.s_activeScenePool.Count
        );
        ServiceGoveeSceneRandomizer.s_lastScene = ServiceGoveeSceneRandomizer.s_activeScenePool[index: index];
        ServiceGoveeSceneRandomizer.s_activeScenePool.RemoveAt(
            index: index
        );
        return ServiceGoveeSceneRandomizer.s_lastScene;
    }

    private static List<string>      s_activeScenePool = [];
    private static string            s_lastScene       = string.Empty;
    private static readonly string[] s_availableScenes = 
    [
        ServiceGoveeSceneNames.BlueRaspberry,
        ServiceGoveeSceneNames.CreamsicleBanana,
        ServiceGoveeSceneNames.CreamsicleBlueberry,
        ServiceGoveeSceneNames.CreamsicleDragonfruit,
        ServiceGoveeSceneNames.CreamsicleLime,
        ServiceGoveeSceneNames.CreamsicleOrange,
        ServiceGoveeSceneNames.CreamsicleStrawberry,
        ServiceGoveeSceneNames.Cyberpunk,
        ServiceGoveeSceneNames.ForestSunset,
        ServiceGoveeSceneNames.Heatwave,
        ServiceGoveeSceneNames.Icy,
        ServiceGoveeSceneNames.OrangePurple,
        ServiceGoveeSceneNames.Rainbow,
        ServiceGoveeSceneNames.RedWhiteBlue,
        ServiceGoveeSceneNames.Toxic,
        ServiceGoveeSceneNames.Vaporwave,
        ServiceGoveeSceneNames.Watermelon,
    ];
}