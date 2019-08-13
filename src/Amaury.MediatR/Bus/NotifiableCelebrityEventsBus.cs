using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Persistence;
using MediatR;

namespace Amaury.MediatR.Bus
{
    public class NotifiableCelebrityEventsBus : INotifiableCelebrityEventsBus
    {
        private readonly IMediator _mediator;
        private readonly ICelebrityEventStore _eventStore;

        public NotifiableCelebrityEventsBus(ICelebrityEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task RaiseEvents<TEvents>(TEvents events) where TEvents : IEnumerable<INotifiableCelebrityEvent>
        {
            foreach (var @event in events)
            {
                await RaiseEvent(@event);
            }
        }

        public async Task RaiseEvent<TEvent>(TEvent @event) where TEvent : INotifiableCelebrityEvent
        {
            await Commit(@event);
            await _mediator.Publish(@event);
        }

        public Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent => _eventStore.Commit(@event);

        public async Task<IReadOnlyCollection<ICelebrityEvent>> Get(string aggregatedId) => await _eventStore.Get(aggregatedId);
    }
}