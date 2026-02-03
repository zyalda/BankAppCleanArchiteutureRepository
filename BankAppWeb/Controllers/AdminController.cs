using Microsoft.AspNetCore.Mvc;


namespace BankAppWeb.Controllers
{
    //Bara inloggade admin användare kommer åt endpoints i denna controller
    //[Authorize(Roles = StaticUserRoles.Admin)]
    public class AdminController : ControllerBase
    {
        public AdminController()
        {
        }

        [HttpGet]
        //[Route("CheckAdminInLogged")]
        public IActionResult CheckAdminInLogged()
        {
            return Ok(); // $"Logged as {StaticUserRoles.Admin}");
        }
        }
}
