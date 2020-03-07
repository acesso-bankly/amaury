using System.Threading.Tasks;

namespace Amaury.V2.Persistence
{
    public interface ICelebrityEventStoreConfiguration
    {
        Task ConfigureAsync();

        Task<bool> TableExist(string tableName);
    }
}