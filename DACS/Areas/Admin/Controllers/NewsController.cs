using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
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
        public async Task<IActionResult> Index(string Searchtext, int? page)
        {
            var news = await _news.GetAllAsync();
            if(page == null)
            {
                page = 1;
            }
            int pageSize = 3;
            int pageNum = page ?? 1;
            if (!string.IsNullOrEmpty(Searchtext)){
                news = news.Where(news => news.Alias.Contains(Searchtext) || news.Title.Contains(Searchtext)).ToList();
            }
            return View(news.ToPagedList(pageNum,pageSize));
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(News news, IFormFile imageUrl)
        { 
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    news.ImageUrl = await SaveImage(imageUrl);
                }
                news.CreateDate = DateTime.Now;
                news.ModifiedDate = DateTime.Now;
                news.Alias = Models.Common.Filter.FilterChar(news.Title);
                await _news.AddAsync(news);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var news1 = await _news.GetAllAsync();
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
        public async Task<IActionResult> Update(int id)
        {
            var news = await _news.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            var news1 = await _news.GetAllAsync();

            return View(news);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                news.CreateDate = DateTime.Now;
                news.ModifiedDate = DateTime.Now;
                news.Alias = Models.Common.Filter.FilterChar(news.Title);
                await _news.UpdateAsync(news);
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _news.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _news.Delete(id);
            return RedirectToAction(nameof(Index));
        }
     
    }
}
