using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.StaticUserRoles;


namespace BankAppWeb.Controllers
{
    //Bara inloggade admin användare kommer åt endpoints i denna controller
    [Authorize(Roles = StaticUserRoles.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ILoanService _loanService;
        private readonly ICustomerAccountService _customerAccountService;

        public AdminController(ICustomerService customerService, ICustomerAccountService customerAccountService
            , ILoanService loanService, IAccountService accountService)
        {
            _customerService = customerService;
            _customerAccountService = customerAccountService;
            _loanService = loanService;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("CheckAdminInLogged")]
        public IActionResult CheckAdminInLogged()
        {
            return Ok($"Logged as {StaticUserRoles.Admin}");
        }

        [HttpPost]
        [Route("AddNewCustomer")]
        public IActionResult AddNewCustomer(string userType, string gender, string givenname, string surname,
                                            string streetaddress, string city, string zipcode, string country, string countryCode, string telephonecountrycode, string telephonenumber, string emailaddress)
        {
            string result = _customerService.AddNewCustomer(userType, gender, givenname, surname,
                                                    streetaddress, city, zipcode, country, countryCode, telephonecountrycode, telephonenumber, emailaddress);

            return Ok(result);
        }

        [HttpPost]
        [Route("AddAccountToTheNewCustomer")]
        public IActionResult AddAccountToTheNewCustomer(int accountTypesId, int customerId)
        {
            var newAccount = _accountService.AddAccount("Monthly", 0, accountTypesId);

            _customerAccountService.AddCustomerAccount(customerId, newAccount.AccountId);
            return Ok($"An account has been added to customer {customerId} with account id {newAccount.AccountId}");
        }


        [HttpPost]
        [Route("AddLoanToCustomer")]
        public IActionResult AddLoanToCustomer(int customerId, int accountId, int amount, int duration, int payments, string status)
        {
            var customer = _customerService.GetCustomerById(customerId);

            if (customer != null)
            {
                var loanAccount = _customerAccountService.GetAccountsByCustomerId(customerId).Where(x => x.AccountId == accountId).SingleOrDefault();

                var loan = new Loan
                {
                    AccountId = loanAccount.AccountId,
                    Status = status,
                    Amount = amount,
                    Duration = duration,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Payments = payments
                };
                _loanService.CreateLoan(loan);

                var account = _accountService.GetAccount(accountId);
                account.Balance += amount;
                _accountService.UpdateAccount(account);
                return Ok();
            }
            else
            {
                return Ok("This customer dose not exist.");
            }
        }
    }
}
