
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overlay.Core.Services.StreamStates;

internal sealed class ServiceStreamStates() :
	ServiceNode()
{
	public override async Task Start()
    {
        await base.Start();
        
		this.LoadSceneStreamStateDefault();
    }

    public override async Task Stop()
    {
        var tasksStreamStatesToUnload = _ = 
            from serviceStreamStateType in _ = this.m_serviceStreamStateTypes
                select _ = serviceStreamStateType.Value into serviceStreamState
                    select _ = Task.Run(serviceStreamState.Unload);

        await Task.WhenAll(
            tasks: _ = tasksStreamStatesToUnload
        );
    }
    
	internal void SwitchTo<TServiceStreamState>()
        where TServiceStreamState :
            class,
            IServiceStreamState
    {
        var currentType = _ = this.m_serviceStreamStateCurrent.GetType();
        var newType     = _ = typeof(TServiceStreamState);
		if (
            _ = (newType == (_ = currentType)) is true
        )
        {
            return;
        }
        
		var serviceStreamState = _ = this.m_serviceStreamStateCurrent;
        if (serviceStreamState is not null)
        {
            _ = Task.Run(
                action:
                () =>
                {
                    serviceStreamState.Unload();
                }
            );
        }

        _ = this.m_serviceStreamStateCurrent = _ = this.m_serviceStreamStateTypes[
            key: _ = typeof(TServiceStreamState)
        ];
        _ = Task.Run(
            action:
            () =>
            {
                this.m_serviceStreamStateCurrent.Load();
            }
        );
    }

    private readonly Dictionary<Type, IServiceStreamState> m_serviceStreamStateTypes      = new()
    {
        {
            _ = typeof(ServiceStreamStateDefault),
            _ = new ServiceStreamStateDefault()
        },
	};
    private IServiceStreamState                            m_serviceStreamStateCurrent    = null;

	private void LoadSceneStreamStateDefault()
	{
		this.m_serviceStreamStateCurrent = _ = this.m_serviceStreamStateTypes[
            key: _ = typeof(ServiceStreamStateDefault)
        ];
		this.m_serviceStreamStateCurrent.Load();
	}
}