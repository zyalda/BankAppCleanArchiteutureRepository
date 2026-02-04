using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Repositories
{
    public class CustomerAccountRepository : GenericRepository<CustomerAccount>, ICustomerAccountRepository
    {
        public CustomerAccountRepository(BankAppDataContext context) : base(context)
        {
        }

        public IEnumerable<CustomerAccount> GetAccountsWithDetailsByCustomerId(int customerId)
        {
            var customerAccounts = _context.CustomerAccounts.Where(x => x.CustomerId == customerId).Include(x => x.Account).ThenInclude(x => x.AccountTypes).AsNoTracking();

            return customerAccounts;
        }
    }
}
