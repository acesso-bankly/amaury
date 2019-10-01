using System.Collections.Generic;
using Newtonsoft.Json;

namespace Amaury.Abstractions
{
    public interface ICelebrity<out TEntity> : IAggregated where TEntity : class, new()
    {
        [JsonIgnore] Queue<ICelebrityEvent> PendingEvents { get; }
    }
}