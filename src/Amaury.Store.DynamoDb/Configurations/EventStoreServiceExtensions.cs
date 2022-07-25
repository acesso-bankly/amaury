using System;
using System.Diagnostics.CodeAnalysis;
using Amaury.Abstractions;
using Amaury.Persistence;
using Amaury.Persistence.Configuration;
using Amaury.Store.DynamoDb.Models;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury.Store.DynamoDb.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class EventStoreServiceExtensions
    {
        public static void AddCelebrityEventStore<TEntity>(this IServiceCollection services, Action<DynamoEventStoreOptions> configure = null)
                where TEntity : CelebrityAggregateBase
        {
            var options = new DynamoEventStoreOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);
            services.AddAWSService<IAmazonDynamoDB>(options);
            services.AddSingleton<ICelebrityEventStoreConfiguration, DynamoDbEventStoreConfiguration>();
            services.AddAsyncInitializer<DynamoDbEventStoreInitializer>();
            services.AddSingleton<ICelebrityEventStore<TEntity>, DynamoDbCelebrityEventStore<TEntity>>();
        }

        public static void AddCelebrityEventFactory<TFactory>(this IServiceCollection services) where TFactory : ICelebrityEventFactory, new()
        {
            var factory = new TFactory();
            services.AddSingleton<ICelebrityEventFactory>(_ => factory);
        }

        public static void AddCelebritySnapshotRepository<TEntity, TModel>(this IServiceCollection services)
                where TEntity : CelebrityAggregateBase
                where TModel : DynamoDbSnapshotModel<TEntity>
        {
            services.AddSingleton<ISnapshotRepository<TEntity>, DynamoDbSnapshotRepository<TEntity, TModel>>();
        }

        public static void AddAddCelebritySnapshotFactory<TFactory, TEntity, TModel>(this IServiceCollection services)
                where TFactory : class, IDynamoDnSnapshotFactory<TEntity, TModel>
                where TEntity : CelebrityAggregateBase
                where TModel : DynamoDbSnapshotModel<TEntity>
        {
            services.AddSingleton<IDynamoDnSnapshotFactory<TEntity, TModel>, TFactory>();
        }
    }
}
