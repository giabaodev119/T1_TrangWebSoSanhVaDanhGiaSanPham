using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DACS.Areas.User.Controllers
{
	[Area("User")]
	[Authorize(Roles = "User")]
	public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IProduct _product;
		private readonly IProductCategory _productcategory;
		private readonly IProductComment _productcomment;
		private readonly ILogger<HomeController> _logger;

		public ProductController(IProduct product, IProductCategory productcategory, IProductComment productComment, ILogger<HomeController> logger,ApplicationDbContext context)
		{
			_product = product;
			_productcategory = productcategory;
			_productcomment = productComment;
			_logger = logger;
			_context = context;
		}
		public async Task<IActionResult> Index(int? productcategoryId, int? page, string Searchtext)
        {

            var categories = _context.ProductCategory.ToList();
            ViewBag.ProductCategory = categories;


            var products = productcategoryId.HasValue ?

                _context.Products.Where(p => p.ProductCategoryId == productcategoryId.Value).ToList() :

                _context.Products.ToList();
            var product = await _product.GetAllAsync();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNum = page ?? 1;
			if (!string.IsNullOrEmpty(Searchtext))
			{
				products = products.Where(products => products.Name.ToUpper().Contains(Searchtext.ToUpper())).ToList();
			}
			return View(products.ToPagedList(pageNum, pageSize));

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
