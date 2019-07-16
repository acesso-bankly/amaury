using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amaury.Sample.MediatR.Domain.Contracts.Repositories;
using Amaury.Sample.MediatR.Domain.Entities;
using Amaury.Sample.MediatR.Infrastructure.Repositories.DataObjects;

namespace Amaury.Sample.MediatR.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public static IList<CustomerData> Customers => new List<CustomerData>();

        public async Task<bool> CustomerAlreadyExists(string document)
            => await Task.Run(() => Customers.Any(c => c.Document == document));

        public async Task Create(Customer customer)
            => await Task.Run(() => Customers.Add(new CustomerData() { Document = customer.Document }));
    }
}
