using System;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Persistence;
using Amaury.Store.DynamoDb.Configurations;
using Amaury.Store.DynamoDb.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Amaury.Store.DynamoDb
{
    public class DynamoDbSnapshotRepository<TEntity, TModel> : ISnapshotRepository<TEntity>
        where TEntity : CelebrityAggregateBase
        where TModel : DynamoDbSnapshotModel<TEntity>
    {
        private readonly IDynamoDBContext _context;
        private readonly DynamoDBOperationConfig _configuration;
        private readonly DynamoEventStoreOptions _options;
        private readonly IDynamoDnSnapshotFactory<TEntity, TModel> _factory;

        public DynamoDbSnapshotRepository(IAmazonDynamoDB client, DynamoEventStoreOptions options, IDynamoDnSnapshotFactory<TEntity, TModel> factory)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            _options = options ?? throw new ArgumentNullException(nameof(options));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

            _configuration = new DynamoDBOperationConfig
            {
                    OverrideTableName = options.EventStore,
                    Conversion = DynamoDBEntryConversion.V2,
                    IndexName = options.IndexName
            };

            _context = new DynamoDBContext(client);
        }


        public async Task SaveAsync(TEntity entity)
        {
            var model = _factory.ToModel(entity);
            await _context.SaveAsync(model, _configuration);
        }

        public async Task<TEntity> LoadAsync(string aggregateId)
        {
            var model = await _context.LoadAsync<TModel>(aggregateId, $"{_options.SnapshotPrefix}#{aggregateId}", _configuration);
            return _factory.ToEntity(model);
        }

        #region dispose

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        private void Dispose(bool disposing)
        {
            if (_disposed) { return; }

            if (disposing) { _context?.Dispose(); }

            _disposed = true;
        }

        ~DynamoDbSnapshotRepository() => Dispose(false);

        #endregion
    }
}