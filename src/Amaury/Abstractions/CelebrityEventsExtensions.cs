using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Amaury.Abstractions
{
    public static class CelebrityEventsExtensions
    {
        /// <summary>
        /// Reduce celebrity events associating typed entity property names
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self) where TEntity : class, new()
        {
            var entity = new TEntity();

            foreach(var @event in self)
            {
                var obj = (object)@event.Data;
                entity = obj.GetState<TEntity>();
            }
            return entity;
        }

        /// <summary>
        /// Reduce celebrity events associating typed entity property names by filter condition
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="self"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self, Func<ICelebrityEvent, bool> filter) where TEntity : class, new()
        {
            var events = self.ToList();
            var filtredEvents = events.Where(filter);
            var state = filtredEvents.Reduce<TEntity>();
            return state;
        }

        /// <summary>
        /// Reduce celebrity events to typed entity using provided function
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self, Func<TEntity, ICelebrityEvent, TEntity> func) where TEntity : class, new()
        {
            var state = self.Reduce(new TEntity(), func);
            return state;
        }

        /// <summary>
        /// Reduce celebrity events associating to provided state using provided function
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="self"></param>
        /// <param name="state"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TEntity Reduce<TEntity>(this IEnumerable<ICelebrityEvent> self, TEntity state, Func<TEntity, ICelebrityEvent, TEntity> func) where TEntity : class, new()
        {
            if(state == null) throw new ArgumentNullException(nameof(state));

            state = self.Aggregate(state, func);
            return state;
        }

        public static TEntity GetState<TEntity>(this object state) where TEntity : class, new()
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
                else
                {
                    throw new InvalidOperationException($"The property \"{item.Name}\" can not be readable and writable");
                }
            }
            return entity;
        }
    }
}
