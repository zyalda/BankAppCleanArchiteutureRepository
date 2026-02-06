using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.ServiceInterfaces;
using MyApp.Application.ServicesInterfaces;
using MyApp.Domain.StaticUserRoles;

namespace BankAppWeb.Controllers
{
    [Authorize(Roles = StaticUserRoles.Customer)]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ITransactionsServices _transactionsServices;
        private readonly ICustomerAccountService _customerAccountService;

        public CustomerController(ICustomerService customerService, ICustomerAccountService customerAccountService, IAccountService accountService, ITransactionsServices transactionsServices)
        {
            _customerService = customerService;
            _accountService = accountService;
            _transactionsServices = transactionsServices;
            _customerAccountService = customerAccountService;
        }

        [HttpPost]
        [Route("ListCustomerAccounts")]     
        public IActionResult ListCustomerAccounts(int customerId)
        {
            var accounts = _customerAccountService.GetAccountsWithDetailsByCustomerId(customerId);
            return Ok(accounts);
        }

        [HttpPost]
        [Route("ListAccountTransactions")]
        public IActionResult ListAccountTransactions(int accountId)
        {
            var accounts = _transactionsServices.GetTransactionsByAccountId(accountId);
            return Ok(accounts);
        }

        [HttpPost]
        [Route("AddAccountToThisCustomer")]
        public IActionResult AddAccountToThisCustomer(int customerId, string frequency,
                                                        int balance, int accountTypesId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            if (customer != null)
            {
                var newAccount = _accountService.Addccount(frequency, balance, accountTypesId);

                _customerAccountService.AddCustomerAccount(customerId, newAccount.AccountId);
                return Ok($"Account {newAccount.AccountId} has been created.");
            }
            else
            {
                return Ok("This customer already exist.");
            }
        }

        [HttpPost]
        [Route("TransactionBetweenTwoCustomers")]
        public IActionResult TransactionBetweenTwoCustomers(int accountIdSender, int accountIdReceiver, int amount)
        {
            var accountSender = _accountService.GetAccount(accountIdSender);
            var balanceSender = accountSender.Balance;

            var transactionResult = _transactionsServices.TransactionBetweenTwoAccounts(accountIdSender, accountIdReceiver, amount, accountSender, balanceSender, _accountService);

            return Ok(transactionResult);
        }
    }
}