using System;
using System.Diagnostics.CodeAnalysis;
using Amaury.Abstractions.Persistence;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury.Store.DynamoDb.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class EventStoreServiceExtensions
    {
        public static void AddEventStore(this IServiceCollection services, Action<EventStoreOptions> configure = null)
        {
            var options = new EventStoreOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);
            services.AddAWSService<IAmazonDynamoDB>(options);
            services.AddSingleton<IEventStoreConfiguration, EventStoreConfiguration>();
            services.AddAsyncInitializer<EventStoreInitializer>();
            services.AddSingleton<ICelebrityEventStore, DynamoDbEventStore>();
        }
    }
}
