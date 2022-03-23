# System.Text.Json.Flatten
**Flatten complex JSON object trees into a simple dictionary with `System.Text.Json`.**

This library provides a `.Flatten()` method which can be used to convert a complex `JsonElement`
graph into a simple `IDictionary<string, JsonElement>`. It is particularly useful in situations
where one needs to compare JSON objects, or provide efficient lookups based on a simple string-based
key (in a simple template engine for example).

## Example
```csharp
namespace Example;

using System.Text.Json;

var json = @"
{
  ""a"": [
    {
      ""b"": {
        ""c"": ""d"",
        ""e"": ""f""
      },
        ""g"": ""h""
    },
    1,
    2,
    3
  ]
}
";

var jsonReader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));
var flattened = JsonElement.ParseValue(ref jsonReader).Flatten();
foreach (var (key, value) in flattened)
{
    Console.WriteLine($"{key} = {value.GetRawText()}");
}

/*
    Output:
    .a[0].b.c = "d"
    .a[0].b.e = "f"
    .a[0].g = "h"
    .a[1] = 1
    .a[2] = 2
    .a[3] = 3
*/
```