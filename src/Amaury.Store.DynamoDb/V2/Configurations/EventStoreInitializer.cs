using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amaury.Abstractions.Persistence;
using AspNetCore.AsyncInitialization;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoDbEventStoreInitializer : IAsyncInitializer
    {
        private readonly IEventStoreConfiguration _configureEventStore;

        public DynamoDbEventStoreInitializer(IEventStoreConfiguration registerTables) => _configureEventStore = registerTables;

        public async Task InitializeAsync() => await _configureEventStore.ConfigureAsync();
    }
}