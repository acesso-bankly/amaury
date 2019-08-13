using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amaury.Abstractions
{
    public interface IEventSourcedAggregate<out TEntity> : IAggregate where TEntity : class, new()
    {
        Queue<ICelebrityEvent> CommittedEvents { get; }
        Queue<ICelebrityEvent> PendingEvents { get; }
    }
}