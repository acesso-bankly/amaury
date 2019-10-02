using System;
using System.Collections.Generic;
using Amaury.Abstractions;
using Amaury.Test.Fixtures;

namespace Amaury.Tests.Fixtures
{
    public sealed class FooBar : ICelebrity<FooBar>
    {
        public FooBar() => PendingEvents = new Queue<ICelebrityEvent>();
        public FooBar(string id)
        {
            Id = id;
            PendingEvents = new Queue<ICelebrityEvent>();
        }

        public FooBar(string foo, string bar)
        {
            Id = Guid.NewGuid().ToString();
            Foo = foo;
            Bar = bar;
            PendingEvents = new Queue<ICelebrityEvent>();
            PendingEvents.Enqueue(new FakeCelebrityWasCreatedEvent(Id, new { Id, Foo, Bar }));
        }

        public string Foo { get; set; }
        public string Bar { get; set; }
        public string Zoo { get; }

        public void RevertPropertyValues()
        {
            var aux = Foo;
            Foo = Bar;
            Bar = aux;
            PendingEvents.Enqueue(new FakeCelebrityWasUpdatedEvent(Id, new { Id, Foo, Bar }));
        }

        public string Id { get;private set; }

        public Queue<ICelebrityEvent> PendingEvents { get; }
    }
}
