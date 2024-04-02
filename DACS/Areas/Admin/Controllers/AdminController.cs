using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateAdminAccount()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var user = new IdentityUser
            {
                UserName = "doantogiabao@gmail.com",
                Email = "doantogiabao@gmail.com"
            };
            var rusult = await _userManager.CreateAsync(user, "Hutech@123");
            if (rusult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return Content("Adimin account created successfully!");
            }
            return BadRequest("Failed to create admin account");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
