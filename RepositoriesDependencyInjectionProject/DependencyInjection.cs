using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Interfaces;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.UnitOfWorks;
using System.Text;

namespace RepositoriesDependencyInjectionProject
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddAuthenticationJwtBearer(this IServiceCollection services)
        {
            //Add JWT Code here.
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               //Här säger vi hur vi skall jobba med JWT
               .AddJwtBearer(opt => {
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       //Issuer är vem (vilken server) som utfärdat en JWT token
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "http://localhost:51597",
                       ValidAudience = "http://localhost:51597",
                       ClockSkew = TimeSpan.FromSeconds(300),
                       IssuerSigningKey =
                  new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mykey1234567&%%485734579453%&//1255362"))
                   };
               });
            return services;
        }

    }
}
