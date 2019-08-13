using System.Threading.Tasks;

namespace Amaury.Abstractions.Persistence
{
    public interface IEventStoreConfiguration
    {
        Task ConfigureAsync();

        Task<bool> TableExist(string tableName);
    }
}