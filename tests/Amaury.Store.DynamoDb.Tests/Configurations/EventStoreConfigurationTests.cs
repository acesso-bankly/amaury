using System;
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
            Options.StoreName = Guid.NewGuid().ToString();
            _configuration = new EventStoreConfiguration(DynamoDb, Options);
        }

        [Fact(DisplayName = "Deve configurar a tabela do event store", Skip = "Because of the error registered on issue https://github.com/microsoft/azure-pipelines-tasks/issues/11902")]
        public async Task ShouldSettingAEventStoreTable()
        {
            await _configuration.ConfigureAsync();

            var result = await _configuration.TableExist(Options.StoreName);
            result.Should().BeTrue();
        }
    }
}
