using DACS.Interface;
using DACS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DACS.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IProduct _product;
        private readonly IProductCategory _productcategory;
        public HomeController(IProduct product, IProductCategory productcategory)
        {
            _product = product;
            _productcategory = productcategory;
        }
        public async Task<IActionResult> Index(string Searchtext)
        {

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
