using Amaury.Abstractions;

namespace Amaury.Persistence
{
    public interface ICelebrityEventFactory<in TData>
    {
        CelebrityEventBase GetEvent(string name, TData data);
    }
}
