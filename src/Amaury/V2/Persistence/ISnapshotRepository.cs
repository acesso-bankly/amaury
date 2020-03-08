using System.Threading.Tasks;
using Amaury.V2.Abstractions;

namespace Amaury.V2.Persistence
{
    public interface ISnapshotRepository 
    {
        Task SaveAsync<TEntity>(TEntity entity) where TEntity : CelebrityAggregateBase;

        Task<TEntity> GetAsync<TEntity>(string aggregateId) where TEntity : CelebrityAggregateBase;
    }
}
