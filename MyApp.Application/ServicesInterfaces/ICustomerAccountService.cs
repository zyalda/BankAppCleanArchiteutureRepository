using MyApp.Domain.Entities;

namespace MyApp.Application.ServiceInterfaces
{
    public interface ICustomerAccountService
    {
        public IEnumerable<CustomerAccount> GetAccountsWithDetailsByCustomerId(int customerId);

        IEnumerable<CustomerAccount> GetAccountsByCustomerId(int customerId);

        Task<bool> AddCustomerAccount(int customerId, int accountId);

        //CustomerAccount GetLastAdded();
    }
}
