using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly ApplicationDbContext _context;

        public HomeController(IProduct product, IProductCategory productcategory, IProductComment productComment, ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _product = product;
            _productcategory = productcategory;
            _productcomment = productComment;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string Searchtext)
        {
            var news = _context.News.Take(3).ToList();
            ViewBag.News = news;
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
