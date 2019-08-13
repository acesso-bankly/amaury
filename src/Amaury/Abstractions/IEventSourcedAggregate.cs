using System.Threading.Tasks;

namespace Amaury.Abstractions
{
    public interface IEventSourcedAggregate<TEntity> where TEntity : class
    {
        Task<TEntity> Reduce(TEntity entity, string aggregatedId);
    }
}