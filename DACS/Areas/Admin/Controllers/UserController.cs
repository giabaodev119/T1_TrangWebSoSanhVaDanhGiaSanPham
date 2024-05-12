﻿using Microsoft.AspNetCore.Authorization;
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
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserRepository userRepository, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _context = context;
            _userManager = userManager;
        }

       
        public async Task<IActionResult> Index(string searchString)
        {
            var KhachHang = await _userManager.GetUsersInRoleAsync("Customer");//chọn role Employee
            var KhachHangId = KhachHang.Select(u => u.Id);
            var all_KhachHang = from s in _context.User
                               where KhachHangId.Contains(s.Id)
                               select s;

            if (!String.IsNullOrEmpty(searchString))//Input có thông tin thì xuất ra
            {
                string lowercaseSearchString = searchString.ToLower();
                all_KhachHang = all_KhachHang.Where(s => s.FullName.ToLower().Contains(lowercaseSearchString)
                                                          || s.Email.ToLower().Contains(lowercaseSearchString)
                                                        || s.UserName.ToLower().Contains(lowercaseSearchString));
            }

            return View(await all_KhachHang.ToListAsync());
        }


        [HttpPost]
        public async Task<JsonResult> GetSearchCUSValue(string search)
        {
            var KhachHang = await _userManager.GetUsersInRoleAsync("User");
            var danhmucResult = KhachHang.Where(x => x.FullName.ToLower().Contains(search))
                                        .Select(x => new {
                                            label = x.FullName.ToLower(),
                                            value = x.FullName.ToLower()
                                        }).ToList();
            return Json(danhmucResult);
        }
        // GET: Displays the form to update an employee
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Updates an employee
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync

                existingUser.FullName = employee.FullName;
                existingUser.UserName = employee.UserName;
                existingUser.Email = employee.Email;

                await _userRepository.UpdateAsync(existingUser);

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Displays the confirmation page for deleting an employee
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
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
            await _userRepository.DeleteAsync(id);
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
                TempData["Message"] = "Khoá Tài Khoản " + user + " Thành Công";
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
                TempData["Message"] = "Mở Khoá Tài Khoản " + user + " Thành Công";
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}