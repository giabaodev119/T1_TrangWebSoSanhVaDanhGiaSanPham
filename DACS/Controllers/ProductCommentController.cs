using DACS.DataAccess;
using DACS.Interface;
using DACS.Models;
using DACS.Models.EF;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using X.PagedList;

namespace DACS.Controllers
{

    public class ProductCommentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProduct _product;
        private readonly IProductCategory _productcategory;
        private readonly IProductComment _productcomment;
        private readonly ILogger<HomeController> _logger;

        public ProductCommentsController(IProduct product, IProductCategory productcategory, IProductComment productComment, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _product = product;
            _productcategory = productcategory;
            _productcomment = productComment;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [Authorize]
        [HttpPost("/Comments/AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentViewModel cmt)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                try
                {
                    ProductComment pCmnt = new ProductComment
                    {
                        ProductId = cmt.ProductId,
                        Name = user.FullName,
                        Email = user.Email,
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
        [AllowAnonymous]
        [HttpGet("/Comments/GetComments")]
        public async Task<ActionResult> GetComments(int productId)
        {
            var comments = await _productcomment.GetByProductIdAsync(productId);

            
            if (comments == null)
            {
                return NotFound();
            }

            // Specify the correct partial view path
            return PartialView("~/Views/Shared/_Comment.cshtml", comments);
        }


    }
}


