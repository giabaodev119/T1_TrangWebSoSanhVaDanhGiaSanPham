using DACS.Interface;
using DACS.Models.EF;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly IProduct _product;
        private readonly IProductCategory _productcategory;
        private readonly IProductComment _productcomment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProduct product, IProductCategory productcategory, IProductComment productComment, ILogger<HomeController> logger)
        {
            _product = product;
            _productcategory = productcategory;
            _productcomment = productComment;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string Searchtext)
        {
            var products = await _product.GetAllAsync();

            if (!string.IsNullOrEmpty(Searchtext))
            {
                products = products.Where(p => p.Name.ToUpper().Contains(Searchtext.ToUpper())).ToList();
            }
            return View(products);
        }

        public async Task<IActionResult> Display(int id)
        {
            var product = await _product.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
