using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amaury.Persistence.Configuration;
using Extensions.Hosting.AsyncInitialization;

namespace Amaury.Store.DynamoDb.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoDbEventStoreInitializer : IAsyncInitializer
    {
        private readonly ICelebrityEventStoreConfiguration _configureEventStore;

        public DynamoDbEventStoreInitializer(ICelebrityEventStoreConfiguration registerTables) => _configureEventStore = registerTables;

        public async Task InitializeAsync() => await _configureEventStore.ConfigureAsync();
    }
}