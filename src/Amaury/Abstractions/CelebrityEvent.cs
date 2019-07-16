using Amaury.Helpers;
using System;

namespace Amaury.Abstractions
{
    public abstract class CelebrityEvent<TEntity> : ICelebrityEvent<TEntity> where TEntity : class
    {
        protected CelebrityEvent(Guid aggregatedId, TEntity data)
        {
            AggregatedId = aggregatedId.ToString();
            Name = GetType().GetEventName();
            Timestamp = DateTime.Now;
            EventId = $"{Timestamp:yyyy-MM-ddTHH:mm:ss.fffffff}:{aggregatedId}";
            Data = data;
        }

        public string Name { get; }
        public string AggregatedId { get; }
        public string EventId { get; }
        public DateTime Timestamp { get; }
        public TEntity Data { get; }
    }
}
