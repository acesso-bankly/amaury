using System.Collections.Generic;

namespace Amaury.V2.Abstractions
{
    public interface ICelebrityAggregate
    {
        long Version { get; }
        IEnumerable<CelebrityEventBase> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
