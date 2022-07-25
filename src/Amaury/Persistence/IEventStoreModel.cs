using System;

namespace Amaury.Persistence
{
    public interface IEventStoreModel<TData>
    {
        string AggregateId { get; set; }
        long AggregateVersion { get; set; }
        string Name { get; set; }
        DateTime Timestamp { get; set; }
        TData Data { get; set; }
    }
}