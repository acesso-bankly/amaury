using System;
using Amaury.Abstractions;
using Amaury.Persistence;
using Newtonsoft.Json;

namespace Amaury.Store.DynamoDb.Models
{
    public class DynamoDbEventModel : IEventStoreModel<string>
    {
        public DynamoDbEventModel() { }

        public DynamoDbEventModel(CelebrityEventBase eventBase, string eventPrefix)
        {
            PartitionKey = AggregateId = eventBase.AggregateId;
            AggregateVersion = eventBase.AggregateVersion;
            Timestamp = eventBase.Timestamp;
            Name = eventBase.Name;

            SortKey = $"{eventPrefix}#{AggregateVersion}#{Name}#{Timestamp:O}";
            Data = JsonConvert.SerializeObject(eventBase);
        }

        public string PartitionKey  { get; set; }
        public string SortKey  { get; set; }
        public string AggregateId { get;set; }
        public long AggregateVersion { get;set; }
        public string Name { get;set; }
        public DateTime Timestamp { get;set; }
        public string Data { get;set; }
    }
}
