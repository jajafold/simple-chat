#nullable enable

using System;
using System.Text.Json;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class Deserializer : IDeserializer
    {
        public T Deserialize<T>(string jsonString, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, settings)!;
        }
    }
}