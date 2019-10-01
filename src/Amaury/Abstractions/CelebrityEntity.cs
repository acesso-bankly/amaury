using System;
using System.Collections.Generic;

namespace Amaury.Abstractions
{
    [Obsolete("Use interface ICelebrity<TEntity>")]
    public class CelebrityEntity<TEntity> : ICelebrity<TEntity> where TEntity : class, new()
    {
        protected CelebrityEntity() => PendingEvents = new Queue<ICelebrityEvent>();

        public string Id { get; protected set; }
        public Queue<ICelebrityEvent> PendingEvents { get; }

    }
}