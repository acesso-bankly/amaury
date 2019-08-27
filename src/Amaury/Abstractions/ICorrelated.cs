using System;

namespace Amaury.Abstractions
{
    public interface ICorrelated
    {
        Guid CorrelationId { get; set; }
    }
}