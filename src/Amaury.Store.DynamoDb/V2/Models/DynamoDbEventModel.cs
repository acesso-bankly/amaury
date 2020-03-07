using System;
using Amaury.V2.Abstractions;
using Amaury.V2.Persistence;
using Newtonsoft.Json;

namespace Amaury.Store.DynamoDb.V2.Models
{
    public class DynamoDbEventModel : IEventStoreModel
    {
        public DynamoDbEventModel() { }

        public DynamoDbEventModel(CelebrityEventBase eventBase)
        {
            AggregateId = eventBase.AggregateId;
            AggregateVersion = eventBase.AggregateVersion;
            Created = eventBase.Created;
            Name = eventBase.Name;
            Data = JsonConvert.SerializeObject(eventBase);
        }

        public string AggregateId { get;set; }
        
        public string Name { get;set; }

        public int? Version { get;set; }

        public long AggregateVersion { get;set; }

        public DateTime Created { get;set; }

        public string Data { get;set; }
    }
}
