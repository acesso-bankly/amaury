using System;

namespace Amaury.V2.Persistence
{
    public interface ISnapshotModel
    {
        string AggregateId { get; set; }
        long AggregateVersion { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}