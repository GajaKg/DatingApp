using datingapp.api.Data;
using datingapp.api.Interfaces;
using datingapp.api.Services;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("DefaultConnection")
                )
            );
            
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}