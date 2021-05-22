using System.Collections.Concurrent;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Amaury.Tests
{
    public class CelebrityAggregateBaseTests
    {
        [Theory(DisplayName = "Should append event when do something")]
        [AutoNSubstituteData]
        public void ShouldAppendEvent_WhenDoSomething(FakeEntity entity, string message)
        {
            entity.DoSomething(message);

            entity.GetUncommittedEvents()
                    .Should()
                    .SatisfyRespectively(item => item.Should().BeAssignableTo<FakeEvent>());
        }

        [Theory(DisplayName = "Should up version when append event")]
        [AutoNSubstituteData]
        public void ShouldUpVersion_WhenAppendEvent(FakeEntity entity, string message)
        {
            var previews = entity.Version;
            entity.DoSomething(message);

            entity.Version.Should().BeGreaterThan(previews);
        }

        [Theory(DisplayName = "Should raise events of type when exist events")]
        [AutoNSubstituteData]
        public async Task ShouldRaiseEventsOfType_WhenExistEvents(FakeEntity entity, string message)
        {
            entity.DoSomething(message);
            var bag = new ConcurrentBag<CelebrityEventBase>();

            await entity.RaiseEventsOfTypeAsync<FakeEvent>(item => PublishAsync(item, bag));

            bag.Count.Should().Be(1);
        }

        [Theory(DisplayName = "Should not raise events of type when not exist events")]
        [AutoNSubstituteData]
        public async Task ShouldNotRaiseEventsOfType_WhenNotExistEvents(FakeEntity entity)
        {
            var bag = new ConcurrentBag<CelebrityEventBase>();

            await entity.RaiseEventsOfTypeAsync<FakeEvent>(item => PublishAsync(item, bag));

            bag.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Should raise events when exist events")]
        [AutoNSubstituteData]
        public async Task ShouldAllRaiseEvents_WhenExistEvents(FakeEntity entity, string message)
        {
            entity.DoSomething(message);
            entity.DoSomething(message);

            var bag = new ConcurrentBag<CelebrityEventBase>();

            await entity.RaiseAllEventsAsync(item => PublishAsync(item, bag));

            bag.Count.Should().Be(2);
        }

        [Theory(DisplayName = "Should not raise events when not exist events")]
        [AutoNSubstituteData]
        public async Task ShouldNotRaiseEvents_WhenNotExistEvents(FakeEntity entity)
        {
            var bag = new ConcurrentBag<CelebrityEventBase>();

            await entity.RaiseAllEventsAsync(item => PublishAsync(item, bag));

            bag.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Should clear events")]
        [AutoNSubstituteData]
        public void ShouldClearEvents(FakeEntity entity, string message)
        {
            entity.DoSomething(message);

            entity.ClearUncommittedEvents();

            entity.GetUncommittedEvents().Should().BeEmpty();
        }

        private static Task PublishAsync(object @event, ConcurrentBag<CelebrityEventBase> bag)
        {
            bag.Add(@event as CelebrityEventBase);

            return Task.CompletedTask;
        }
    }
}
