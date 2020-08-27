using System;
using Amaury.V2.Abstractions;

namespace Amaury.Tests.Fixtures
{
    public class FakeEntity : CelebrityAggregateBase
    {
        public FakeEntity() => Id = Guid.NewGuid().ToString();

        public FakeEntity(string id) => Id = id;

        public override string GetAggregateId() => Id;

        public string Id { get;private set; }

        public string Message { get; private set; }

        public void DoSomething(string message) => AppendEvent(new FakeEvent(Id, message));

        public void Apply(FakeEvent @event)
        {
            Id = @event.Id;
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
