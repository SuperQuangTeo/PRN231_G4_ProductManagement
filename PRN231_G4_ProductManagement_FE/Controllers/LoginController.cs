using Microsoft.AspNetCore.Mvc;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
