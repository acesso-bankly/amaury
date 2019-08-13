using System;
using System.Collections.Generic;
using Amaury.Abstractions;
using Amaury.Test.Fixtures;

namespace Amaury.Tests.Fixtures
{
    public class FooBar : EventSourcedAggregate<FooBar>
    {
        public FooBar(Queue<ICelebrityEvent> commitedEvents) : base(commitedEvents) { }

        public FooBar() : this(new Queue<ICelebrityEvent>()) { }

        public string Foo { get; private set; }
        public string Bar { get; private set; }

        public FooBar GetState() => base.Reduce(CommittedEvents);

        public void RevertPropertyValues()
        {
            var fooBar = Reduce(CommittedEvents);
            
            Id = fooBar.Id;
            Bar = fooBar.Foo;
            Foo = fooBar.Bar;

            Append(new FakeCelebrityEvent(Id, new { Id, Foo, Bar }));
        }
    }
}
