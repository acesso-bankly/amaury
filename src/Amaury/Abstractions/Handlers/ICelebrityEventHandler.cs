using MediatR;

namespace Amaury.Abstractions.Handlers
{
    public interface ICelebrityEventHandler<TEntity, in TEvent> : INotificationHandler<TEvent> where TEvent : ICelebrityEvent<TEntity> where TEntity : class
    {
    }
}
