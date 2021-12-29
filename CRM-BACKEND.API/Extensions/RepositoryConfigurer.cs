using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DatabaseRepo;
using Repository.DatatableRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_BACKEND.API.Extensions
{
    public static class RepositoryConfigurer
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddScoped<DatabaseChecker>();

            services.AddScoped<DatatableChecker>();
        }
    }
}
