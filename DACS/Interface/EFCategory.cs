using DACS.DataAccess;
using DACS.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace DACS.Interface
{
    public class EFCategory : ICategory
    {
        private readonly ApplicationDbContext _context;
        public EFCategory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateAsync(Category category)
        {
           _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
