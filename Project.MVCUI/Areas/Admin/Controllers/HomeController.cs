using Microsoft.AspNetCore.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult ListAdmin()
        {
            return View();
        }
    }
}
