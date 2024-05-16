using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DACS.Areas.User.Controllers
{

	[Area("User")]
	[Authorize(Roles = "User")]
	public class ProductCommentsController : Controller
    {
		private readonly IProduct _product;
		private readonly IProductCategory _productcategory;
		private readonly IProductComment _productcomment;
		private readonly ILogger<HomeController> _logger;

		public ProductCommentsController(IProduct product, IProductCategory productcategory, IProductComment productComment, ILogger<HomeController> logger)
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

		[HttpPost("/Comments/AddComment")]
		public async Task<IActionResult> AddComment([FromBody] CommentViewModel cmt)
		{
			if (ModelState.IsValid)
			{
				try
				{
					ProductComment pCmnt = new ProductComment
					{
						ProductId = cmt.ProductId,
						Name = cmt.Name,
						Email = cmt.Email,
						Content = cmt.Content,
						Rating = cmt.Rating, // Ensure Rating is set
						CreationDate = DateTime.Now,


					};

					await _productcomment.AddAsync(pCmnt);
					return Ok();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error adding comment");
					return StatusCode(500, "Internal server error: " + ex.GetBaseException().Message);
				}
			}
			_logger.LogWarning("Invalid model state: {@ModelState}", ModelState);
			return BadRequest(ModelState);
		}

		[HttpGet("/Comments/GetComments/{productId}")]
		public async Task<ActionResult> GetComments(int productId)
		{
			var comments = await _productcomment.GetByIdAsync(productId);

			if (comments == null)
			{
				return NotFound();
			}
			return PartialView("_CommentsPartial", comments);
		}
	}
}


