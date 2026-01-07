using Application.Settings;
using Serilog;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class RedisAndSessionExtension
    {
        private static readonly string RedisInstanceId = "KpiRemastered_";
        public static void RedisAndSessionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfig = configuration.GetSection("Redis").Get<RedisSettings>();

            if(redisConfig != null && redisConfig.Enabled)
            {
                string redisConnStr = $"{redisConfig.Host}:{redisConfig.Port},defaultDatabase={redisConfig.Database},abortConnect=false";

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnStr;
                    options.InstanceName = RedisInstanceId;
                });

                Log.Information("REDIS Configured pointing to {RedisHost}:{RedisPort}", redisConfig.Host, redisConfig.Port);
                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(60);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.Cookie.Name = ".KpiRemastered.Session";
                });
            } else
            {
                Log.Warning(" REDIS is Disabled. Falling back to In-Memory Cache.");
                // Fallback if Redis crash(use local Memory cache) 
                services.AddDistributedMemoryCache();
               services.AddSession();
            }
        }
    }
}
