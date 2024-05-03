using DACS.Models.EF;

namespace DACS.Interface
{
    public interface IProductComment
    {
        Task<IEnumerable<ProductComment>> GetAllAsync();
        Task<ProductComment> GetByIdAsync(int id);
        Task AddAsync(ProductComment productcomment);
        Task UpdateAsync(ProductComment productcomment);
        Task Delete(int id);
    }
}
