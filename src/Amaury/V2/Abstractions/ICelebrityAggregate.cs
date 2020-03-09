using System.Collections.Generic;
using System.Linq;

namespace Amaury.V2.Abstractions
{
    public interface ICelebrityAggregate
    {
        long Version { get; }
        bool HasUncommittedEvents { get; }

        ICollection<CelebrityEventBase> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}
