using Amaury.Abstractions;
using Amaury.Abstractions.Bus;

namespace Amaury.Tests.Fixtures
{
    public class FooBar : EventSourcedAggregate<FooBar>
    {
        public FooBar(ICelebrityEventsBus bus) : base(bus)
        {

        }

        public string Foo { get; private set; }
        public string Bar { get; private set; }
    }
}
