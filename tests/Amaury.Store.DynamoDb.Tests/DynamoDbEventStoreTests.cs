using Amaury.Abstractions;
using Amaury.Abstractions.Persistence;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Test.Fixtures;
using Xunit;

namespace Amaury.Store.DynamoDb.Tests
{
    public class DynamoDbEventStoreTests : DynamoDbBaseTest
    {
        private readonly ICelebrityEventStore _eventStore;

        public DynamoDbEventStoreTests() => _eventStore = new DynamoDbEventStore(DynamoDb, Options);

        [Fact(DisplayName = "Deve salvar um evento mesmo quando nenhuma entidade tenha sido salva")]
        public async Task ShouldCommitAEventWHenAnyEntityNotBeSaved()
        {
            var expectedEvent = new FakeCelebrityWasCreatedEvent(Guid.NewGuid().ToString(), new { Foo = "Foo", Bar = "Bar" }); 
            await _eventStore.Commit(expectedEvent);

            await CheckIfItemExist(expectedEvent);
        }

        //[Fact(DisplayName = "Deve adicionar o novo evento ao registro já existente")]
        //public async Task ShouldAppendTheNewEventToExistingRecord()
        //{
        //    var expectedAggreagatedId = Guid.NewGuid().ToString();
        //    var fisrtEvent = new FakeCelebrityWasCreatedEvent(expectedAggreagatedId, new { Foo = "Foo", Bar = "Bar" });
        //    await _eventStore.Commit(fisrtEvent);

        //    var secondEvent = new FakeCelebrityWasCreatedEvent(expectedAggreagatedId, new { Foo = "Foo", Bar = "Bar" });
        //    await _eventStore.Commit(secondEvent);

        //    await CheckIfItemExist(secondEvent);
        //}

        //[Fact(DisplayName = "Deve retornar os eventos relacionados ao aggragated id")]
        //public async Task ShouldReturnEventsRelatedByAggregatedId()
        //{
        //    var expectedAggreagatedId = Guid.NewGuid().ToString();
        //    var fisrtEvent = new FakeCelebrityWasCreatedEvent(expectedAggreagatedId, new { Foo = "Foo", Bar = "Bar" });
        //    await _eventStore.Commit(fisrtEvent);

        //    var secondEvent = new FakeCelebrityWasCreatedEvent(expectedAggreagatedId, new { Foo = "Foo", Bar = "Bar" });
        //    await _eventStore.Commit(secondEvent);

        //    var result = await _eventStore.Get(fisrtEvent.AggregatedId);

        //    result.Should().BeAssignableTo<IReadOnlyCollection<ICelebrityEvent>>();
        //}
    }
}
