using System.Collections.Generic;
using Newtonsoft.Json;

namespace Amaury.Abstractions
{
    public interface ICelebrity<out TEntity> where TEntity : class, new()
    {
        string Id { get; }
        [JsonIgnore] Queue<ICelebrityEvent> PendingEvents { get; }
    }
}