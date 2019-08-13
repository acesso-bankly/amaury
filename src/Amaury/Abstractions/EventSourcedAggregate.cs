using System;
using System.Collections.Generic;
using System.Linq;

namespace Amaury.Abstractions
{
    public class EventSourcedAggregate<TEntity> : IEventSourcedAggregate<TEntity> where TEntity : class, new()
    {
        protected EventSourcedAggregate(Queue<ICelebrityEvent> commitedEvents)
        {
            CommittedEvents = commitedEvents;
            PendingEvents = new Queue<ICelebrityEvent>();
        }

        public string Id { get; protected set; }
        public Queue<ICelebrityEvent> CommittedEvents { get; }
        public Queue<ICelebrityEvent> PendingEvents { get; }

        protected virtual TEntity Reduce(IReadOnlyCollection<ICelebrityEvent> events)
        {
            var entity = new TEntity();
            var properties = typeof(TEntity).GetProperties();

            foreach (var @event in events)
            {
                var data = @event.Data;
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

        protected virtual void Append(ICelebrityEvent @event) => PendingEvents.Enqueue(@event);
    }
}