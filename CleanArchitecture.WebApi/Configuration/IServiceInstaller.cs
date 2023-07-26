namespace CleanArchitecture.WebApi.Configuration
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder builder);
    }
}
