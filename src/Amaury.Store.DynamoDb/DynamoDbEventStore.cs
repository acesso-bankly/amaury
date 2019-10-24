using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Persistence;
using Amaury.Store.DynamoDb.Configurations;
using Amaury.Store.DynamoDb.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;

namespace Amaury.Store.DynamoDb
{
    public class DynamoDbEventStore : ICelebrityEventStore
    {
        private readonly DynamoDBContext _context;
        private readonly DynamoDBOperationConfig _configuration;

        public DynamoDbEventStore(IAmazonDynamoDB dynamoDb, EventStoreOptions options)
        {
            _context = new DynamoDBContext(dynamoDb);
            _configuration = new DynamoDBOperationConfig(){
                OverrideTableName = options.StoreName,
                Conversion = DynamoDBEntryConversion.V2
            };
        }

        public async Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent
        {
            var model = await _context.LoadAsync<EventStoreModel>(@event.AggregatedId, _configuration) ?? new EventStoreModel();

            var config = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            model.AggregatedId = @event.AggregatedId;
            model.Timestamp = @event.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
            model.Events.Add(JsonConvert.SerializeObject(@event, config));

            await _context.SaveAsync(model, _configuration);
        }

        [Obsolete("Method Get is obsolete, use GetEvents instead")]
        public async Task<IReadOnlyCollection<ICelebrityEvent>> Get(string aggregatedId)
        {
            var model = await _context.LoadAsync<EventStoreModel>(aggregatedId, _configuration);
            if (model is null) return new List<ICelebrityEvent>();

            var events = new List<ICelebrityEvent>();

            foreach(var item in model.Events)
            {
                var @event = JsonConvert.DeserializeObject<EventModel>(item);
                events.Add(@event);
            }

            return events;
        }

        public async Task<IReadOnlyCollection<ICelebrityEvent>> GetEvents(string aggregatedId)
        {
            var eventStoreModel = await _context.LoadAsync<EventStoreModel>(aggregatedId, _configuration);
            if(eventStoreModel is null) return new List<ICelebrityEvent>();

            var events = new List<ICelebrityEvent>();

            foreach(var item in eventStoreModel.Events)
            {
                var @event = JsonConvert.DeserializeObject<EventModel>(item);
                events.Add(@event);
            }

            return events;
        }

    }
}