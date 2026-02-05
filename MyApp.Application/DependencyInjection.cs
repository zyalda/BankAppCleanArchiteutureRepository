using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.ServiceInterfaces;
using MyApp.Application.Services;
using MyApp.Application.ServicesInterfaces;

namespace MyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            //Row below is in case we add AutoMapper objects to DB entites.
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthenticateUserService, AuthenticateUserService>();
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<ITransactionsServices, TransactionsServices>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILoanService, LoanService>();
            return services;
        }
    }
}
