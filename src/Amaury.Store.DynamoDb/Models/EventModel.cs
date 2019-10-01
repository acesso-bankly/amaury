using System;
using Amaury.Abstractions;

namespace Amaury.Store.DynamoDb.Models
{
    public class EventModel : ICelebrityEvent
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public dynamic Data { get; set; }
    }
}