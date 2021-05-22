using System.Threading.Tasks;

namespace Amaury.Persistence.Configuration
{
    public interface ICelebrityEventStoreConfiguration
    {
        Task ConfigureAsync();

        Task<bool> TableExist(string tableName);
    }
}