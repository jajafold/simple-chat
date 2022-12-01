using Newtonsoft.Json;

namespace Infrastructure;

public interface IDeserializer
{
    public T Deserialize<T>(string jsonString, JsonSerializerSettings settings = default!);
}

public interface ISerializer
{
    public string Serialize(object? value, Formatting formatting = Formatting.Indented, JsonSerializerSettings? settings = default);
}