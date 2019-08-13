using System.Collections.Generic;
using System.Linq;

namespace Amaury.Abstractions
{
    public class EventSourcedAggregate<TEntity> : IEventSourcedAggregate<TEntity> where TEntity : class
    {
        protected EventSourcedAggregate(Queue<ICelebrityEvent> commitedEvents) => CommitedEvents = commitedEvents;

        protected Queue<ICelebrityEvent> CommitedEvents { get; }
        protected Queue<ICelebrityEvent> PendingEvents { get; set; }

        public virtual TEntity Reduce(TEntity entity, string aggregatedId)
        {
            var properties = typeof(TEntity).GetProperties();

            if (CommitedEvents.Count == 0) { return default; }

            foreach (var @event in CommitedEvents)
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