using System;

namespace Infrastructure
{
    public class SerializedObject
    {
        public readonly object Obj;
        public readonly Type Type;

        public SerializedObject(object obj, Type type)
        {
            Obj = obj;
            Type = type;
        }
    }
}