using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Extensions.NETCore.Setup;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    public class DynamoDbSnapshotRepositoryOptions : AWSOptions
    {
        public string TableName { get; set; }
        public BillingMode BillingMode { get; set; }
        public ProvisionedThroughput ProvisionedThroughput { get; set; }
        public string SnapshotIndex { get; set; }
        public string AggregateKey { get; set; } = "AggregateId";
        public bool UseConsistentlyRead { get; set; }
        public bool UseConsistentlyWrite { get; set; }
    }
}