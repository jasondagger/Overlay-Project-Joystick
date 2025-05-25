
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Services.StreamStates;
using Overlay.Core.Services.Joysticks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overlay.Core.Services.Geminis;
using Overlay.Core.Services.Govee;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Lovense;
using Overlay.Core.Services.OBS;
using Overlay.Core.Services.Spotifies;

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
		{ typeof(ServiceDatabase),           new ServiceDatabase()           },
        { typeof(ServiceGemini),             new ServiceGemini()             },
        { typeof(ServiceGodots),             new ServiceGodots()             },
        { typeof(ServiceGovee),              new ServiceGovee()              },
        { typeof(ServiceJoystick),           new ServiceJoystick()           },
        { typeof(ServiceJoystickBot),        new ServiceJoystickBot()        },
        { typeof(ServiceLovense),            new ServiceLovense()            },
        { typeof(ServiceOBS),                new ServiceOBS()                },
        { typeof(ServicePastelInterpolator), new ServicePastelInterpolator() },
        { typeof(ServiceSpotify),            new ServiceSpotify()            },
        { typeof(ServiceStreamStates),       new ServiceStreamStates()       },
	};
}