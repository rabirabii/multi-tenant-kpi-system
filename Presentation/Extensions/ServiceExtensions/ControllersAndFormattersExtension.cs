using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;

namespace Presentation.Extensions.ServiceExtensions
{
    public static class ControllersAndFormattersExtension
    {
        public static void ConfigureControllersAndFormatters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            })
            .AddXmlDataContractSerializerFormatters()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }
    }
}
