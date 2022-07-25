using Amaury.Abstractions;

namespace Amaury.Persistence;

public interface ISnapshotModel<TEntity>
    where TEntity : CelebrityAggregateBase
{
    string AggregateId { get; set; }
    long Version { get; set; }
}