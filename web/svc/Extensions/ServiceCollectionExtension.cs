using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using src.Services;

namespace src.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Assembly.GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces()
                                          .Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Impementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Impementation));

            return services;
        }
    }
}