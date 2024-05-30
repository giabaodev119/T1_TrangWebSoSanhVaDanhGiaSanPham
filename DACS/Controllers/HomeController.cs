using DACS.DataAccess;
using DACS.Interface;
using DACS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DACS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _product;
        private readonly IProductCategory _productcategory;
        public HomeController(IProduct product, IProductCategory productcategory, ApplicationDbContext context)
        {
            _product = product;
            _productcategory = productcategory;
            _context = context;
        }
        public async Task<IActionResult> Index(string Searchtext)
        {
            var news = _context.News.Take(3).ToList();
            ViewBag.News = news;
            var products = await _product.GetAllAsync();

            if (!string.IsNullOrEmpty(Searchtext))
            {
                products = products.Where(products => products.Name.ToUpper().Contains(Searchtext.ToUpper())).ToList();
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
