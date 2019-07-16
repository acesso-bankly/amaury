using System;

namespace Amaury.Sample.MediatR.Domain.ValueObjects
{
    public class Name : IEquatable<Name>
    {
        public Name(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

        public string FirstName { get; }

        public string Surname { get; }

        public override string ToString() => $"{FirstName} {Surname}";

        public void Deconstruct(out string firstName, out string surname)
        {
            firstName = FirstName;
            surname = Surname;
        }

        #region Equatable
        public bool Equals(Name other)
                => !(other is null) && (ReferenceEquals(this, other) || (string.Equals(FirstName, other.FirstName) && string.Equals(Surname, other.Surname)));

        public override bool Equals(object obj)
            => !(obj is null) && (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((Name)obj)));

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FirstName != null ? FirstName.GetHashCode() : 0) * 397) ^ (Surname != null ? Surname.GetHashCode() : 0);
            }
        }
        #endregion
    }
}