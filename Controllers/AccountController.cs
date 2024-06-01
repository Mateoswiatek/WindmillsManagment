using Microsoft.AspNetCore.Mvc;
using MgWindManager.Models;
using Microsoft.AspNetCore.Identity;

namespace MgWindManager.Controllers
{
    public class AccountController(UserManager<UserModel> userManager,
        SignInManager<UserModel> signInManager) : Controller
    {
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(Register registerData)
        {
            if(!ModelState.IsValid)
            {
                return View(registerData); // silne typowanie
            }

            var newUser = new UserModel
            {
                UserName = registerData.UserName,
                Email = registerData.Email,
            };
            
            await userManager.CreateAsync(newUser, registerData.Password);
            
            


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login loginData)
        {
            if(!ModelState.IsValid)
            {
                return View(loginData); // silne typowanie
            }

            var result = await signInManager.PasswordSignInAsync(loginData.UserName, loginData.Password, false, false);

            if (result.IsLockedOut)
            {
                // Jakieś tam poinformowanie, że nie udało się.
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}