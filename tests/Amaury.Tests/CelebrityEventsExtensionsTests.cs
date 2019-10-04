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

            var reduced = events.Reduce<FooBar>((a, b) =>
            {
                a.Foo = b.Data.Foo;
                a.Bar = b.Data.Bar;
                return a;
            });

            reduced.Should().NotBeEquivalentTo(fisrtEvent.Data);
            reduced.Should().BeEquivalentTo(secondEvent.Data);
        }

        [Fact(DisplayName = "Should reduce events to entity in your initial state when not exist committed events")]
        public void ShouldReduceEventsToEntityInYourInitialStateWhenNotExistCommitedEvents()
        {
            var reduced = new Queue<ICelebrityEvent>().Reduce<FooBar>();

            reduced.Should().BeEquivalentTo(new FooBar());
        }

        [Fact(DisplayName = "Should reduce events to entity through func")]
        public void ShouldReduceEventsToEntityThroughFunc()
        {
            var fooBar = new FooBar("bar", "foo");

            fooBar.RevertPropertyValues();

            var state = fooBar.PendingEvents.Reduce<FooBar>((a, b) =>
            {
                a.Foo = b.Data.Foo;
                a.Bar = b.Data.Bar;
                return a;
            });

            state.Foo.Should().Be("foo");
            state.Bar.Should().Be("bar");
        }

        [Fact(DisplayName = "Should append event to pending events queue")]
        public void ShouldAppendEventToPpenddingEventsQueue()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var fisrtEvent = new FakeCelebrityWasCreatedEvent(expectedAggregatedId, new { Id = expectedAggregatedId, Foo = "Bar", Bar = "Foo" });
            var events = new Queue<ICelebrityEvent>();
            events.Enqueue(fisrtEvent);

            var state = new FooBar(expectedAggregatedId);
            var fooBar = events.Reduce<FooBar>(state, (a, b) =>
            {
                a.Foo = b.Data.Foo;
                a.Bar = b.Data.Bar;
                return state;
            });

            fooBar.RevertPropertyValues();

            var pendingEvent = fooBar.PendingEvents.Should().HaveCount(1).And.Subject.First();
            pendingEvent.AggregatedId.Should().Be(expectedAggregatedId);

            var data = (object)pendingEvent.Data;
            data.Should().Be(new { Id = expectedAggregatedId, Foo = "Foo", Bar = "Bar" });
        }

        [Fact(DisplayName = "Should reduce events to entity without condition")]
        public void ShouldReduceEventsToEntityByCondition()
        {
            var fooBar = new FooBar("bar", "foo");

            fooBar.RevertPropertyValues();

            var state = fooBar.PendingEvents.Reduce<FooBar>();

            state.Foo.Should().Be("foo");
            state.Bar.Should().Be("bar");
        }

        [Fact(DisplayName = "Should reduce events to entity by filter")]
        public void ShouldReduceEventsToEntityByFilter()
        {
            var fooBar = new FooBar("bar", "foo");

            fooBar.RevertPropertyValues();

            var state = fooBar.PendingEvents.Reduce<FooBar>(@event => @event.Name.Equals("FAKE_CELEBRITY_WAS_UPDATED"));

            state.Foo.Should().Be("foo");
            state.Bar.Should().Be("bar");
        }

        [Fact(DisplayName = "Should throw InvalidOperationException when reduce events and exists a property not readable and writable into data")]
        public void ShouldAppendEventToPpenddingEventsQueueA()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var fisrtEvent = new FakeCelebrityWasCreatedEvent(expectedAggregatedId, new { Id = expectedAggregatedId, Foo = "Bar", Bar = "Foo", Zoo = "Zoo" });
            var events = new Queue<ICelebrityEvent>();
            events.Enqueue(fisrtEvent);

            var exception = Assert.Throws<InvalidOperationException>(() => events.Reduce<FooBar>());
            exception.Message.Should().Be($"The property \"Zoo\" can not be readable and writable");
        }

        [Fact(DisplayName = "Should get state from event")]
        public void ShouldGetStateFromEvent()
        {
            var @event = new FakeCelebrityWasCreatedEvent(Guid.NewGuid().ToString(), new { Foo = "Foo", Bar = "Bar" });

            var state = @event.Data.GetState<FooBar>();

            state.Foo.Should().Be("Foo");
            state.Bar.Should().Be("Bar");
        }
    }
}
