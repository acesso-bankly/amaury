using System;

namespace Amaury.Abstractions
{
    public interface ICelebrityEvent : IAggregated
    {
        string EventId { get; set; }
        string Name { get; set; }
        DateTime Timestamp { get; set; }
        dynamic Data { get; set; }
    }
}
