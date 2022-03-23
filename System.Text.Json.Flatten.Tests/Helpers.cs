using System.IO;
using System.Text.Json;

namespace System.Text.Json.Flatten;

public static class Helpers
{
    public static JsonElement ParseJson(string json)
    {
        var jr = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        return JsonElement.ParseValue(ref jr);
    }
}