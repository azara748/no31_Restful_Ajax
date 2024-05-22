using AJ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AJ.Controllers
{
    public class HWController : Controller
    {
        private readonly MyDbContext _context;
        public HWController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult Index2API(string id)
        {
            var a = _context.Members.FirstOrDefault(x=>x.Name==id);
            if (a==null) return Content("o", "text/plain");
            else return Content("x", "text/plain");
        }
    }
}
