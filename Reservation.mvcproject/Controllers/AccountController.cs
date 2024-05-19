using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Reservation.mvcproject.Models;
using Reservation.mvcproject.ViewModels;
using Reservation.mvcproject.Interfaceses;
namespace Reservation.mvcproject.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IMailService _mailService;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMailService mailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this._mailService = mailService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (model.VerificationCode == null)
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // Random kodu burada oluştur
                        Random rnd = new();
                        var AuthenticationCode = rnd.Next(100000, 1000000);

                        // AuthenticationCode'u ve email'i TempData'da sakla
                        TempData["AuthenticationCode"] = AuthenticationCode;
                        TempData["Username"] = model.Username;

                        string? toMail = model.Username;
                        string subject = "Two Factor Code";
                        string body = $"Authentication Code : {AuthenticationCode}";
                        await _mailService.SendEmailAsync(toMail, subject, body);

                        // Doğrulama kodu girişi için bir görünüm gösterebilirsiniz
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt");
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // TempData'dan AuthenticationCode'u ve Username'i al
                    var storedCode = TempData["AuthenticationCode"] as int?;
                    var storedUsername = TempData["Username"] as string;

                    if (storedCode.HasValue && storedUsername == model.Username)
                    {
                        bool IsVerify = IsVerifyUser(model.Username, model.VerificationCode, storedCode.Value);
                        if (IsVerify)
                        {
                            var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        ModelState.AddModelError("", "Invalid login attempt");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid verification code or email");
                    }
                }
            }
            return View(model);
        }



        public static bool IsVerifyUser(string? email, string? code, int? firstGenerateTwoFactorCode)
        {
            if (!int.TryParse(code, out var code1))
            {
                return false;
            }

            return firstGenerateTwoFactorCode == code1;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Adress=model.Adress,
                    PhoneNumber = model.PhoneNumber,
                    Gender=model.Gender
                };
                var result = await userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    string? toMail = user.Email;
                    string subject = "Information!";
                    string body = "Your registration has been successfully created.";

                    await _mailService.SendEmailAsync(toMail, subject, body);

                    return RedirectToAction("Index","Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

      
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}
