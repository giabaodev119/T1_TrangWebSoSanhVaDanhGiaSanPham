using DACS.DataAccess;
using DACS.Interface;
using DACS.Models.EF;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DACS.Interface
{
    public class EFPost : IPost
    {
        private readonly ApplicationDbContext _context;
        public EFPost(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            
            return await _context.Posts.ToArrayAsync();
        }
        public async Task<IEnumerable<Post>> GetWithIsActiveAsync()
        {
            return await _context.Posts.Where(p => p.IsActive).ToArrayAsync();
            
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
