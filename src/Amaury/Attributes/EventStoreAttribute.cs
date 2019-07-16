using System;

namespace Amaury.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EventStoreAttribute : Attribute
    {
        public EventStoreAttribute(string name) => Name = name;

        public string Name { get; }
    }
}