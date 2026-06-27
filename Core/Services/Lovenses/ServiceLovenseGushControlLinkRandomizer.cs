
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overlay.Core.Services.Lovenses;

internal static class ServiceLovenseGushControlLinkRandomizer
{
    internal static string GetRandomGushControlLink()
    {
        if (ServiceLovenseGushControlLinkRandomizer.s_activeGushControlLinkPool.Count == 0)
        {
            ServiceLovenseGushControlLinkRandomizer.s_activeGushControlLinkPool = ServiceLovenseGushControlLinkRandomizer.s_availableGushControlLinks.Where(
                predicate: s => s != ServiceLovenseGushControlLinkRandomizer.s_lastGushControlLink
            ).ToList();
        }

        var index = Random.Shared.Next(
            maxValue: ServiceLovenseGushControlLinkRandomizer.s_activeGushControlLinkPool.Count
        );
        ServiceLovenseGushControlLinkRandomizer.s_lastGushControlLink = ServiceLovenseGushControlLinkRandomizer.s_activeGushControlLinkPool[index: index];
        ServiceLovenseGushControlLinkRandomizer.s_activeGushControlLinkPool.RemoveAt(
            index: index
        );
        return ServiceLovenseGushControlLinkRandomizer.s_lastGushControlLink;
    }

    private static List<string>      s_activeGushControlLinkPool = [];
    private static string            s_lastGushControlLink       = string.Empty;
    private static readonly string[] s_availableGushControlLinks = 
    [
        $"https://c.lovense-api.com/t2/adzjnveg",
        $"https://c.lovense-api.com/t2/bgjts3mt",
        $"https://c.lovense-api.com/t2/c30hvlda",
        $"https://c.lovense-api.com/t2/dbdzhfew",
        $"https://c.lovense-api.com/t2/ew9zpwj8",
        $"https://c.lovense-api.com/t2/f72uhr7j",
        $"https://c.lovense-api.com/t2/g1hkahkk",
        $"https://c.lovense-api.com/t2/hj9ced6j",
        $"https://c.lovense-api.com/t2/iu16uxlb",
        $"https://c.lovense-api.com/t2/j3xy1nl9",
    ];
}