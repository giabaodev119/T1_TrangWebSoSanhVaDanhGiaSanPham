using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DACS.DataAccess;
using DACS.Models;

namespace DACS.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;


        public EFUserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            // Query the list of users with the "Employee" role
            var employees = await _userManager.GetUsersInRoleAsync("User");
            return employees;
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            // Find a user by their ID
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            // Update a user
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            // Delete a user by their ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}