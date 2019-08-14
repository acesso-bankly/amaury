using System.Threading.Tasks;
using Amaury.Abstractions.Persistence;
using Amaury.Store.DynamoDb.Configurations;
using FluentAssertions;
using Xunit;

namespace Amaury.Store.DynamoDb.Tests.Configurations
{
    public class EventStoreConfigurationTests : DynamoDbBaseTest
    {
        private readonly IEventStoreConfiguration _configuration;

        public EventStoreConfigurationTests()
        {
            _configuration = new EventStoreConfiguration(DynamoDb, Options);
        }

        [Fact(DisplayName = "Deve configurar a tabela do event store")]
        public async Task ShouldSettingAEventStoreTable()
        {


            var result = await _configuration.TableExist(Options.StoreName);
            result.Should().BeTrue();
        }
    }
}
