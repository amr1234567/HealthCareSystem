using HSS.DataAccess.Contexts;
using HSS.DataAccess.Helpers;
using HSS.DataAccess.Interceptors;
using HSS.DataAccess.Repositories;
using HSS.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HSS.DataAccess
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("defaultAmr");
            services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<SoftDeleteInterceptor>();
            services.AddScoped<Helper>();
            services.AddSingleton<AccountServicesHelpers>();
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var softDeleteInterceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();
                options.UseSqlServer(connectionString).AddInterceptors(softDeleteInterceptor);
            });

            
            return services;
        }
    }
}
