using MyApp.Application.ServiceInterfaces;

namespace MyApp.Application.ServicesInterfaces
{
    public interface IAuthenticateUserService
    {
        bool Login(int id, ICustomerService customerService, IUserTypeService userTypeService);

        UserManagerResponse GenerateToken();

    }
}
