using System;
using System.Linq;
using System.Threading.Tasks;
using Amaury.Store.DynamoDb.V2.Configurations;
using Amaury.V2.Abstractions;
using Amaury.V2.Persistence;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Amaury.Store.DynamoDb.V2
{
    public sealed class DynamoDbGenericSnapshotRepository<TEntity, TModel> : ISnapshotRepository<TEntity>, IDisposable
        where TEntity : CelebrityAggregateBase
        where TModel : ISnapshotModel
    {
        private readonly ISnapshotModelFactory<TEntity, TModel> _factoryModel;
        private readonly IDynamoDBContext _dbContext;
        private readonly DynamoDBOperationConfig _configuration;
        private readonly DynamoDbSnapshotRepositoryOptions _options;

        public DynamoDbGenericSnapshotRepository(IAmazonDynamoDB client, ISnapshotModelFactory<TEntity, TModel> factoryModel, DynamoDbSnapshotRepositoryOptions options)
        {
            if(client == null) { throw new ArgumentNullException(nameof(client)); }
            _factoryModel = factoryModel ?? throw new ArgumentNullException(nameof(factoryModel));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _dbContext = new DynamoDBContext(client);

            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = _options.TableName,
                Conversion = DynamoDBEntryConversion.V2
            };
        }

        public async Task SaveAsync(TEntity entity)
        {
            var model = _factoryModel.ToModel(entity);
            _configuration.ConsistentRead = true;
            await _dbContext.SaveAsync(model, _configuration);
        }

        public async Task<TEntity> GetAsync(string aggregateId)
        {
            _configuration.IndexName = _options.SnapshotIndex;
            _configuration.ConsistentRead = false;

            var search = _dbContext.QueryAsync<TModel>(aggregateId, _configuration);

            var items = await search.GetNextSetAsync();

            return _factoryModel.ToEntity(items.FirstOrDefault());
        }

        #region dispose pattern

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if(_disposed) { return; }

            if(disposing) { _dbContext?.Dispose(); }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DynamoDbGenericSnapshotRepository() => Dispose(false);

        #endregion
    }
}