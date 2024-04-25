using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Models.Request;
using Reservation.mvcproject.ViewModels;
using Serilog;

namespace Reservation.mvcproject.Controllers
{
    [Authorize]
    public class AspUserController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AspUserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult GetUserIndex()
        {
            var users = _dbContext.Users.ToList();
            Log.Information("Hotels listed.");
            return View(users);
        }
        public IActionResult DeleteUserIndex()
        {
            return View();
        }
        public IActionResult GetUserByEmailIndex()
        {
            return View();
        }
        public IActionResult UpdateUserIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            if (userToDelete == null)
            {
                Log.Information("User not found during deletion!");
                return NotFound();
            }
            else
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();

                Log.Information($"{userToDelete.Name} kullanıcı silindi.");
                return RedirectToAction("GetUserIndex", "AspUser");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string UserName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            if (user == null)
            {
                Log.Information($"User with mail address {UserName} not found.");
                TempData["GetUserFailed"] = "No user with this id was found!";
                return View("GetUserByEmailIndex");
            }
            Log.Information($"User with maill adress {UserName} is listed.");
            TempData["FullName"] = ($"Username: {user.Name} ");
            TempData["Email"] = ($"Name: {user.UserName} ");
            TempData["PhoneNumber"] = ($"Phone Number: {user.PhoneNumber}");
            TempData["Adress"] = ($"Address: {user.Adress} ");
            return View("GetUserByEmailIndex");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel user)
        {
            var updatedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (updatedUser == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(user.Name))
            {
                updatedUser.Name = user.Name;
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                updatedUser.Email = user.Email;
                updatedUser.UserName = user.Email; 
            }

            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                updatedUser.PhoneNumber = user.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(user.Adress))
            {
                updatedUser.Adress = user.Adress;
            }
            if (!string.IsNullOrEmpty(user.Gender))
            {
                updatedUser.Gender = user.Gender;
                if (user.Gender != "M" && user.Gender != "F")
                {
                    updatedUser.Gender = null;
                }
            }

            _dbContext.Users.Update(updatedUser);
            await _dbContext.SaveChangesAsync();

            Log.Information($"{updatedUser} user updated.");
            return RedirectToAction("Index", "Home");
        }

    }
}

