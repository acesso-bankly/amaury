using System.Threading.Tasks;

namespace Amaury.Abstractions
{
    public interface IEventSourcedAggregate<out TEntity> where TEntity : class, new()
    {
        TEntity Reduce();
    }
}