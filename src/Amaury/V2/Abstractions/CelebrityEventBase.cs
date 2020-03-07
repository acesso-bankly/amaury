using System;

namespace Amaury.V2.Abstractions
{
    public abstract class CelebrityEventBase
    {
        public CelebrityEventBase() => Created = DateTime.UtcNow;

        public string AggregateId { get; protected set; }
        public long AggregateVersion { get; protected set; }
        public DateTime Created { get; protected set; }

        public abstract string Name { get; }

        public void SetAggregateId(string aggregateId)
            => AggregateId = aggregateId ?? throw new ArgumentNullException(nameof(aggregateId));

        public void SetAggregateVersion(long aggregateVersion)
        {
            if(aggregateVersion <= 0) throw new ArgumentOutOfRangeException(nameof(aggregateVersion));

            AggregateVersion = aggregateVersion;
        }

        public void SetCreated(DateTime created)
            => Created = created;
    }
}
