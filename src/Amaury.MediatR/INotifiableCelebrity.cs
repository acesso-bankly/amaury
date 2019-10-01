using System.Collections.Generic;
using Amaury.Abstractions;
using Newtonsoft.Json;

namespace Amaury.MediatR
{
    public interface INotifiableCelebrity<out TEntity> : IAggregated where TEntity : class, new()
    {
        [JsonIgnore] Queue<INotifiableCelebrityEvent> PendingEvents { get; }
    }
}
