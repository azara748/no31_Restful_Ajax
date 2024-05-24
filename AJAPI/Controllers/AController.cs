using AJAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AJAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AController : ControllerBase
    {
        private readonly MyDbContext _context;
        public AController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        //[Route("a")]
        public async Task<ActionResult<josn回傳>> getIndex5([FromBody] josn接收 j)
        {
            var a = j.categoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(x => x.CategoryId == j.categoryId);
            if (!string.IsNullOrEmpty(j.keyword))
                a = a.Where(x => x.SpotTitle.Contains(j.keyword) || x.SpotDescription.Contains(j.keyword));
            if (!string.IsNullOrEmpty(j.categories))
            { 
                int i = _context.Categories.FirstOrDefault(x => x.CategoryName == j.categories).CategoryId;
                a = a.Where(x => x.CategoryId==i);
            }
            switch (j.sortBy)
            {
                case "spotTitle":
                    a = j.sortType == "asc" ? a.OrderBy(x => x.SpotTitle) : a.OrderByDescending(x => x.SpotTitle);
                    break;
                default:
                    a = j.sortType == "asc" ? a.OrderBy(x => x.SpotId) : a.OrderByDescending(x => x.SpotId);
                    break;
            }
            int 總數 = a.Count();
            int 每頁資料數 = (int)j.pageSize;
            int 第幾頁 = (int)j.page;
            int 總頁數 = (int)Math.Ceiling((decimal)(總數 / 每頁資料數));
            a = a.Skip((int)((第幾頁 - 1) * 每頁資料數)).Take(每頁資料數);



            josn回傳 b = new josn回傳();
            b.TotalPages = 總頁數;
            b.TotalCount = 總數;
            b.Categories= await _context.Categories.ToListAsync();
            b.SpotsResult = await a.ToListAsync();
            //foreach (var x in b.SpotsResult)
            //{
            //    var w= await  _context.Categories.FirstOrDefaultAsync(y => y.CategoryId == x.CategoryId);
            //    b.Categories.Add(w.CategoryName);
            //}
            return b;
        }
    }
}
