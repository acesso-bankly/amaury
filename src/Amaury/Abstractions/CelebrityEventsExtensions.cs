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
            foreach(var @event in self) { entity = @event.Data.TakeSnapshot<TEntity>(); }
            return entity;
        }

        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self, Func<ICelebrityEvent, ICelebrityEvent, ICelebrityEvent> filter) where TEntity : class, new()
        {
            var events = self.Aggregate(filter);
            var state = events.Data.TakeSnapshot<TEntity>();
            return state;
        }

        public static TEntity TakeSnapshot<TEntity>(this object state) where TEntity : class, new()
        {
            var entity = new TEntity();
            var properties = typeof(TEntity).GetProperties();
            
            var propertyNames = state.GetType().GetProperties().Select(p => p.Name).ToArray();

            foreach(var propertyName in propertyNames)
            {
                var item = properties.FirstOrDefault(c => c.Name.Equals(propertyName));
                if(item is null || item.Name.Equals("PendingEvents")) { continue; }

                if(item.CanRead && item.CanWrite)
                {
                    var value = state.GetType().GetProperty(propertyName)?.GetValue(state, null);
                    item.SetValue(entity, value);
                }
            }
            return entity;
        }
    }
}
