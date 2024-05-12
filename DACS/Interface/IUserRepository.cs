using System.Collections.Generic;
using System.Threading.Tasks;
using DACS.Models;

namespace DACS.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string userId);
    }
}
