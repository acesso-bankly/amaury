using Amaury.Abstractions;
using Amaury.Persistence;

namespace Amaury.Store.DynamoDb.Models
{
    public class DynamoDbSnapshotModel<TEntity> : ISnapshotModel<TEntity> where TEntity : CelebrityAggregateBase
    {
        public DynamoDbSnapshotModel(TEntity entity, string snapshotPrefix)
        {
            PartitionKey = AggregateId = entity.AggregateId;
            SortKey = $"{snapshotPrefix}{entity.AggregateId}";
            Version = entity.Version;
        }

        public string PartitionKey  { get; set; }

        public string SortKey  { get; set; }

        public string AggregateId { get;set; }

        public long Version { get;set; }
    }
}