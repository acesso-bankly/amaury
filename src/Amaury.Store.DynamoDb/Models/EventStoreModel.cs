using System.Collections.Generic;
using Amaury.Abstractions.Persistence;
using Amazon.DynamoDBv2.DataModel;

namespace Amaury.Store.DynamoDb.Models
{
    public class EventStoreModel : IEventStoreModel
    {
        public EventStoreModel() => Events = new List<string>();

        [DynamoDBHashKey]
        public string AggregatedId { get; set; }

        [DynamoDBVersion]
        public int? Version { get; set; }

        public string Timestamp { get; set; }

        public List<string> Events { get; set; }
    }
}