using System;
using System.Collections.Generic;
using System.Linq;

namespace Amaury.V2.Abstractions
{
    public abstract class CelebrityAggregateBase : ICelebrityAggregate
    {
        private readonly ICollection<CelebrityEventBase> _uncommittedEvents = new LinkedList<CelebrityEventBase>();

        public CelebrityAggregateBase() => Version = 0;

        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public long Version { get; protected set; }

        public void ApplyEvent(CelebrityEventBase @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
            Version = @event.AggregateVersion;
        }
        
        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

        public IEnumerable<CelebrityEventBase> GetUncommittedEvents() => _uncommittedEvents.AsEnumerable();

        protected void AppendEvent<TEvent>(TEvent @event) where TEvent : CelebrityEventBase
        {
            UpVersion();
            @event.SetAggregateVersion(Version);
            @event.SetAggregateId(Id);

            _uncommittedEvents.Add(@event);
            ApplyEvent(@event);
        }

        protected void UpVersion() => Version += 1;
    }
}
