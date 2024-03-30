using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {    
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        public async Task<IActionResult> Add()
        {
            var categories = await _category.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Category category)
        {
            
            if (ModelState.IsValid)
            {
                category.CreateDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;
                category.Alias = Models.Common.Filter.FilterChar(category.Title);
                await _category.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _category.GetAllAsync();
            return View(category);
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _category.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categories = await _category.GetAllAsync();

            return View(category);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _category.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _category.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _category.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var category = await _category.GetAllAsync();
            return View(category);
        }

        

    }
}
