using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.V2.Abstractions;

namespace Amaury.V2.Persistence
{
    public interface ICelebrityEventStore
    {
        Task CommitBatchAsync(IEnumerable<CelebrityEventBase> events, CancellationToken cancellationToken = default);
        Task CommitAsync(CelebrityEventBase @event, CancellationToken cancellationToken = default);
        Task<IEnumerable<CelebrityEventBase>> ReadEventsAsync(string aggregateId, long? version = null, CancellationToken cancellationToken = default);
    }
}
