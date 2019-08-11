using System;
using System.Linq;
using System.Threading.Tasks;
using Amaury.Abstractions.Bus;

namespace Amaury.Abstractions
{
    public class EventSourcedAggregate<TEntity> : IEventSourcedAggregate<TEntity> where TEntity : class
    {
        protected EventSourcedAggregate(ICelebrityEventsBus bus)
        {
            Bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public ICelebrityEventsBus Bus { get; }

        public virtual async Task<TEntity> Reduce(TEntity entity, string aggregatedId)
        {
            var properties = typeof(TEntity).GetProperties();

            var events = await Bus.Get(aggregatedId);

            if (events.Count == 0) { return default; }

            foreach (var @event in events)
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
    }
}