using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task02_MVC.Models;
using Task02_MVC.ViewModel;

namespace Task02_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegister(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                // Mapping
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = registerViewModel.UserName,
                    PasswordHash = registerViewModel.Password,
                    Address = registerViewModel.Address
                };

                // Save Data base
                IdentityResult result = await userManager.CreateAsync(appUser, registerViewModel.Password);
                if(result.Succeeded)
                {
                    // assign to role
                    await userManager.AddToRoleAsync(appUser, "Trainee");
                    // Create Cookie
                    await signInManager.SignInAsync(appUser,false);
                    return RedirectToAction("Index" , "Home");
                }
                foreach(var error in  result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View("Register", registerViewModel);
        }


        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid)
            {
                // check found
                ApplicationUser appUser = await userManager.FindByNameAsync(loginUserViewModel.UserName);
                if(appUser != null)
                {
                    // check his password
                    bool found  = await userManager.CheckPasswordAsync(appUser, loginUserViewModel.Password);
                    if (found)
                    {
                        // if i need to get address so in need list of claims
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("UserAddress", appUser.Address));
                       await signInManager.SignInWithClaimsAsync(appUser,loginUserViewModel.RememberMe, claims);

                        // give his cookie
                        // await signInManager.SignInAsync(appUser, loginUserViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "UserName or Password are Wrong");
                // create cookie
            }
            return View("Login" , loginUserViewModel);
        }


        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
