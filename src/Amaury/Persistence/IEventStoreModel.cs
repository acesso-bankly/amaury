using System;

namespace Amaury.Persistence
{
    public interface IEventStoreModel
    {
        string AggregateId { get; set; }
        string Name { get; set; }
        long AggregateVersion { get; set; }
        DateTime Timestamp { get; set; }
        string Data { get; set; }
    }
}