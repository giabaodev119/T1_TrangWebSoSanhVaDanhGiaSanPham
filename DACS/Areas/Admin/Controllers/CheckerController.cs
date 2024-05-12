using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DACS.DataAccess;
using DACS.Models;
using DACS.Interface;
using DACS.Repositories;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
    public class CheckerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICheckerRepository _checkerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckerController(ICheckerRepository checkerRepository, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _checkerRepository = checkerRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString)
        {       
            var NhanVien = await _userManager.GetUsersInRoleAsync("Employee");//chọn role Employee
            var nhanvienId = NhanVien.Select(u => u.Id); 
            var all_Nhanvien = from s in _context.User
                               where nhanvienId.Contains(s.Id) 
                               select s;

            if (!String.IsNullOrEmpty(searchString))//Input có thông tin thì xuất ra
            {
                string lowercaseSearchString = searchString.ToLower();
                all_Nhanvien = all_Nhanvien.Where(s => s.FullName.ToLower().Contains(lowercaseSearchString)
                                                        ||s.Email.ToLower().Contains(lowercaseSearchString)
                                                        || s.UserName.ToLower().Contains(lowercaseSearchString));
                                                        
            }               
            return View(await all_Nhanvien.ToListAsync());
        }

        [HttpPost]
        public async Task<JsonResult> GetSearchEMValue(string search)
        {
            var NhanVien = await _userManager.GetUsersInRoleAsync("Employee");
            var NVResult = NhanVien.Where(x => x.FullName.ToLower().Contains(search))
                                        .Select(x => new {
                                            label = x.FullName.ToLower(),
                                            value = x.FullName.ToLower()
                                        }).ToList();
            return Json(NVResult);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var employee = await _checkerRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

   
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingEmployee = await _checkerRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync

                existingEmployee.FullName = employee.FullName;
                existingEmployee.UserName = employee.UserName;
                existingEmployee.Email = employee.Email;

                await _checkerRepository.UpdateAsync(existingEmployee);

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var user = await _checkerRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Deletes an employee
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _checkerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> LockAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Khóa tài khoản
            var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            if (result.Succeeded)
            {
                TempData["Message"] = "Khoá Tài Khoản " + user +" Thành Công";
                return RedirectToAction("Index");
            }

            return View("Error"); 
        }

        [HttpPost]
        public async Task<IActionResult> UnlockAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // Mở khóa tài khoản
            var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            if (result.Succeeded)
            {
                TempData["Message"] = "Mở Khoá Tài Khoản "+user+" Thành Công";
                return RedirectToAction("Index");
            }
            return View("Error"); 
        }
    }
}