using System;
using MediatR;

namespace Amaury.Abstractions
{
    public interface ICelebrityEvent : INotification
    {
        string AggregatedId { get; set; }
        string EventId { get; set; }
        string Name { get; set; }
        DateTime Timestamp { get; set; }
        object Data { get; set; }
    }
}
