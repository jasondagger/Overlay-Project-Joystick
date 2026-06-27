
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overlay.Core.Services.ColorInterpolators;

internal static class ServiceColorInterpolatorColorRandomizer
{
	internal static IServiceColorInterpolatorDefinition.ColorType GetRandomColorType()
	{
		if (ServiceColorInterpolatorColorRandomizer.s_activeColorPool.Count == 0)
		{
			ServiceColorInterpolatorColorRandomizer.s_activeColorPool = ServiceColorInterpolatorColorRandomizer.s_availableColors.Where(
				predicate: c => c != ServiceColorInterpolatorColorRandomizer.s_lastColor
			).ToList();
		}

		var index = Random.Shared.Next(
			maxValue: ServiceColorInterpolatorColorRandomizer.s_activeColorPool.Count
		);
		ServiceColorInterpolatorColorRandomizer.s_lastColor = ServiceColorInterpolatorColorRandomizer.s_activeColorPool[index: index];
		ServiceColorInterpolatorColorRandomizer.s_activeColorPool.RemoveAt(
			index: index
		);
		return ServiceColorInterpolatorColorRandomizer.s_lastColor;
	}
	
	private static List<IServiceColorInterpolatorDefinition.ColorType>      s_activeColorPool = [];
	private static IServiceColorInterpolatorDefinition.ColorType            s_lastColor       = IServiceColorInterpolatorDefinition.ColorType.Off;
	private static readonly IServiceColorInterpolatorDefinition.ColorType[] s_availableColors = 
	[
		IServiceColorInterpolatorDefinition.ColorType.Blue,
		IServiceColorInterpolatorDefinition.ColorType.Cyan,
		IServiceColorInterpolatorDefinition.ColorType.Green,
		IServiceColorInterpolatorDefinition.ColorType.Magenta,
		IServiceColorInterpolatorDefinition.ColorType.Red,
		IServiceColorInterpolatorDefinition.ColorType.Yellow,
		IServiceColorInterpolatorDefinition.ColorType.White,
	];
}