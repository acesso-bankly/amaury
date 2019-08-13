using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amaury.Abstractions.Bus
{
    public interface ICelebrityEventsBus
    {
        Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent;
        Task<IReadOnlyCollection<ICelebrityEvent>> Get(string aggregatedId);
    }
}
