using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Amaury.V2.Abstractions;
using Newtonsoft.Json;

namespace Amaury.V2.Extensions
{
    public static class CelebrityEventExtensions
    {
        public static TEntity TakeSnapshot<TEntity>(this IEnumerable<CelebrityEventBase> self) where TEntity : CelebrityAggregateBase
        {
            var instance = (TEntity) Activator.CreateInstance(typeof(TEntity), true);
            
            foreach(var item in self) { instance.ApplyEvent(item); }

            return instance;
        }

        public static CelebrityEventBase TakeEvent(this IEnumerable<CelebrityEventBase> self, string name = null, long? aggregatedVersion = null)
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
