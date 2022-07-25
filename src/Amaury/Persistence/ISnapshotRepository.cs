using System.Threading.Tasks;
using Amaury.Abstractions;

namespace Amaury.Persistence
{
    public interface ISnapshotRepository<TEntity> where TEntity : CelebrityAggregateBase
    {
        Task SaveAsync(TEntity entity);

        Task<TEntity> LoadAsync(string aggregateId);
    }
}
