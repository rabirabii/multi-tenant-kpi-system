using Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class AuthExtension
    {
        public static void AuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var keycloakSettings = configuration.GetSection("keycloak").Get<KeycloakSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = keycloakSettings.Authority;
                    options.Audience = keycloakSettings.Audience;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = keycloakSettings.Authority,
                        ValidateAudience = true,
                        ValidAudience = keycloakSettings.Audience,
                        ValidateLifetime = true,
                      
                        NameClaimType = "preferred_username"
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            // Log or handle authentication failures if needed
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
