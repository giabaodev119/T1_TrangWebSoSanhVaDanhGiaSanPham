using System.Collections.Generic;
using System.Threading.Tasks;
using DACS.Models;

namespace   DACS.Repositories
{
    public interface ICheckerRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string userId); 
        Task UpdateAsync(ApplicationUser employee);
        Task DeleteAsync(string userId);
    }
}
