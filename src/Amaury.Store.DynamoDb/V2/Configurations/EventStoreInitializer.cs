using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amaury.V2.Persistence;
using AspNetCore.AsyncInitialization;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoDbEventStoreInitializer : IAsyncInitializer
    {
        private readonly ICelebrityEventStoreConfiguration _configureEventStore;

        public DynamoDbEventStoreInitializer(ICelebrityEventStoreConfiguration registerTables) => _configureEventStore = registerTables;

        public async Task InitializeAsync() => await _configureEventStore.ConfigureAsync();
    }
}