#nullable enable

using System;
using System.Text.Json;

namespace Infrastructure
{
    public static class Deserializer
    {
        public static object? Deserialize(string jsonString)
        {
            var typeIndex = jsonString.IndexOf('{');
            var typeStringRepresentation = jsonString[..typeIndex];
            var type = Type.GetType(typeStringRepresentation);
            
            var serializedData = jsonString[typeIndex..];
            return JsonSerializer.Deserialize(serializedData, type!);
        }
    }
}