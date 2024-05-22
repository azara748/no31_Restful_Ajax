using AJ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AJ.Controllers
{
    public class APIController : Controller
    {
        private readonly MyDbContext _context;
        public IWebHostEnvironment _img;
        public APIController(MyDbContext context, IWebHostEnvironment img)
        {
            _context = context;
            _img = img;
        }
        public IActionResult Index1()
        {
            var a = _context.Addresses.Select(x => x.City).Distinct();
            return Json(a);
        }
        public IActionResult Index()
        {
            var a = _context.Addresses.Select(x => x.City).Distinct();
            return Json(a);
        }
        public IActionResult Index11(string id)
        {
            var a = _context.Addresses.Where(x => x.City == id).Select(x => x.SiteId).Distinct();
            return Json(a);
        }
        public IActionResult Index12(string id)
        {
            var a = _context.Addresses.Where(x => x.SiteId == id).Select(x => x.Road);
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
        public IActionResult Index4(Member a, IFormFile fil)
        {
            if (a.Email == null) a.Email = "無";
            if (a.Age == null) a.Age = 99;
            string s = "名字" + a.Name + "信箱" + a.Email + "年齡" + a.Age;
            if (fil != null)
            {
                string filePath = Path.Combine(_img.WebRootPath, "img", fil.FileName);
                //using (var v = new FileStream(filePath, FileMode.Create))
                //{
                //    fil.CopyTo(v);
                //}
                byte[] bt;
                fil.CopyTo(new FileStream(_img.WebRootPath + "/img/" + fil.FileName, FileMode.Create));
                using (var v = new MemoryStream())
                {
                    fil.CopyTo(v);
                    bt = v.ToArray();
                }
                a.FileData = bt;
                a.FileName = fil.FileName;
                s += "檔案名" + fil.FileName + "大小" + fil.Length + "byte 類型" + fil.ContentType;
            }
            else s += "無照片";
            _context.Members.Add(a);
            _context.SaveChanges();
            //return Content(fil.FileName + "," + fil.Length + "," + fil.ContentType, "text/plain", Encoding.UTF8);
            return Content(s, "text/plain", Encoding.UTF8);
            //https://localhost:7045/API/Index4/1?name=aaa&sai=100
        }
        public IActionResult Index5([FromBody] josn接收 j)
        {
            var a = j.categoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(x => x.CategoryId == j.categoryId);
            if (!string.IsNullOrEmpty(j.keyword))
                a = a.Where(x => x.SpotTitle.Contains(j.keyword) || x.SpotDescription.Contains(j.keyword));
            int 總數 = a.Count();
            int 每頁資料數 = j.pageSize??9;
            int 第幾頁 = j.page ?? 1;
            int 總頁數 = (int)Math.Ceiling((decimal)(總數 / 每頁資料數));
            a = a.Skip((int)((第幾頁 - 1) * 每頁資料數)).Take(每頁資料數);



            josn回傳 b = new josn回傳();
            b.TotalPages = 總頁數;
            b.TotalCount = 總數;
            b.SpotsResult=a.ToList();
            return Json(b);
        }
    }
}

