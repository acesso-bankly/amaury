using System;
using System.Collections.Generic;
using System.Linq;

namespace Amaury.Abstractions
{
    public static class CelebrityEventsExtensions
    {
        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self) where TEntity : class, new()
        {
            var entity = new TEntity();
            var properties = typeof(TEntity).GetProperties();

            foreach(var @event in self)
            {
                object data = @event.Data;
                var propertyNames = data.GetType().GetProperties().Select(p => p.Name).ToArray();

                foreach(var propertyName in propertyNames)
                {
                    var item = properties.FirstOrDefault(c => c.Name.Equals(propertyName));
                    if(item is null) { continue; }

                    if(item.CanRead && item.CanWrite)
                    {
                        var value = data.GetType().GetProperty(propertyName)?.GetValue(data, null);
                        item.SetValue(entity, value);
                    }
                }
            }
            return entity;
        }

        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self, Func<ICelebrityEvent, bool> filter) where TEntity : class, new()
        {
            var events = self.Where(filter);
            return events.Reduce<TEntity>();
        }
    }
}
