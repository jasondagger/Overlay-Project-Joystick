
using Overlay.Core.Services.Databases;
using Overlay.Core.Services.Godots;
using Overlay.Core.Services.PastelInterpolators;
using Overlay.Core.Services.StreamStates;
using Overlay.Core.Services.Joysticks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.OBS;

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
        return _ = Services.c_serviceTypes[
            key: _ = typeof(TService)
        ] as TService;
    }

    internal static async Task Start()
    {
        var tasksSetup = _ = new List<Task>();
        
        // Service Setup
        tasksSetup.AddRange(
            from serviceTypes in _ = Services.c_serviceTypes 
                select _ = serviceTypes.Value into service 
                select _ = service.Setup()
        );
        
        await Task.WhenAll(
            tasks: _ = tasksSetup
        );

        // Service Start
        var tasksStart = _ = new List<Task>();
        
        tasksStart.AddRange(
            from serviceTypes in _ = Services.c_serviceTypes
                select _ = serviceTypes.Value into service
                select _ = service.Start()
        );
        
        await Task.WhenAll(
            tasks: _ = tasksStart
        );
    }

    internal static async Task Stop()
    {
        var tasksStop = _ = new List<Task>();
        
        tasksStop.AddRange(
            from serviceTypes in _ = Services.c_serviceTypes
                select _ = serviceTypes.Value into service 
                    select _ = service.Stop()
        );
        
        await Task.WhenAll(
            tasks: _ = tasksStop
        );
    }

    private static readonly Dictionary<Type, IService> c_serviceTypes = new()
    {
		{ _ = typeof(ServiceDatabase),           _ = new ServiceDatabase()           },
        { _ = typeof(ServiceGodots),             _ = new ServiceGodots()             },
        { _ = typeof(ServiceJoystick),           _ = new ServiceJoystick()           },
        { _ = typeof(ServiceJoystickBot),        _ = new ServiceJoystickBot()        },
        { _ = typeof(ServiceOBS),                _ = new ServiceOBS()                },
        { _ = typeof(ServicePastelInterpolator), _ = new ServicePastelInterpolator() },
        { _ = typeof(ServiceStreamStates),       _ = new ServiceStreamStates()       },
	};
}