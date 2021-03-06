using System;
using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Extensions.NETCore.Setup;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoEventStoreOptions : AWSOptions
    {
        [Obsolete("Prefer use EventStore instead")]
        public string StoreName { get; set; }

        /// <summary>
        /// Set event store table name
        /// </summary>
        public string EventStore { get; set; }

        /// <summary>
        /// Set snapshot table name
        /// </summary>
        public string SnapshotTable { get; set; }

        /// <summary>
        /// Set billing mode. Choose between PAY_PER_REQUEST or PROVISIONED. When chosen PROVISIONED, should set provisioned throughput
        /// </summary>
        public BillingMode BillingMode { get; set; }

        /// <summary>
        /// Set provisioned throughput when billing mode is PROVISIONED
        /// </summary>
        public ProvisionedThroughput ProvisionedThroughput { get; set; }

        /// <summary>
        /// Set time, in hours, for dada to expires
        /// </summary>
        public long ExpireDataAfterTime { get; set; } = 1;
        public string IndexName { get; set; }
    }
}