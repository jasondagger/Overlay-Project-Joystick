
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Godots.Inputs;
using Overlay.Core.Services.Godots.TextToSpeeches;
using Overlay.Core.Tools;
using Overlay.Core.Tools.ResourcePaths;
// ReSharper disable All

namespace Overlay.Core.Services.Godots;

using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Godots.Inputs;
using Overlay.Core.Tools;
using Overlay.Core.Tools.ResourcePaths;

internal sealed class ServiceGodots :
	ServiceNode
{
    public override async Task Setup()
    {
        await base.Setup();

        this.AddServiceGodotScenes();
    }

    internal TServiceGodot GetServiceGodot<TServiceGodot>()
        where TServiceGodot :
            ServiceGodot
    {
        return this.m_serviceGodots[key: typeof(TServiceGodot)] as TServiceGodot;
    }

    private static readonly Dictionary<Type, string> c_serviceGodotTypePaths = new()
    {
        { typeof(ServiceGodotAudio),        ResourcePaths.GodotAudio        },
        { typeof(ServiceGodotHttp),         ResourcePaths.GodotHttp         },
        { typeof(ServiceGodotInput),        ResourcePaths.GodotInput        },
        { typeof(ServiceGodotTextToSpeech), ResourcePaths.GodotTextToSpeech },
	};
	private readonly Dictionary<Type, ServiceGodot>  m_serviceGodots         = new();

    private void AddServiceGodotScenes()
    {
		var type   = this.GetType();
        var method = type.GetMethod(
            name:        $"{nameof(ServiceGodots.AddServiceGodotScene)}",
            bindingAttr: _ =
                BindingFlags.Instance |
                BindingFlags.NonPublic
		);

        if (method is null)
        {
            return;
        }

        foreach (var serviceGodotTypePath in ServiceGodots.c_serviceGodotTypePaths)
        {
            var serviceGodotType = serviceGodotTypePath.Key;
            var serviceGodotPath = serviceGodotTypePath.Value;

            var parameters    = new object[] 
            {
                serviceGodotPath
            };
            var genericMethod = method.MakeGenericMethod(
                typeArguments: serviceGodotType
            );

            genericMethod.Invoke(
                obj:        this,
                parameters: parameters
            );
        }
    }

    private void AddServiceGodotScene<TServiceGodot>(
        string path
    )
		where TServiceGodot :
			ServiceGodot
	{
        var packedScene = ResourceLoader.Load(
            path:      path,
            typeHint:  $"{nameof(PackedScene)}",
            cacheMode: ResourceLoader.CacheMode.Reuse
        ) as PackedScene;
        if (packedScene is not null)
        {
            var serviceGodot = packedScene.Instantiate() as TServiceGodot;
            serviceGodot.Start();

			this.m_serviceGodots[key: typeof(TServiceGodot)] = serviceGodot;

			base.Node.AddChild(
                node: serviceGodot
			);
        }
        else
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{nameof(ServiceGodots)}." +
                    $"{nameof(ServiceGodots.AddServiceGodotScene)}() - " +
                    $"Failed to load from {path}"
            );
        }
    }
}