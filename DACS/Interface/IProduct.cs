using DACS.Models.EF;

namespace DACS.Interface
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task Delete(int id);
    }
}
