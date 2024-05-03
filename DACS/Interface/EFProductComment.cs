using DACS.DataAccess;
using DACS.Models.EF;

namespace DACS.Interface
{
    public class EFProductComment : IProductComment
    {
        private readonly ApplicationDbContext _context;
        public EFProductComment(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            _context.ProductComment.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.ProductComment.FindAsync(id);
            _context.ProductComment.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.ProductComment.ToArrayAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.ProductComment.FindAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.ProductComment.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
