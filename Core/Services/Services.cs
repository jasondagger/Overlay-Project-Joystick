
using Overlay.Core.Services.Breaks;
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Geminis;
using Overlay.Core.Services.Giveaways;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.Hydrations;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks;
using Overlay.Core.Services.Lovenses;
using Overlay.Core.Services.NSFWs;
using Overlay.Core.Services.OBS;
using Overlay.Core.Services.ColorInterpolators;
using Overlay.Core.Services.Spotifies;
using Overlay.Core.Services.StreamStates;
using Overlay.Core.Services.Stretches;
using Overlay.Core.Services.TeamFortress2s;
using Overlay.Core.Services.WorkOuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overlay.Core.Services.Achievements;

namespace Overlay.Core.Services;

internal static class Services
{
    static Services()
    {

    }

    internal static TService GetService<TService>()
        where TService :
            class,
            IService
    {
        return Services.c_serviceTypes[
            key: typeof(TService)
        ] as TService;
    }

    internal static async Task Start()
    {
        var tasksSetup = new List<Task>();
        
        // Service Setup
        tasksSetup.AddRange(
            from serviceTypes in Services.c_serviceTypes 
                select serviceTypes.Value into service 
                select service.Setup()
        );
        
        await Task.WhenAll(
            tasks: tasksSetup
        );

        // Service Start
        var tasksStart = new List<Task>();
        
        tasksStart.AddRange(
            from serviceTypes in Services.c_serviceTypes
                select serviceTypes.Value into service
                select service.Start()
        );
        
        await Task.WhenAll(
            tasks: tasksStart
        );
    }

    internal static async Task Stop()
    {
        var tasksStop = new List<Task>();
        
        tasksStop.AddRange(
            from serviceTypes in Services.c_serviceTypes
                select serviceTypes.Value into service 
                    select service.Stop()
        );
        
        await Task.WhenAll(
            tasks: tasksStop
        );
    }

    private static readonly Dictionary<Type, IService> c_serviceTypes = new()
    {
        { typeof(ServiceAchievement),              new ServiceAchievement()              },
        { typeof(ServiceBreak),                    new ServiceBreak()                    },
        { typeof(ServiceColorInterpolatorInverse), new ServiceColorInterpolatorInverse() },
        { typeof(ServiceColorInterpolatorNormal),  new ServiceColorInterpolatorNormal()  },
		{ typeof(ServiceDatabase),                 new ServiceDatabase()                 },
        { typeof(ServiceGemini),                   new ServiceGemini()                   },
        { typeof(ServiceGiveaway),                 new ServiceGiveaway()                 },
        { typeof(ServiceGodots),                   new ServiceGodots()                   },
        { typeof(ServiceGovee),                    new ServiceGovee()                    },
        { typeof(ServiceHydrate),                  new ServiceHydrate()                  },
        { typeof(ServiceJoystick),                 new ServiceJoystick()                 },
        { typeof(ServiceJoystickBot),              new ServiceJoystickBot()              },
        { typeof(ServiceLovense),                  new ServiceLovense()                  },
        { typeof(ServiceNSFW),                     new ServiceNSFW()                     },
        { typeof(ServiceOBS),                      new ServiceOBS()                      },
        { typeof(ServiceSpotify),                  new ServiceSpotify()                  },
        { typeof(ServiceStreamStates),             new ServiceStreamStates()             },
        { typeof(ServiceStretch),                  new ServiceStretch()                  },
        { typeof(ServiceTeamFortress2),            new ServiceTeamFortress2()            },
        { typeof(ServiceWorkOut),                  new ServiceWorkOut()                  },
	};
}