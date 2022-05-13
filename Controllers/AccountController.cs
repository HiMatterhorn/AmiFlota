using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Controllers
{
    public class AccountController : Controller
    {
        private readonly AmiFlotaContext _db;
        UserManager<ApplicationUserModel> _userManager;
        SignInManager<ApplicationUserModel> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(AmiFlotaContext db, UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Register()
        {
            var adminList = _userManager.GetUsersInRoleAsync(UserRole.admin.ToString()).GetAwaiter().GetResult().ToList();

            if (!_roleManager.RoleExistsAsync(UserRole.admin.ToString()).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.admin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(UserRole.manager.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(UserRole.user.ToString()));
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUserModel
                {
                    UserName = model.Name,
                    Email = model.Email,      
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    if (!User.IsInRole(UserRole.admin.ToString()))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        TempData["newAdminSignedUp"] = user.UserName;
                    }

                    return RedirectToAction("Index", "CarModels");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            //TODO Redirection
            return View(model);
        }


    }

    
}
