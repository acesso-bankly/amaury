using System;
using System.Collections.Generic;
using System.Linq;
using Amaury.Abstractions;
using Amaury.Test.Fixtures;

namespace Amaury.Tests.Fixtures
{
    public sealed class FooBar : EventSourcedAggregate<FooBar>
    {
        public FooBar(Queue<ICelebrityEvent> commitedEvents) : base(commitedEvents) { }

        public FooBar() : this(new Queue<ICelebrityEvent>()) { }

        public FooBar(string foo, string bar) : this(new Queue<ICelebrityEvent>())
        {
            Id = Guid.NewGuid().ToString();
            Foo = foo;
            Bar = bar;
            Append(new FakeCelebrityWasCreatedEvent(Id, new { Id, Foo, Bar }));
        }

        public string Foo { get; private set; }
        public string Bar { get; private set; }

        public void RevertPropertyValues()
        {
            var aux = Foo;
            Foo = Bar;
            Bar = aux;
            
            Append(new FakeCelebrityWasUpdatedEvent(Id, new { Id, Foo, Bar }));
        }
    }
}
