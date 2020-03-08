using System.Collections.Generic;
using System.Linq;
using Amaury.V2.Abstractions;
using Newtonsoft.Json;

namespace Amaury.V2.Extensions
{
    public static class CelebrityEventExtensions
    {
        public static TEntity TakeSnapshot<TEntity>(this IEnumerable<CelebrityEventBase> self, string name = null, long? aggregatedVersion = null) where TEntity : CelebrityAggregateBase
        {
            var @event = name is null ? self.TakeEvent(aggregatedVersion) : self.TakeEvent(name, aggregatedVersion);

            return @event.TakeSnapshot<TEntity>();
        }

        public static TEntity TakeSnapshot<TEntity>(this CelebrityEventBase self) where TEntity : CelebrityAggregateBase
        {
            var json = JsonConvert.SerializeObject(self);
            var entity = JsonConvert.DeserializeObject<TEntity>(json);
            entity.SetVersion(self.AggregateVersion);

            return entity;
        }

        private static CelebrityEventBase TakeEvent(this IEnumerable<CelebrityEventBase> self, string name = null, long? aggregatedVersion = null)
        {
            var events = self.Where(item => item.Name.Equals(name));
            var @event = events.TakeEvent(aggregatedVersion);

            return @event;
        }

        private static CelebrityEventBase TakeEvent(this IEnumerable<CelebrityEventBase> self, long? aggregatedVersion = null)
        {
            var events = self.ToList();
            var version = aggregatedVersion ?? events.Max(a => a.AggregateVersion);
            var @event = events.Find(e => e.AggregateVersion.Equals(version));

            return @event;
        }
    }
}
