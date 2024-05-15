using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DACS.Controllers
{
    public class NewsController : Controller
    {
		private readonly INews _news;
        public NewsController(INews news)
        {
            _news = news;
        }
        public async Task<IActionResult> Index(string Searchtext, int? page)
        {
            var news = await _news.GetAllAsync();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNum = page ?? 1;
            if (!string.IsNullOrEmpty(Searchtext))
            {
                news = news.Where(news => news.Alias.Contains(Searchtext) || news.Title.Contains(Searchtext)).ToList();
            }
            return View(news.ToPagedList(pageNum, pageSize));
        }
        public async Task<IActionResult> Display(int id)
        {
            var news = await _news.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }
    }
}
