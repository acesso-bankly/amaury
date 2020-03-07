using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.V2.Abstractions;

namespace Amaury.V2.Persistence
{
    public interface ICelebrityEventStore
    {
        Task CommitEventsAsync(IEnumerable<CelebrityEventBase> events, CancellationToken cancellationToken);
        Task<IEnumerable<CelebrityEventBase>> ReadEventsAsync(string aggregateId, CancellationToken cancellationToken);
    }
}
