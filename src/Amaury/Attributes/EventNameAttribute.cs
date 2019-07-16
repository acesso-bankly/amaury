using System;

namespace Amaury.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EventNameAttribute : Attribute
    {
        public EventNameAttribute(string name) => Name = name;

        public string Name { get; }
    }
}
