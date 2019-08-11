using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amaury.Abstractions.Persistence;
using AspNetCore.AsyncInitialization;

namespace Amaury.Store.DynamoDb.Configurations
{
    [ExcludeFromCodeCoverage]
    public class EventStoreInitializer : IAsyncInitializer
    {
        private readonly IEventStoreConfiguration _configureEventStore;

        public EventStoreInitializer(IEventStoreConfiguration registerTables) => _configureEventStore = registerTables;

        public async Task InitializeAsync() => await _configureEventStore.ConfigureAsync();
    }
}