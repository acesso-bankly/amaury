using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amaury.V2.Persistence.Configuration;
using AspNetCore.AsyncInitialization;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoDbEventStoreInitializer : IAsyncInitializer
    {
        private readonly ICelebrityEventStoreConfiguration _configuration;

        public DynamoDbEventStoreInitializer(ICelebrityEventStoreConfiguration configuration) => _configuration = configuration;

        public async Task InitializeAsync() => await _configuration.ConfigureAsync();
    }
}