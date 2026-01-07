using Presentation.Extensions.ServiceExtensions;
using System.Runtime.CompilerServices;

namespace Presentation.Extensions
{
    public static class ServiceInjectionExtension
    {
        public static void ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
        builder.Services.ConfigureDatabase(builder.Configuration);
            builder.Services.RedisAndSessionInfrastructure(builder.Configuration);
            builder.Services.AuthInfrastructure(builder.Configuration);
            builder.Services.AddHangfireInfrastructure(builder.Configuration);
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureDependecyInjection();
            builder.Services.ConfigureControllersAndFormatters();
        }
    }
}
