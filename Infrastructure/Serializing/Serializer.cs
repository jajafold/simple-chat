using Newtonsoft.Json;

namespace Infrastructure;

public class Serializer : ISerializer
{
    public string Serialize(object? value, Formatting formatting = Formatting.Indented,
        JsonSerializerSettings? settings = default)
    {
        return JsonConvert.SerializeObject(value, formatting, settings);
    }
}