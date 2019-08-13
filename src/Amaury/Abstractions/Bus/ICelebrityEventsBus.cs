using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amaury.Abstractions.Bus
{
    public interface ICelebrityEventsBus
    {
        Task RaiseEvent<TEvent>(TEvent celebrityEvent) where TEvent : ICelebrityEvent;
        Task RaiseEvents<TEvents>(TEvents events) where TEvents : IEnumerable<ICelebrityEvent>;

        Task<IReadOnlyCollection<ICelebrityEvent>> Get(string aggregatedId);
    }
}
