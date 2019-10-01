using System;
using Amaury.Abstractions;

namespace Amaury.Test.Fixtures
{
    public class FakeCelebrityWasCreatedEvent : ICelebrityEvent
    {
        public FakeCelebrityWasCreatedEvent(string aggregatedId, object data)
        {
            Id = aggregatedId;
            Name = "FAKE_CELEBRITY_WAS_CREATED";
            EventId = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            Data = data;
        }

        public string Id { get; set; }

        public string EventId { get; set; }

        public string Name { get; set; }

        public DateTime Timestamp { get; set; }

        public object Data { get; set; }
    }
}
