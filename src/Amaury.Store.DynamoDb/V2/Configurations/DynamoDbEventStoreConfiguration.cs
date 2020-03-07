using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.V2.Persistence;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    public class DynamoDbEventStoreConfiguration : ICelebrityEventStoreConfiguration
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDbEventStoreConfiguration(IAmazonDynamoDB dynamoDb, EventStoreOptions options)
        {
            _dynamoDb = dynamoDb ?? throw new ArgumentNullException(nameof(dynamoDb));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private EventStoreOptions Options { get; }

        public async Task ConfigureAsync()
        {
            var request = new CreateTableRequest
            {
                TableName = Options.StoreName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("AggregateId", ScalarAttributeType.S),
                    new AttributeDefinition("AggregateVersion", ScalarAttributeType.N),
                    new AttributeDefinition("Name", ScalarAttributeType.S),
                    new AttributeDefinition("Timestamp", ScalarAttributeType.S),
                    new AttributeDefinition("Data", ScalarAttributeType.S)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("AggregatedId", KeyType.HASH),
                    new KeySchemaElement("AggregateVersion", KeyType.RANGE)
                },
                BillingMode = Options.BillingMode,
                ProvisionedThroughput = Options.BillingMode == BillingMode.PAY_PER_REQUEST ? null : Options.ProvisionedThroughput,
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