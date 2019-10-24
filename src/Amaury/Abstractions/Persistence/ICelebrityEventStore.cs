using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amaury.Abstractions.Persistence
{
    public interface ICelebrityEventStore
    {
        Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent;
        Task<IReadOnlyCollection<ICelebrityEvent>> GetFromEventStore(string aggregatedId);

        [Obsolete("Method Get is obsolete, use GetFromEventStore instead")]
        Task<IReadOnlyCollection<ICelebrityEvent>> Get(string aggregatedId);

    }
}
