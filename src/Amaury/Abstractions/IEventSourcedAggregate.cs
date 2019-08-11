using System.Threading.Tasks;
using Amaury.Abstractions.Bus;

namespace Amaury.Abstractions
{
    public interface IEventSourcedAggregate<TEntity> where TEntity : class
    {
        ICelebrityEventsBus Bus { get; }
        Task<TEntity> Reduce(TEntity entity, string aggregatedId);
    }
}