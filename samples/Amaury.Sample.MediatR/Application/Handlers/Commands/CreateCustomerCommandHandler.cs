using System;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions.Bus;
using Amaury.Sample.MediatR.Domain.Commands;
using Amaury.Sample.MediatR.Domain.Contracts.Repositories;
using Amaury.Sample.MediatR.Domain.Entities;
using Amaury.Sample.MediatR.Domain.Events;
using Amaury.Sample.MediatR.Domain.ValueObjects;
using MediatR;

namespace Amaury.Sample.MediatR.Application.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICelebrityEventsBus<Customer> _eventsBus;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository,
                                            ICelebrityEventsBus<Customer> eventsBus)
        {
            _customerRepository = customerRepository;
            _eventsBus = eventsBus;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if(await CustomerAlreadyExistsAsync(request.Document))
            {
                throw new InvalidOperationException();
            }

            var name = new Name(request.FirstName, request.Surname);
            var phone = new Phone(request.DDI, request.Number);
            var address = new Address(request.ZipCode,
                    new Place(request.Street, request.Number, request.District,
                            new City(request.City, request.State, request.Country)));

            var customer = Customer.Create(request.Document, name, phone, address);

            await _customerRepository.Create(customer);
            await _eventsBus.RaiseEvent(new CustomerWasCreated(customer.Id, customer));

            return Unit.Value;
        }

        private async Task<bool> CustomerAlreadyExistsAsync(string document) => await _customerRepository.CustomerAlreadyExists(document);
    }
}
