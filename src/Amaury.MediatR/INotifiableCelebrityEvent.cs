using Amaury.Abstractions;
using MediatR;

namespace Amaury.MediatR
{
    public interface INotifiableCelebrityEvent : ICelebrityEvent, INotification { }
}
