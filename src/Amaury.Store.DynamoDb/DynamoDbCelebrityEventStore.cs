using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Extensions;
using Amaury.Persistence;
using Amaury.Store.DynamoDb.Configurations;
using Amaury.Store.DynamoDb.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Amaury.Store.DynamoDb
{
    public class DynamoDbCelebrityEventStore<TEntity> : ICelebrityEventStore<TEntity>
            where TEntity : CelebrityAggregateBase
    {
        private readonly IDynamoDBContext _context;
        private readonly ICelebrityEventFactory<string> _eventFactory;
        private readonly DynamoDBOperationConfig _configuration;
        private readonly DynamoEventStoreOptions _options;

        public DynamoDbCelebrityEventStore(IAmazonDynamoDB client, ICelebrityEventFactory<string> eventFactory, DynamoEventStoreOptions options)
        {
            if(client is null) throw new ArgumentNullException(nameof(client));
            _eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));

            _options = options;
            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = options.EventStore,
                Conversion = DynamoDBEntryConversion.V2,
                IndexName = options.IndexName
            };

            _context = new DynamoDBContext(client);
        }

        public async Task CommitAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var uncommittedEvents = entity.GetUncommittedEvents();
            await CommitEventsAsync(uncommittedEvents, cancellationToken);
            entity.ClearUncommittedEvents();
        }

        public async Task<TEntity> LoadAsync(string aggregateId, long? version = null, bool consistentRead = false, CancellationToken cancellationToken = default)
        {
            var committedEvents = await ReadEventsAsync(aggregateId, version, consistentRead, cancellationToken: cancellationToken);
            return committedEvents.TakeSnapshot<TEntity>();
        }

        private async Task CommitEventsAsync(IEnumerable<CelebrityEventBase> events, CancellationToken cancellationToken = default)
        {
            if(events is null) throw new ArgumentNullException(nameof(events));

            var celebrityEvents = events.ToList();
            if(celebrityEvents.Any() is false) return;

            var table = _context.GetTargetTable<DynamoDbEventModel>(_configuration);
            var writer = table.CreateBatchWrite();

            var eventModels = ParseToDynamoDbEventModels(celebrityEvents);
            foreach(var document in eventModels.Select(@event => _context.ToDocument(@event))) { writer.AddDocumentToPut(document); }

            await writer.ExecuteAsync(cancellationToken);
        }

        private async Task<IEnumerable<CelebrityEventBase>> ReadEventsAsync(string aggregateId, long? version = null, bool consistentRead = false, CancellationToken cancellationToken = default)
        {
            if(aggregateId is null) throw new ArgumentNullException(nameof(aggregateId));

            var table = _context.GetTargetTable<DynamoDbEventModel>(_configuration);

            var search = table.Query(new QueryOperationConfig
            {
                KeyExpression = new Expression
                {
                    ExpressionStatement = "#partition_key = :v_partition_key and #aggregate_version >= :v_aggregate_version",
                    ExpressionAttributeNames = new Dictionary<string, string>
                    {
                        { "#partition_key", "PartitionKey" },
                        { "#aggregate_version", "AggregateVersion" }
                    },
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                    {
                        { ":v_partition_key", aggregateId },
                        { ":v_aggregate_version", version ?? 1 }
                    },
                },
                IndexName = FindByPartitionKeyAndAggregateVersionIndex.INDEX_NAME,
                ConsistentRead = consistentRead
            });

            var events = new List<DynamoDbEventModel>();
            do
            {
                var items = await search.GetNextSetAsync(cancellationToken);

                if(items.Any()) events.AddRange(_context.FromDocuments<DynamoDbEventModel>(items));
            }
            while(search.IsDone is false);

            return ParseToCelebrityEvents(events);
        }

        private IEnumerable<CelebrityEventBase> ParseToCelebrityEvents(IEnumerable<DynamoDbEventModel> documents)
            => documents.Select(ParseToCelebrityEventBase);

        private CelebrityEventBase ParseToCelebrityEventBase(IEventStoreModel<string> document)
        {
            var @event = _eventFactory.GetEvent(document.Name, document.Data);

            @event.SetAggregateId(document.AggregateId);
            @event.SetAggregateVersion(document.AggregateVersion);
            @event.SetTimestamp(document.Timestamp);

            return @event;
        }

        private IEnumerable<DynamoDbEventModel> ParseToDynamoDbEventModels(IEnumerable<CelebrityEventBase> events)
            => events.Select(@event => new DynamoDbEventModel(@event, _options.EventPrefix));
    }
}
