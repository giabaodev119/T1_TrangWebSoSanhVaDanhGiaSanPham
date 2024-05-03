using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DACS.Areas.User.Controllers
{
    [Route("api/products/{productId}/comments")]
    [ApiController]
    public class ProductCommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductComment>>> GetCommentsForProduct(int productId)
        {
            var comments = await _context.productComments.Where(c => c.ProductId == productId).ToListAsync();
            return comments;
        }

        [HttpPost]
        public async Task<ActionResult<ProductComment>> PostCommentForProduct(int productId, ProductComment comment)
        {
            comment.ProductId = productId;
            _context.productComments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentsForProduct", new { productId = productId }, comment);
        }
    }
}


