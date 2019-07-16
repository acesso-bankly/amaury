using System;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Bus;
using Amaury.Abstractions.Stores;
using MediatR;

namespace Amaury.Bus
{
    public sealed class CelebrityEventsBus<TEntity> : ICelebrityEventsBus<TEntity> where TEntity : class
    {
        private readonly IMediator _mediator;
        private readonly ICelebrityEventStore<TEntity> _celebrityEventStore;

        public CelebrityEventsBus(IMediator mediator, ICelebrityEventStore<TEntity> celebrityEventStore)
        {
            _celebrityEventStore = celebrityEventStore ?? throw  new ArgumentNullException(nameof(celebrityEventStore));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        public async Task RaiseEvent<TEvent>(TEvent celebrityEvent) where TEvent : ICelebrityEvent<TEntity>
        {
            await _celebrityEventStore.Commit(celebrityEvent);
            await _mediator.Publish(celebrityEvent);
        }
    }
}
