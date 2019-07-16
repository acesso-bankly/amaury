using System;

namespace Amaury.Sample.MediatR.Domain.ValueObjects
{
    public class City : IEquatable<City>
    {
        public City(string name, string state, string country)
        {
            Name = name;
            State = state;
            Country = country;
        }

        public string Name { get; }
        public string State { get; }
        public string Country { get; }

        public override string ToString() => $"{Name} - {State}, {Country} ";

        public void Deconstruct(out string name, out string state, out string country)
        {
            name = Name;
            state = State;
            country = Country;
        }

        #region Equatable
        public bool Equals(City other) => !(other is null) && (ReferenceEquals(this, other)
                                                               || (string.Equals(Name, other.Name)
                                                               && string.Equals(State, other.State)
                                                               && string.Equals(Country, other.Country)));

        public override bool Equals(object obj)
            => !(obj is null) && (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((City)obj)));

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Country != null ? Country.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}
