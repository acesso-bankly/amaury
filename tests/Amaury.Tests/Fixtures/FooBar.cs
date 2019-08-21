using System;
using System.Collections.Generic;
using System.Linq;
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

        public FooBar GetState() => CommittedEvents.Reduce<FooBar>();

        public void RevertPropertyValues()
        {
            var aux = Foo;
            Foo = Bar;
            Bar = aux;
            
            Append(new FakeCelebrityWasUpdatedEvent(Id, new { Id, Foo, Bar }));
        }

        public FooBar GetStateByEventName(string name) => CommittedEvents.Reduce<FooBar>(events => events.Where(e => e.Name.Equals(name)).ToList());
        
    }
}
