using System.Threading.Tasks;

namespace Amaury.V2.Persistence.Configuration
{
    public interface IDatabaseContextConfiguration
    {
        Task ConfigureAsync();

        Task<bool> TableExist(string tableName);
    }
}