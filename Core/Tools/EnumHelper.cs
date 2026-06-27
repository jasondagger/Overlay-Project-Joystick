
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Overlay.Core.Tools;

internal static partial class EnumHelper
{
    internal static TEnum GetRandomValue<TEnum>()
        where
            TEnum :
                Enum
    {
        var values = Enum.GetValues(
            enumType: typeof(TEnum)
        );
        return (TEnum)values.GetValue(
            index: EnumHelper.s_random.Next(
                maxValue: values.Length
            )
        );
    }

    public static TEnum[] GetRandomizedValues<TEnum>() 
        where
            TEnum :
                Enum
    {
        var values = Enum.GetValues(
            enumType: typeof(TEnum)
        );
        return values.Cast<TEnum>().OrderBy(
            v => EnumHelper.s_random.Next()
        ).ToArray();
    }
    

    public static string ToSpacedPascalCase(
        Enum enumValue
    )
    {
        var name  = enumValue.ToString();
        var regex = EnumHelper.PascalCaseRegex();
        return regex.Replace(
            input:       name, 
            replacement: "$1 $2"
        );
    }

    private static readonly Random s_random = new();

    [GeneratedRegex(
        pattern: "([a-z0-9])([A-Z])"
    )]
    private static partial Regex PascalCaseRegex();
}