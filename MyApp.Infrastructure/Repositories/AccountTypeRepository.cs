using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Repositories
{
    public class AccountTypeRepository : GenericRepository<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(BankAppDataContext context) : base(context)
        {
        }       
    }
}
