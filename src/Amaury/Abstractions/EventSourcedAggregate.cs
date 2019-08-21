using System.Collections.Generic;

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

        protected virtual void Append(ICelebrityEvent @event) => PendingEvents.Enqueue(@event);
    }
}