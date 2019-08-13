using System;
using System.Collections.Generic;
using Amaury.Abstractions;
using Amaury.Test.Fixtures;
using Amaury.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Amaury.Tests
{
    public class EventSourcedAggregateTests
    {
        [Fact(DisplayName = "Should reduce events to entity in your last state when exist commited events")]
        public void ShouldReduceEventsToEntityInYourLastStateWhenExistCommitedEvents()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var events = new Queue<ICelebrityEvent>();
            var fisrtEvent = new FakeCelebrityEvent(expectedAggregatedId, new { Foo = "Bar", Bar = "Foo" });
            var secondEvent = new FakeCelebrityEvent(expectedAggregatedId, new { Foo = "Foo", Bar = "Bar" });
            events.Enqueue(fisrtEvent);
            events.Enqueue(secondEvent);

            var fooBar = new FooBar(events);

            var reduced = fooBar.Reduce();

            reduced.Should().NotBeEquivalentTo(fisrtEvent.Data);
            reduced.Should().BeEquivalentTo(secondEvent.Data);
        }

        [Fact(DisplayName = "Should reduce events to entity in your initial state when not exist committed events")]
        public void ShouldReduceEventsToEntityInYourInitialStateWhenNotExistCommitedEvents()
        {
            var fooBar = new FooBar();

            var reduced = fooBar.Reduce();

            reduced.Should().BeEquivalentTo(new FooBar());
        }
    }
}
