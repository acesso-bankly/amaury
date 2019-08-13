using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions.Bus;

namespace Amaury.MediatR.Bus
{
    public interface INotifiableCelebrityEventsBus : ICelebrityEventsBus
    {
        Task RaiseEvent<TEvent>(TEvent celebrityEvent) where TEvent : INotifiableCelebrityEvent;
        Task RaiseEvents<TEvents>(TEvents events) where TEvents : IEnumerable<INotifiableCelebrityEvent>;
    }
}
