using DACS.DataAccess;
using DACS.Helper;
using DACS.Interface;
using DACS.Models;
using DACS.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class CheckingCartController : Controller
    {
        private readonly IProduct _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly IProductComment _productcomment;
		public CheckingCartController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, IProduct productRepository, IProductComment cmt)
        {
            _context = context;
            _userManager = userManager;
            _productRepository = productRepository;
			_productcomment = cmt;
        }
        public async Task<IActionResult> AddToCart(int productId)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId 
            var product = await GetProductFromDatabase(productId);
            var cartItem = new CheckItem
            {
                ProductId = productId,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Detail = product.Detail,
                AddressAndPrice = product.AddressAndPrice,
                ProductCategoryId = product.ProductCategoryId,
                AvgRating = _productcomment.AvgComment(productId)

			};
            var cart = HttpContext.Session.GetObjectFromJson<CheckingCart>("Cart") ?? new CheckingCart();

            if (cart.totalcart() == 2)
            {
                TempData["Message"] = "Không được thêm quá 2 sản phẩm";
                return RedirectToAction("Index", "Product");
            }
            else if (cart.condition(cartItem) == false)
            {
                TempData["Message"] = "Không được thêm 2 sản phẩm khác loại";
                return RedirectToAction("Index", "Product");
            }
            
            cart.AddItem(cartItem);

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index", "Product");
        }
        public IActionResult Index(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CheckingCart>("Cart") ?? new CheckingCart();
			return View(cart);
        }
        // Các actions khác... 
        private async Task<Product> GetProductFromDatabase(int productId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm 
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CheckingCart>("Cart");

            if (cart is not null)
            {
                cart.RemoveItem(productId);

                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục 
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        } 
    }

}
