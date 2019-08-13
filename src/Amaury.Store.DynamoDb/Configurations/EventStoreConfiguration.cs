using Amaury.Abstractions.Persistence;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amaury.Store.DynamoDb.Configurations
{
    public class EventStoreConfiguration : IEventStoreConfiguration
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public EventStoreConfiguration(IAmazonDynamoDB dynamoDb, EventStoreOptions options)
        {
            _dynamoDb = dynamoDb;
            Options = options;
        }

        private EventStoreOptions Options { get; }

        public async Task ConfigureAsync()
        {
            var request = new CreateTableRequest
            {
                    TableName = Options.StoreName,
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                            new AttributeDefinition("AggregatedId", ScalarAttributeType.S)
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                            new KeySchemaElement("AggregatedId", KeyType.HASH)
                    },
                    ProvisionedThroughput = new ProvisionedThroughput(10, 5),
            };

            await CreateIfNotExist(request, Options.StoreName);
        }

        public async Task<bool> TableExist(string tableName)
        {
            var tables = await _dynamoDb.ListTablesAsync();
            var existTable = tables.TableNames.Contains(Options.StoreName);
            return existTable;
        }

        private async Task CreateIfNotExist(CreateTableRequest request, string tableName)
        {
            if(await TableExist(tableName)) { return; }

            await _dynamoDb.CreateTableAsync(request);
        }
    }
}