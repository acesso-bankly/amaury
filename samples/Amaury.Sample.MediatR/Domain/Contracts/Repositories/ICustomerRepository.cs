using System.Threading.Tasks;
using Amaury.Sample.MediatR.Domain.Entities;

namespace Amaury.Sample.MediatR.Domain.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> CustomerAlreadyExists(string document);
        Task Create(Customer customer);
    }
}
