using MyApp.Domain.Entities;

namespace MyApp.Application.ServiceInterfaces
{
    public interface IAccountService
    {
        void UpdateAccount(Account account);
        Account GetAccount(int id);

        Account Addccount(Account account);

    }
}
