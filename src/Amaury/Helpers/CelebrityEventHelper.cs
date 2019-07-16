using System;
using Amaury.Attributes;

namespace Amaury.Helpers
{
    internal static class CelebrityEventExtensions
    {
        public static string GetEventName(this Type type)
        {
            var eventNameAttribute = (EventNameAttribute)Attribute.GetCustomAttribute(type, typeof(EventNameAttribute));

            return eventNameAttribute != null ? eventNameAttribute.Name : throw new InvalidOperationException("Define a event name to event using EventNameAttribute");
        }
    }
}
