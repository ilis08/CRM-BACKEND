using LoggerService;
using Microsoft.OpenApi.Models;

namespace CRM_BACKEND.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CRM_BACKEND.API",
                    Version = "v1"
                });
            });
        }
    }
}
