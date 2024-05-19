using DACS.Models.EF;

namespace DACS.Interface
{
    public interface INews
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News> GetByIdAsync(int id);
        Task AddAsync(News news);
        Task UpdateAsync(News news);
        Task Delete(int id);
        Task<IEnumerable<News>> GetWithIsActiveAsync();
    }
}
