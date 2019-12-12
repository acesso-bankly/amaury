using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Persistence;
using Amaury.Store.DynamoDb.Configurations;
using Amaury.Store.DynamoDb.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using Polly;

namespace Amaury.Store.DynamoDb
{
    public class DynamoDbEventStore : ICelebrityEventStore
    {
        private readonly DynamoDBContext _context;
        private readonly DynamoDBOperationConfig _configuration;

        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public DynamoDbEventStore(IAmazonDynamoDB dynamoDb, EventStoreOptions options)
        {
            _context = new DynamoDBContext(dynamoDb);
            _configuration = new DynamoDBOperationConfig()
            {
                OverrideTableName = options.StoreName,
                Conversion = DynamoDBEntryConversion.V2
            };
        }

        public async Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent
        {
            await Policy.Handle<ConditionalCheckFailedException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                    .ExecuteAsync(async () =>
                    {
                        var model = await _context.LoadAsync<EventStoreModel>(@event.AggregatedId, _configuration)
                                    ?? new EventStoreModel();

                        model.AggregatedId = @event.AggregatedId;
                        model.Timestamp = @event.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
                        model.Events.Add(JsonConvert.SerializeObject(@event, _jsonSerializerSettings));

                        await _context.SaveAsync(model, _configuration);
                    });
        }

        public async Task<IReadOnlyCollection<ICelebrityEvent>> GetEvents(string aggregatedId)
        {
            var eventStoreModel = await _context.LoadAsync<EventStoreModel>(aggregatedId, _configuration);
            if(eventStoreModel is null)
                return new List<ICelebrityEvent>();

            return eventStoreModel.Events.Select(JsonConvert.DeserializeObject<EventModel>).Cast<ICelebrityEvent>().ToList();
        }

    }
}