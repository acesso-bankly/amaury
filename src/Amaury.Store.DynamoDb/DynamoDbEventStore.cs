using System;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Stores;
using Amaury.EventStore.DynamoDb;
using Amaury.Helpers;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;

namespace Amaury.Store.DynamoDb
{
    public class DynamoDbEventStore<TEntity> : ICelebrityEventStore<TEntity> where TEntity : class
    {
        private readonly DynamoDBContext _context;
        private readonly DynamoDBOperationConfig _dynamoDbOperationConfig;

        public DynamoDbEventStore(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDbOperationConfig = new DynamoDBOperationConfig()
            {
                    OverrideTableName = typeof(TEntity).GetEventStore()
            };
            _context = new DynamoDBContext(dynamoDb);
        }

        public async Task Commit<TEvent>(TEvent @event) where TEvent : ICelebrityEvent<TEntity>
        {
            var model = await _context.LoadAsync<EventStoreModel>(@event.AggregatedId, _dynamoDbOperationConfig) ?? new EventStoreModel();

            model.AggregatedId = @event.AggregatedId;
            model.Timestamp = @event.Timestamp.Ticks;
            model.LastAuthor = "John Doe";
            model.Events.Add(JsonConvert.SerializeObject(@event) );
            
            await _context.SaveAsync(model, _dynamoDbOperationConfig);
        }

        public Task<TEntity> Reduce(Guid aggredateId) => throw new NotImplementedException();
    }
}
