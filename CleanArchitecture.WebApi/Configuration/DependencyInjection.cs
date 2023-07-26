using System.Reflection;

namespace CleanArchitecture.WebApi.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostBuilder builder,
            params Assembly[] assemblies)
        {
            IEnumerable<IServiceInstaller> serviceInstallers =
                assemblies.SelectMany(s => s.DefinedTypes)
                .Where(IsAssignableToType<IServiceInstaller>)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>();

            foreach (IServiceInstaller serviceInstaller in serviceInstallers)
            {
                serviceInstaller.Install(services, configuration, builder);
            }

            return services;

            static bool IsAssignableToType<T>(TypeInfo typeInfo)
                => typeof(T).IsAssignableFrom(typeInfo)
                && !typeInfo.IsInterface && !typeInfo.IsAbstract;
        }
    }
}
