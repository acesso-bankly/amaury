using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Store.DynamoDb.V2.Models;
using Amaury.V2.Abstractions;
using Amaury.V2.Persistence;
using Amazon.DynamoDBv2.DataModel;

namespace Amaury.Store.DynamoDb.V2
{
    public class DynamoDbCelebrityEventStore : ICelebrityEventStore
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ICelebrityEventFactory _eventFactory;

        public DynamoDbCelebrityEventStore(IDynamoDBContext dbContext, ICelebrityEventFactory eventFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
        }

        public async Task CommitEventsAsync(IEnumerable<CelebrityEventBase> events, CancellationToken cancellationToken)
        {
            var batchWrite = _dbContext.CreateBatchWrite<DynamoDbEventModel>();
            var cardEvents = ParseCardEventData(events);
            batchWrite.AddPutItems(cardEvents);
            await batchWrite.ExecuteAsync(cancellationToken);
        }
    
        public async Task<IEnumerable<CelebrityEventBase>> ReadEventsAsync(string aggregateId, CancellationToken cancellationToken)
        {
            var query = _dbContext.QueryAsync<DynamoDbEventModel>(aggregateId);
            var eventModels = await query.GetNextSetAsync(cancellationToken); // TODO Ver para percorrer lista até o fim
            var listEvents = new List<CelebrityEventBase>();

            foreach(var eventModel in eventModels)
            {
                var @event = _eventFactory.GetEvent(eventModel.Name, eventModel.Data);

                //TODO rever formar para retirar este trecho de código
                @event.SetAggregateId(eventModel.AggregateId);
                @event.SetAggregateVersion(eventModel.AggregateVersion);
                @event.SetCreated(eventModel.Created);

                listEvents.Add(@event);
            }

            return listEvents;
        }

        private IEnumerable<DynamoDbEventModel> ParseCardEventData(IEnumerable<CelebrityEventBase> events)
        {
            foreach(var @event in events) yield return new DynamoDbEventModel(@event);
        }
    }
}