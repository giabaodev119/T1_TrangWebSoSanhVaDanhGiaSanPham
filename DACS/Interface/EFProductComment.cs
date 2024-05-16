using DACS.DataAccess;
using DACS.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DACS.Interface
{
    public class EFProductComment : IProductComment
    {
        private readonly ApplicationDbContext _context;
        public EFProductComment(ApplicationDbContext context)
        {
            _context = context;
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


        public async Task UpdateAsync(ProductComment productComment)
        {
            _context.productComments.Update(productComment);
            await _context.SaveChangesAsync();
        }
    }
}
