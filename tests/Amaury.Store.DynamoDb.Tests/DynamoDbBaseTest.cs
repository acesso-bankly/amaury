using System;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Store.DynamoDb.Configurations;
using Amaury.Store.DynamoDb.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FluentAssertions;
using Newtonsoft.Json;

namespace Amaury.Store.DynamoDb.Tests
{
    public class DynamoDbBaseTest : IDisposable
    {
        protected DynamoDbBaseTest() : this("root", "secret", "http://localhost:8000") { }

        private DynamoDbBaseTest(string accessKey, string secretKey, string serviceUrl)
        {
            var amazonDynamoDbConfig = new AmazonDynamoDBConfig { ServiceURL = serviceUrl };
            DynamoDb = new AmazonDynamoDBClient(accessKey, secretKey, amazonDynamoDbConfig);
            Options = new EventStoreOptions { StoreName = Guid.NewGuid().ToString() };
            var configurations = new EventStoreConfiguration(DynamoDb, Options);
            configurations.ConfigureAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            Context = new DynamoDBContext(DynamoDb);
        }

        protected IAmazonDynamoDB DynamoDb { get; }
        protected EventStoreOptions Options { get; }
        private DynamoDBContext Context { get; }

        protected async Task CheckIfItemExist(ICelebrityEvent @event)
        {
            var model = await Context.LoadAsync<EventStoreModel>(@event.AggregatedId, new DynamoDBOperationConfig { OverrideTableName = Options.StoreName });

            model.Should().NotBeNull();
            model.Events.Should().Contain(JsonConvert.SerializeObject(@event));
        }

        #region Dispose

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(disposed)
                return;

            if(disposing)
            {
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        ~DynamoDbBaseTest()
        {
            Dispose(false);
        }

        #endregion
    }
}
