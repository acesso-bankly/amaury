using System;

namespace Amaury.Sample.MediatR.Domain.ValueObjects
{
    public class Place : IEquatable<Place>
    {
        public Place(string street, string number, string district, City city)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
        }

        public string Street { get; }
        public string Number { get; }
        public string District { get; }

        public City City { get; }

        public override string ToString() => $"{Street},{Number} - {District} - {City}";
        
        public void Deconstruct(out string street, out string number, out string district, out City city)
        {
            street = Street;
            number = Number;
            district = District;
            city = City;
        }

        #region Equatable
        public bool Equals(Place other)
            => !(other is null) && (ReferenceEquals(this, other) || (string.Equals(Street, other.Street)
                                                                 && string.Equals(Number, other.Number)
                                                                 && string.Equals(District, other.District)
                                                                 && string.Equals(City, other.City)));

        public override bool Equals(object obj)
            => !(obj is null) && (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((Place)obj)));

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Street != null ? Street.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Number != null ? Number.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (District != null ? District.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (City != null ? City.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}