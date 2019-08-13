using System;

namespace Amaury.Abstractions
{
    public interface ICelebrityEvent
    {
        string AggregatedId { get; set; }
        string EventId { get; set; }
        string Name { get; set; }
        DateTime Timestamp { get; set; }
        object Data { get; set; }
    }
}
