using Hangfire;
using Serilog;

namespace Presentation.Extensions
{
    public static class AppBuildExtensions
    {
        public static void ConfigurationApplicationPipeline(this WebApplication app)
        {
            app.ConfigureExceptionHandler();
            app.ConfigureSecurityMiddleware();
            app.ConfigureSwaggerMiddleware();
            app.ConfigureHangfireDashboard();
            app.ConfigurationStandardMiddleware();
            app.ConfigurationRoutingAndEndpoints();
            
        }
        
        private static void ConfigureExceptionHandler(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();

        }

        private static void ConfigureSecurityMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowSpecificOrigin");
        }

        private static void ConfigureSwaggerMiddleware(this WebApplication app)
        {
            // Swagger only active in Development env (Best Practice Security)
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Kpi Remastered v1");
                });
            }
        }
        private static void ConfigurationStandardMiddleware(this WebApplication app)
        {
            app.UseSession(); 

            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void ConfigureHangfireDashboard(this WebApplication app)
        {

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "MsDigi Job Monitor",
                // we can add authorization filters here
                // Authorization = new [] { new MyHangfireAuthFilter() } 
            });
        }

        private static void ConfigurationRoutingAndEndpoints(this WebApplication app)
        {
            app.UseRouting();

            app.MapControllers(); // API Controllers

            // app.MapRazorPages(); 

            // Health Check Endpoint (Needed for Kubernetes/Docker)
            app.MapGet("/health", () => Results.Ok("System Healthy"));
        }
    }
}
