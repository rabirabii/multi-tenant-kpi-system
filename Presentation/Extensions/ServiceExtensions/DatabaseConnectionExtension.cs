using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class DatabaseConnectionExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("KpiRemastered");

            services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(connectionString));

            services.AddSingleton<DapperContext>();
        }
    }
}
