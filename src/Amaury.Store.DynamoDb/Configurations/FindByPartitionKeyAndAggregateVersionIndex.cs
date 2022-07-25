using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Amaury.Store.DynamoDb.Configurations;

public sealed class FindByPartitionKeyAndAggregateVersionIndex : LocalSecondaryIndex
{
    public const string INDEX_NAME = "FIND_BY_PARTITION_KEY_AND_AGGREGATE_VERSION_INDEX";

    public FindByPartitionKeyAndAggregateVersionIndex() => Create();

    public void Create()
    {
        IndexName = INDEX_NAME;
        KeySchema = new List<KeySchemaElement>
                {
                        new KeySchemaElement("PartitionKey", KeyType.HASH),
                        new KeySchemaElement("AggregateVersion", KeyType.RANGE)
                };
        Projection = new Projection { ProjectionType = ProjectionType.ALL };
    }
}