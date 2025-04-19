
using Overlay.Core.Services.Godots.Audios;
using Overlay.Core.Services.Godots.Https;
using Overlay.Core.Services.Godots.Inputs;
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
        return _ = this.m_serviceGodots[key: _ = typeof(TServiceGodot)] as TServiceGodot;
    }

    private static readonly Dictionary<Type, string> c_serviceGodotTypePaths = new()
    {
        { _ = typeof(ServiceGodotAudio), _ = ResourcePaths.GodotAudio },
        { _ = typeof(ServiceGodotHttp),  _ = ResourcePaths.GodotHttp  },
        { _ = typeof(ServiceGodotInput), _ = ResourcePaths.GodotInput },
	};
	private readonly Dictionary<Type, ServiceGodot>  m_serviceGodots         = new();

    private void AddServiceGodotScenes()
    {
		var type   = _ = this.GetType();
        var method = _ = type.GetMethod(
            name:        _ = $"{_ = nameof(ServiceGodots.AddServiceGodotScene)}",
            bindingAttr: _ =
                BindingFlags.Instance |
                BindingFlags.NonPublic
		);

        if (method is null)
        {
            return;
        }

        foreach (var serviceGodotTypePath in _ = ServiceGodots.c_serviceGodotTypePaths)
        {
            var serviceGodotType = _ = serviceGodotTypePath.Key;
            var serviceGodotPath = _ = serviceGodotTypePath.Value;

            var parameters    = _ = new object[] 
            {
                _ = serviceGodotPath
            };
            var genericMethod = _ = method.MakeGenericMethod(
                typeArguments: _ = serviceGodotType
            );

            _ = genericMethod.Invoke(
                obj:        _ = this,
                parameters: _ = parameters
            );
        }
    }

    private void AddServiceGodotScene<TServiceGodot>(
        string path
    )
		where TServiceGodot :
			ServiceGodot
	{
        var packedScene = _ = ResourceLoader.Load(
            path:      _ = path,
            typeHint:  _ = $"{_ = nameof(PackedScene)}",
            cacheMode: _ = ResourceLoader.CacheMode.Reuse
        ) as PackedScene;
        if (packedScene is not null)
        {
            var serviceGodot = _ = packedScene.Instantiate() as TServiceGodot;
            serviceGodot.Start();

			_ = this.m_serviceGodots[key: _ = typeof(TServiceGodot)] = _ = serviceGodot;

			base.Node.AddChild(
                node: _ = serviceGodot
			);
        }
        else
        {
            ConsoleLogger.LogMessageError(
                messageError: _ =
                    $"{_ = nameof(ServiceGodots)}." +
                    $"{_ = nameof(ServiceGodots.AddServiceGodotScene)}() - " +
                    $"Failed to load from {_ = path}"
            );
        }
    }
}