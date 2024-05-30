using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing.Imaging;
using X.PagedList;

namespace DACS.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IProduct _product;
		private readonly IProductComment _productComment;
		private readonly IProductCategory _productcategory;
		public ProductController(IProduct product, IProductCategory productcategory, ApplicationDbContext context,IProductComment cmt)
		{
			_product = product;
			_productcategory = productcategory;
			_context = context;
			_productComment = cmt;
		}
		public async Task<IActionResult> Index(int? productcategoryId, int? page ,string Searchtext)
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
			ViewBag.total = _productComment.TotalCommentCount(id);
			ViewBag.avgrating = _productComment.AvgComment(id);
			return View(product);
		}
    }
}
