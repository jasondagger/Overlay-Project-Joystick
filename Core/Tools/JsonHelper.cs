
using System.Text.Json;

namespace Overlay.Core.Tools;

internal static class JsonHelper
{
    internal static TObject Deserialize<TObject>(
        string json
    )
    {
        return JsonSerializer.Deserialize<TObject>(
            json:    json,
            options: JsonHelper.JsonSerializerOptions
        );
    }

    internal static string Serialize<TObject>(
        TObject @object
    )
    {
        return JsonSerializer.Serialize(
            value:   @object,
            options: JsonHelper.JsonSerializerOptions
        );
    }

    private static JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        IncludeFields = true,
    };
}