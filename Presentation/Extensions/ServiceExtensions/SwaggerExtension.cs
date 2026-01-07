using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;


namespace Presentation.Extensions.ServiceExtensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection service)
        {
            service.AddOpenApi();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kpi Remastered",
                    Version = "v1",
                    Description = "API Documentation for .NET 10"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(document =>
                {
                    var schemeReference = new OpenApiSecuritySchemeReference("Bearer", document);

                    return new OpenApiSecurityRequirement
                    {
                        { schemeReference, new List<string>() }
                    };
                });
            });
        }
    }
}