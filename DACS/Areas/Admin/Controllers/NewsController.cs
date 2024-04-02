using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly INews _news;
        public NewsController(INews news)
        {
            _news = news;
        }
        public async Task<IActionResult> Index()
        {
            var news = await _news.GetAllAsync();
            return View(news);
        }
    }
}
