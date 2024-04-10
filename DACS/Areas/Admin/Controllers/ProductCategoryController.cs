using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {

        private readonly IProductCategory _productcategory;
        public ProductCategoryController(IProductCategory productcategory)
        {
            _productcategory = productcategory;
        }
        public async Task<IActionResult> Add()
        {
            var productcategory = await _productcategory.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductCategory productcategory)
        {

            if (ModelState.IsValid)
            {
                productcategory.CreateDate = DateTime.Now;
                productcategory.ModifiedDate = DateTime.Now;
                await _productcategory.AddAsync(productcategory);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var productcategories = await _productcategory.GetAllAsync();
            return View(productcategory);
        }
        public async Task<IActionResult> Update(int id)
        {
            var productcategory = await _productcategory.GetByIdAsync(id);
            if (productcategory == null)
            {
                return NotFound();
            }
            var productcategories = await _productcategory.GetAllAsync();

            return View(productcategory);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductCategory productcategory)
        {
            if (id != productcategory.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                productcategory.CreateDate = DateTime.Now;
                productcategory.ModifiedDate = DateTime.Now;
                await _productcategory.UpdateAsync(productcategory);
                return RedirectToAction(nameof(Index));
            }
            return View(productcategory);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var productcategory = await _productcategory.GetByIdAsync(id);
            if (productcategory == null)
            {
                return NotFound();
            }
            return View(productcategory);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productcategory.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var productcategory = await _productcategory.GetAllAsync();
            return View(productcategory);
        }
    }
}
