using System;

namespace Amaury.V2.Persistence
{
    public interface IEventStoreModel
    {
        string AggregateId { get; set; }
        string Name { get; set; }
        int? Version { get; set; }
        long AggregateVersion { get; set; }
        DateTime Created { get; set; }
        string Data { get; set; }
    }
}