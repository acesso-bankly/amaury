using Amaury.Sample.MediatR.Domain.ValueObjects;

namespace Amaury.Sample.MediatR.Domain.Entities
{
    public class Address
    {
        public Address(string zipCode, Place place)
        {
            ZipCode = zipCode;
            Place = place;
        }
        
        public string ZipCode { get; }

        public Place Place { get; }
    }
}
