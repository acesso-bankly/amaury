using System;

namespace Amaury.V2.Persistence
{
    public interface IEventStoreModel
    {
        string AggregateId { get; set; }
        long AggregateVersion { get; set; }
        string Name { get; set; }
        DateTime Timestamp { get; set; }
        string Data { get; set; }
    }
}