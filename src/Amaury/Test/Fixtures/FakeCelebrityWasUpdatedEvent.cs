using System;
using Amaury.Abstractions;

namespace Amaury.Test.Fixtures
{
    public class FakeCelebrityWasUpdatedEvent : ICelebrityEvent
    {
        public FakeCelebrityWasUpdatedEvent(string aggregatedId, dynamic data)
        {
            AggregatedId = aggregatedId;
            Name = "FAKE_CELEBRITY_WAS_UPDATED";
            EventId = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            Data = data;
        }

        public string AggregatedId { get;set; }

        public string EventId { get;set; }

        public string Name { get;set; }

        public DateTime Timestamp { get;set; }

        public dynamic Data { get;set; }
    }
}