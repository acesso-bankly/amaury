using System;
using System.Collections.Generic;
using Amaury.V2.Abstractions;

namespace Amaury.V2.Persistence
{
    public interface ICelebrityEventFactory
    {
        CelebrityEventBase GetEvent(string name, string json);
    }
}
