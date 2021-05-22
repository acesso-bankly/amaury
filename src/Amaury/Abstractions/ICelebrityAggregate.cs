using System.Collections.Generic;

namespace Amaury.Abstractions
{
    public interface ICelebrityAggregate
    {
        long Version { get; }
        bool HasUncommittedEvents { get; }

        ICollection<CelebrityEventBase> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}
