using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Models.Request;
using Reservation.mvcproject.ViewModels;
using Serilog;

namespace Reservation.mvcproject.Controllers
{
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
                return RedirectToAction("Index", "Home");
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
            TempData["FullName"] = ($"Name: {user.Name} ");
            TempData["Email"] = ($"Name: {user.UserName} ");
            TempData["PhoneNumber"] = ($"Email: {user.PhoneNumber}");
            TempData["Adress"] = ($"PhoneNumber: {user.Adress} ");
            return View("GetUserByEmailIndex");
        }
    }
}

