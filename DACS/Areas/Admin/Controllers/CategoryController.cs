using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                await _category.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _category.GetAllAsync();
            return View(category);
        }
        public async Task<IActionResult> Index()
        {
            var category = await _category.GetAllAsync();
            return View(category);
        }

        

    }
}
