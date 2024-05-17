using DACS.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Interface
{
    public interface IProductComment
    {
        Task<IEnumerable<ProductComment>> GetAllAsync();
        Task<ProductComment> GetByIdAsync(int id);

		Task<IEnumerable<ProductComment>> GetByProductIdAsync(int id);

		Task AddAsync(ProductComment productComment);
        Task UpdateAsync(ProductComment productComment);
        Task Delete(int id);
    }
}
