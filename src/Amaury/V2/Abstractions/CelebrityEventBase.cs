using System;
using Newtonsoft.Json;

namespace Amaury.V2.Abstractions
{
    public abstract class CelebrityEventBase
    {
        public CelebrityEventBase() => Timestamp = DateTime.UtcNow;

        [JsonIgnore] public string AggregateId { get; protected set; }
        [JsonIgnore] public long AggregateVersion { get; protected set; }
        [JsonIgnore] public DateTime Timestamp { get; protected set; }

        [JsonIgnore] public abstract string Name { get; }

        public void SetAggregateId(string aggregateId)
            => AggregateId = aggregateId ?? throw new ArgumentNullException(nameof(aggregateId));

        public void SetAggregateVersion(long aggregateVersion)
        {
            if(aggregateVersion < 0) throw new ArgumentOutOfRangeException(nameof(aggregateVersion));

            AggregateVersion = aggregateVersion;
        }

        public void SetTimestamp(DateTime timestamp) => Timestamp = timestamp;
    }
}
