using System.Collections.Generic;

namespace Amaury.EventStore.DynamoDb
{
    public class EventStoreModel
    {
        public EventStoreModel() => Events = new List<string>();

        public string AggregatedId { get; set; }
        
        public int? Version { get; set; }

        public long Timestamp { get; set; }

        public string LastAuthor { get; set; }

        public List<string> Events { get; set; }
    }
}
