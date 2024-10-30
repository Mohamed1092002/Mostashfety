
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.ViewModels.AdminVM;
using Mostashfety.DAL.Models;

namespace Mostashfety.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(CreateAdminVM adminVM)
        {
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByEmailAsync(adminVM.Email);
                if (admin is null)
                {
                    admin = new Admin()
                    {
                        FullName = adminVM.FullName,
                        UserName = adminVM.UserName,
                        Email = adminVM.Email,
                        Password = adminVM.Password,
                        PhoneNumber = adminVM.PhoneNumber


                    };
                    var res = await _userManager.CreateAsync(admin, adminVM.Password);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
            }
            ModelState.AddModelError("", "InValid Account");
            return View(adminVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByEmailAsync(loginVM.Email);
                if (admin is not null)
                {
                    var check = await _userManager.CheckPasswordAsync(admin, loginVM.Password);
                    if (check)
                    {
                        // Use loginVM.RememberMe instead of admin.RememberMe
                        var res = await _signInManager.PasswordSignInAsync(admin, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: true);
                        if (res.IsLockedOut)
                        {
                            ModelState.AddModelError("", "Your Account is Locked for 1 min");
                        }
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No account found with that email.");
                }
            }

            // If we got this far, something failed; redisplay the form
            return View(loginVM);
        }




        public new async Task<IActionResult> SiginOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}


