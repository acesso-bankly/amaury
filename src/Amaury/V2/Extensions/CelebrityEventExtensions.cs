using System.Collections.Generic;
using System.Linq;
using Amaury.V2.Abstractions;
using Newtonsoft.Json;

namespace Amaury.V2.Extensions
{
    public static class CelebrityEventExtensions
    {
        public static TEntity TakeSnapshot<TEntity>(this IEnumerable<CelebrityEventBase> self) where TEntity : CelebrityAggregateBase
            => self.Take<TEntity>().FirstOrDefault();

        private static IEnumerable<TEntity> Take<TEntity>(this IEnumerable<CelebrityEventBase> self) where TEntity : CelebrityAggregateBase
        {
            var events = self.ToList();
            foreach(var item in events) { yield return item.ToEntity<TEntity>(); }
        }

        private static TEntity ToEntity<TEntity>(this CelebrityEventBase self) where TEntity : CelebrityAggregateBase
        {
            var json = JsonConvert.SerializeObject(self);
            var entity = JsonConvert.DeserializeObject<TEntity>(json);
            entity.SetVersion(self.AggregateVersion);

            return entity;
        }
    }
}
