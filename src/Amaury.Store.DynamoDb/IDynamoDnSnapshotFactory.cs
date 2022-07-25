using Amaury.Abstractions;
using Amaury.Persistence;
using Amaury.Store.DynamoDb.Models;

namespace Amaury.Store.DynamoDb
{
    public interface IDynamoDnSnapshotFactory<TEntity, TModel> : ISnapshotFactory<TEntity, TModel>
            where TEntity : CelebrityAggregateBase
            where TModel : DynamoDbSnapshotModel<TEntity>


    {

    }
}
