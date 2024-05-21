using AJ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AJ.Controllers
{
    public class APIController : Controller
    {
        private readonly MyDbContext _context;
        public APIController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var a = _context.Addresses.Select(x => x.City).Distinct();
            return Json(a);
        }
        public IActionResult Index2()
        {
            Thread.Sleep(3000);
            return Content("世界", "text/plain", Encoding.UTF8);
            //return Content("<h1>世界</h1>", "text/html", Encoding.UTF8);
        }
        public IActionResult Index3(int id = 1)
        {
            //var a = _context.Members.SingleOrDefault();
            var a = _context.Members.Find(id);
            if (a != null) ;
            {
                byte[] bt = a.FileData;
                if (bt != null)
                {
                    return File(bt, "image/jpg");
                }
            }
            return NotFound();
        }
        public IActionResult Index4(int id,string name="xxx",int sai=5)
        {
            return Content("id:"+id+"名字"+name+"  "+sai+"歲", "text/plain", Encoding.UTF8);
            //https://localhost:7045/API/Index4/1?name=aaa&sai=100
        }
    }
}

