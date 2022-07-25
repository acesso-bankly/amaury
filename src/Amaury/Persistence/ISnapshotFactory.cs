using Amaury.Abstractions;

namespace Amaury.Persistence;

public interface ISnapshotFactory<TEntity, TModel> where TEntity : CelebrityAggregateBase
        where TModel : class, ISnapshotModel<TEntity>
{
    TModel ToModel(TEntity entity);
    TEntity ToEntity(TModel model);
}