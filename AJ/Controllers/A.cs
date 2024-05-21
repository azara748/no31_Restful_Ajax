using Microsoft.AspNetCore.Mvc;

namespace AJ.Controllers
{
    public class A : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
