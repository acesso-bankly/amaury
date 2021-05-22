using Amaury.Abstractions;

namespace Amaury.Persistence
{
    public interface ICelebrityEventFactory
    {
        CelebrityEventBase GetEvent(string name, string json);
    }
}
