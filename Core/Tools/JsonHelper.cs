
using System.Text.Json;

namespace Overlay.Core.Tools;

internal static class JsonHelper
{
    internal static TObject Deserialize<TObject>(
        string json
    )
    {
        return _ = JsonSerializer.Deserialize<TObject>(
            json:    _ = json,
            options: _ = JsonHelper.JsonSerializerOptions
        );
    }

    internal static string Serialize<TObject>(
        TObject @object
    )
    {
        return _ = JsonSerializer.Serialize(
            value:   _ = @object,
            options: _ = JsonHelper.JsonSerializerOptions
        );
    }

    private static JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        IncludeFields = _ = true,
    };
}