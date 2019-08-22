using System.Collections.Generic;

namespace Amaury.Abstractions
{
    public class EventSourcedAggregate<TEntity> : IEventSourcedAggregate<TEntity> where TEntity : class, new()
    {
        protected EventSourcedAggregate() { }

        public string Id { get; protected set; }
        public Queue<ICelebrityEvent> PendingEvents { get; } = new Queue<ICelebrityEvent>();

        protected virtual void Append(ICelebrityEvent @event) => PendingEvents.Enqueue(@event);
    }
}