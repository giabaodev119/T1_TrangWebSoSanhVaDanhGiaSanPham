using DACS.DataAccess;
using DACS.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DACS.Interface
{
    public class EFProductComment : IProductComment
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _product;
        public EFProductComment(ApplicationDbContext context, IProduct product)
        {
            _context = context;
            _product = product;
        }
        public async Task AddAsync(ProductComment productComment)
        {
            _context.productComments.Add(productComment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var productComment = await _context.productComments.FindAsync(id);
            _context.productComments.Remove(productComment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductComment>> GetAllAsync()
        {
            return await _context.productComments.ToListAsync();
        }

        public async Task<ProductComment> GetByIdAsync(int id)
        {
            return await _context.productComments.FindAsync(id);
        }

		public async Task<IEnumerable<ProductComment>> GetByProductIdAsync(int id)
		{
            var tmp =  _context.productComments.Where(x => x.ProductId == id).ToList();
            return  tmp;
		}


		public async Task UpdateAsync(ProductComment productComment)
        {
            _context.productComments.Update(productComment);
            await _context.SaveChangesAsync();
        }
		public int TotalCommentCount(int productId)
		{
			return _context.productComments.Count(x => x.ProductId == productId);
		}
        public double AvgComment(int productId)
        {
            var count = _context.productComments.Count(x => x.ProductId == productId);
            var tmp = _context.productComments.Where(x => x.ProductId == productId).ToList();
            if (count != 0)
            {
                int total = 0;
                foreach (var item in tmp)
                {
                    total += item.Rating;

                }
                var avgrating = (double)total / count;
                return Math.Round(avgrating, 1);
            }
            return  0;
		}

	}
}
