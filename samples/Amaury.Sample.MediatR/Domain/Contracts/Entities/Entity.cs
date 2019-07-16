using System;

namespace Amaury.Sample.MediatR.Domain.Contracts.Entities
{
    public class Entity
    {
        public Entity(Guid id) => Id = id;

        public Guid Id { get; }
    }
}
