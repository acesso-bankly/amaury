using System;
using Amaury.Abstractions;

namespace Amaury.Tests.Fixtures
{
    public class FakeEntity : CelebrityAggregateBase
    {
        private string _id;
        public FakeEntity() => _id = Guid.NewGuid().ToString();

        public FakeEntity(string id) => _id = id;

        public override string GetAggregateId() => Id;

        public string Message { get; private set; }

        public void DoSomething(string message) => AppendEvent(new FakeEvent(Id, message));

        public void Apply(FakeEvent @event)
        {
            _id = @event.Id;
            Message = @event.Message;
        }
    }

    public class FakeEvent : CelebrityEventBase
    {
        public FakeEvent(string id, string message)
        {
            Id = id;
            Message = message;
        }

        public override string Name => "FAKE_EVENT";

        public string Id { get; }
        public string Message { get; }
    }
}
