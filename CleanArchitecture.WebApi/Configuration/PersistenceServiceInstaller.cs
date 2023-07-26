using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.WebApi.Configuration
{
    public sealed class PersistenceServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder builder)
        {
            services.AddAutoMapper(typeof(CleanArchitecture.Persistence.AssemblyReference).Assembly);

            string connectionString = configuration.GetConnectionString("SqlServer");
            // Add services to the container.
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                connectionString: connectionString,
                tableName: "Logs",
                autoCreateSqlTable: true)
                .CreateLogger();

            builder.UseSerilog();
        }
    }
}
