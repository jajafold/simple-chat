using System;
using System.Text.Json;

namespace Infrastructure
{
    public static class Deserializer
    {
        public static SerializedObject Deserialize(string jsonString)
        {
            var typeIndex = jsonString.IndexOf('{');
            var type = jsonString[..typeIndex];
            var t = Type.GetType(type);
            
            var json = jsonString[typeIndex..];
            var a = JsonSerializer.Deserialize(json, t);

            return new SerializedObject(a, t);
        }
    }
}