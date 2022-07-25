using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Extensions.NETCore.Setup;

namespace Amaury.Store.DynamoDb.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoEventStoreOptions : AWSOptions
    {
        /// <summary>
        /// Set event store table name
        /// </summary>
        public string EventStore { get; set; }

        /// <summary>
        /// Set billing mode. Choose between PAY_PER_REQUEST or PROVISIONED. When chosen PROVISIONED, should set provisioned throughput
        /// </summary>
        public BillingMode BillingMode { get; set; }

        /// <summary>
        /// Set provisioned throughput when billing mode is PROVISIONED
        /// </summary>
        public ProvisionedThroughput ProvisionedThroughput { get; set; }

        public string IndexName { get; set; }

        public string EventPrefix { get; set; } = "EVENT#";
        public string SnapshotPrefix { get; set; } = "SNAPSHOT#";
    }
}