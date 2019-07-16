using MediatR;

namespace Amaury.Sample.MediatR.Domain.Commands
{
    public class CreateCustomerCommand : IRequest
    {
        public string Document { get;set; }
        public string FirstName { get;set; }
        public string Surname { get;set; }
        public string DDI { get;set; }
        public string PhoneNumber { get;set; }
        public string ZipCode { get;set; }
        public string Street { get;set; }
        public string Number { get;set; }
        public string District { get;set; }
        public string City { get;set; }
        public string State { get;set; }
        public string Country { get;set; }
    }
}
