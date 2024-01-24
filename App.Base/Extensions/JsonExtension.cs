using System;
using System.Text.Json;

namespace App.Base.Extensions;

public static class JsonExtension
{
    public static string ToJson<T>(this T value, Action<JsonSerializerOptions>? options = null)
    {
        var newOptions = new JsonSerializerOptions();
        options?.Invoke(newOptions);
        return JsonSerializer.Serialize(value, newOptions);
    }
}