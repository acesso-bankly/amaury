using System;
using Amaury.Abstractions;
using Amaury.Persistence;
using Newtonsoft.Json;

namespace Amaury.Store.DynamoDb.Models
{
    public class DynamoDbEventModel : IEventStoreModel<string>
    {
        public DynamoDbEventModel() { }

        public DynamoDbEventModel(CelebrityEventBase eventBase)
        {
            AggregateId = eventBase.AggregateId;
            AggregateVersion = eventBase.AggregateVersion;
            Timestamp = eventBase.Timestamp;
            Name = eventBase.Name;
            Data = JsonConvert.SerializeObject(eventBase);
        }

        public string AggregateId { get;set; }
        public long AggregateVersion { get;set; }
        public string Name { get;set; }
        public DateTime Timestamp { get;set; }
        public string Data { get;set; }
    }
}
