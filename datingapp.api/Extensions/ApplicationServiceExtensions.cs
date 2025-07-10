using datingapp.api.Interfaces;
using datingapp.api.Services;
using datingapp.data.Data;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            
            var connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<DataContext>(opts =>
                opts.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    mySqlOpts => mySqlOpts.MigrationsAssembly("datingapp.data")
                )
            );

            services.AddScoped<ITokenService, TokenService>(); // AddSingleton() best for cashing

            return services;
        }
    }
}