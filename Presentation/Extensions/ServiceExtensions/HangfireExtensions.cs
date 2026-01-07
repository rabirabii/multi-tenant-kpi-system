using Hangfire;
using Hangfire.PostgreSql;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class HangfireExtensions
    {
        public static void AddHangfireInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("KpiRemastered");

            services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>

                options.UseNpgsqlConnection(connectionString)));

            services.AddHangfireServer(options =>
            {
                options.WorkerCount = Environment.ProcessorCount * 2;
            });
        }

    }
}
