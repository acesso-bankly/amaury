using System;

namespace Amaury.MediatR.Test.Fixtures
{
    public class FakeNotifiableCelebrityEvent : INotifiableCelebrityEvent
    {
        public FakeNotifiableCelebrityEvent(string aggregatedId, dynamic data)
        {
            Id = aggregatedId;
            Name = "FAKE_EVENT";
            EventId = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            Data = data;
        }

        public string Id { get;set; }

        public string EventId { get;set; }

        public string Name { get;set; }

        public DateTime Timestamp { get;set; }

        public dynamic Data { get;set; }
    }
}
