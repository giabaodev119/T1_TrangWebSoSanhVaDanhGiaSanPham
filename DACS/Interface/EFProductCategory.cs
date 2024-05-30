using DACS.DataAccess;
using DACS.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace DACS.Interface
{
    public class EFProductCategory : IProductCategory
    {
        private readonly ApplicationDbContext _context;
        public EFProductCategory(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ProductCategory productcategory)
        {
            _context.ProductCategory.Add(productcategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var productcategory = await _context.ProductCategory.FindAsync(id);
            _context.ProductCategory.Remove(productcategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.ProductCategory.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _context.ProductCategory.FindAsync(id);
        }

        public async Task UpdateAsync(ProductCategory productcategory)
        {
            _context.ProductCategory.Update(productcategory);
            await _context.SaveChangesAsync();
        }

    }
}
