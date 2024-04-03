using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Add()
        {
            var news = await _news.GetAllAsync();
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var news = await _news.GetAllAsync();
            return View(news);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(News news, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    news.Image = await SaveImage(imageUrl);
                }
                news.CreateDate = DateTime.Now;
                news.ModifiedDate = DateTime.Now;
                news.Alias = Models.Common.Filter.FilterChar(news.Title);
                await _news.AddAsync(news);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _news.GetAllAsync();
            return View(news);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }
    }
}
