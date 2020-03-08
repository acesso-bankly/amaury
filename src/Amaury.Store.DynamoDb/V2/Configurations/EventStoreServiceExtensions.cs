using System;
using System.Diagnostics.CodeAnalysis;
using Amaury.V2.Abstractions;
using Amaury.V2.Persistence;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class EventStoreServiceExtensions
    {
        public static void AddCelebrityEventStore(this IServiceCollection services, Action<DynamoEventStoreOptions> configure = null)
        {
            var options = new DynamoEventStoreOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);
            services.AddAWSService<IAmazonDynamoDB>(options);
            services.AddSingleton<ICelebrityEventStoreConfiguration, DynamoDbEventStoreConfiguration>();
            services.AddAsyncInitializer<DynamoDbEventStoreInitializer>();
            services.AddSingleton<ICelebrityEventStore, DynamoDbCelebrityEventStore>();
        }

        public static void AddCelebrityEventFactory<TFactory>(this IServiceCollection services) where TFactory : ICelebrityEventFactory, new()
        {
            var factory = new TFactory();
            services.AddSingleton<ICelebrityEventFactory>(_ => factory);
        }

        public static void AddCelebritySnapshotRepository<TEntity, TImplementation>(this IServiceCollection services)
                where TEntity : CelebrityAggregateBase
                where TImplementation : class, ISnapshotRepository<TEntity>
            => services.AddSingleton<ISnapshotRepository<TEntity>, TImplementation>();

        public static void AddEventStore(this IServiceCollection services, IAmazonDynamoDB client, Action<DynamoEventStoreOptions> configure = null)
        {
            var options = new DynamoEventStoreOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<ICelebrityEventStoreConfiguration, DynamoDbEventStoreConfiguration>();
            services.AddAsyncInitializer<DynamoDbEventStoreInitializer>();
            services.AddSingleton<ICelebrityEventStore, DynamoDbCelebrityEventStore>();
        }
    }
}
