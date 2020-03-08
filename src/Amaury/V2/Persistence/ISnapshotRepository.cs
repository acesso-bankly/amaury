using System.Threading.Tasks;
using Amaury.V2.Abstractions;

namespace Amaury.V2.Persistence
{
    public interface ISnapshotRepository<TEntity> where TEntity : CelebrityAggregateBase
    {
        Task SaveAsync(TEntity entity);

        Task<TEntity> GetAsync(string aggregateId);
    }
}
