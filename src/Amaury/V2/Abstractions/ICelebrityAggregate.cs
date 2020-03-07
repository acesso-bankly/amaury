using System.Collections.Generic;

namespace Amaury.V2.Abstractions
{
    public interface ICelebrityAggregate
    {
        long Version { get; }
        ICollection<CelebrityEventBase> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
