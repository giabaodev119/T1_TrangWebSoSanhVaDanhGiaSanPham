using DACS.Models.EF;

namespace DACS.Interface
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task Delete(int id);
    }
}
