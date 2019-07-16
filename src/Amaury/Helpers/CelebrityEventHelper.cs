using System;
using System.Runtime.CompilerServices;
using Amaury.Attributes;

[assembly:InternalsVisibleTo("Amaury.Store.DynamoDb")]
namespace Amaury.Helpers
{
    internal static class CelebrityEventExtensions
    {
        public static string GetEventName(this Type type)
        {
            var eventNameAttribute = (EventNameAttribute)Attribute.GetCustomAttribute(type, typeof(EventNameAttribute));

            return eventNameAttribute != null ? eventNameAttribute.Name : throw new InvalidOperationException("Define a event name to event using EventNameAttribute");
        }

        public static string GetEventStore(this Type type)
        {
            var eventStoreAttribute = (EventStoreAttribute)Attribute.GetCustomAttribute(type, typeof(EventStoreAttribute));

            return eventStoreAttribute != null ? eventStoreAttribute.Name : throw new InvalidOperationException("Define a event name to event using EventStoreAttribute");
        }
    }
}
