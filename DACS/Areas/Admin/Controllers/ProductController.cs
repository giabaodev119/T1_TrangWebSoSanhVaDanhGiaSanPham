using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        
        private readonly IProduct _product;
        private readonly IProductCategory _productcategory;
        public ProductController(IProduct product, IProductCategory productcategory)
        {
            _product = product;
            _productcategory = productcategory;
        }
        public async  Task<IActionResult> Index(string Searchtext, int? page)
        {
            var product = await _product.GetAllAsync();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNum = page ?? 1;
            if (!string.IsNullOrEmpty(Searchtext))
            {
                product = product.Where(product => product.Alias.Contains(Searchtext) || product.Name.Contains(Searchtext)).ToList();
            }
            var category = await _productcategory.GetAllAsync();
            ViewBag.ProductCategory = new SelectList(category, "Id", "Name");
            return View(product.ToPagedList(pageNum, pageSize));
        }
        public async Task<IActionResult> Add()
        {
            var category = await _productcategory.GetAllAsync();
            ViewBag.ProductCategory = new SelectList(category, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    product.ImageUrl = await SaveImage(imageUrl);
                }
                product.CreateDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                product.Alias = Models.Common.Filter.FilterChar(product.Name);
                await _product.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var products = await _product.GetAllAsync();
            var category = await _productcategory.GetAllAsync();
            ViewBag.ProductCategory = new SelectList(category, "Id", "Name");
            return View(product);
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
            var product = await _product.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var products = await _product.GetAllAsync();
            var category = await _productcategory.GetAllAsync();
            ViewBag.ProductCategory = new SelectList(category, "Id", "Name");
            return View(product);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                product.CreateDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                product.Alias = Models.Common.Filter.FilterChar(product.Name);
                await _product.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var category = await _productcategory.GetAllAsync();
            ViewBag.ProductCategory = new SelectList(category, "Id", "Name");
            return View(product);
        }

        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _product.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _product.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
