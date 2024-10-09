using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task02_MVC.Models;

namespace Task02_MVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult AddRole()
        {
            return View("AddRole");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel RoleViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = RoleViewModel.RoleName;
               IdentityResult result = await roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    ViewBag.sucess = true;
                    TempData["SuccessMessage"] = "Role has been successfully created.";
                    return View("AddRole");
                }
                foreach(var error in  result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("AddRole", RoleViewModel);
        }
    }
}
