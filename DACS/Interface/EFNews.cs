using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace DACS.Interface
{
    public class EFNews : INews
    {
        private readonly ApplicationDbContext _context;
        public EFNews(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task UpdateAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}