namespace Presentation.Extensions.ServiceExtensions
{
    public static class DependecyInjectionExtension
    {
        public static void ConfigureDependecyInjection(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            // services.AddScoped<IYourService, YourServiceImplementation>();
        }
    }
}
