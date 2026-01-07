using System.Security.Cryptography.X509Certificates;

namespace Presentation.Extensions
{
    public static class ConfigurationHelper
    {
        public static void ValidateRequiredConfiguration(IConfiguration configuration)
        {
            var RequiredSections = new[]
            {
                   "ConnectionStrings:KpiRemastered",
                
                // Redis (if Enabled=true, Host must is a must)
                "Redis:Host",
                "Redis:Port",

                // Keycloak
                "Keycloak:Authority",
                "Keycloak:Audience",
                "Keycloak:ClientId",

                "EmailSettings:SmtpServer",
                "EmailSettings:Username",
                "EmailSettings:Password" // in Production , consider using secure storage for sensitive data like .env or Azure Key Vault
            };
            var missingSettings = new List<string>();
            foreach(var section in RequiredSections)
            {
                if(string.IsNullOrEmpty(configuration[section]) &&
                        !configuration.GetSection(section).Exists())
                {
                    missingSettings.Add(section);
                }
            }
            if (missingSettings.Count > 0)
            {
                throw new InvalidOperationException
                    ($"Missing required configuration sections or keys: {string.Join(", ", missingSettings)}");
            }

        }
        // Log deprecated configuration usage (Optional, We use Serilog)
        public static void LogConfigurationInfo (ILogger logger, IConfiguration configuration)
        {
            logger.LogWarning("Using deprecated configuration method. Please migrate to the new configuration system.");

            var aspnetEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            logger.LogInformation("ASPNETCORE_ENVIRONMENT Variable: {AspNetEnvironment}", aspnetEnv ?? "Not Set");

            var configEnvironment = configuration.GetValue<string>("App:ENVIRONMENT");
            logger.LogInformation("Config Environment Setting: {ConfigEnvironment}", configEnvironment);

        }

    }
}
