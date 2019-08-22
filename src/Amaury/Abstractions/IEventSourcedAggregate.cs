using System.Collections.Generic;
using Newtonsoft.Json;

namespace Amaury.Abstractions
{
    public interface IEventSourcedAggregate<out TEntity> : IAggregate where TEntity : class, new()
    {
        [JsonIgnore] Queue<ICelebrityEvent> PendingEvents { get; }
    }
}