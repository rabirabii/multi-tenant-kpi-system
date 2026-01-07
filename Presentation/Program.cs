using Infrastructure.Context;
using Presentation.Extensions;
using Presentation.Extensions.ServiceExtensions;
using Serilog;

// Just for early logging during startup and later will be deleted
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Project Kpi Setup");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureLoggingInfrastructure();

    ConfigurationHelper.ValidateRequiredConfiguration(builder.Configuration);

    builder.ConfigureApplicationServices();

    var app = builder.Build();

    app.ConfigurationApplicationPipeline();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (db.Database.CanConnect())
            {
                Log.Information("DATABASE Connected Successfully!");
            }
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "DATABASE CONNECTION FAILED!");
        }
    }
    Log.Information("Startup Complete. Application Running");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly during startup!");
}
finally
{
    Log.CloseAndFlush();
}
