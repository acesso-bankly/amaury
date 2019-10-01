using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions.Persistence;
using Amaury.MediatR.Bus;
using Amaury.MediatR.Test.Fixtures;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Amaury.MediatR.Tests
{
    public class NotifiableCelebrityEventsBusTests
    {
        private readonly IMediator _mediator;
        private readonly ICelebrityEventStore _eventStore;
        private readonly INotifiableCelebrityEventsBus _eventsBus;

        public NotifiableCelebrityEventsBusTests()
        {
            _eventStore = Substitute.For<ICelebrityEventStore>();
            _mediator = Substitute.For<IMediator>();
            _eventsBus = new NotifiableCelebrityEventsBus(_eventStore, _mediator);
        }

        [Fact(DisplayName = "Dado que um novo evento seja lançado, então, deve commitar o evento no event store")]
        public async Task GivenThatANewEventBeRaisedThenShouldCommitTheEventIntoEventStore()
        {
            var expectedEvent = new FakeNotifiableCelebrityEvent(Guid.NewGuid().ToString(), new { Foo = "Foo", Bar = "Bar" });

            await _eventsBus.RaiseEvent(expectedEvent);

            await _eventStore.Received().Commit(Arg.Is<INotifiableCelebrityEvent>(e => e.Id == expectedEvent.Id));
        }


        [Fact(DisplayName = "Dado que um novo evento seja lançado, então, deve notificar outros manipuladores")]
        public async Task GivenThatANewEventBeRaisedThenShouldNotifyOtherHandlers()
        {
            var expectedEvent = new FakeNotifiableCelebrityEvent(Guid.NewGuid().ToString(), new { Foo = "Foo", Bar = "Bar" });

            await _eventsBus.RaiseEvent(expectedEvent);

            await _mediator.Received().Publish(Arg.Is<INotifiableCelebrityEvent>(e => e.Id == expectedEvent.Id));
        }

        [Fact(DisplayName = "Dado que exita uma lista de eventos, então, deve lançar todos os eventos da lista")]
        public async Task GivenThatExistaAListOfEventsThenShouldBeRaisedAllEventsFromList()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var events = new Queue<INotifiableCelebrityEvent>();
            events.Enqueue(new FakeNotifiableCelebrityEvent(expectedAggregatedId, new { Foo = "Foo", Bar = "Bar" }));
            events.Enqueue(new FakeNotifiableCelebrityEvent(expectedAggregatedId, new { Foo = "Foo", Bar = "Bar" }));

            await _eventsBus.RaiseEvents(events);

            await _eventStore.Received(events.Count).Commit(Arg.Is<INotifiableCelebrityEvent>(e => e.Id == expectedAggregatedId));
            await _mediator.Received(events.Count).Publish(Arg.Is<INotifiableCelebrityEvent>(e => e.Id == expectedAggregatedId));
        }

        [Fact(DisplayName = "Dado que exita uma lista de eventos, então, deve retornar uma lista de eventos para o aggregated id")]
        public async Task GivenThatExistaAListOfEventsThenShouldBeRaisedAllEventsFromLista()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var fakeEvents = new Queue<INotifiableCelebrityEvent>();
            fakeEvents.Enqueue(new FakeNotifiableCelebrityEvent(expectedAggregatedId, new { Foo = "Foo", Bar = "Bar" }));

            _eventStore.Get(Arg.Any<string>()).ReturnsForAnyArgs(fakeEvents);

            var events = await _eventStore.Get(expectedAggregatedId);

            events.Should().HaveCount(fakeEvents.Count);
        }
    }
}
