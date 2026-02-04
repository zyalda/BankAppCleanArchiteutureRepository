using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface ICustomerAccountRepository : IGenericRepository<CustomerAccount>
    {
        IEnumerable<CustomerAccount> GetAccountsWithDetailsByCustomerId(int customerId);
    }
}
