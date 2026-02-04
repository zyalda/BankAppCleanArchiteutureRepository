using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.ServiceInterfaces;
using MyApp.Application.ServicesInterfaces;

namespace BankAppWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserTypeService _userTypeService;
        private readonly IAuthenticateUserService _authenticateUserService;

        public LoginController(ICustomerService customerService, IAuthenticateUserService authenticateUserService, IUserTypeService userTypeService)
        {
            _customerService = customerService;
            _authenticateUserService = authenticateUserService;
            _userTypeService = userTypeService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(int userId)
        {
            if (_authenticateUserService.Login(userId, _customerService, _userTypeService))
            {
                return Ok(_authenticateUserService.GenerateToken());
            }
            else
            {
                //Invalid user.
                return Unauthorized("Invalid login");
            }
        }
    }
}
