using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Extensions.NETCore.Setup;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DynamoEventStoreOptions : AWSOptions
    {
        public string StoreName { get; set; }
        public BillingMode BillingMode { get; set; }
        public ProvisionedThroughput ProvisionedThroughput { get; set; }
        public string IndexName { get; set; }
    }
}