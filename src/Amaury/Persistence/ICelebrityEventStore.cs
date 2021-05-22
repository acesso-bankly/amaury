using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions;

namespace Amaury.Persistence
{
    public interface ICelebrityEventStore
    {
        Task CommitBatchAsync(IEnumerable<CelebrityEventBase> events, CancellationToken cancellationToken = default);
        Task CommitAsync(CelebrityEventBase @event, CancellationToken cancellationToken = default);
        Task<IEnumerable<CelebrityEventBase>> ReadEventsAsync(string aggregateId, long? version = null, bool consistentRead = false, CancellationToken cancellationToken = default);
    }
}
