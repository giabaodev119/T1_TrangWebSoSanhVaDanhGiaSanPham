using DACS.Interface;
using DACS.Models;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace DACS.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private readonly IPost _post;
        private readonly ICategory _category;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPost post, UserManager<ApplicationUser>userManager, ICategory category)
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
            var post = await _post.GetWithIsActiveAsync();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 3;
            int pageNum = page ?? 1;
            if (!string.IsNullOrEmpty(Searchtext))
            {
                post = post.Where(post => post.Alias.ToUpper().Contains(Searchtext) || post.Title.ToUpper().Contains(Searchtext)).ToList();
            }
            return View(post.ToPagedList(pageNum, pageSize));
        }
        [HttpPost]
        public async Task<IActionResult> Add(Post post, IFormFile imageUrl)
        {
            var user=await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    post.ImageUrl = await SaveImage(imageUrl);
                }
                post.CreateBy = user.FullName;
                post.CreateDate = DateTime.Now;
                post.ModifiedDate = DateTime.Now;
                post.Alias = Models.Common.Filter.FilterChar(post.Title);
                await _post.AddAsync(post);
                EmailHelper EmailHelper = new EmailHelper();
                bool Email = EmailHelper.SendEmail(user.Email, "Cảm ơn bạn đã đăng bài. Bài viết của bạn sẽ được hiển thị sau khi chúng tôi kiểm duyệt xong! Thân chào");
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
                var existingProduct = await _post.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                if (imageUrl == null)
                {
                    post.ImageUrl = existingProduct.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    post.ImageUrl = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingProduct.Title = post.Title;
                existingProduct.Detail = post.Detail;
                existingProduct.IsActive = post.IsActive;
                existingProduct.ModifiedDate = post.ModifiedDate;
                existingProduct.Alias = Models.Common.Filter.FilterChar(post.Title);
                existingProduct.ImageUrl = post.ImageUrl;

                await _post.UpdateAsync(existingProduct);

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _post.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _post.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
