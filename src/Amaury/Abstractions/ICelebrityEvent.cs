using System;
using MediatR;

namespace Amaury.Abstractions
{
    public interface ICelebrityEvent<out TEntity> : INotification where TEntity : class
    {
        string AggregatedId { get; }

        TEntity Data { get; }

        string EventId { get; }

        string Name { get; }

        DateTime Timestamp { get; }
    }
}
