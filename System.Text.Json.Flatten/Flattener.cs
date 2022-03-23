namespace System.Text.Json;

/// <summary>
/// Extension methods for the <see cref="System.Text.Json.JsonElement"/>
/// interface which provide the ability to flatten a JSON structure into
/// a single-level dictionary.
/// </summary>
public static class Flattener
{
    /// <summary>
    /// Flattens a JSON structure into a single-level dictionary.
    /// </summary>
    /// <param name="element">The <see cref="JsonElement"/> which should be flattened.</param>
    /// <returns>A dictionary mapping each JSON path to its corresponding value.</returns>
    public static IDictionary<string, JsonElement> Flatten(this JsonElement element)
    {
        var result = new Dictionary<string, JsonElement>();
        Visit(element, "", result);
        return result;
    }

    private static void Visit(JsonElement element, string path, IDictionary<string, JsonElement> result)
    {
         switch (element.ValueKind)
         {
             case JsonValueKind.Array:
                VisitArray(element, path, result);
                break;
            case JsonValueKind.Object:
                VisitObject(element, path, result);
                break;
            default:
                result[path] = element;
                break;
         };
    }

    private static void VisitArray(JsonElement element, string path, IDictionary<string, JsonElement> result)
    {
        var i = 0;
        foreach (var item in element.EnumerateArray())
        {
            var itemPath = $"{path}[{i}]";
            Visit(item, itemPath, result);
            i++;
        }
    }

    private static void VisitObject(JsonElement element, string path, IDictionary<string, JsonElement> result)
    {
        foreach (var property in element.EnumerateObject())
        {
            var propertyPath = $"{path}.{property.Name}";
            Visit(property.Value, propertyPath, result);
        }
    }
}
