using Amaury.V2.Abstractions;
using Amaury.V2.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury.V2.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddSnapshotModelFactory<TFactory, TEntity, TModel>(this IServiceCollection services)
                where TFactory : class, ISnapshotModelFactory<TEntity, TModel>
                where TEntity : CelebrityAggregateBase
                where TModel : class, ISnapshotModel
            => services.AddSingleton<ISnapshotModelFactory<TEntity, TModel>, TFactory>();

        public static void AddCelebrityEventFactory<TFactory>(this IServiceCollection services) where TFactory : class, ICelebrityEventFactory
            => services.AddSingleton<ICelebrityEventFactory, TFactory>();
    }
}
