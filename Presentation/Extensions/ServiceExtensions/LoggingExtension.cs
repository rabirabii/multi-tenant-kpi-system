using Serilog;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class LoggingExtension
    {
        public static void ConfigureLoggingInfrastructure(this 
            IHostBuilder host)
        {
            host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });
        }
    }
}


