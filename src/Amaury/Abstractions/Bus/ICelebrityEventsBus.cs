using System.Threading.Tasks;

namespace Amaury.Abstractions.Bus
{
    public interface ICelebrityEventsBus<in TEntity> where TEntity : class
    {
        Task RaiseEvent<TEvent>(TEvent celebrityEvent) where TEvent : ICelebrityEvent<TEntity>;
    }
}
