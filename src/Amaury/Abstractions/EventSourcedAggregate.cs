using System.Collections.Generic;
using System.Linq;

namespace Amaury.Abstractions
{
    public class EventSourcedAggregate<TEntity> : IEventSourcedAggregate<TEntity> where TEntity : class, new()
    {
        protected EventSourcedAggregate(Queue<ICelebrityEvent> commitedEvents) => CommittedEvents = commitedEvents;

        protected Queue<ICelebrityEvent> CommittedEvents { get; }
        protected Queue<ICelebrityEvent> PendingEvents { get; set; }

        public virtual TEntity Reduce()
        {
            var entity = new TEntity();
            var properties = typeof(TEntity).GetProperties();

            foreach (var @event in CommittedEvents)
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