using System;
using System.Collections.Generic;
using System.Linq;
using Amaury.Attributes;
using Amaury.Sample.MediatR.Domain.Contracts.Entities;
using Amaury.Sample.MediatR.Domain.ValueObjects;

namespace Amaury.Sample.MediatR.Domain.Entities
{
    [EventStore("CustomerEventStore")]
    public class Customer : Entity
    {
        //TODO: Dá pra simplificar essa entidade
        private readonly IList<Phone> _phones;
        private readonly IList<Address> _addresses;

        private Customer(Guid guid, string document, Name name, Phone phone, Address address) : base(guid)
        {
            Document = document;
            Name = name;
            _phones = new List<Phone>();
            _addresses = new List<Address>();
            AddPhone(phone);
            AddAddress(address);
        }

        public string Document { get; }
        public Name Name { get; }
        
        public IReadOnlyList<Phone> Phones => _phones.ToList();
        public IReadOnlyList<Address> Addresses => _addresses.ToList();
        
        public static Customer Create(string document, Name name, Phone phone, Address address)
            => new Customer(Guid.NewGuid(), document, name, phone, address);

        public void AddPhone(Phone phone)
        {
            if(_phones.Any(p => p.Equals(phone)))
            {
                throw new InvalidOperationException();
            }

            _phones.Add(phone);
        }

        public void AddAddress(Address address)
        {
            if(_addresses.Any(p => p.Equals(address)))
            {
                throw new InvalidOperationException();
            }

            _addresses.Add(address);
        }
    }
}