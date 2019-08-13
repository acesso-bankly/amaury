namespace Amaury.Abstractions.Persistence
{
    public interface IEventStoreModel
    {
        string AggregatedId { get; set; }
        int? Version { get; set; }
    }
}