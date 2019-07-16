using System;
using System.Threading.Tasks;

namespace Amaury.Abstractions.Stores
{
    public interface ICelebrityEventStore<TEntity> where TEntity : class
    {
        Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent<TEntity>;

        Task<TEntity> Reduce(Guid aggredateId);
    }
}
