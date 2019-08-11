using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Bus;
using Amaury.Abstractions.Persistence;
using MediatR;

namespace Amaury.Bus
{
    public class CelebrityEventsBus : ICelebrityEventsBus
    {
        private readonly IMediator _mediator;
        private readonly ICelebrityEventStore _eventStore;

        public CelebrityEventsBus(ICelebrityEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task RaiseEvents<TEvents>(TEvents events) where TEvents : IEnumerable<ICelebrityEvent>
        {
            foreach (var @event in events)
            {
                await RaiseEvent(@event);
            }
        }

        public async Task RaiseEvent<TEvent>(TEvent celebrityEvent) where TEvent : ICelebrityEvent
        {
            await _eventStore.Commit(celebrityEvent);
            await _mediator.Publish(celebrityEvent);
        }

        public async Task<IReadOnlyCollection<ICelebrityEvent>> Get(string aggregatedId) => await _eventStore.Get(aggregatedId);
    }
}