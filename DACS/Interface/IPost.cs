using DACS.Models.EF;

namespace DACS.Interface
{
    public interface IPost
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task Delete(int id);
        Task<IEnumerable<Post>> GetWithIsActiveAsync();
    }
}
