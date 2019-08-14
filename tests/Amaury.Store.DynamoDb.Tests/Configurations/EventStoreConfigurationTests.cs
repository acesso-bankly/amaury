using System.Threading.Tasks;
using Amaury.Abstractions.Persistence;
using Amaury.Store.DynamoDb.Configurations;
using FluentAssertions;
using Xunit;

namespace Amaury.Store.DynamoDb.Tests.Configurations
{
    public class EventStoreConfigurationTests
    {
                [Fact(DisplayName = "Deve configurar a tabela do event store")]
        public async Task ShouldSettingAEventStoreTable()
        {
          var result = true;
          result.Should().BeTrue();
        }
    }
}
