namespace Presentation.Extensions.ServiceExtensions
{
    public static class DependecyInjectionExtension
    {
        public static void ConfigureDependecyInjection(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        }
    }
}
