#nullable enable

using Newtonsoft.Json;

namespace Infrastructure;

public class Deserializer : IDeserializer
{
    public T Deserialize<T>(string jsonString, JsonSerializerSettings settings = default!)
    {
        return JsonConvert.DeserializeObject<T>(jsonString, settings)!;
    }
}