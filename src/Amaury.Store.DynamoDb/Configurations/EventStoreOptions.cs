using System.Diagnostics.CodeAnalysis;
using Amazon.Extensions.NETCore.Setup;

namespace Amaury.Store.DynamoDb.Configurations
{
    [ExcludeFromCodeCoverage]
    public class EventStoreOptions : AWSOptions
    {
        public string StoreName { get; set; }
    }
}