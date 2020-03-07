using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amaury.Abstractions.Persistence
{
    public interface ICelebrityEventStore
    {
        Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent;
        Task<IReadOnlyCollection<ICelebrityEvent>> GetEvents(string aggregatedId);
    }
}
