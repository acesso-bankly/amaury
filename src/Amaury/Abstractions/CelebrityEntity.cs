using System.Collections.Generic;

namespace Amaury.Abstractions
{
    public class CelebrityEntity<TEntity> : ICelebrity<TEntity> where TEntity : class, new()
    {
        protected CelebrityEntity() => PendingEvents = new Queue<ICelebrityEvent>();

        public string Id { get; protected set; }
        public Queue<ICelebrityEvent> PendingEvents { get; }

        protected virtual void Append(ICelebrityEvent @event) => PendingEvents.Enqueue(@event);
    }
}