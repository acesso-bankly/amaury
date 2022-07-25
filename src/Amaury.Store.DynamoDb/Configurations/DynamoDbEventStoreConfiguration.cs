using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Persistence.Configuration;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Amaury.Store.DynamoDb.Configurations
{
    public class DynamoDbEventStoreConfiguration : ICelebrityEventStoreConfiguration
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDbEventStoreConfiguration(IAmazonDynamoDB dynamoDb, DynamoEventStoreOptions options)
        {
            _dynamoDb = dynamoDb ?? throw new ArgumentNullException(nameof(dynamoDb));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private DynamoEventStoreOptions Options { get; }

        public async Task ConfigureAsync()
        {
            var tableName = Options.EventStore;
            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("PartitionKey", ScalarAttributeType.S),
                    new AttributeDefinition("SortKey", ScalarAttributeType.S)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("PartitionKey", KeyType.HASH),
                    new KeySchemaElement("SortKey", KeyType.RANGE)
                },
                BillingMode = Options.BillingMode,
                ProvisionedThroughput = Options.BillingMode == BillingMode.PAY_PER_REQUEST ? null : Options.ProvisionedThroughput,
                LocalSecondaryIndexes = new List<LocalSecondaryIndex>
                {
                        new FindByPartitionKeyAndAggregateVersionIndex()
                }
            };

            await CreateIfNotExistAsync(request, tableName);
        }

        public async Task<bool> TableExist(string tableName)
        {
            var tables = await _dynamoDb.ListTablesAsync();
            var existTable = tables.TableNames.Contains(tableName);
            return existTable;
        }

        private async Task CreateIfNotExistAsync(CreateTableRequest request, string tableName)
        {
            if(await TableExist(tableName)) return;

            await _dynamoDb.CreateTableAsync(request);
        }
    }
}