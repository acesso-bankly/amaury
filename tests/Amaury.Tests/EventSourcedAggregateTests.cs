using System;
using System.Collections.Generic;
using System.Linq;
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
            var fisrtEvent = new FakeCelebrityWasCreatedEvent(expectedAggregatedId, new { Foo = "Bar", Bar = "Foo" });
            var secondEvent = new FakeCelebrityWasCreatedEvent(expectedAggregatedId, new { Foo = "Foo", Bar = "Bar" });
            events.Enqueue(fisrtEvent);
            events.Enqueue(secondEvent);

            var fooBar = new FooBar(events);

            var reduced = fooBar.GetState();

            reduced.Should().NotBeEquivalentTo(fisrtEvent.Data);
            reduced.Should().BeEquivalentTo(secondEvent.Data);
        }

        [Fact(DisplayName = "Should reduce events to entity in your initial state when not exist committed events")]
        public void ShouldReduceEventsToEntityInYourInitialStateWhenNotExistCommitedEvents()
        {
            var fooBar = new FooBar();

            var reduced = fooBar.GetState();

            reduced.Should().BeEquivalentTo(new FooBar());
        }

        [Fact(DisplayName = "Should reduce events to entity filter by event name")]
        public void ShouldAppendEventToPpenddingEventsQueueA()
        {
            var fooBar = new FooBar();

            fooBar.RevertPropertyValues();

            var state = fooBar.GetStateByEventName("FAKE_CELEBRITY_WAS_CREATED");
            
            state.Should().BeEquivalentTo(new FooBar());
        }

        [Fact(DisplayName = "Should append event to pending events queue")]
        public void ShouldAppendEventToPpenddingEventsQueue()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var fisrtEvent = new FakeCelebrityWasCreatedEvent(expectedAggregatedId, new { Id = expectedAggregatedId, Foo = "Bar", Bar = "Foo" });
            var events = new Queue<ICelebrityEvent>();
            events.Enqueue(fisrtEvent);

            var fooBar = events.Reduce<FooBar>();

            fooBar.RevertPropertyValues();

            var pendingEvent = fooBar.PendingEvents.Should().HaveCount(1).And.Subject.First();
            pendingEvent.AggregatedId.Should().Be(expectedAggregatedId);
            pendingEvent.Data.Should().Be(new { Id = expectedAggregatedId, Foo = "Foo", Bar = "Bar" });
        }
    }
}
