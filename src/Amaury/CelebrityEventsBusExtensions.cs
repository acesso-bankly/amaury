using Amaury.Abstractions.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury
{
    public static class CelebrityEventsBusExtensions
    {
        public static void AddCelebrityBus<T>(this IServiceCollection services) where T : ICelebrityEventsBus => services.AddTransient(typeof(ICelebrityEventsBus), typeof(T));
    }
}
