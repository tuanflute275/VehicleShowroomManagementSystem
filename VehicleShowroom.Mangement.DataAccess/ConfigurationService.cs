using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleShowroom.Mangement.DataAccess.DataAccess;

namespace VehicleShowroom.Mangement.DataAccess
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddSqlServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}
