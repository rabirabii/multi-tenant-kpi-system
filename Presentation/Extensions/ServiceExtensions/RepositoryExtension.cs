using Core.Interface;
using Core.Interface.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class RepositoryExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<AuditableEntityInterceptor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
