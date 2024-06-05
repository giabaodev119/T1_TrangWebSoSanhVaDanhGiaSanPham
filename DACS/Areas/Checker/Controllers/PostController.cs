using DACS.Interface;
using DACS.Models;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace DACS.Areas.Checker.Controllers
{
    [Area("Checker")]
    [Authorize(Roles = "Checker")]
    public class PostController : Controller
    {
        private readonly IPost _post;
        private readonly ICategory _category;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPost post, UserManager<ApplicationUser> userManager, ICategory category)
        {
            _userManager = userManager;
            _post = post;
            _category = category;
        }
        public async Task<IActionResult> Add()
        {
            var category = await _category.GetAllAsync();
            ViewBag.Category = new SelectList(category, "Id", "Title");
            return View();
        }
        public async Task<IActionResult> Index(string Searchtext, int? page)
        {
            var post = await _post.GetAllAsync();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 3;
            int pageNum = page ?? 1;
            if (!string.IsNullOrEmpty(Searchtext))
            {
                post = post.Where(post => post.Alias.Contains(Searchtext) || post.Title.Contains(Searchtext)).ToList();
            }
            return View(post.ToPagedList(pageNum, pageSize));
        }
        [HttpPost]
        public async Task<IActionResult> Add(Post post, IFormFile imageUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    post.ImageUrl = await SaveImage(imageUrl);
                }
                post.CreateDate = DateTime.Now;
                post.ModifiedDate = DateTime.Now;
                post.CreateBy = user.UserName;
                post.Alias = Models.Common.Filter.FilterChar(post.Title);
                await _post.AddAsync(post);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var category = await _category.GetAllAsync();
            ViewBag.Category = new SelectList(category, "Id", "Title");
            var posts = await _post.GetAllAsync();
            return View(post);
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
            var category = await _category.GetAllAsync();
            ViewBag.Category = new SelectList(category, "Id", "Title");
            var post = await _post.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var posts = await _post.GetAllAsync();
            ViewData["CreateAt"] = post.CreateDate;
            return View(post);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Post post, IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != post.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var existingPost = await _post.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                if (imageUrl == null)
                {
                    post.ImageUrl = existingPost.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    post.ImageUrl = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingPost.Title = post.Title;
                existingPost.Detail = post.Detail;
                existingPost.IsActive = post.IsActive;
                existingPost.ModifiedDate = post.ModifiedDate;
                existingPost.Alias = Models.Common.Filter.FilterChar(post.Title);
                existingPost.ImageUrl = post.ImageUrl;
                existingPost.CategoryId = post.CategoryId;
                await _post.UpdateAsync(existingPost);

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
