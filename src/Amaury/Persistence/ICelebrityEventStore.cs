using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions;

namespace Amaury.Persistence
{
    public interface ICelebrityEventStore<TEntity> where TEntity : CelebrityAggregateBase
    {
        Task CommitAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> LoadAsync(string aggregateId, long? version = null, bool consistentRead = false, CancellationToken cancellationToken = default);
    }
}
