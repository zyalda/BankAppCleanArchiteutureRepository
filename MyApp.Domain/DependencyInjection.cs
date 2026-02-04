using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Entities;

namespace MyApp.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBankAppDataBaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BankAppDataContext>(options =>
               options.UseSqlServer(defaultConnectionString));
            return services;
        }
    }
}
