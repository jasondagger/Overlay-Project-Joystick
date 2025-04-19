
using System;
using System.Linq;

namespace Overlay.Core.Tools;

internal static class EnumHelper
{
    internal static TEnum GetRandomValue<TEnum>()
        where
            TEnum :
                Enum
    {
        var values = _ = Enum.GetValues(
            enumType: _ = typeof(TEnum)
        );
        return _ = (TEnum)values.GetValue(
            index: _ = EnumHelper.s_random.Next(
                maxValue: _ = values.Length
            )
        );
    }

    public static TEnum[] GetRandomizedValues<TEnum>() 
        where
            TEnum :
                Enum
    {
        var values = _ = Enum.GetValues(
            enumType: _ = typeof(TEnum)
        );
        return _ = values.Cast<TEnum>().OrderBy(
            v => EnumHelper.s_random.Next()
        ).ToArray();
    }

    private static readonly Random s_random = new();
}