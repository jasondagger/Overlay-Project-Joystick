
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overlay.Core.Services.TeamFortress2s;

internal static class ServiceTeamFortress2KillStreakTracks
{
    internal static string GetRandomTrackKillStreak5()  => ServiceTeamFortress2KillStreakTracks.GetNextTrack(
        killStreak: 5,
        source:     ServiceTeamFortress2KillStreakTracks.s_tracksKillStreak5
    );
    internal static string GetRandomTrackKillStreak10() => ServiceTeamFortress2KillStreakTracks.GetNextTrack(
        killStreak: 10,
        source:     ServiceTeamFortress2KillStreakTracks.s_tracksKillStreak10
    );
    internal static string GetRandomTrackKillStreak15() => ServiceTeamFortress2KillStreakTracks.GetNextTrack(
        killStreak: 15,
        source:     ServiceTeamFortress2KillStreakTracks.s_tracksKillStreak15
    );
    internal static string GetRandomTrackKillStreak20() => ServiceTeamFortress2KillStreakTracks.GetNextTrack(
        killStreak: 20,
        source:     ServiceTeamFortress2KillStreakTracks.s_tracksKillStreak20
    );
    internal static string GetRandomTrackKillStreak25() => ServiceTeamFortress2KillStreakTracks.GetNextTrack(
        killStreak: 25,
        source:     ServiceTeamFortress2KillStreakTracks.s_tracksKillStreak25
    );
    
    private static readonly Dictionary<int, List<string>> s_activePools        = new();
    private static readonly Dictionary<int, string>       s_lastPlayed         = new();
    private static readonly string[]                      s_tracksKillStreak5  =
    [
        $"Dr. Dre, Eminem, Xzibit - What's The Difference",
        $"Dr. Dre, Eminem - Forgot About Dre",
        $"Dr. Dre, Eminem, Knoc-Turn'al - The Watcher",
        $"Dr. Dre, Hitman, Six-Two, Nate Dogg, Kurupt - Xxplosive",
        $"Dr. Dre, Snoop Dogg - Still D.R.E.",
        $"Dr. Dre, Snoop Dogg - The Next Episode",
        $"Eminem - Houdini",
        $"Eminem - Insane",
        $"Eminem - Just Lose It",
        $"Eminem - Lose Yourself",
        $"Eminem - My Name Is",
        $"Eminem - Not Afraid",
        $"Eminem - Rap God",
        $"Eminem - Sing For The Moment",
        $"Eminem - Same Song & Dance",
        $"Eminem - Stan",
        $"Eminem - The Real Slim Shady",
        $"Eminem - The Way I Am",
        $"Eminem - Without Me",
        $"Eminem, 50 Cent, Nate Dogg - Never Enough",
        $"Eminem, Dr. Dre, 50 Cent - Crack A Bottle (feat. Dr. Dre & 50 Cent)",
        $"Eminem, Dr. Dre, Snoop Dogg, Xzibit, Nate Dogg - Bitch Please II",
        $"Eminem, Juice WRLD - Godzilla (feat. Juice WRLD)",
        $"Eminem, Nate Dogg - Shake That",
        $"Eminem, Nate Dogg - Till I Collapse",
        $"Eminem, Rihanna - The Monster",
        $"Snoop Dogg, Xzibit - Bitch Please",
        $"Xzibit - X",
        $"Xzibit, Eminem, Nate Dogg - My Name (feat. Eminem & Nate Dogg)",
    ];
    private static readonly string[]                      s_tracksKillStreak10 =
    [
        $"Mac Miller - Blue World",
        $"Mac Miller - Good News",
        $"Mac Miller - Kool Aid & Frozen Pizza",
        $"Mac Miller - Knock Knock",
        $"Mac Miller - Ladders",
        $"Mac Miller - Love Lost",
        $"Mac Miller - Nikes on My Feet",
        $"Mac Miller - Poppy",
        $"Mac Miller - Self Care",
        $"Mac Miller - Surf",
        $"Mac Miller - The Spins",
        $"Mac Miller - Thoughts from a Balcony",
        $"Mac Miller - Weekend (feat. Miguel)",
        $"Mac Miller - Woods",
    ];
    private static readonly string[]                      s_tracksKillStreak15 =
    [
        $"Twenty One Pilots - Chlorine",
        $"Twenty One Pilots - Heathens",
        $"Twenty One Pilots - Holding on to You",
        $"Twenty One Pilots - Migraine",
        $"Twenty One Pilots - Morph",
        $"Twenty One Pilots - Polarize",
        $"Twenty One Pilots - Ride",
        $"Twenty One Pilots - Routines In The Night",
        $"Twenty One Pilots - Stressed Out",
        $"Twenty One Pilots - Tear in My Heart",
    ];
    private static readonly string[]                      s_tracksKillStreak20 =
    [
        $"Maroon 5 - Lucky Strike",
        $"Maroon 5 - Maps",
        $"Maroon 5 - Misery",
        $"Maroon 5 - Moves Like Jagger - Studio Recording From \"The Voice\" Performance",
        $"Maroon 5 - One More Night",
        $"Maroon 5 - Payphone",
        $"Maroon 5 - Sugar",
        $"Maroon 5 - This Love",
        $"Maroon 5 - This Summer",
        $"Maroon 5, Megan Thee Stallion - Beautiful Mistakes (feat. Megan Thee Stallion)",
    ];
    private static readonly string[]                      s_tracksKillStreak25 =
    [
        $"AC/DC - Highway to Hell",
        $"AC/DC - T.N.T.",
        $"Avicii - Levels - Radio Edit",
        $"Avicii - Levels - Radio Edit",
        $"Bee Gees - Stayin' Alive - From \"Saturday Night Fever\" Soundtrack",
        $"Calvin Harris, Dua Lipa - One Kiss (with Dua Lipa)",
        $"Dirty Heads - Vacation",
        $"Franz Ferdinand - Take Me Out",
        $"Godsmack - I Stand Alone",
        $"Katy Perry - Roar",
        $"Kevin Rudolph, Lil Wayne - Let It Rock",
        $"Kid Cudi, André Benjamin - By Design",
        $"LMFAO - Sexy And I Know It",
        $"LMFAO - Yes",
        $"Linkin Park - Somewhere I Belong",
        $"Justin Timberlake - CAN'T STOP THE FEELING! (from DreamWorks Animation's \"TROLLS\")",
        $"Lupe Fiasco - The Show Goes On",
        $"Panic At The Disco - High Hopes",
        $"Papa Roach - Last Resort",
        $"Queen - Don't Stop Me Now",
        $"Queen - Another One Bites The Dust - Remastered 2011",
        $"Smash Mouth - All Star",
        $"The Notorious B.I.G. - Hypnotize - 2014 Remaster",
    ];
    
    private static string GetNextTrack(
        int      killStreak, 
        string[] source
    )
    {
        if (
            ServiceTeamFortress2KillStreakTracks.s_activePools.TryGetValue(
                key:   killStreak, 
                value: out var pool
            ) is false || 
            pool.Count is 0
        )
        {
            ServiceTeamFortress2KillStreakTracks.s_lastPlayed.TryGetValue(
                key:   killStreak, 
                value: out var last
            );
            ServiceTeamFortress2KillStreakTracks.s_activePools[key: killStreak] = pool = source.Where(
                predicate: t => t != last
            ).ToList();
        }

        var index = Random.Shared.Next(
            maxValue: pool.Count
        );
        var track = pool[index];
        pool.RemoveAt(
            index: index
        );
    
        return ServiceTeamFortress2KillStreakTracks.s_lastPlayed[key: killStreak] = track;
    }
}
