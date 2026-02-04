using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CustomerAccount> GetAccountsByCustomerId(int customerId)
        {
            var accounts = _unitOfWork.CustomerAccountRepository.GetAll().Where(x => x.CustomerId == customerId);
            return accounts;
        }

        public async Task<bool> AddCustomerAccount(int customerId, int accountId)
        {
            var customerAccount = new CustomerAccount {AccountId = accountId, CustomerId = customerId };
            _unitOfWork.CustomerAccountRepository.Add(customerAccount);
            var result = _unitOfWork.Complete();
            return result > 0;
        }

        //public CustomerAccount GetLastAdded() 
        //{
        //    var customerAccount = _unitOfWork.CustomerAccountRepository.GetAll().LastOrDefault();
        //    if(customerAccount == null)
        //        return new CustomerAccount();
        //    return customerAccount;
        //}

        public IEnumerable<CustomerAccount> GetAccountsWithDetailsByCustomerId(int customerId)
        {
            return _unitOfWork.CustomerAccountRepository.GetAccountsWithDetailsByCustomerId(customerId);
        }
    }
}
