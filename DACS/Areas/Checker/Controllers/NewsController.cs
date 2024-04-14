using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace DACS.Areas.Checker.Controllers
{
    [Area("Checker")]
    [Authorize(Roles = "Checker")]
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
            ViewData["CreateAt"] = news.CreateDate;
            return View(news);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, News news, IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != news.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var existingProduct = await _news.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                if (imageUrl == null)
                {
                    news.ImageUrl = existingProduct.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    news.ImageUrl = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingProduct.Title = news.Title;
                existingProduct.Detail = news.Detail;
                existingProduct.Description = news.Description;
                existingProduct.IsActive = news.IsActive;
                existingProduct.SeoDescription = news.SeoDescription;
                existingProduct.SeoKeywords = news.SeoKeywords;
                existingProduct.SeoTitle = news.SeoTitle;
                existingProduct.ModifiedDate = news.ModifiedDate;
                existingProduct.Alias = Models.Common.Filter.FilterChar(news.Title);
                existingProduct.ImageUrl = news.ImageUrl;

                await _news.UpdateAsync(existingProduct);

                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }
    }
}
