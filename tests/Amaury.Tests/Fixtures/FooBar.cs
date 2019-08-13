using System.Collections.Generic;
using Amaury.Abstractions;

namespace Amaury.Tests.Fixtures
{
    public class FooBar : EventSourcedAggregate<FooBar>
    {
        public FooBar(Queue<ICelebrityEvent> commitedEvents) : base(commitedEvents) { }

        public FooBar() : this(new Queue<ICelebrityEvent>()) { }

        public string Foo { get; private set; }
        public string Bar { get; private set; }

    }
}
