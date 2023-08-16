using Microsoft.AspNetCore.Mvc;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
