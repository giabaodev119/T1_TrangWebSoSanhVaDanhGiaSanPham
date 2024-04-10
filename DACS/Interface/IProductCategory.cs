using DACS.Models.EF;

namespace DACS.Interface
{
    public interface IProductCategory
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(int id);
        Task AddAsync(ProductCategory productcategory);
        Task UpdateAsync(ProductCategory productcategory);
        Task Delete(int id);
    }
}
