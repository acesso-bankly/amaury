using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Amaury.V2.Abstractions
{
    public abstract class CelebrityAggregateBase : ICelebrityAggregate
    {
        private readonly ICollection<CelebrityEventBase> _uncommittedEvents;

        public CelebrityAggregateBase() => _uncommittedEvents = new LinkedList<CelebrityEventBase>();

        public string Id => GetAggregateId();
        [JsonProperty] public DateTime CreatedAt { get; protected set; }
        [JsonProperty] public DateTime? UpdatedAt { get; protected set; }

        public long Version { get; protected set; }

        public void ApplyEvent(CelebrityEventBase @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
            Version = @event.AggregateVersion;
        }

        public bool HasUncommittedEvents => _uncommittedEvents.Any();

        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
        
        public ICollection<CelebrityEventBase> GetUncommittedEvents() => _uncommittedEvents;

        protected void AppendEvent<TEvent>(TEvent @event) where TEvent : CelebrityEventBase
        {
            UpVersion();
            @event.SetAggregateVersion(Version);
            @event.SetAggregateId(Id);

            _uncommittedEvents.Add(@event);
            ApplyEvent(@event);
        }

        protected void UpVersion() => SetVersion(Version + 1);

        public void SetVersion(long version)
        {
            if(version < Version) throw new ArgumentOutOfRangeException();
            Version = version;
        }

        public abstract string GetAggregateId();

        public Task RaiseEventsOfTypeAsync<TEvent>(Func<TEvent, Task> func) where TEvent : CelebrityEventBase
        {
            var events = GetUncommittedEvents().OfType<TEvent>().ToList();
            
            if(events.Any())
            {
                var tasks = events.Select(func).ToArray();

                Task.WaitAll(tasks);
            }
            
            return Task.CompletedTask;
        }

        public Task RaiseAllEventsAsync(Func<CelebrityEventBase, Task> func)
        {
            var events = GetUncommittedEvents().ToList();

            if(events.Any())
            {
                var tasks = events.Select(func).ToArray();

                Task.WaitAll(tasks);
            }

            return Task.CompletedTask;
        }
    }
}
