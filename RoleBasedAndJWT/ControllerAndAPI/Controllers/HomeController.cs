using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAndJWT
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
