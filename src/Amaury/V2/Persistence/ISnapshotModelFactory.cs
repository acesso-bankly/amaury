using Amaury.V2.Abstractions;

namespace Amaury.V2.Persistence
{
    public interface ISnapshotModelFactory<TEntity, TModel> where TEntity : CelebrityAggregateBase where TModel : ISnapshotModel
    {
        TModel ToModel(TEntity entity);

        TEntity ToEntity(TModel model);
    }
}